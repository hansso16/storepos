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
using SosesPOS.DTO;
using SosesPOS;

namespace SosesPOS
{
    public partial class Form1 : Form
    {
        UserDTO user = null;
        RoleDTO role = null;
        formLogin formLogin = null;
        public Form1(formLogin formLogin, UserDTO userDTO)
        {
            InitializeComponent();
            user = userDTO;
            role = userDTO.role;
            this.lblUsername.Text = user.username;
            this.lblRole.Text = role.roleName;
            this.formLogin = formLogin;
        }

        public void SetAccessLevel()
        {
            SetStaffLevel();
            SetOfficerLevel();
            SetMgrLevel();
        }

        private void SetStaffLevel()
        {
            if (role.accessLevel >= 100) 
            {
                this.btnBillingInvoice.Visible = true;
            }
        }

        private void SetOfficerLevel()
        {
            if (role.accessLevel >= 500)
            {
                this.btnProduct.Visible = true;
                this.btnCustomer.Visible = true;
                this.btnVendor.Visible = true;
                this.btnUnits.Visible = true;
                //this.btnBrand.Visible = true;
                //this.btnBank.Visible = true;
                //this.btnCategory.Visible = true;
                //this.btnStockLocation.Visible = true;
                this.btnReceivePayments.Visible = true;
                this.btnStockIn.Visible = true;
                this.btnStockTransfer.Visible = true;
            }
        }

        private void SetMgrLevel()
        {
            if (role.accessLevel >= 900)
            {
                this.button5.Visible = true; // Store Invoice
                this.btnUser.Visible = true;
            }
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
            // Logout
            panel3.Controls.Clear();
            this.Hide();
            formLogin form = new formLogin();
            form.ShowDialog();
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

        private void btnBillingInvoice_Click(object sender, EventArgs e)
        {
            formBillingPOS form = new formBillingPOS();
            form.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            System.Environment.Exit(1);
        }

        private void btnUnits_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            formUnits form = new formUnits();
            form.TopLevel = false;
            panel3.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {

        }
    }
}
