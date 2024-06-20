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

namespace SosesPOS
{
    public partial class formCustomer : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        formCustomerList formCustomerList = null;
        public formCustomer(formCustomerList formCustomerList)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            this.formCustomerList = formCustomerList;
        }

        public void LoadAreaList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
                    //dataSource.Add(new ComboBoxDTO() { Name = "ALL", Value = "0" });
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "SELECT AreaCode, AreaName from tblArea " +
                            "WHERE EndDate = '9999-12-31'";
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    dataSource.Add(new ComboBoxDTO() { Name = dr["AreaName"].ToString(), Value = dr["AreaCode"].ToString() });
                                }
                                cboAreaList.DataSource = dataSource;
                                cboAreaList.DisplayMember = "Name";
                                cboAreaList.ValueMember = "Value";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            this.lblCustomerId.Text = "";
            this.txtAddress.Clear();
            this.txtCCode.Clear();
            this.txtName.Clear();
            this.txtCCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this customer?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string ccode = null;
                    using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                    {
                        con.Open();
                        SqlTransaction transaction = con.BeginTransaction();
                        int cid = 0;

                        using (SqlCommand com = con.CreateCommand())
                        {
                            com.Transaction = transaction;
                            com.CommandText = "SELECT NEXT VALUE FOR sqx_customer_code";
                            using (SqlDataReader dr = com.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    ccode = dr[0].ToString();
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(ccode))
                        {
                            throw new Exception("Error retrieving Customer Code.");
                        }

                        while (ccode.Length < 4)
                        {
                            ccode = "0" + ccode;
                        }
                        ccode = cboAreaList.SelectedValue.ToString() + ccode;


                        using (SqlCommand com = con.CreateCommand())
                        {
                            com.Transaction = transaction;
                            com.CommandText = "INSERT INTO tblCustomer (CustomerCode, CustomerName, CustomerAddress, DateAdded, AreaCode) " +
                                "OUTPUT inserted.CustomerId " +
                                "VALUES (@ccode, @cname, @caddress, @dateadded, @areacode)";
                            com.Parameters.AddWithValue("@ccode", ccode);
                            com.Parameters.AddWithValue("@cname", txtName.Text);
                            com.Parameters.AddWithValue("@caddress", txtAddress.Text);
                            com.Parameters.AddWithValue("@dateadded", DateTime.Now);
                            com.Parameters.AddWithValue("@areacode", cboAreaList.SelectedValue.ToString());
                            cid = Convert.ToInt32(com.ExecuteScalar());
                        }

                        if (cid <= 0)
                        {
                            throw new Exception("Error in creating Customer profile. Unable to retrieve Customer ID.");
                        }

                        using (SqlCommand com = con.CreateCommand())
                        {
                            com.Transaction = transaction;
                            com.CommandText = "INSERT INTO tblCustomerCollection (CustomerId, CustomerCode, OpenBalance) " +
                                "VALUES (@cid, @ccode, @balance)";
                            com.Parameters.AddWithValue("@ccode", ccode);
                            com.Parameters.AddWithValue("@cid", cid);
                            com.Parameters.AddWithValue("@balance", "0");
                            com.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        con.Close();
                    }

                    MessageBox.Show("Saved Successfully.\nNew Customer Code: " + ccode);
                    Clear();
                    formCustomerList.LoadCustomerRecords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this customer?", "Update Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("update tblCustomer SET CustomerCode = @ccode, CustomerName = @cname, CustomerAddress = @caddress " +
                        "WHERE CustomerId = @id", con);
                    com.Parameters.AddWithValue("@id", lblCustomerId.Text);
                    com.Parameters.AddWithValue("@ccode", txtCCode.Text.Trim());
                    com.Parameters.AddWithValue("@cname", txtName.Text.Trim());
                    com.Parameters.AddWithValue("@caddress", txtAddress.Text.Trim());
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer hass been successfully updated.");
                    Clear();
                    formCustomerList.LoadCustomerRecords();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
