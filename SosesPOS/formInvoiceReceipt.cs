using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using SosesPOS.dataset;
using System.Drawing.Printing;
using System.IO;
using System.Drawing.Imaging;

namespace SosesPOS
{
    public partial class formInvoiceReceipt : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        DbConnection dbcon = new DbConnection();
        SqlDataReader dr = null;
        SqlDataAdapter sda = new SqlDataAdapter();

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        public formInvoiceReceipt()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
        }

        private void formInvoiceReceipt_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        public void LoadReport(string RefNo)
        {
            ReportDataSource rptDataSource;
            try
            {
                //this.reportViewer1.LocalReport.ReportPath = System.IO.Path.GetDirectoryName(Application.StartupPath) + @"\..\report\rptInvoiceReceipt.rdlc";
                //this.reportViewer1.LocalReport.ReportPath = "~/report/rptInvoiceReceipt.rdlc";
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SosesPOS.report.rptInvoiceReceipt.rdlc";
                //MessageBox.Show(this.reportViewer1.LocalReport.ReportEmbeddedResource);
                this.reportViewer1.LocalReport.DataSources.Clear();

                ReportParameter pRefNo = new ReportParameter("pRefNo", RefNo);
                ReportParameter pCCode = null;
                ReportParameter pCName = null;
                ReportParameter pCAddress = null;
                ReportParameter pDate = null;

                con.Open();
                com = new SqlCommand("SELECT c.CustomerCode, c.CustomerName, c.CustomerAddress, o.EntryTimestamp FROM tblInvoice i " +
                    "INNER JOIN tblOrder o ON i.OrderId = o.OrderId " +
                    "INNER JOIN tblCustomer c ON c.CustomerId = o.CustomerId " +
                    "WHERE i.ReferenceNo = @refno", con);
                com.Parameters.AddWithValue("@refno", RefNo);
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        pCCode = new ReportParameter("pCCode", dr["CustomerCode"].ToString());
                        pCName = new ReportParameter("pCName", dr["CustomerName"].ToString());
                        pCAddress = new ReportParameter("pCAddress", dr["CustomerAddress"].ToString());
                        pDate = new ReportParameter("pDate", Convert.ToDateTime(dr["EntryTimestamp"]).ToString("MM/dd/yyyy"));
                    }
                }
                dr.Close();
                con.Close();

                reportViewer1.LocalReport.SetParameters(pRefNo);
                reportViewer1.LocalReport.SetParameters(pCCode);
                reportViewer1.LocalReport.SetParameters(pCName);
                reportViewer1.LocalReport.SetParameters(pCAddress);
                reportViewer1.LocalReport.SetParameters(pDate);

                dsInvoiceReceipt ds = new dsInvoiceReceipt();
                sda.SelectCommand = new SqlCommand("select i.InvoiceId id, i.ReferenceNo refno, p.pcode, id.SellingPrice price, id.Qty qty" +
                    ", u.type uom , i.EntryTimestamp date, id.TotalItemPrice total " +
                    ", CAST(CASE WHEN id.location = '1' THEN '*'+p.pdesc ELSE p.pdesc END AS nvarchar) as pdesc" +
                    ", p.VAT " +
                    "from tblInvoice i " +
                    "inner join tblInvoiceDetails id on id.InvoiceId = i.InvoiceId " +
                    "inner join tblProduct p on p.pcode = id.PCode " +
                    //"inner join tblProductDetails pd on pd.pcode = id.PCode and pd.uom = id.UOM " +
                    "left join tblUOM u on u.id = id.UOM " +
                    "where i.ReferenceNo = @refno " +
                    "ORDER BY p.pdesc ASC ", con);
                sda.SelectCommand.Parameters.AddWithValue("@refno", RefNo);
                sda.Fill(ds.Tables["dtSold"]);
                con.Close();

                rptDataSource = new ReportDataSource("DataSet1", ds.Tables["dtSold"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);


                PageSettings page = new PageSettings();
                PaperSize size = new PaperSize("Sales Invoice", 591, 846);
                size.RawKind = (int)PaperKind.Custom;
                page.PaperSize = size;

                page.Margins.Top = 0;
                page.Margins.Bottom = 0;
                page.Margins.Left = 0;
                page.Margins.Right = 0;

                reportViewer1.SetPageSettings(page);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

                Export(reportViewer1.LocalReport);
                Print();

                this.Dispose();
            } 
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception("Error printing InvoiceReceipt: " + ex.Message);
            }
        }

        public void LoadBillingReport(string RefNo, bool isWhole)
        {
            ReportDataSource rptDataSource;
            try
            {
                //this.reportViewer1.LocalReport.ReportPath = System.IO.Path.GetDirectoryName(Application.StartupPath) + @"\..\report\rptInvoiceReceipt.rdlc";
                //this.reportViewer1.LocalReport.ReportPath = "~/report/rptInvoiceReceipt.rdlc";
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SosesPOS.report.rptInvoiceReceipt.rdlc";
                //MessageBox.Show(this.reportViewer1.LocalReport.ReportEmbeddedResource);
                this.reportViewer1.LocalReport.DataSources.Clear();

                ReportParameter pRefNo = new ReportParameter("pRefNo", RefNo);
                ReportParameter pCCode = null;
                ReportParameter pCName = null;
                ReportParameter pCAddress = null;
                ReportParameter pDate = null;

                con.Open();
                com = new SqlCommand("SELECT c.CustomerCode, c.CustomerName, c.CustomerAddress, o.EntryTimestamp FROM tblInvoice i " +
                    "INNER JOIN tblOrder o ON i.OrderId = o.OrderId " +
                    "INNER JOIN tblCustomer c ON c.CustomerId = o.CustomerId " +
                    "WHERE i.ReferenceNo = @refno", con);
                com.Parameters.AddWithValue("@refno", RefNo);
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        pCCode = new ReportParameter("pCCode", dr["CustomerCode"].ToString());
                        pCName = new ReportParameter("pCName", dr["CustomerName"].ToString());
                        pCAddress = new ReportParameter("pCAddress", dr["CustomerAddress"].ToString());
                        pDate = new ReportParameter("pDate", Convert.ToDateTime(dr["EntryTimestamp"]).ToString("MM/dd/yyyy"));
                    }
                }
                dr.Close();
                //con.Close();

                reportViewer1.LocalReport.SetParameters(pRefNo);
                reportViewer1.LocalReport.SetParameters(pCCode);
                reportViewer1.LocalReport.SetParameters(pCName);
                reportViewer1.LocalReport.SetParameters(pCAddress);
                reportViewer1.LocalReport.SetParameters(pDate);

                dsInvoiceReceipt ds = new dsInvoiceReceipt();

                if (isWhole)
                {
                    sda.SelectCommand = new SqlCommand("select i.InvoiceId id, i.ReferenceNo refno, p.pcode, id.SellingPrice price, id.Qty qty" +
                        ", u.type uom , i.EntryTimestamp date, id.TotalItemPrice total " +
                        ", CAST(CASE WHEN id.location = '1' THEN '*'+p.pdesc ELSE p.pdesc END AS nvarchar) as pdesc" +
                        ", p.VAT " +
                        "from tblInvoice i " +
                        "inner join tblInvoiceDetails id on id.InvoiceId = i.InvoiceId " +
                        "inner join tblProduct p on p.pcode = id.PCode " +
                        //"inner join tblProductDetails pd on pd.pcode = id.PCode and pd.uom = id.UOM " +
                        "left join tblUOM u on u.id = id.UOM " +
                        "where i.ReferenceNo = @refno and u.code = '0'" +
                        "ORDER BY p.pdesc ASC ", con);
                } else
                {
                    sda.SelectCommand = new SqlCommand("select i.InvoiceId id, i.ReferenceNo refno, p.pcode, id.SellingPrice price, id.Qty qty" +
                        ", u.type uom , i.EntryTimestamp date, id.TotalItemPrice total " +
                        ", CAST(CASE WHEN id.location = '1' THEN '*'+p.pdesc ELSE p.pdesc END AS nvarchar) as pdesc " +
                        ", p.VAT" +
                        "from tblInvoice i " +
                        "inner join tblInvoiceDetails id on id.InvoiceId = i.InvoiceId " +
                        "inner join tblProduct p on p.pcode = id.PCode " +
                        //"inner join tblProductDetails pd on pd.pcode = id.PCode and pd.uom = id.UOM " +
                        "left join tblUOM u on u.id = id.UOM " +
                        "where i.ReferenceNo = @refno and u.code = '1'" +
                        "ORDER BY p.pdesc ASC ", con);
                }
                sda.SelectCommand.Parameters.AddWithValue("@refno", RefNo);
                sda.Fill(ds.Tables["dtSold"]);
                con.Close();

                rptDataSource = new ReportDataSource("DataSet1", ds.Tables["dtSold"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);


                PageSettings page = new PageSettings();
                PaperSize size = new PaperSize("Sales Invoice", 591, 846);
                size.RawKind = (int)PaperKind.Custom;
                page.PaperSize = size;

                page.Margins.Top = 0;
                page.Margins.Bottom = 0;
                page.Margins.Left = 0;
                page.Margins.Right = 0;

                reportViewer1.SetPageSettings(page);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

                Export(reportViewer1.LocalReport);
                Print();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception("Error printing InvoiceReceipt: " + ex.Message);
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
                //MessageBox.Show("Document is ready for printing.");
                printDoc.Print();
                //MessageBox.Show("DONE");
            }
        }
        // Create a local report for Report.rdlc, load the data, export the report to an .emf file, and print it.
        private void Run()
        {
            //LocalReport report = new LocalReport();
            //report.ReportPath = @"..\..\Report.rdlc";
            Export(reportViewer1.LocalReport);
            Print();
        }
    }
}
