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
    public partial class formBankList : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        public formBankList()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            LoadBankRecords();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
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
            if (colName == "Edit")
            {
                formBank form = new formBank(this);
                form.hlblBankId.Text = dgvBankList.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                form.txtBankName.Text = dgvBankList.Rows[e.RowIndex].Cells["NAME"].Value.ToString();
                form.txtBankFullName.Text = dgvBankList.Rows[e.RowIndex].Cells["FULLNAME"].Value.ToString();
                form.btnSave.Enabled = false;
                form.btnUpdate.Enabled = true;
                form.ShowDialog();
            }
            else if (colName == "Delete")
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
    }
}
