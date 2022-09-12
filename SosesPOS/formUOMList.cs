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
    public partial class formUOMList : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();

        public formUOMList()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            LoadUOMRecords();
        }

        public void LoadUOMRecords()
        {
            int i = 0;
            uomListView.Rows.Clear();
            con.Open();
            com = new SqlCommand("SELECT * FROM tblUOM", con);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                string location = null;
                if ("0".Equals(dr["code"].ToString()))
                {
                    location = "Whole";
                } else
                {
                    location = "Broken";
                }
                uomListView.Rows.Add(++i, dr["id"].ToString(), dr["type"].ToString(), location, dr["description"].ToString());
            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = uomListView.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                formUOM form = new formUOM(this);
                form.lblUOMId.Text = uomListView[1, e.RowIndex].Value.ToString();
                form.txtUOM.Text = uomListView[2, e.RowIndex].Value.ToString();
                form.txtDescription.Text = uomListView[4, e.RowIndex].Value.ToString();
                string code = uomListView[3, e.RowIndex].Value.ToString();
                if ("Whole".Equals(code))
                {
                    form.rbtnWhl.Checked = true;
                } else if ("Broken".Equals(code))
                {
                    form.rbtnBkn.Checked = true;
                }
                form.btnSave.Enabled = false;
                form.btnUpdate.Enabled = true;
                form.ShowDialog();
            } else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this UOM?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("DELETE from tblUOM WHERE id = @id", con);
                    com.Parameters.AddWithValue("@id", uomListView[1, e.RowIndex].Value.ToString());
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("UOM has been successfully deleted.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUOMRecords();
                }
            }
        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            formUOM formBrand = new formUOM(this);
            formBrand.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            formUOM form = new formUOM(this);
            form.btnSave.Enabled = true;
            form.btnUpdate.Enabled = false;
            form.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
