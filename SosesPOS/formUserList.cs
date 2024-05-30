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
    public partial class formUserList : Form
    {
        DbConnection dbcon = new DbConnection();
        UserDTO userDTO = null;
        RoleDTO roleDTO = null;

        public formUserList(UserDTO userDTO)
        {
            InitializeComponent();
            this.userDTO = userDTO;
            this.roleDTO = userDTO.role;
            LoadUserRecords();
        }

        public void LoadUserRecords()
        {
            this.dgvUserList.Rows.Clear();
            using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
            {
                con.Open();

                using (SqlCommand com = con.CreateCommand())
                {
                    com.CommandText = "SELECT u.UserCode, u.Username, r.RoleCode, r.RoleName, u.TerminationDate, u.InvoiceType " +
                        "FROM tblUser u INNER JOIN tblRole r ON r.RoleId = u.RoleId " +
                        "WHERE r.AccessLevel <= @accesslevel AND u.Username LIKE '%'+@username+'%'";
                    com.Parameters.AddWithValue("@accesslevel", roleDTO.accessLevel);
                    com.Parameters.AddWithValue("@username", this.txtSearch.Text);
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        int i = 0;
                        DateTime date = new DateTime(9999,12,31);
                        while (reader.Read())
                        {
                            DateTime terminationDate = Convert.ToDateTime(reader["TerminationDate"]);
                            string invoiceType = reader["InvoiceType"].ToString();
                            dgvUserList.Rows.Add(++i, reader["UserCode"], reader["Username"]
                                , reader["RoleCode"], reader["RoleName"], invoiceType == GlobalConstant.INV_TYPE_WIN? "WALK-IN" : "DELIVERY"
                                , terminationDate == date? "ACTIVE" : "TERMINATED");
                        }
                    }
                }
            }
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dgvUserList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dgvUserList.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                string usercode = dgvUserList.Rows[e.RowIndex].Cells["UserCode"].Value.ToString();
                formUserEdit form = new formUserEdit(this, usercode, userDTO);
                form.ShowDialog();
            }
            else if (colName == "Delete")
            {
                //if (MessageBox.Show("Are you sure you want to delete this bank record?", "Bank Module", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    con.Open();
                //    com = new SqlCommand("DELETE from tblBank WHERE BankId = @id", con);
                //    //com.Parameters.AddWithValue("@id", dgvBankList[1, e.RowIndex].Value.ToString());
                //    com.Parameters.AddWithValue("@id", dgvBankList.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                //    com.ExecuteNonQuery();
                //    con.Close();
                //    MessageBox.Show("Bank record has been successfully deleted.", "Bank Module", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    LoadBankRecords();
                //}
            }
        }

        private void pbAddUser_Click(object sender, EventArgs e)
        {
            formUser form = new formUser(this, userDTO);
            form.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadUserRecords();
        }
    }
}
