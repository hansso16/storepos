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
using SosesPOS.util;
using System.Text.RegularExpressions;

namespace SosesPOS
{
    public partial class formCustomerPayment : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        public formCustomerPayment()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        
        private void ClearForm()
        {
            this.lblCustomerId.Text = "";
            this.lblOpenBalance.Text = "";
            this.cboCustomer.Text = "";
            this.txtAmount.Text = "";
            this.dtpPaymentDate.Value = DateTime.Today;
            this.txtCheckNo.Text = "";
            this.dtpCheckDate.Value = DateTime.Today;
            this.cboPaymentMethod.Text = "";
            this.cboBank.Text = "";
            this.txtBankBranch.Text = "";
        }

        public void LoadCustomerList()
        {
            con.Open();
            com = new SqlCommand("select CustomerId, CustomerCode, CustomerName from tblCustomer", con);
            dr = com.ExecuteReader();
            List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
            //AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                //MessageBox.Show("HERE");
                dataSource.Add(new ComboBoxDTO() { Name = dr["CustomerName"].ToString(), Value = dr["CustomerCode"].ToString() });
                //collection.Add(dr["pdesc"].ToString());
            }
            cboCustomer.DataSource = dataSource;
            cboCustomer.DisplayMember = "Name";
            cboCustomer.ValueMember = "Value";
            //cboTest.AutoCompleteCustomSource = collection;
            dr.Close();
            con.Close();
        }

        public void LoadBankList()
        {
            try
            {
                con.Open();
                com = new SqlCommand("SELECT BankId, BankName FROM tblBank ORDER BY BankName", con);
                dr = com.ExecuteReader();
                List<ComboBoxDTO> ds = new List<ComboBoxDTO>();
                while (dr.Read())
                {
                    ds.Add(new ComboBoxDTO() { Name = dr["BankName"].ToString(), Value = dr["BankId"].ToString() });
                }
                cboBank.DataSource = ds;
                cboBank.DisplayMember = "Name";
                cboBank.ValueMember = "Value";
                dr.Close();
                con.Close();
            } catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Customer Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboPaymentMethod_SelectedValueChanged(object sender, EventArgs e)
        {
            if ("Check".Equals(cboPaymentMethod.Text))
            {
                this.lblCheckNo.Visible = true;
                this.lblCheckDate.Visible = true;
                this.txtCheckNo.Visible = true;
                this.dtpCheckDate.Visible = true;
                this.lblCheckBank.Visible = true;
                //this.txtCheckBank.Visible = true;
                this.cboBank.Visible = true;
                this.lblBankBranch.Visible = true;
                this.txtBankBranch.Visible = true;

                this.txtCheckNo.Focus();
            } else if ("Cash".Equals(cboPaymentMethod.Text))
            {
                this.lblCheckNo.Visible = false;
                this.lblCheckDate.Visible = false;
                this.txtCheckNo.Visible = false;
                this.dtpCheckDate.Visible = false;
                this.lblCheckBank.Visible = false;
                //this.txtCheckBank.Visible = false;
                this.cboBank.Visible = false;
                this.lblBankBranch.Visible = false;
                this.txtBankBranch.Visible = false;
            }
        }

        private void cboCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectCustomer();
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                // accept backspace
            }
            else if (e.KeyChar == 46)
            {
                // accept . character
            }
            else if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = true;
            }

            if (Regex.IsMatch(txtAmount.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }
        }

        private void txtCheckNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                // accept backspace
            }
            else if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = true;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // VALIDATE FORM
            if (String.IsNullOrEmpty(lblOpenBalance.Text) || String.IsNullOrEmpty(txtAmount.Text)
                || String.IsNullOrEmpty(cboPaymentMethod.Text) || String.IsNullOrEmpty(lblCustomerId.Text))
            {
                Console.WriteLine(lblOpenBalance.Text);
                Console.WriteLine(txtAmount.Text);
                Console.WriteLine(cboPaymentMethod.SelectedText);
                Console.WriteLine(lblCustomerId.Text);
                MessageBox.Show("Invalid form details. Please check and try again", "Customer Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // VALIDATE CHECK DETAILS
            string checkno = txtCheckNo.Text;
            string checkDate = dtpCheckDate.Value.ToString("yyyy-MM-dd");
            string checkStatus = "0";
            decimal openbalance = Convert.ToDecimal(lblOpenBalance.Text);
            decimal amount = Convert.ToDecimal(txtAmount.Text);
            decimal runningbalance = openbalance - amount;

            if (MessageBox.Show("Are you sure you want to save?", "Customer Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    com.Transaction = transaction;

                    com = new SqlCommand("INSERT INTO tblCustomerPayment (CustomerId, CustomerCode, ProcessTimestamp, Amount, PaymentDate, Type, CheckNo, CheckDate, CheckStatus, CheckBank, BankBranch, RunningBalance) " +
                        "VALUES (@customerid, @customercode, @processtimestamp, @amount, @paymentdate, @type, @checkno, @checkdate, @checkstatus, @checkbank, @bankbranch, @runningbalance)", con, transaction);
                    com.Parameters.AddWithValue("@customerid", lblCustomerId.Text);
                    com.Parameters.AddWithValue("@customercode", cboCustomer.SelectedValue);
                    com.Parameters.AddWithValue("@processtimestamp", DateTime.Now);
                    com.Parameters.AddWithValue("@amount", amount);
                    com.Parameters.AddWithValue("@paymentdate", dtpPaymentDate.Value.ToString("yyyy-MM-dd"));
                    com.Parameters.AddWithValue("@type", cboPaymentMethod.Text.ToUpper());
                    com.Parameters.AddWithValue("@runningbalance", runningbalance);
                    if ("Check".Equals(cboPaymentMethod.Text))
                    {
                        if (String.IsNullOrEmpty(checkno) || String.IsNullOrEmpty(dtpCheckDate.Text) || String.IsNullOrEmpty(cboBank.Text))
                        {
                            MessageBox.Show("Invalid check details. Please check and try again", "Customer Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return;
                        } else
                        {
                            com.Parameters.AddWithValue("@checkno", checkno);
                            com.Parameters.AddWithValue("@checkdate", checkDate);
                            com.Parameters.AddWithValue("@checkstatus", checkStatus);
                            com.Parameters.AddWithValue("@checkbank", cboBank.SelectedValue.ToString());
                            com.Parameters.AddWithValue("@bankbranch", txtBankBranch.Text);
                        }
                    } else
                    {
                        com.Parameters.AddWithValue("@checkno", DBNull.Value);
                        com.Parameters.AddWithValue("@checkdate", DBNull.Value);
                        com.Parameters.AddWithValue("@checkstatus", DBNull.Value);
                        com.Parameters.AddWithValue("@checkbank", DBNull.Value);
                        com.Parameters.AddWithValue("@bankbranch", DBNull.Value);
                    }
                    com.ExecuteNonQuery();

                    com = new SqlCommand("UPDATE tblCustomerCollection SET OpenBalance -= @openbalance WHERE CustomerId = @customerid", con, transaction);
                    com.Parameters.AddWithValue("@customerid", lblCustomerId.Text);
                    com.Parameters.AddWithValue("@openbalance", amount);
                    com.ExecuteNonQuery();


                    transaction.Commit();
                    con.Close();
                    MessageBox.Show("Payment successfully recorded.", "Customer Payment", MessageBoxButtons.OK
                        , MessageBoxIcon.Information);
                    ClearForm();
                }
                catch (Exception ex)
                {
                    con.Close();
                    //dr.Close();
                    MessageBox.Show(ex.Message, "Customer Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtAmount.Text = String.Format("{0:n}", Convert.ToDecimal(this.txtAmount.Text));
                cboPaymentMethod.Focus();
                
            }
        }

        private void txtCheckNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboBank.Focus();
            }
        }

        private void SelectCustomer()
        {
            this.lblOpenBalance.Text = "";
            string ccode = null;
            if (cboCustomer.SelectedValue == null)
            {
                ccode = cboCustomer.Text;
                cboCustomer.SelectedValue = ccode;
            }
            else
            {
                ccode = cboCustomer.SelectedValue.ToString();
                ComboBoxDTO cbo = (ComboBoxDTO) cboCustomer.SelectedItem;
                cboCustomer.Text = cbo.Name;
            }
            con.Open();
            com = new SqlCommand("select CustomerId, OpenBalance from tblCustomerCollection " +
                "where CustomerCode = @ccode", con);
            com.Parameters.AddWithValue("@ccode", ccode);
            dr = com.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    this.lblOpenBalance.Text = String.Format("{0:n}", dr["OpenBalance"]);
                    this.lblCustomerId.Text = dr["CustomerId"].ToString();
                }
            }
            else
            {
                dr.Close();
                con.Close();
                MessageBox.Show("Invalid Customer Code. Please try again", "Customer Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboCustomer.Focus();
                cboCustomer.SelectAll();
                return;
            }
            dr.Close();
            con.Close();

            this.txtAmount.Focus();
        }

        private void cboCustomer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SelectCustomer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void cboBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBankBranch.Focus();
            }
        }

        private void txtBankBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpCheckDate.Focus();
            }
        }
    }
}
