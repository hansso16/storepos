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
            this.cboPaymentMethod.SelectedText = "";
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

        private void cboPaymentMethod_SelectedValueChanged(object sender, EventArgs e)
        {
            if ("Check".Equals(cboPaymentMethod.Text))
            {
                this.lblCheckNo.Visible = true;
                this.lblCheckDate.Visible = true;
                this.txtCheckNo.Visible = true;
                this.dtpCheckDate.Visible = true;
            } else if ("Cash".Equals(cboPaymentMethod.Text))
            {
                this.lblCheckNo.Visible = false;
                this.lblCheckDate.Visible = false;
                this.txtCheckNo.Visible = false;
                this.dtpCheckDate.Visible = false;
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
            if ("Check".Equals(cboPaymentMethod.SelectedText))
            {
                if (String.IsNullOrEmpty(txtCheckNo.Text) || String.IsNullOrEmpty(dtpCheckDate.Text))
                {
                    MessageBox.Show("Invalid form details. Please check and try again", "Customer Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            } else
            {
                checkno = "";
                checkDate = "";
                checkStatus = "";
            }

            if (MessageBox.Show("Are you sure you want to save?", "Customer Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    com.Transaction = transaction;

                    com = new SqlCommand("INSERT INTO tblCustomerPayment (CustomerId, CustomerCode, ProcessDate, Amount, PaymentDate, Type, CheckNo, CheckDate, CheckStatus) " +
                        "VALUES (@customerid, @customercode, @processdate, @amount, @paymentdate, @type, @checkno, @checkdate, @checkstatus)", con, transaction);
                    com.Parameters.AddWithValue("@customerid", lblCustomerId.Text);
                    com.Parameters.AddWithValue("@customercode", cboCustomer.SelectedValue);
                    com.Parameters.AddWithValue("@processdate", DateTime.Today);
                    com.Parameters.AddWithValue("@amount", txtAmount.Text);
                    com.Parameters.AddWithValue("@paymentdate", dtpPaymentDate.Value.ToString("yyyy-MM-dd"));
                    com.Parameters.AddWithValue("@type", cboPaymentMethod.SelectedText.ToUpper());
                    com.Parameters.AddWithValue("@checkno", DBNull.Value);
                    com.Parameters.AddWithValue("@checkdate", checkDate);
                    com.Parameters.AddWithValue("@checkstatus", checkStatus);
                    com.ExecuteNonQuery();

                    com = new SqlCommand("UPDATE tblCustomerCollection SET OpenBalance -= @openbalance WHERE CustomerId = @customerid", con, transaction);
                    com.Parameters.AddWithValue("@customerid", lblCustomerId.Text);
                    com.Parameters.AddWithValue("@openbalance", txtAmount.Text);
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
            dtpCheckDate.Focus();
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
    }
}
