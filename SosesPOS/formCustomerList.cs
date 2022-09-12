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
    public partial class formCustomerList : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();

        public formCustomerList()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
        }

        public void LoadCustomerRecords()
        {
            int i = 0;
            customerListView.Rows.Clear();
            con.Open();
            com = new SqlCommand("select * from tblCustomer " +
                "where CustomerName like '%"+this.txtSearch.Text+"%'" +
                "or CustomerCode like '%"+this.txtSearch.Text+"%'", con);
            //com.Parameters.AddWithValue("@customername", this.txtSearch.Text);
            //com.Parameters.AddWithValue("@customercode", this.txtSearch.Text);
            dr = com.ExecuteReader();
            
            while (dr.Read())
            {
                customerListView.Rows.Add(++i, dr["CustomerId"].ToString()
                    , dr["CustomerCode"].ToString(), dr["CustomerName"].ToString()
                    , dr["CustomerAddress"].ToString());
            }
            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadCustomerRecords();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            formCustomer form = new formCustomer(this);
            form.btnSave.Enabled = true;
            form.btnUpdate.Enabled = false;
            form.ShowDialog();
        }

        private void customerListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = customerListView.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                formCustomer form = new formCustomer(this);
                form.lblCustomerId.Text = customerListView[1, e.RowIndex].Value.ToString();
                form.txtCCode.Text = customerListView[2, e.RowIndex].Value.ToString();
                form.txtName.Text = customerListView[3, e.RowIndex].Value.ToString();
                form.txtAddress.Text = customerListView[4, e.RowIndex].Value.ToString();
                form.btnSave.Enabled = false;
                form.btnUpdate.Enabled = true;
                form.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this brand?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("DELETE from tblCustomer WHERE CustomerId = @id", con);
                    com.Parameters.AddWithValue("@id", customerListView[1, e.RowIndex].Value.ToString());
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer has been successfully deleted.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomerRecords();
                }
            }
        }
    }
}
