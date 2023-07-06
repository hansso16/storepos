using SosesPOS.DTO;
using SosesPOS.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SosesPOS
{
    public partial class formPayee : Form
    {
        DbConnection dbcon = new DbConnection();
        UserDTO user = null;
        formWriteCheckList formWriteCheckList = null;
        public formPayee(UserDTO user, formWriteCheckList formWriteCheckList)
        {
            InitializeComponent();
            this.user = user;
            this.formWriteCheckList = formWriteCheckList;

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    LoadCategory(con, 101);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Payee: formPayee(): " + ex.Message, "Payee", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategory(SqlConnection con, int categoryID)
        {
            List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
            using (SqlCommand com = con.CreateCommand())
            {
                com.CommandText = "SELECT id, category FROM tblCategory";
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataSource.Add(new ComboBoxDTO() { Name = reader["category"].ToString(), Value = reader["id"].ToString() });
                    }
                    cboCategory.DataSource = dataSource;
                    cboCategory.DisplayMember = "Name";
                    cboCategory.ValueMember = "Value";
                    cboCategory.SelectedValue = categoryID;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtVendorName.Text))
            {
                MessageBox.Show("Invalid Vendor Name", "Payee", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVendorName.Focus();
                txtVendorName.SelectAll();
                return false;
            }
            if (string.IsNullOrEmpty(txtPayeeName.Text))
            {
                MessageBox.Show("Invalid Payee Name", "Payee", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPayeeName.Focus();
                txtVendorName.SelectAll();
                return false;
            }
            if (string.IsNullOrEmpty(txtTerm.Text) || Convert.ToInt32(txtTerm.Text) < 0)
            {
                MessageBox.Show("Invalid Term", "Payee", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTerm.Focus();
                txtTerm.SelectAll();
                return false;
            }
            if (string.IsNullOrEmpty(cboCategory.Text) || cboCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Invalid Category", "Payee", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboCategory.Focus();
                cboCategory.SelectAll();
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                {
                    return;
                }
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "INSERT INTO tblPayee (PayeeShortName, PayeeName, Term, CategoryID, EntryTimestamp, LastChangedTimestamp, LastChangedUser) " +
                            "VALUES (@payeeshortname, @payeename, @term, @categoryid, @entrytimestamp, @lastchangedtimestamp, @lastchangeduser)";
                        com.Parameters.AddWithValue("@payeeshortname", txtVendorName.Text);
                        com.Parameters.AddWithValue("@payeename", txtPayeeName.Text);
                        com.Parameters.AddWithValue("@term", txtTerm.Text);
                        com.Parameters.AddWithValue("@categoryid", cboCategory.SelectedValue);
                        com.Parameters.AddWithValue("@entrytimestamp", DateTime.Now);
                        com.Parameters.AddWithValue("@lastchangedtimestamp", DateTime.Now);
                        com.Parameters.AddWithValue("@lastchangeduser", user.userCode);
                        com.ExecuteNonQuery();
                    }
                }
                //MessageBox.Show("Successfully saved new payee record.", "Payee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                formWriteCheckList.LoadVendorList();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save Payee: btnSave_Click(): " + ex.Message, "Payee", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTerm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8 || e.KeyChar == 13)
            {
                // accept backspace and enter
            }
            else if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm() || string.IsNullOrEmpty(txtPayeeCode.Text))
                {
                    return;
                }
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "UPDATE tblPayee SET PayeeShortName = @payeeshortname, PayeeName = @payeename, Term = @term " +
                            ", CategoryID = @categoryid, LastChangedTimestamp = @lastchangedtimestamp, LastChangedUser = @lastchangeduser " +
                            "WHERE PayeeCode = @payeecode";
                        com.Parameters.AddWithValue("@payeeshortname", txtVendorName.Text);
                        com.Parameters.AddWithValue("@payeename", txtPayeeName.Text);
                        com.Parameters.AddWithValue("@term", txtTerm.Text);
                        com.Parameters.AddWithValue("@categoryid", cboCategory.SelectedValue);
                        com.Parameters.AddWithValue("@payeecode", txtPayeeCode.Text);
                        com.Parameters.AddWithValue("@lastchangedtimestamp", DateTime.Now);
                        com.Parameters.AddWithValue("@lastchangeduser", user.userCode);
                        com.ExecuteNonQuery();
                    }
                }
                //MessageBox.Show("Successfully updated payee record.", "Payee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                formWriteCheckList.LoadVendorList();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save Payee: btnSave_Click(): " + ex.Message, "Payee", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtVendorName.Clear();
            txtPayeeName.Clear();
            txtTerm.Clear();
        }
    }
}
