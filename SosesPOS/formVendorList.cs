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
    public partial class formVendorList : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        public formVendorList()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadVendorList()
        {
            int i = 0;
            dgvVendor.Rows.Clear();
            con.Open();
            com = new SqlCommand("select VendorID, VendorCode, VendorName, VendorAddress from tblVendor " +
                "where VendorCode like '%" + this.txtSearch.Text + "%'" +
                "or VendorName like '%" + this.txtSearch.Text + "%'", con);
            //com.Parameters.AddWithValue("@customername", this.txtSearch.Text);
            //com.Parameters.AddWithValue("@customercode", this.txtSearch.Text);
            dr = com.ExecuteReader();

            while (dr.Read())
            {
                dgvVendor.Rows.Add(++i, dr["VendorID"].ToString()
                    , dr["VendorCode"].ToString(), dr["VendorName"].ToString()
                    , dr["VendorAddress"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadVendorList();
        }

        private void dgvVendor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dgvVendor.Columns[e.ColumnIndex].Name;
            if (colName == "View")
            {
                //formCustomerModule form = new formCustomerModule(customerListView[1, e.RowIndex].Value.ToString());
                //form.hlblCustomerId.Text = customerListView[1, e.RowIndex].Value.ToString();
                //form.LoadCustomerProfile();
                //form.ShowDialog();
            }
            else if (colName == "Edit")
            {
                formVendor form = new formVendor(this);
                form.hlblVendorID.Text = dgvVendor[1, e.RowIndex].Value.ToString();
                
                con.Open();
                com = new SqlCommand("select VendorID, VendorCode, VendorName, VendorAddress, ContactPerson, ContactNumber, EmailAddress " +
                    "from tblVendor " +
                    "where VendorId = @vid", con);
                com.Parameters.AddWithValue("@vid", dgvVendor[1, e.RowIndex].Value.ToString());
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        form.txtVCode.Text = dr["VendorCode"].ToString();
                        form.txtName.Text = dr["VendorName"].ToString();
                        form.txtAddress.Text = dr["VendorAddress"].ToString();
                        form.txtContactPerson.Text = dr["ContactPerson"].ToString();
                        form.txtContactNumber.Text = dr["ContactNumber"].ToString();
                        form.txtEmailAddress.Text = dr["EmailAddress"].ToString();
                    }
                }
                dr.Close();
                con.Close();

                form.btnSave.Enabled = false;
                form.btnUpdate.Enabled = true;
                form.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Vendor?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("DELETE from tblVendor WHERE VendorId = @id", con);
                    com.Parameters.AddWithValue("@id", dgvVendor[1, e.RowIndex].Value.ToString());
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Vendor has been successfully deleted.", "Vendor Module", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVendorList();
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            formVendor form = new formVendor(this);
            form.btnSave.Enabled = true;
            form.btnUpdate.Enabled = false;
            form.ShowDialog();
        }
    }
}
