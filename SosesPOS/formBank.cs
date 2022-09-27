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
    public partial class formBank : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        DbConnection dbcon = new DbConnection();
        formBankList formBankList;
        public formBank(formBankList formBankList)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            this.formBankList = formBankList;
            this.hlblBankId.Text = "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            this.hlblBankId.Text = "";
            this.txtBankFullName.Clear();
            this.txtBankName.Clear();
            this.btnSave.Enabled = true;
            this.btnUpdate.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this bank record?", "Bank Module", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("INSERT INTO tblBank (BankName, BankFullName) VALUES (@bankname, @bankfullname)", con);
                    com.Parameters.AddWithValue("@bankname", txtBankName.Text);
                    com.Parameters.AddWithValue("@bankfullname", txtBankFullName.Text);
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Bank recordhas been successfully saved");
                    Clear();
                    formBankList.LoadBankRecords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bank Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this bank record?", "Bank Module", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("update tblBank SET BankName = @bankname, BankFullName = @bankfullname WHERE BankId = @id", con);
                    com.Parameters.AddWithValue("@bankname", txtBankName.Text.Trim());
                    com.Parameters.AddWithValue("@bankfullname", txtBankFullName.Text.Trim());
                    com.Parameters.AddWithValue("@id", hlblBankId.Text);
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Bank record has been successfully updated.");
                    Clear();
                    formBankList.LoadBankRecords();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bank Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
