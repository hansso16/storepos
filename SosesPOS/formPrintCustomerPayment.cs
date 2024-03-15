using Microsoft.Reporting.WinForms;
using SosesPOS.dataset;
using SosesPOS.DTO;
using SosesPOS.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SosesPOS
{
    public partial class formPrintCustomerPayment : Form
    {
        DbConnection dbcon = new DbConnection();
        public formPrintCustomerPayment()
        {
            InitializeComponent();
        }

        public void PrintCustomerPayment(List<AreaDTO> areaList)
        {
            try
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SosesPOS.report.rptCustomerPayment.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                ReportParameter pRefNo = null;
                ReportParameter pArea = null;

                if (areaList == null || areaList.Count <= 0)
                {
                    MessageBox.Show("No payment to report", "Customer Payment Print Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();

                    foreach (AreaDTO areaDTO in areaList)
                    {
                        String refNo = null;
                        using (SqlCommand com = con.CreateCommand())
                        {
                            com.CommandText = "SELECT NEXT VALUE FOR sqx_customer_payment_ref_no AS 'SEQ_NO'";
                            using (SqlDataReader dr = com.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    if (dr.Read())
                                    {
                                        refNo = dr["SEQ_NO"].ToString();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Error retrieving Ref No", "Customer Payment Print Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }

                        pRefNo = new ReportParameter("pRefNo", refNo);
                        pArea = new ReportParameter("pArea", areaDTO.areaName);

                        reportViewer1.LocalReport.SetParameters(pRefNo);
                        reportViewer1.LocalReport.SetParameters(pArea);


                        // populate SDA/report
                        dsCustomerPayment ds = new dsCustomerPayment();
                        SqlDataAdapter sda = new SqlDataAdapter();

                        sda.SelectCommand = new SqlCommand("select c.CustomerName, cp.Amount " +
                            "From tblCustomerPayment cp " +
                            "INNER JOIN tblCustomer c on cp.CustomerId = c.CustomerId " +
                            "INNER JOIN tblArea a ON a.AreaCode = c.AreaCode " +
                            "WHERE cp.IsPrinted = @isprinted AND a.AreaCode = @areacode AND cp.Amount > 0", con);
                        sda.SelectCommand.Parameters.AddWithValue("@areacode", areaDTO.areaCode);
                        sda.SelectCommand.Parameters.AddWithValue("@isprinted", 0);
                        sda.Fill(ds.Tables["dtCustomerPayment"]);

                        ReportDataSource rptDataSource = new ReportDataSource("dsCustomerPayment", ds.Tables["dtCustomerPayment"]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(rptDataSource);

                        // Paper Settings
                        PageSettings page = new PageSettings();
                        PaperSize size = new PaperSize("HALF-SHORT", 650, 850); // name, width, height
                        size.RawKind = (int)PaperKind.Custom;
                        page.PaperSize = size;

                        page.Margins.Top = 0;
                        page.Margins.Bottom = 0;
                        page.Margins.Left = 0;
                        page.Margins.Right = 0;

                        reportViewer1.SetPageSettings(page);
                        reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

                        // PRINT
                        BasePrintHelper print = new BasePrintHelper(624, 816);
                        string deviceInfo =
                          @"<DeviceInfo>
                            <OutputFormat>EMF</OutputFormat>
                            <PageWidth>6.5in</PageWidth>
                            <PageHeight>8.5in</PageHeight>
                            <MarginTop>0in</MarginTop>
                            <MarginLeft>0in</MarginLeft>
                            <MarginRight>0in</MarginRight>
                            <MarginBottom>0in</MarginBottom>
                          </DeviceInfo>";
                        print.Export(reportViewer1.LocalReport, deviceInfo);
                        print.Print();
                    }

                    //Update values
                    using (SqlCommand com = new SqlCommand("Update tblCustomerPayment " +
                        "SET IsPrinted = @newisprinted " +
                        "WHERE IsPrinted = @oldisprinted AND Amount > 0", con))
                    {
                        com.Parameters.AddWithValue("@oldisprinted", 0);
                        com.Parameters.AddWithValue("@newisprinted", 1);
                        com.ExecuteNonQuery();
                    }

                    MessageBox.Show("Printing Completed");
                    this.Focus();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("PrintCheckReport->PrintCheck(list)->" + ex.Message);
                throw new Exception("Print Check Report error:" + ex.Message);
            }
        }
    }
}
