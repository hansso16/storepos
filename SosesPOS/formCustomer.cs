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
                    con.Open();

                    com = new SqlCommand("SELECT 1 FROM tblCustomer WHERE CustomerCode = @ccode", con);
                    com.Parameters.AddWithValue("@ccode", txtCCode.Text);
                    dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Customer Code already exist. Please choose another.", "", MessageBoxButtons.OK, MessageBoxIcon.Error); // error
                        dr.Close();
                        con.Close();
                        return;
                    }
                    dr.Close();


                    com = new SqlCommand("INSERT INTO tblCustomer (CustomerCode, CustomerName, CustomerAddress) " +
                        "OUTPUT inserted.CustomerId " +
                        "VALUES (@ccode, @cname, @caddress)", con);
                    com.Parameters.AddWithValue("@ccode", txtCCode.Text);
                    com.Parameters.AddWithValue("@cname", txtName.Text);
                    com.Parameters.AddWithValue("@caddress", txtAddress.Text);
                    int cid = Convert.ToInt32(com.ExecuteScalar());

                    com = new SqlCommand("INSERT INTO tblCustomerCollection(CustomerId, CustomerCode, OpenBalance " +
                        "VALUES (@cid, @ccode, @balance", con);
                    com.Parameters.AddWithValue("@ccode", txtCCode.Text);
                    com.Parameters.AddWithValue("@cid", cid);
                    com.Parameters.AddWithValue("@balance", "0");
                    com.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("Customer record has been successfully saved");
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
