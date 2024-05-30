using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using System.Drawing.Imaging;
using SosesPOS.DTO;
using SosesPOS.util;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using SosesPOS.dataset;

namespace SosesPOS
{
    public partial class formBillingCustomerSummary : Form
    {
        DbConnection dbcon = new DbConnection();
        UserDTO user = null;
        public formBillingCustomerSummary(UserDTO user)
        {
            InitializeComponent();
            this.user = user;
        }

        public void LoadReport()
        {
            try
            {
                //this.reportViewer1.LocalReport.ReportPath = System.IO.Path.GetDirectoryName(Application.StartupPath) + @"\..\report\rptBillingSummary.rdlc";
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SosesPOS.report.rptBillingCustomerSummary.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                ReportParameter pDate = new ReportParameter("pDate", DateTime.Today.ToString("ddd, MM/dd/yyyy"));
                reportViewer1.LocalReport.SetParameters(pDate);

                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();

                    dsBillingCustomerSummary ds = new dsBillingCustomerSummary();
                    SqlDataAdapter sda = new SqlDataAdapter();

                    sda.SelectCommand = new SqlCommand("SELECT c.CustomerName CustomerName, count(InvoiceId) NoOfInv, sum(i.TotalPrice) TotalAmount " +
                        "FROM tblOrder o INNER JOIN tblInvoice i ON i.OrderId = o.OrderId " +
                        "LEFT JOIN tblCustomer c ON c.CustomerId = o.CustomerId " +
                        "WHERE o.orderstatus = '15' AND c.CustomerId IS NOT NULL " +
                        "GROUP BY c.CustomerName", con);
                    //sda.SelectCommand.Parameters.AddWithValue("@areacode", areaDTO.areaCode);
                    sda.Fill(ds.Tables["dtBillingCustomerSummary"]);

                    ReportDataSource rptDataSource = new ReportDataSource("dsBillingCustomerSummary", ds.Tables["dtBillingCustomerSummary"]);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rptDataSource);

                    // Update Order status to issued after printing.
                    using (SqlCommand com = new SqlCommand("Update tblOrder SET OrderStatus = @neworderstatus, LastUpdatedTimestamp = @lastupdatedtimestamp " +
                        "WHERE OrderStatus = @oldorderstatus AND UserCode = @usercode", con))
                    {
                        com.Parameters.AddWithValue("@oldorderstatus", OrderStatusConstant.INV_PRINTED);
                        com.Parameters.AddWithValue("@neworderstatus", OrderStatusConstant.INV_ISSUED);
                        com.Parameters.AddWithValue("@lastupdatedtimestamp", DateTime.Now);
                        com.Parameters.AddWithValue("@usercode", user.userCode);
                        com.ExecuteNonQuery();
                    }
                }

                //Print
                BasePrintHelper print = new BasePrintHelper(528, 768);
                string deviceInfo =
                    @"<DeviceInfo>
                    <OutputFormat>EMF</OutputFormat>
                    <PageWidth>5.5in</PageWidth>
                    <PageHeight>8in</PageHeight>
                    <MarginTop>0in</MarginTop>
                    <MarginLeft>0in</MarginLeft>
                    <MarginRight>0in</MarginRight>
                    <MarginBottom>0in</MarginBottom>
                </DeviceInfo>";
                print.Export(reportViewer1.LocalReport, deviceInfo);
                print.Print();

                this.Focus();
                MessageBox.Show("Invoice Summary Printing Completed. Click Ok to proceed to Payment Summary");
                this.Focus();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Generate Billing Customer Summary", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
