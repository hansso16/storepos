using SosesPOS.Auth;
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
using BCryptNet = BCrypt.Net.BCrypt;

namespace SosesPOS
{
    public partial class formResetPassword : Form
    {
        DbConnection dbcon = new DbConnection();
        formUserEdit formUserEdit = null;
        UserDTO userDTO = null;
        string username = null;

        public formResetPassword(formUserEdit formUserEdit, UserDTO userDTO)
        {
            InitializeComponent();
            this.formUserEdit = formUserEdit;
            this.userDTO = userDTO;
            this.username = formUserEdit.txtUsername.Text;
        }

        private void ResetForm()
        {
            this.txtCNewPassword.Clear();
            this.txtNewPassword.Clear();
            this.txtNewPassword.Focus();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string newPassword = this.txtNewPassword.Text;
            string cNewPassword = this.txtCNewPassword.Text;
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Invalid User Details", "Reset Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            } else if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(cNewPassword))
            {
                this.lblMessage.Text = "Invalid Password. Please try again.";
                ResetForm();
            }

            // validate password
            if (!PasswordUtil.isPasswordValid(newPassword))
            {
                this.lblMessage.Text = "Password must be at least 8 characters. Please try again.";
                ResetForm();
                return;
            }

            // compare new password
            if (!PasswordUtil.isPasswordEqual(newPassword, cNewPassword))
            {
                this.lblMessage.Text = "New password does not match. Please try again.";
                ResetForm();
                return;
            }

            // save new password
            using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
            {
                con.Open();
                UserService userService = new UserService();
                userService.updateUserPassword(con, username, BCryptNet.HashPassword(newPassword));
            }
            MessageBox.Show("Reset password is successful for user: " + username, "Reset Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
        }
    }
}
