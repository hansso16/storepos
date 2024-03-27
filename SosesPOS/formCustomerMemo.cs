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
using SosesPOS.DTO;

namespace SosesPOS
{
    public partial class formCustomerMemo : Form
    {
        DbConnection dbcon = new DbConnection();
        public formCustomerMemo()
        {
            InitializeComponent();
            this.cboMemoType.SelectedIndex = 0;
        }

        private void ClearForm()
        {
            this.lblCustomerId.Text = "";
            this.lblOpenBalance.Text = "";
            this.cboCustomer.Text = "";
            this.txtAmount.Text = "";
            this.dtpPaymentDate.Value = DateTime.Today;
        }

        public void LoadCustomerList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    List<ComboBoxDTO> dataSource = null;
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "select CustomerId, CustomerCode, CustomerName from tblCustomer";
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dataSource = new List<ComboBoxDTO>();
                                while (dr.Read())
                                {
                                    dataSource.Add(new ComboBoxDTO() { Name = dr["CustomerName"].ToString(), Value = dr["CustomerCode"].ToString() });
                                }
                                cboCustomer.DataSource = dataSource;
                                cboCustomer.DisplayMember = "Name";
                                cboCustomer.ValueMember = "Value";
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Customer Memo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearForm();
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
                ComboBoxDTO cbo = (ComboBoxDTO)cboCustomer.SelectedItem;
                cboCustomer.Text = cbo.Name;
            }
            using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
            {
                con.Open();
                using (SqlCommand com = con.CreateCommand())
                {
                    com.CommandText = "select CustomerId, OpenBalance from tblCustomerCollection " +
                        "where CustomerCode = @ccode";
                    com.Parameters.AddWithValue("@ccode", ccode);
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
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
                            MessageBox.Show("Invalid Customer Code. Please try again", "Customer Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cboCustomer.Focus();
                            cboCustomer.SelectAll();
                            return;
                        }
                    }
                }
            }
            //this.txtAmount.Focus();
            this.txtRefNo.Focus();
        }

        private void cboCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectCustomer();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // VALIDATE
            if (string.IsNullOrEmpty(txtAmount.Text) || Convert.ToDecimal(txtAmount.Text) <= 0 || string.IsNullOrEmpty(lblCustomerId.Text))
            {
                MessageBox.Show("Invalid Form. Please Try again.", "Customer Memo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            decimal amount = Convert.ToDecimal(txtAmount.Text);
            decimal openbalance = Convert.ToDecimal(lblOpenBalance.Text);
            char memoType = cboMemoType.Text.Equals("CREDIT")? 'C' : 'D';
            decimal runningbalance = 0;
            if (memoType == 'C')
            {
                runningbalance = openbalance - amount;
            } else if (memoType == 'D')
            {
                runningbalance = openbalance + amount;
            }

            try
            {

                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    // Insert into tblMemo
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.Transaction = transaction;
                        com.CommandText = "INSERT INTO tblMemo (CustomerID, CustomerCode, ReferenceNo, MemoType, ProcessTimestamp, Amount, MemoDate, RunningBalance) " +
                            "VALUES (@customerid, @customercode, @referenceno, @memotype, @processtimestamp, @amount, @memodate, @runningbalance)";
                        com.Parameters.AddWithValue("@customerid", lblCustomerId.Text);
                        com.Parameters.AddWithValue("@customercode", cboCustomer.SelectedValue);
                        com.Parameters.AddWithValue("@referenceno", txtRefNo.Text.ToString());
                        com.Parameters.AddWithValue("@memotype", memoType);
                        com.Parameters.AddWithValue("@processtimestamp", DateTime.Now);
                        com.Parameters.AddWithValue("@amount", amount);
                        com.Parameters.AddWithValue("@memodate", dtpPaymentDate.Value.ToString("yyyy-MM-dd"));
                        com.Parameters.AddWithValue("@runningbalance", runningbalance);
                        com.ExecuteNonQuery();
                    }

                    // Update tblCustomerCollection
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.Transaction = transaction;
                        com.Transaction = transaction;
                        if (memoType == 'C')
                        {
                            com.CommandText = "UPDATE tblCustomerCollection SET OpenBalance -= @openbalance WHERE CustomerId = @customerid";
                        } else if (memoType == 'D')
                        {
                            com.CommandText = "UPDATE tblCustomerCollection SET OpenBalance += @openbalance WHERE CustomerId = @customerid";
                        }
                        com.Parameters.AddWithValue("@customerid", lblCustomerId.Text);
                        com.Parameters.AddWithValue("@openbalance", amount);
                        com.ExecuteNonQuery();
                    }
                    MessageBox.Show("Updated Successfully");
                    transaction.Commit();
                    con.Close();
                }
                ClearForm();
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Customer Memo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtAmount.Text = String.Format("{0:n}", Convert.ToDecimal(this.txtAmount.Text));
                btnSubmit.Focus();

            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-')
            {
                // accept '-' 
            }
            else if (e.KeyChar == 8)
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

        private void txtRefNo_KeyDown(object sender, KeyEventArgs e)
        {
            this.txtAmount.Focus();
        }
    }
}
