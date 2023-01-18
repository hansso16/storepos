using Microsoft.Reporting.WinForms;
using SosesPOS.dataset;
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
    public partial class formStockTransferPrint : Form
    {

        public formStockTransferPrint()
        {
            InitializeComponent();
        }

        public void LoadReport(SqlConnection con, SqlTransaction transaction, string refNo)
        {
            try
            {
                //this.reportViewer1.LocalReport.ReportPath = System.IO.Path.GetDirectoryName(Application.StartupPath) + @"\..\report\rptStockTransferRequest.rdlc";
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SosesPOS.report.rptStockTransferRequest.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                ReportParameter pDate = new ReportParameter("pDate", DateTime.Today.ToString("ddd, MM/dd/yyyy"));

                reportViewer1.LocalReport.SetParameters(pDate);

                dsStockTransferRequest ds = new dsStockTransferRequest();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = new SqlCommand("SELECT StockTransferID, StockTransferNo, Note " +
                    "FROM tblStockTransfer WHERE StockTransferNo = @stocktransferno", con, transaction);
                sda.SelectCommand.Parameters.AddWithValue("@stocktransferno", refNo);
                sda.Fill(ds.Tables["dtStockTransfer"]);

                DataTable table = ds.Tables["dtStockTransfer"];
                if (table.Rows.Count <= 0)
                {
                    MessageBox.Show("No Records qualified.");
                    return;
                }
                foreach (DataRow row in table.Rows)
                {
                    int stockTransferId = Convert.ToInt32(row["StockTransferID"]);
                    Console.WriteLine(row["StockTransferNo"].ToString());
                    Console.WriteLine(row["Note"].ToString());
                    SqlDataAdapter sdaItems = new SqlDataAdapter();
                    sdaItems.SelectCommand = new SqlCommand("SELECT str.StockTransferID, str.PCode, str.Qty, p.pdesc " +
                        "FROM tblStockTransferRequest str INNER JOIN tblProduct p ON p.pcode = str.PCode " +
                        "WHERE str.StockTransferID = @stocktransferid", con, transaction);
                    sdaItems.SelectCommand.Parameters.AddWithValue("@stocktransferid", stockTransferId);
                    sdaItems.Fill(ds.Tables["dtStockTransferItems"]);
                }

                ReportDataSource rptDataSourceStockTransfer = new ReportDataSource("dtStockTransfer", ds.Tables["dtStockTransfer"]);
                ReportDataSource rptDataSourceStockTransferItems = new ReportDataSource("dtStockTransferItems", ds.Tables["dtStockTransferItems"]);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rptDataSourceStockTransfer);
                reportViewer1.LocalReport.DataSources.Add(rptDataSourceStockTransferItems);



                // Paper Settings
                PageSettings page = new PageSettings();
                PaperSize size = new PaperSize("Stock Transfer Request", 528, 816); // name, width, height
                size.RawKind = (int)PaperKind.Custom;
                page.PaperSize = size;

                page.Margins.Top = 0;
                page.Margins.Bottom = 0;
                page.Margins.Left = 0;
                page.Margins.Right = 0;

                reportViewer1.SetPageSettings(page);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

                // PRINT
                BasePrintHelper print = new BasePrintHelper(591, 846);
                print.Export(reportViewer1.LocalReport);
                print.Print();

                //MessageBox.Show("Printing Completed");
            } 
            catch (Exception ex)
            {
                throw new Exception("Stock Transfer Print error:" +ex.Message);
            }
        }
    }
}
