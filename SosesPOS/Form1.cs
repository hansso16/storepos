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
        private bool isCollapsed = true;
        public Form1(formLogin formLogin, UserDTO userDTO)
        {
            InitializeComponent();
            user = userDTO;
            role = userDTO.role;
            this.lblUsername.Text = user.username;
            this.lblRole.Text = role.roleName;
            this.lblUserCode.Text = user.userCode.ToString();
            this.formLogin = formLogin;
        }

        public void SetAccessLevel()
        {
            SetStaffLevel();
            SetOfficerLevel();
            SetMgrLevel();
            SetPresLevel();
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
                this.btnStockReplenishment.Visible = true;
                this.StockTransferPanel.Visible = true;
                this.btnStockTransfer.Visible = true;
                this.btnTransferRequest.Visible = true;
                this.btnTransferDispatch.Visible = true;
                this.btnTransferAccept.Visible = true;
                this.btnLiteCheckWriter.Visible = true;
            }
        }

        private void SetMgrLevel()
        {
            if (role.accessLevel >= 800)
            {
                this.button5.Visible = true; // Store Invoice
                this.btnUser.Visible = true;
                this.btnInvoiceList.Visible = true;
                this.btnReceivePayments.Visible = true;
                this.btnCustomerMemo.Visible = true;
            }
        }

        private void SetPresLevel()
        {
            if (role.accessLevel == 50 || role.accessLevel == 999)
            {
                this.btnWriteCheck.Visible = true;
                this.btnInvoiceList.Visible = true;
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
            formPOS form = new formPOS(user);
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
            formLogin.ClearForm();
            formLogin.Show();
            formLogin.txtUsername.Focus();
            formLogin.txtUsername.SelectAll();

            this.FormClosing -= Form1_FormClosing;
            this.Close();
            this.FormClosing += Form1_FormClosing;
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
            formBillingPOS form = new formBillingPOS(user);
            form.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
                System.Environment.Exit(1);
            }
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
            panel3.Controls.Clear();
            formUserList form = new formUserList(user);
            form.TopLevel = false;
            panel3.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            formChangePassword form = new formChangePassword(this.lblUsername.Text);
            form.TopLevel = false;
            panel3.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                btnStockTransfer.Image = SosesPOS.Properties.Resources.collapse_icon;
                StockTransferPanel.Height += 10;
                if (StockTransferPanel.Size == StockTransferPanel.MaximumSize)
                {
                    timer1.Stop();
                    isCollapsed = false;
                }
            } 
            else
            {
                btnStockTransfer.Image = SosesPOS.Properties.Resources.expand_icon1;
                StockTransferPanel.Height -= 10;
                if (StockTransferPanel.Size == StockTransferPanel.MinimumSize)
                {
                    timer1.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void btnStockTransfer_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btnTransferRequest_Click(object sender, EventArgs e)
        {
            formStockTransfer form = new formStockTransfer(user);
            form.ShowDialog();
        }

        private void btnTransferDispatch_Click(object sender, EventArgs e)
        {
            formStockTransferDispatch form = new formStockTransferDispatch(user);
            form.Show();
        }

        private void btnTransferAccept_Click(object sender, EventArgs e)
        {
            formStockTransferAccept form = new formStockTransferAccept(user);
            form.Show();
        }

        private void btnWriteCheck_Click(object sender, EventArgs e)
        {
            InitiateWriteCheck();
        }

        private void btnLiteCheckWriter_Click(object sender, EventArgs e)
        {
            formLiteCheckWriter form = new formLiteCheckWriter();
            form.ShowDialog();
        }

        public void InitiateWriteCheck()
        {
            panel3.Controls.Clear();
            formWriteCheckList form = new formWriteCheckList(user);
            form.TopLevel = false;
            panel3.Controls.Add(form);
            form.BringToFront();
            form.LoadVendorList();
            form.Show();
        }

        private void btnCustomerMemo_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            formCustomerMemo form = new formCustomerMemo();
            form.LoadCustomerList();
            form.TopLevel = false;
            panel3.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void btnInvoiceList_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            formInvoiceList form = new formInvoiceList(user);
            form.TopLevel = false;
            panel3.Controls.Add(form);
            form.BringToFront();
            form.LoadUserList();
            form.LoadInvoiceList();
            form.Show();
        }
    }
}
