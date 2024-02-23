using Microsoft.Reporting.WinForms;
using SosesPOS.dataset;
using SosesPOS.DTO;
using SosesPOS.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SosesPOS
{
    public partial class formPurchaseReportPrint : Form
    {
        public formPurchaseReportPrint()
        {
            InitializeComponent();
        }

        public void LoadReport(PurchaseReportDTO dto)
        {
            if (dto == null || dto.purchaseItemDTO == null || dto.purchaseItemDTO.Count <= 0)            {
                return;
            }
            try
            {
                //this.reportViewer1.LocalReport.ReportPath = System.IO.Path.GetDirectoryName(Application.StartupPath) + @"\..\report\rptStockTransferRequest.rdlc";
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SosesPOS.report.rptPurchaseReport.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear(); // remove?????

                ReportParameter pVendorCode = new ReportParameter("pVendorCode", dto.vendorCode);
                ReportParameter pEntryTimestamp = new ReportParameter("pEntryTimestamp", DateTime.Today.ToString("ddd, MMM dd, yyyy"));
                ReportParameter pSite = new ReportParameter("pSite", dto.site);
                ReportParameter pTotalQty = new ReportParameter("pTotalQty", dto.totalQty.ToString());
                ReportParameter pFreight = new ReportParameter("pFreight", dto.freight);
                ReportParameter pTotalCost = new ReportParameter("pTotalCost", dto.totalCost.ToString());
                ReportParameter pVendorRefNo = new ReportParameter("pVendorRefNo", dto.vendorRefNo);

                reportViewer1.LocalReport.SetParameters(pVendorCode);
                reportViewer1.LocalReport.SetParameters(pEntryTimestamp);
                reportViewer1.LocalReport.SetParameters(pSite);
                reportViewer1.LocalReport.SetParameters(pTotalQty);
                reportViewer1.LocalReport.SetParameters(pFreight);
                reportViewer1.LocalReport.SetParameters(pTotalCost);
                reportViewer1.LocalReport.SetParameters(pVendorRefNo);

                dsPurchaseReport ds = new dsPurchaseReport();
                DataTable dt = new DataTable("dtPurchaseItem");
                dt.Columns.Add("productCode", typeof(string));
                dt.Columns.Add("productDescription", typeof(string));
                dt.Columns.Add("count", typeof(string));
                dt.Columns.Add("qty", typeof(int));
                dt.Columns.Add("bal", typeof(int));
                dt.Columns.Add("cost", typeof(decimal));
                dt.Columns.Add("freight", typeof(decimal));
                dt.Columns.Add("totalCost", typeof(decimal));
                foreach (PurchaseItemDTO item in dto.purchaseItemDTO)
                {
                    dt.Rows.Add(item.productCode, item.productDescription, item.count, item.qty, item.bal, item.cost, item.freight, item.totalCost);
                }

                ReportDataSource rptDataSourcePurchaseReport = new ReportDataSource("dsPurchaseReport", dt);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rptDataSourcePurchaseReport);

                // Paper Settings
                PageSettings page = new PageSettings();
                PaperSize size = new PaperSize("Purchase Report", 816, 1056); // name, width, height
                size.RawKind = (int)PaperKind.Custom;
                page.PaperSize = size;

                page.Margins.Top = 0;
                page.Margins.Bottom = 0;
                page.Margins.Left = 0;
                page.Margins.Right = 0;

                reportViewer1.SetPageSettings(page);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

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

                // PRINT
                BasePrintHelper print = new BasePrintHelper(816, 1056);
                print.Export(reportViewer1.LocalReport, deviceInfo);
                print.Print();

                this.Dispose();
                //MessageBox.Show("Printing Completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Stock Transfer Print error:" + ex.Message);
            }
        }
    }
}
