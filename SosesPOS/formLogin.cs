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
using SosesPOS.DTO;
using SosesPOS.Auth;
using BCryptNet = BCrypt.Net.BCrypt;

namespace SosesPOS
{
    public partial class formLogin : Form
    {
        DbConnection dbcon = new DbConnection();
        public formLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.lblError.Text = "";
            string username = this.txtUsername.Text;
            string password = this.txtPassword.Text;
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
            {
                this.lblError.Text = "Invalid Username/Password. Please try again.";
                return;
            }
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();

                    UserService userService = new UserService();
                    UserDTO userDTO = userService.retrieverUserDetails(con, username);
                    if (userDTO == null || !BCryptNet.Verify(password, userDTO.password)
                        || DateTime.Today >= userDTO.terminationDate)
                    {
                        this.lblError.Text = "Invalid Username/Password. Please try again.";
                        ResetForm();
                        return;
                    }

                    RoleService roleService = new RoleService();
                    RoleDTO roleDTO = roleService.retrieverRoleDetails(con, userDTO);
                    if (roleDTO == null)
                    {
                        this.lblError.Text = "Invalid Username/Password. Please try again.";
                        ResetForm();
                        return;
                    }
                    userDTO.role = roleDTO;

                    this.Hide();
                    Form1 form = new Form1(this, userDTO);
                    form.SetAccessLevel();
                    form.ShowDialog();
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ClearForm()
        {
            txtUsername.Clear();
            ResetForm();
        }

        private void ResetForm()
        {
            txtPassword.Clear();
            txtUsername.Focus();
            txtUsername.SelectAll();
        }
    }
}
