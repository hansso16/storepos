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
using SosesPOS.DTO;
using SosesPOS.util;

namespace SosesPOS
{
    public partial class formBillingSummary : Form
    {
        DbConnection dbcon = new DbConnection();
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        public formBillingSummary()
        {
            InitializeComponent();
        }

        private void formBillingSummary_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        public void LoadReport()
        {
            ReportDataSource rptDataSource;
            try
            {
                //this.reportViewer1.LocalReport.ReportPath = System.IO.Path.GetDirectoryName(Application.StartupPath) + @"\..\report\rptBillingSummary.rdlc";
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SosesPOS.report.rptBillingSummary.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                ReportParameter pDate = new ReportParameter("pDate", DateTime.Today.ToString("ddd, MM/dd/yyyy"));
                ReportParameter pAreaName = null;

                reportViewer1.LocalReport.SetParameters(pDate);

                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();

                    // Get Area into a List
                    List<AreaDTO> areaList = null;
                    using (SqlCommand com = new SqlCommand("SELECT a.AreaCode, a.AreaName FROM tblOrder o " +
                        "INNER JOIN tblCustomer c ON c.CustomerId = o.CustomerId INNER JOIN tblArea a ON a.AreaCode = c.AreaCode " +
                        "WHERE o.OrderStatus = '15' GROUP BY a.AreaCode, a.AreaName", con))
                    {
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                areaList = new List<AreaDTO>();
                                while (dr.Read())
                                {
                                    AreaDTO dto = new AreaDTO();
                                    dto.areaCode = Convert.ToInt32(dr["AreaCode"]);
                                    dto.areaName = dr["AreaName"].ToString();
                                    areaList.Add(dto);
                                }
                            }
                        }
                    }

                    // Iterate List
                    if (areaList != null && areaList.Count > 0)
                    {
                        foreach (AreaDTO areaDTO in areaList) 
                        {
                            // populate SDA/report
                            dsBillingSummary ds = new dsBillingSummary();
                            SqlDataAdapter sda = new SqlDataAdapter();
                            pAreaName = new ReportParameter("pAreaName", areaDTO.areaName);
                            reportViewer1.LocalReport.SetParameters(pAreaName);

                            sda.SelectCommand = new SqlCommand("SELECT id.PCode, p.pdesc, '' AS PREV, SUM(id.Qty) AS OUT, '' AS CRNT , c.AreaCode, id.Location " +
                                "FROM tblOrder o " +
                                "INNER JOIN tblCustomer c ON c.CustomerId = o.CustomerId " +
                                "INNER JOIN tblInvoice i ON i.OrderId = o.OrderId " +
                                "INNER JOIN tblInvoiceDetails id ON id.InvoiceId = i.InvoiceId " +
                                "INNER JOIN tblProduct p ON p.pcode = id.PCode " +
                                "WHERE o.OrderStatus = '15' and c.AreaCode = @areacode " +
                                "GROUP BY id.PCode, p.pdesc, c.AreaCode, id.location ORDER BY p.pdesc ", con);
                            sda.SelectCommand.Parameters.AddWithValue("@areacode", areaDTO.areaCode);
                            sda.Fill(ds.Tables["dtItems"]);

                            DataTable table = ds.Tables["dtItems"];
                            foreach (DataRow row in table.Rows)
                            {
                                if (row["Location"].Equals("1")) // 1 = Store Code
                                {
                                    row["prev"] = "";
                                    row["current"] = "";
                                    continue;
                                }
                                using (SqlCommand com = new SqlCommand("SELECT SUM(i.Qty) qty, p.pcode, p.count " +
                                    "FROM tblInventory i INNER JOIN tblProduct p ON i.PCode = p.pcode " +
                                    "WHERE i.pcode = @pcode and i.Qty > 0 " +
                                    "GROUP BY p.pcode, p.count", con))
                                {
                                    com.Parameters.AddWithValue("@pcode", row["pcode"]);
                                    using (SqlDataReader dr = com.ExecuteReader()) 
                                    {
                                        int currentInvt = 0;
                                        int prevInvt = 0;
                                        int outInvt = Convert.ToInt32(row["out"]);
                                        if (dr.HasRows && dr.Read())
                                        {
                                            currentInvt = Convert.ToInt32(dr["qty"]);
                                            prevInvt = currentInvt + outInvt;
                                        }
                                        row["prev"] = prevInvt;
                                        row["current"] = currentInvt;
                                    }
                                }
                            }
                            table.DefaultView.Sort = "Location DESC";
                            table = table.DefaultView.ToTable();

                            ds.Tables.Clear();
                            ds.Tables.Add(table);

                            rptDataSource = new ReportDataSource("DataSet1", ds.Tables["dtItems"]);
                            reportViewer1.LocalReport.DataSources.Clear();
                            reportViewer1.LocalReport.DataSources.Add(rptDataSource);

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
                            Print(areaDTO.areaName);
                            this.Focus();
                        }
                        
                        // Update Order status to issued after printing.
                        using (SqlCommand com = new SqlCommand("Update tblOrder SET OrderStatus = @neworderstatus, LastUpdatedTimestamp = @lastupdatedtimestamp " +
                            "WHERE OrderStatus = @oldorderstatus", con))
                        {
                            com.Parameters.AddWithValue("@oldorderstatus", OrderStatusConstant.INV_PRINTED);
                            com.Parameters.AddWithValue("@neworderstatus", OrderStatusConstant.INV_ISSUED);
                            com.Parameters.AddWithValue("@lastupdatedtimestamp", DateTime.Now);
                            com.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Printing Completed");
                    this.Dispose();
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

        private void Print(string area)
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
                MessageBox.Show("Printing SUMMARY for Area: " + area.ToUpper());
                printDoc.Print();
                //MessageBox.Show("DONE");
            }
        }
    }
}
