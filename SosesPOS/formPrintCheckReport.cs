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
        public void PrintCheck(List<CheckReportDTO> list)
        {
            try
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SosesPOS.report.rptCheckReport.rdlc"; 
                this.reportViewer1.LocalReport.DataSources.Clear();


                dsCheckReport ds = new dsCheckReport();
                DataTable dt = ds.Tables["dtCheckReport"];

                if (list.Count > 0)
                {
                    foreach (CheckReportDTO dto in list)
                    {
                        dt.Rows.Add(dto.CheckDate, dto.CheckNo, dto.Payee, dto.Amount, dto.CheckId);
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
                PaperSize size = new PaperSize("LETTER", 850, 1100); // name, width, height
                size.RawKind = (int)PaperKind.Custom;
                page.PaperSize = size;

                page.Margins.Top = 0;
                page.Margins.Bottom = 0;
                page.Margins.Left = 0;
                page.Margins.Right = 0;

                reportViewer1.SetPageSettings(page);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

                // PRINT
                BasePrintHelper print = new BasePrintHelper(816, 1056);
                string deviceInfo =
                  @"<DeviceInfo>
                        <OutputFormat>EMF</OutputFormat>
                        <PageWidth>8.5in</PageWidth>
                        <PageHeight>11in</PageHeight>
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
                Console.WriteLine("PrintCheckReport->PrintCheck(list)->"+ex.Message);
                throw new Exception("Print Check Report error:" + ex.Message);
            }
        }
    }
}
