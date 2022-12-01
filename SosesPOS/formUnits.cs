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
    public partial class formUnits : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        public formUnits()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
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
                }
                else
                {
                    location = "Broken";
                }
                uomListView.Rows.Add(++i, dr["id"].ToString(), dr["type"].ToString(), location, dr["description"].ToString());
            }
            con.Close();
        }

        private void pbAddUOM_Click(object sender, EventArgs e)
        {
            formUOM form = new formUOM(this);
            form.btnSave.Enabled = true;
            form.btnUpdate.Enabled = false;
            form.ShowDialog();
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
                }
                else if ("Broken".Equals(code))
                {
                    form.rbtnBkn.Checked = true;
                }
                form.btnSave.Enabled = false;
                form.btnUpdate.Enabled = true;
                form.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this UOM?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            LoadBankRecords();
        }

        private void pbAddBank_Click(object sender, EventArgs e)
        {
            formBank form = new formBank(this);
            form.btnSave.Enabled = true;
            form.btnUpdate.Enabled = false;
            form.ShowDialog();
        }

        public void LoadBankRecords()
        {
            int i = 0;
            dgvBankList.Rows.Clear();
            con.Open();
            com = new SqlCommand("SELECT BankId, BankName, BankFullName FROM tblBank order by BankId", con);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                dgvBankList.Rows.Add(++i, dr["BankId"].ToString(), dr["BankName"].ToString()
                    , dr["BankFullName"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void dgvBankList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dgvBankList.Columns[e.ColumnIndex].Name;
            if (colName == "BankEdit")
            {
                formBank form = new formBank(this);
                form.hlblBankId.Text = dgvBankList.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                form.txtBankName.Text = dgvBankList.Rows[e.RowIndex].Cells["NAME"].Value.ToString();
                form.txtBankFullName.Text = dgvBankList.Rows[e.RowIndex].Cells["FULLNAME"].Value.ToString();
                form.btnSave.Enabled = false;
                form.btnUpdate.Enabled = true;
                form.ShowDialog();
            }
            else if (colName == "BankDelete")
            {
                if (MessageBox.Show("Are you sure you want to delete this bank record?", "Bank Module", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("DELETE from tblBank WHERE BankId = @id", con);
                    //com.Parameters.AddWithValue("@id", dgvBankList[1, e.RowIndex].Value.ToString());
                    com.Parameters.AddWithValue("@id", dgvBankList.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Bank record has been successfully deleted.", "Bank Module", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBankRecords();
                }
            }
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

        private void pbAddCategory_Click(object sender, EventArgs e)
        {
            formCategory form = new formCategory(this);
            form.btnSave.Enabled = true;
            form.btnUpdate.Enabled = false;
            form.ShowDialog();
        }

        private void categoryListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = categoryListView.Columns[e.ColumnIndex].Name;
            if (colName == "CategoryEdit")
            {
                formCategory form = new formCategory(this);
                form.lblCategoryId.Text = categoryListView[1, e.RowIndex].Value.ToString();
                form.txtCategoryName.Text = categoryListView[2, e.RowIndex].Value.ToString();
                form.ShowDialog();
            }
            else if (colName == "CategoryDelete")
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

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            LoadCategoryRecords();
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            LoadUOMRecords();
        }

        private void tpStockLocation_Enter(object sender, EventArgs e)
        {
            LoadStockLocationList();
        }

        private void pbAddStockLocation_Click(object sender, EventArgs e)
        {
            formStockLocation form = new formStockLocation(this);
            form.btnSave.Enabled = true;
            form.btnUpdate.Enabled = false;
            form.ShowDialog();
        }

        public void LoadStockLocationList()
        {
            int i = 0;
            dgvStockLocation.Rows.Clear();
            con.Open();
            com = new SqlCommand("select SLID, LocationName, LocationType from tblStockLocation " +
                "where LocationName like '%" + this.slTxtSearch.Text + "%'", con);
            dr = com.ExecuteReader();

            while (dr.Read())
            {
                dgvStockLocation.Rows.Add(++i, dr["SLID"].ToString()
                    , dr["LocationName"].ToString(), dr["LocationType"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void dgvStockLocation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dgvStockLocation.Columns[e.ColumnIndex].Name;
            if (colName == "slEdit")
            {
                formStockLocation form = new formStockLocation(this);
                form.hlblSLID.Text = dgvStockLocation[1, e.RowIndex].Value.ToString();
                form.txtLocationName.Text = dgvStockLocation[2, e.RowIndex].Value.ToString();
                form.txtLocationType.Text = dgvStockLocation[3, e.RowIndex].Value.ToString();

                form.btnSave.Enabled = false;
                form.btnUpdate.Enabled = true;
                form.ShowDialog();
            }
            else if (colName == "slDelete")
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

        private void slTxtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadStockLocationList();
        }
    }
}
