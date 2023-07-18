using System.Data.SqlClient;
using SosesPOS.dataset;
using SosesPOS.DTO;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;
using SosesPOS.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SosesPOS
{
    public partial class formPrintCheckReport : Form
    {
        DbConnection dbcon = new DbConnection();
        public formPrintCheckReport()
        {
            InitializeComponent();
        }
        //List<CheckIssueDTO> list
        public void PrintCheck()
        {
            try
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SosesPOS.report.rptCheckReport.rdlc"; 
                this.reportViewer1.LocalReport.DataSources.Clear();


                dsCheckReport ds = new dsCheckReport();
                DataTable dt = ds.Tables["dtCheckReport"];
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        using (SqlCommand com = con.CreateCommand())
                        {
                            com.CommandText = "SELECT CheckDate, CheckNo, PayeeName, CheckAmount, EntryTimestamp " +
                                "FROM tblCheckIssue WHERE CAST(EntryTimestamp AS DATE) = CAST(GETDATE() AS DATE)";
                            sda.SelectCommand = com;
                            sda.Fill(dt);
                        }
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No records found today.");
                    return;
                }

                ReportDataSource rptDataSource = new ReportDataSource("dsCheckReport", ds.Tables["dtCheckReport"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);


                // Paper Settings
                PageSettings page = new PageSettings();
                PaperSize size = new PaperSize("Check Print", 550, 850); // name, width, height
                size.RawKind = (int)PaperKind.Custom;
                page.PaperSize = size;

                page.Margins.Top = 0;
                page.Margins.Bottom = 0;
                page.Margins.Left = 0;
                page.Margins.Right = 0;

                reportViewer1.SetPageSettings(page);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

                // PRINT
                BasePrintHelper print = new BasePrintHelper(528, 816);
                string deviceInfo =
                  @"<DeviceInfo>
                        <OutputFormat>EMF</OutputFormat>
                        <PageWidth>5.5in</PageWidth>
                        <PageHeight>8.5in</PageHeight>
                        <MarginTop>0in</MarginTop>
                        <MarginLeft>0in</MarginLeft>
                        <MarginRight>0in</MarginRight>
                        <MarginBottom>0in</MarginBottom>
                    </DeviceInfo>";
                print.Export(reportViewer1.LocalReport, deviceInfo);
                print.Print();

                //MessageBox.Show("Printing Completed");
            }
            catch (Exception ex)
            {
                throw new Exception("Print Check Report error:" + ex.Message);
            }
        }
    }
}
