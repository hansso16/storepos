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
    public partial class formCategoryList : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();

        public formCategoryList()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            LoadCategoryRecords();
        }

        public void LoadCategoryRecords()
        {
            int i = 0;
            categoryListView.Rows.Clear();
            con.Open();
            com = new SqlCommand("SELECT * FROM tblCategory order by category", con);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                categoryListView.Rows.Add(++i, dr["id"].ToString(), dr["category"].ToString());
            }
            con.Close();
        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            formCategory form = new formCategory(this);
            form.btnSave.Enabled = true;
            form.btnUpdate.Enabled = false;
            form.ShowDialog();
        }

        private void categoryListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = categoryListView.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                formCategory form = new formCategory(this);
                form.lblCategoryId.Text = categoryListView[1, e.RowIndex].Value.ToString();
                form.txtCategoryName.Text = categoryListView[2, e.RowIndex].Value.ToString();
                form.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this brand?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("DELETE from tblCategory WHERE id = @id", con);
                    com.Parameters.AddWithValue("@id", categoryListView[1, e.RowIndex].Value.ToString());
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Brand has been successfully deleted.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoryRecords();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
