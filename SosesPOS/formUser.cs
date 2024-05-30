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
    public partial class formUser : Form
    {
        DbConnection dbcon = new DbConnection();
        formUserList formUserList = null;
        UserDTO userDTO = null;
        RoleDTO roleDTO = null;
        public formUser(formUserList formUserList, UserDTO userDTO)
        {
            InitializeComponent();
            this.formUserList = formUserList;
            this.userDTO = userDTO;
            this.roleDTO = userDTO.role;
            LoadRoleRecords();
            LoadInvoiceTypeRecords();
        }

        private void LoadRoleRecords()
        {
            using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
            {
                con.Open();
                RoleService roleService = new RoleService();
                List<RoleDTO> roles = roleService.retrieveRoleByAccessLevel(con, roleDTO);
                if (roles == null || roles.Count == 0)
                {
                    MessageBox.Show("You are not authorized to access this.");
                    this.Dispose();
                    return;
                }

                List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
                foreach (RoleDTO role in roles)
                {
                    dataSource.Add(new ComboBoxDTO() { Name = role.roleCode + " - " + role.roleName, Value = role.roleId.ToString() });
                }
                cboRole.DataSource = dataSource;
                cboRole.DisplayMember = "Name";
                cboRole.ValueMember = "Value";
            }
        }

        private void LoadInvoiceTypeRecords()
        {
            using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
            {
                con.Open();

                List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
                dataSource.Add(new ComboBoxDTO() { Name = "Walk-In", Value = GlobalConstant.INV_TYPE_WIN });
                dataSource.Add(new ComboBoxDTO() { Name = "Wholesale Delivery", Value = GlobalConstant.INV_TYPE_DEL });
                cboInvoiceType.DataSource = dataSource;
                cboInvoiceType.DisplayMember = "Name";
                cboInvoiceType.ValueMember = "Value";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            this.txtUsername.Clear();
            ResetPassword();
            this.cboRole.Text = "";
            this.txtUsername.Focus();
        }

        private void ResetPassword()
        {
            this.txtPassword.Clear();
            this.txtCPassword.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string cpassowrd = txtCPassword.Text;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)
                || string.IsNullOrEmpty(cpassowrd))
            {
                MessageBox.Show("Invalid Details. Please try again", "User Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
                txtUsername.SelectAll();
                return;
            }
            if (String.IsNullOrEmpty(cboRole.Text) || cboRole.SelectedIndex < 0)
            {
                MessageBox.Show("Invalid Role. Please try again", "User Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboRole.Focus();
                cboRole.SelectAll();
                return;
            }

            // validate password
            if (!PasswordUtil.isPasswordValid(password))
            {
                this.lblMessage.Text = "Password must be at least 8 characters. Please try again.";
                ResetPassword();
                this.txtPassword.Focus();
                return;
            }

            // compare new password
            if (!PasswordUtil.isPasswordEqual(password, cpassowrd))
            {
                this.lblMessage.Text = "New password does not match. Please try again.";
                ResetPassword();
                this.txtPassword.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();

                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "SELECT 1 FROM tblUser WHERE Username = @username";
                        com.Parameters.AddWithValue("@username", username);
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                //error
                                this.lblMessage.Text = "Username already exists. Please try again.";
                                ResetForm();
                                return;
                            }
                        }
                    }

                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "INSERT INTO tblUser (Username, Password, TerminationDate" +
                            ", LastChangedTimestamp, LastChangedUserCode, RoleId, InvoiceType) " +
                            "VALUES (@username, @password, @terminationdate, @lastchangedtimestamp, @lastchangedusercode, @roleid, @invoicetype)";
                        com.Parameters.AddWithValue("@username", username);
                        com.Parameters.AddWithValue("@password", BCryptNet.HashPassword(password));
                        com.Parameters.AddWithValue("@terminationdate", new DateTime(9999,12,31));
                        com.Parameters.AddWithValue("@lastchangedtimestamp", DateTime.Now);
                        com.Parameters.AddWithValue("@lastchangedusercode", userDTO.userCode);
                        com.Parameters.AddWithValue("@roleid", cboRole.SelectedValue.ToString());
                        com.Parameters.AddWithValue("@invoicetype", cboInvoiceType.SelectedValue.ToString());
                        com.ExecuteNonQuery();
                    }
                    this.lblMessage.Text = "Successfully added new user: " + username;
                    this.lblMessage.ForeColor = System.Drawing.Color.Green;
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Please contact your system administrator: "+ex.Message, "User Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            formUserList.LoadUserRecords();
            ResetForm();
        }
    }
}
