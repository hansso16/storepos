﻿using Microsoft.Reporting.WinForms;
using SosesPOS.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SosesPOS
{
    public partial class formCheckPrint : Form
    {
        public formCheckPrint()
        {
            InitializeComponent();
        }

        private string formatWithDateWithSpace(string value)
        {
            return string.Join("   ", value.ToCharArray());

        }

        public void PrintCheck(DateTime checkDate, string checkAmount, string payee)
        {
            try
            {
                //this.reportViewer1.LocalReport.ReportPath = System.IO.Path.GetDirectoryName(Application.StartupPath) + @"\..\report\rptCheckPrint.rdlc";
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SosesPOS.report.rptCheckPrint.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();
                decimal amount = Convert.ToDecimal(checkAmount);
                amount = IntegerUtil.Normalize(amount);
                string writtenFigures = " ";
                string strAmount = " ";
                payee = payee.ToUpper();
                //char[] cArray = checkDate.ToCharArray();
                //checkDate = String.Join("  ", cArray);

                //DateTime parsedDate = Convert.ToDateTime(checkDate);
                string month = checkDate.ToString("MM");
                string day = checkDate.ToString("dd");
                string year = checkDate.ToString("yyyy");

                month = formatWithDateWithSpace(month);
                day = formatWithDateWithSpace(day);
                year = formatWithDateWithSpace(year);

                //MessageBox.Show("Month: " + month);
                //MessageBox.Show("Day: " + day);
                //MessageBox.Show("Year: " + year);
                //return;


                ReportParameter pCheckMonth = new ReportParameter("pCheckMonth", month);
                ReportParameter pCheckDay = new ReportParameter("pCheckDay", day);
                ReportParameter pCheckYear = new ReportParameter("pCheckYear", year);
                ReportParameter pCheckDate = new ReportParameter("pCheckDate", checkDate.ToString());
                ReportParameter pPayee = new ReportParameter("pPayee", payee);
                if (!amount.Equals(decimal.Zero))
                {
                    writtenFigures = IntegerUtil.NumberToCurrencyText(amount, MidpointRounding.AwayFromZero);
                    if (!String.IsNullOrEmpty(writtenFigures))
                    {
                        //writtenFigures = writtenFigures;
                    }
                    strAmount = checkAmount;
                    //strAmount = checkAmount;
                }
                ReportParameter pCheckAmount = new ReportParameter("pCheckAmount", strAmount);
                ReportParameter pWrittenFigures = new ReportParameter("pWrittenFigures", writtenFigures);

                reportViewer1.LocalReport.SetParameters(pCheckMonth);
                reportViewer1.LocalReport.SetParameters(pCheckDay);
                reportViewer1.LocalReport.SetParameters(pCheckYear);
                reportViewer1.LocalReport.SetParameters(pCheckDate);
                reportViewer1.LocalReport.SetParameters(pCheckAmount);
                reportViewer1.LocalReport.SetParameters(pPayee);
                reportViewer1.LocalReport.SetParameters(pWrittenFigures);

                // Paper Settings
                PageSettings page = new PageSettings();
                PaperSize size = new PaperSize("Check Print", 800, 300); // name, width, height
                size.RawKind = (int)PaperKind.Custom;
                page.PaperSize = size;

                page.Margins.Top = 0;
                page.Margins.Bottom = 0;
                page.Margins.Left = 0;
                page.Margins.Right = 0;

                reportViewer1.SetPageSettings(page);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

                // PRINT
                BasePrintHelper print = new BasePrintHelper(768, 288);
                string deviceInfo = 
                  @"<DeviceInfo>
                        <OutputFormat>EMF</OutputFormat>
                        <PageWidth>8in</PageWidth>
                        <PageHeight>3in</PageHeight>
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
                throw new Exception("Stock Transfer Print error:" + ex.Message);
            }
        }
    }
}
