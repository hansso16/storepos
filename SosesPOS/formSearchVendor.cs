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
    public partial class formSearchVendor : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        public formSearchVendor()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            LoadVendorList();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void LoadVendorList()
        {
            int i = 0;
            dgvVendorList.Rows.Clear();
            con.Open();
            com = new SqlCommand("select VendorID, VendorCode, VendorName, VendorAddress from tblVendor " +
                "where VendorName like '%'+@search+'%'" +
                "or VendorCode like '%'+@search+'%'", con);
            com.Parameters.AddWithValue("@search", this.txtSearch.Text);
            //com.Parameters.AddWithValue("@customercode", this.txtSearch.Text);
            dr = com.ExecuteReader();

            while (dr.Read())
            {
                dgvVendorList.Rows.Add(++i, dr["VendorID"].ToString()
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
    }
}
