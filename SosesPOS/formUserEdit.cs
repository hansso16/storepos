using SosesPOS.DTO;
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
    public partial class formUserEdit : Form
    {
        DbConnection dbcon = new DbConnection();
        UserDTO userDTO = null;
        RoleDTO roleDTO = null;
        string userCode = null;
        formUserList formUserList = null;

        public formUserEdit(formUserList formUserList, string userCode, UserDTO userDTO)
        {
            InitializeComponent();
            this.formUserList = formUserList;
            this.userCode = userCode;
            this.userDTO = userDTO;
            this.roleDTO = userDTO.role;
            LoadUserDetails();
        }

        private void LoadUserDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "SELECT u.UserCode, u.Username, u.TerminationDate" +
                            ", u.LastChangedTimestamp, uc.Username LastChangedUserCode, r.RoleCode, r.RoleName " +
                            "FROM tblUser u INNER JOIN tblRole r ON r.RoleId = u.RoleId " +
                            "INNER JOIN tblUser uc ON uc.UserCode = u.LastChangedUserCode " +
                            "WHERE u.UserCode = @usercode";
                        com.Parameters.AddWithValue("@usercode", userCode);
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                this.lblUserCode.Text = reader["UserCode"].ToString();
                                this.txtUsername.Text = reader["Username"].ToString();
                                this.txtAccessLevel.Text = reader["RoleCode"].ToString() + " - " + reader["RoleName"].ToString();
                                DateTime terminationDate = Convert.ToDateTime(reader["TerminationDate"]);
                                if (terminationDate > DateTime.Today)
                                {
                                    this.txtStatus.Text = "ACTIVE";
                                    this.btnUpdateTerminationDate.Visible = true;
                                    this.btnReactivateUser.Visible = false;
                                } else
                                {
                                    this.txtStatus.Text = "TERMINATED - " + terminationDate.ToString("MM/dd/yyyy");
                                    this.btnUpdateTerminationDate.Visible = false;
                                    this.btnReactivateUser.Visible = true;
                                }
                                this.txtLastChangedTimestamp.Text = reader["LastChangedTimestamp"].ToString();
                                this.txtLastChangedUserCode.Text = reader["LastChangedUserCode"].ToString();
                            } else
                            {
                                MessageBox.Show("Something went wrong. Please contact your system administrator"
                                    , "User Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Dispose();
                                return;
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "User Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            } 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            formUserList.LoadUserRecords();
            this.Dispose();
        }

        private void btnUpdateTerminationDate_Click(object sender, EventArgs e)
        {
            formTerminateUser form = new formTerminateUser(this, userDTO);
            form.Show();
        }

        private void btnUpdateAccessLevel_Click(object sender, EventArgs e)
        {
            formUpdateAccessLevel form = new formUpdateAccessLevel(this, userDTO);
            form.Show();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            formResetPassword form = new formResetPassword(this, userDTO);
            form.Show();
        }

        private void btnReactivateUser_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you wish to reactivate this user? Username: " + txtUsername.Text, "Reactivate User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                    {
                        con.Open();
                        using (SqlCommand com = con.CreateCommand())
                        {
                            com.CommandText = "UPDATE tblUser SET TerminationDate = @terminationdate, " +
                                "LastChangedTimestamp = @lastchangedtimestamp, LastChangedUserCode = @lastchangedusercode " +
                                "WHERE UserCode = @usercode";
                            com.Parameters.AddWithValue("@terminationdate", new DateTime (9999,12,31));
                            com.Parameters.AddWithValue("@lastchangedtimestamp", DateTime.Now);
                            com.Parameters.AddWithValue("@lastchangedusercode", userDTO.userCode);
                            com.Parameters.AddWithValue("@usercode", this.lblUserCode.Text);
                            com.ExecuteNonQuery();
                            this.txtStatus.Text = "ACTIVE";
                            this.btnUpdateTerminationDate.Visible = true;
                            this.btnReactivateUser.Visible = false;
                            MessageBox.Show("User access reactivated. Username: " + txtUsername.Text, "Reactivate User"
                                , MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "User Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
