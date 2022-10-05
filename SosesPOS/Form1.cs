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
using SosesPOS;

namespace SosesPOS
{
    public partial class Form1 : Form
    {

        //SqlConnection con = new SqlConnection();
        //SqlCommand com = new SqlCommand();
        //DbConnection dbcon = new DbConnection();


        public Form1()
        {
            InitializeComponent();
        }

        private void dashboardBtn_Click(object sender, EventArgs e)
        {

        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            formUOMList form = new formUOMList();
            form.TopLevel = false;
            panel3.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            formCategoryList form = new formCategoryList();
            form.TopLevel = false;
            panel3.Controls.Add(form);
            form.BringToFront();
            form.LoadCategoryRecords();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            formProductList form = new formProductList();
            form.TopLevel = false;
            panel3.Controls.Add(form);
            form.LoadProductRecords();
            form.BringToFront();
            form.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //panel3.Controls.Clear();
            //formStockIn form = new formStockIn();
            formPurchase form = new formPurchase();
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //panel3.Controls.Clear();
            formPOS form = new formPOS();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            formCustomerList form = new formCustomerList();
            form.TopLevel = false;
            panel3.Controls.Add(form);
            form.BringToFront();
            form.LoadCustomerRecords();
            form.Show();
        }

        private void btnVendor_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            formVendorList form = new formVendorList();
            form.TopLevel = false;
            form.LoadVendorList();
            panel3.Controls.Add(form);
            form.BringToFront();
            form.Show();
            //formCrystal form = new formCrystal();
            //form.ShowDialog();
            //formPrintDocument form = new formPrintDocument();
            //form.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            //formInvoiceReceipt form = new formInvoiceReceipt();
            //form.LoadReport("2208161021");
            //form.ShowDialog();
        }

        private void btnReceivePayments_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            formCustomerPayment form = new formCustomerPayment();
            form.LoadCustomerList();
            form.LoadBankList();
            form.TopLevel = false;
            panel3.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void btnBank_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            formBankList form = new formBankList();
            form.TopLevel = false;
            panel3.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void btnStockLocation_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            formStockLocationList form = new formStockLocationList();
            form.TopLevel = false;
            panel3.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }
    }
}
