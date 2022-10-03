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
    public partial class formStockLocationList : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        public formStockLocationList()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            LoadStockLocationList();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadStockLocationList()
        {
            int i = 0;
            dgvStockLocation.Rows.Clear();
            con.Open();
            com = new SqlCommand("select SLID, LocationName, LocationType from tblStockLocation " +
                "where LocationName like '%" + this.txtSearch.Text + "%'", con);
            dr = com.ExecuteReader();

            while (dr.Read())
            {
                dgvStockLocation.Rows.Add(++i, dr["SLID"].ToString()
                    , dr["LocationName"].ToString(), dr["LocationType"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            formStockLocation form = new formStockLocation(this);
            form.btnSave.Enabled = true;
            form.btnUpdate.Enabled = false;
            form.ShowDialog();
        }

        private void dgvVendor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dgvStockLocation.Columns[e.ColumnIndex].Name;
            if (colName == "View")
            {
                //formCustomerModule form = new formCustomerModule(customerListView[1, e.RowIndex].Value.ToString());
                //form.hlblCustomerId.Text = customerListView[1, e.RowIndex].Value.ToString();
                //form.LoadCustomerProfile();
                //form.ShowDialog();
            }
            else if (colName == "Edit")
            {
                formStockLocation form = new formStockLocation(this);
                form.hlblSLID.Text = dgvStockLocation[1, e.RowIndex].Value.ToString();
                form.txtLocationName.Text = dgvStockLocation[2, e.RowIndex].Value.ToString();
                form.txtLocationType.Text = dgvStockLocation[3, e.RowIndex].Value.ToString();

                form.btnSave.Enabled = false;
                form.btnUpdate.Enabled = true;
                form.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Location?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("DELETE from tblStockLocation WHERE SLID = @id", con);
                    com.Parameters.AddWithValue("@id", dgvStockLocation[1, e.RowIndex].Value.ToString());
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Location has been successfully deleted.", "Stock Location Module", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStockLocationList();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadStockLocationList();
        }
    }
}
