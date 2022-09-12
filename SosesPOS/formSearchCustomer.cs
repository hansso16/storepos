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
    public partial class formSearchCustomer : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        public formSearchCustomer()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            LoadCustomerRecords();
        }

        private void LoadCustomerRecords()
        {
            int i = 0;
            customerListView.Rows.Clear();
            con.Open();
            com = new SqlCommand("select * from tblCustomer " +
                "where CustomerName like '%'+@search+'%'" +
                "or CustomerCode like '%'+@search+'%'", con);
            com.Parameters.AddWithValue("@search", this.txtSearch.Text);
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadCustomerRecords();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
