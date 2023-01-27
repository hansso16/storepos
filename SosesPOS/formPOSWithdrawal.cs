using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using SosesPOS.dataset;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using SosesPOS.DTO;
using SosesPOS.util;

namespace SosesPOS
{
    public partial class formPOSWithdrawal : Form
    {
        DbConnection dbcon = new DbConnection();
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        public formPOSWithdrawal()
        {
            InitializeComponent();
        }

        public void LoadReport()
        {
            try
            {
                //this.reportViewer1.LocalReport.ReportPath = System.IO.Path.GetDirectoryName(Application.StartupPath) + @"\..\report\rptInvoiceWithdrawal.rdlc";
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SosesPOS.report.rptInvoiceWithdrawal.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                ReportParameter pDate = new ReportParameter("pDate", DateTime.Today.ToString("ddd, MM/dd/yyyy"));

                reportViewer1.LocalReport.SetParameters(pDate);

                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();

                    dsInvoiceWithdrawal ds = new dsInvoiceWithdrawal();
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = new SqlCommand("SELECT i.InvoiceId invoiceId, c.CustomerName customerName FROM tblOrder o " +
                        "INNER JOIN tblCustomer c ON c.CustomerId = o.CustomerId INNER JOIN tblInvoice i ON i.OrderId = o.OrderId " +
                        "WHERE o.OrderStatus = @orderstatus", con);
                    sda.SelectCommand.Parameters.AddWithValue("@orderstatus", OrderStatusConstant.INV_PRINTED_BODEGA_OUT);
                    //sda.SelectCommand.Parameters.AddWithValue("@orderstatus", OrderStatusConstant.INV_PRINTED);
                    sda.Fill(ds.Tables["dtCustomer"]);

                    DataTable table = ds.Tables["dtCustomer"];
                    if (table.Rows.Count <= 0)
                    {
                        MessageBox.Show("No Records qualified.");
                        return;
                    }
                    foreach (DataRow row in table.Rows)
                    {
                        int invoiceId = Convert.ToInt32(row["InvoiceId"]);
                        SqlDataAdapter sdaItems = new SqlDataAdapter();
                        sdaItems.SelectCommand = new SqlCommand("SELECT id.InvoiceId, p.pcode, p.pdesc, id.Qty out " +
                            "from tblInvoiceDetails id INNER JOIN tblProduct p ON p.pcode = id.PCode " +
                            "WHERE id.InvoiceId = @invoiceid AND id.Location = @location", con);
                        sdaItems.SelectCommand.Parameters.AddWithValue("@invoiceid", invoiceId);
                        sdaItems.SelectCommand.Parameters.AddWithValue("@location", GlobalConstant.WH_CODE);
                        sdaItems.Fill(ds.Tables["dtItems"]);
                    }

                    ReportDataSource rptDataSourceCustomer = new ReportDataSource("dtCustomer", ds.Tables["dtCustomer"]);
                    ReportDataSource rptDataSourceItems = new ReportDataSource("dtItems", ds.Tables["dtItems"]);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rptDataSourceCustomer);
                    reportViewer1.LocalReport.DataSources.Add(rptDataSourceItems);

                    // Paper Settings
                    PageSettings page = new PageSettings();
                    PaperSize size = new PaperSize("Billing Summary", 528, 816); // name, width, height
                    size.RawKind = (int)PaperKind.Custom;
                    page.PaperSize = size;

                    page.Margins.Top = 0;
                    page.Margins.Bottom = 0;
                    page.Margins.Left = 0;
                    page.Margins.Right = 0;

                    reportViewer1.SetPageSettings(page);
                    reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

                    //Print
                    Export(reportViewer1.LocalReport);
                    Print();

                    // Update Order status to issued after printing.
                    using (SqlCommand com = new SqlCommand("Update tblOrder SET OrderStatus = @neworderstatus, LastUpdatedTimestamp = @lastupdatedtimestamp " +
                        "WHERE OrderStatus = @oldorderstatus", con))
                    {
                        com.Parameters.AddWithValue("@oldorderstatus", OrderStatusConstant.INV_PRINTED_BODEGA_OUT);
                        com.Parameters.AddWithValue("@neworderstatus", OrderStatusConstant.INV_ISSUED);
                        com.Parameters.AddWithValue("@lastupdatedtimestamp", DateTime.Now);
                        com.ExecuteNonQuery();
                    }
                    this.Focus();
                    MessageBox.Show("Printing Completed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Generate Billing Summary", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Routine to provide to the report renderer, in order to save an image for each page of the report.
        private Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                    <OutputFormat>EMF</OutputFormat>
                    <PageWidth>15cm</PageWidth>
                    <PageHeight>21.5cm</PageHeight>
                    <MarginTop>0in</MarginTop>
                    <MarginLeft>0in</MarginLeft>
                    <MarginRight>0in</MarginRight>
                    <MarginBottom>0in</MarginBottom>
                </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                591,
                846);
            //ev.PageBounds.Width,
            //ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                //MessageBox.Show("Printing SUMMARY for Area: " + area.ToUpper());
                printDoc.Print();
                this.Dispose();
                //MessageBox.Show("DONE");
            }
        }

        private void formPOSWithdrawal_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
    }
}
