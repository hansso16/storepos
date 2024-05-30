using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SosesPOS.DTO;
using SosesPOS.util;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using SosesPOS.dataset;
using System.Drawing.Printing;
using System.IO;
using System.Drawing.Imaging;

namespace SosesPOS
{
    public partial class formPaymentSummaryReport : Form
    {
        DbConnection dbcon = new DbConnection();
        UserDTO user = null;
        public formPaymentSummaryReport(UserDTO user)
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
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SosesPOS.report.rptPaymentSummary.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                ReportParameter pDate = new ReportParameter("pDate", DateTime.Today.ToString("ddd, MM/dd/yyyy"));
                reportViewer1.LocalReport.SetParameters(pDate);

                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();

                    dsPaymentSummary ds = new dsPaymentSummary();
                    SqlDataAdapter sda = new SqlDataAdapter();

                    sda.SelectCommand = new SqlCommand("SELECT c.CustomerName CustomerName, cp.Amount TotalAmount " +
                        "FROM tblCustomerPayment cp " +
                        "INNER JOIN tblCustomer c ON c.CustomerId = cp.CustomerId " +
                        "WHERE cp.Amount > 0 " +
                        "AND cp.ProcessTimestamp >= @datetoday AND cp.ProcessTimestamp < @datetomorrow ", con);
                    sda.SelectCommand.Parameters.AddWithValue("@datetoday", DateTime.Now.Date);
                    sda.SelectCommand.Parameters.AddWithValue("@datetomorrow", DateTime.Now.AddDays(1).Date);
                    sda.Fill(ds.Tables["dtPaymentSummary"]);

                    ReportDataSource rptDataSource = new ReportDataSource("dsPaymentSummary", ds.Tables["dtPaymentSummary"]);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rptDataSource);
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

                this.Activate();
                MessageBox.Show("Printing Completed");
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Generate Billing Customer Summary", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
