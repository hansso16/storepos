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
using Microsoft.Reporting.WinForms;

namespace SosesPOS
{
    public partial class formWriteCheckList : Form
    {
        DbConnection dbcon = new DbConnection();
        UserDTO user = null;
        public formWriteCheckList(UserDTO user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadVendorList()
        {
            try
            {
                dgvVendorList.Rows.Clear();
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection())) 
                {
                    con.Open();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        string search = txtSearch.Text;
                        if (int.TryParse(search, out _))
                        {
                            com.CommandText = "SELECT PayeeCode, PayeeShortName, PayeeName, Term, CategoryID " +
                                "FROM tblPayee " +
                                "WHERE PayeeShortName LIKE '%'+@search+'%' " +
                                "OR PayeeName LIKE '%'+@search+'%' " +
                                "OR PayeeCode = @search ORDER BY PayeeCode, PayeeShortName, PayeeName";
                        } else
                        {
                            com.CommandText = "SELECT PayeeCode, PayeeShortName, PayeeName, Term, CategoryID " +
                                "FROM tblPayee " +
                                "WHERE PayeeShortName LIKE '%'+@search+'%' " +
                                "OR PayeeName LIKE '%'+@search+'%' " +
                                "ORDER BY PayeeCode, PayeeShortName, PayeeName";
                        }
                        com.Parameters.AddWithValue("@search", this.txtSearch.Text);
                        Console.Write(com.CommandText);
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            int i = 1;
                            while (reader.Read())
                            {
                                //dgvPayeeList.Rows.Add();
                                dgvVendorList.Rows.Add(i++, ""
                                    , reader["PayeeCode"], reader["PayeeShortName"]
                                    , reader["PayeeName"], reader["Term"]
                                    , reader["CategoryID"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Write Check List: LoadVendorList(): " + ex.Message, "Write Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadVendorList();
        }

        private void SelectPayee(int rowIndex)
        {
            formWriteCheck form = new formWriteCheck(user, this);
            form.txtPayee.Text = dgvVendorList[4, rowIndex].Value.ToString();
            int term = Convert.ToInt32(dgvVendorList[5, rowIndex].Value);
            form.dtpCheckDate.Value = DateTime.Now;
            form.txtVendorShortName.Text = dgvVendorList[3, rowIndex].Value.ToString();
            form.hlblPayeeCode.Text = dgvVendorList[2, rowIndex].Value.ToString();
            string categoryId = dgvVendorList[6, rowIndex].Value.ToString();
            form.LoadCategory(categoryId);
            form.Show();
            //form.txtCheckNo.Focus();
            //form.txtCheckNo.SelectAll();
            form.txtAmount.Focus();
            form.txtAmount.SelectAll();
        }

        private void dgvVendorList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dgvVendorList.Columns[e.ColumnIndex].Name;
            if (e.RowIndex >= 0)
            {
                if (colName == "WriteCheck")
                {
                    SelectPayee(e.RowIndex);
                }
                else if (colName == "Edit")
                {
                    formPayee form = new formPayee(user, this);
                    form.btnSave.Enabled = false;

                    form.txtPayeeCode.Text = dgvVendorList[2, e.RowIndex].Value.ToString();
                    form.txtVendorName.Text = dgvVendorList[3, e.RowIndex].Value.ToString();
                    form.txtPayeeName.Text = dgvVendorList[4, e.RowIndex].Value.ToString();
                    form.txtTerm.Text = dgvVendorList[5, e.RowIndex].Value.ToString();
                    form.cboCategory.SelectedValue = dgvVendorList[6, e.RowIndex].Value.ToString();
                    form.ShowDialog();
                }
                else if (colName == "Delete")
                {
                    String payeeCode = dgvVendorList[2, e.RowIndex].Value.ToString();
                    if (String.IsNullOrEmpty(payeeCode))
                    {
                        return;
                    }
                    if (MessageBox.Show("Are you sure you want to delete this payee?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                        {
                            con.Open();
                            using (SqlCommand com = con.CreateCommand())
                            {
                                //com.CommandText = "DELETE FROM tblPayee WHERE PayeeCode = @payeecode";
                                //com.Parameters.AddWithValue("@payeecode", payeeCode);
                                //com.ExecuteNonQuery();

                                com.CommandText = "UPDATE tblPayee SET PayeeShortName = '', PayeeName = ''" +
                                    ", LastChangedTimestamp = CURRENT_TIMESTAMP, LastChangedUser = @user " +
                                    "WHERE PayeeCode = @payeecode";
                                com.Parameters.AddWithValue("@payeecode", payeeCode);
                                com.Parameters.AddWithValue("@user", user.username);
                                com.ExecuteNonQuery();
                            }
                        }
                        //MessageBox.Show("Payee has been successfully deleted.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadVendorList();
                    }
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            formPayee form = new formPayee(user, this);
            form.btnUpdate.Enabled = false;
            form.ShowDialog();
        }

        private void btnBlankCheck_Click(object sender, EventArgs e)
        {
            try
            {
                //PrintCheck();
                formCheckReport form = new formCheckReport();
                form.ShowDialog();
            } catch (Exception ex)
            {
                MessageBox.Show("Check Writer: formWriteCheckList(): " + ex.Message, "Check Writer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int noOfRows = dgvVendorList.RowCount;
                if (noOfRows == 1)
                {
                    noOfRows -= 1;
                    SelectPayee(noOfRows);
                }
                else
                {
                    dgvVendorList.Focus();
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                try
                {
                    e.Handled = true;
                    if (1 < dgvVendorList.RowCount)
                    {
                        dgvVendorList.ClearSelection();
                        dgvVendorList.CurrentCell = dgvVendorList[2,1];
                    }
                    dgvVendorList.Focus();
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgvVendorList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                int rowIndex = dgvVendorList.CurrentCell.RowIndex;
                SelectPayee(rowIndex);
            } 
        }
    }
}
