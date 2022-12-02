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
using SosesPOS.Auth;
using SosesPOS.DTO;
using SosesPOS.util;
using BCryptNet = BCrypt.Net.BCrypt;

namespace SosesPOS
{
    public partial class formChangePassword : Form
    {
        DbConnection dbcon = new DbConnection();
        public formChangePassword(string username)
        {
            InitializeComponent();
            this.txtUsername.Text = username;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.lblErrorMsg.Text = "";
            this.lblErrorMsg.ForeColor = System.Drawing.Color.Red;
            string password = txtPassword.Text;
            string newPassword = txtNewPassword.Text;
            string cNewpassword = txtCNewPassword.Text;
            string username = txtUsername.Text;
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password)
                || String.IsNullOrWhiteSpace(newPassword) || String.IsNullOrWhiteSpace(cNewpassword))
            {
                this.lblErrorMsg.Text = "Something went wrong. Please try again.";
                ResetForm();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    UserService userService = new UserService();

                    // verify and authenticate password
                    UserDTO userDTO = userService.retrieverUserDetails(con, username);
                    if (userDTO == null || !BCryptNet.Verify(password, userDTO.password))
                    {
                        this.lblErrorMsg.Text = "Invalid Password. Please try again.";
                        ResetForm();
                        return;
                    }

                    // validate password
                    if (!PasswordUtil.isPasswordValid(newPassword))
                    {
                        this.lblErrorMsg.Text = "Password must be at least 8 characters. Please try again.";
                        ResetForm();
                        return;
                    }

                    // compare new password
                    if (!PasswordUtil.isPasswordEqual(newPassword, cNewpassword))
                    {
                        this.lblErrorMsg.Text = "New password does not match. Please try again.";
                        ResetForm();
                        return;
                    }

                    // save new password
                    userService.updateUserPassword(con, username, BCryptNet.HashPassword(newPassword));

                    this.lblErrorMsg.Text = "Password updated successfully!";
                    this.lblErrorMsg.ForeColor = Color.Green;
                    ResetForm();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void ResetForm()
        {
            this.txtPassword.Clear();
            this.txtNewPassword.Clear();
            this.txtCNewPassword.Clear();
            this.txtPassword.Focus();
        }
    }
}
