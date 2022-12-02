using SosesPOS.DTO;
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
using SosesPOS.Auth;
using SosesPOS.util;

namespace SosesPOS
{
    public partial class formUpdateAccessLevel : Form
    {
        DbConnection dbcon = new DbConnection();
        formUserEdit formUserEdit = null;
        UserDTO userDTO = null;
        RoleDTO roleDTO = null;
        string username = null;

        public formUpdateAccessLevel(formUserEdit formUserEdit, UserDTO userDTO)
        {
            InitializeComponent();
            this.formUserEdit = formUserEdit;
            this.userDTO = userDTO;
            this.roleDTO = userDTO.role;
            this.lblUserCode.Text = formUserEdit.lblUserCode.Text;
            this.username = formUserEdit.txtUsername.Text;
            LoadRoleRecords();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you wish to update the access level of this user? Username: " + username, "Access Level", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "UPDATE tblUser SET RoleId = @roleid, " +
                            "LastChangedTimestamp = @lastchangedtimestamp, LastChangedUserCode = @lastchangedusercode " +
                            "WHERE UserCode = @usercode";
                        com.Parameters.AddWithValue("@roleid", cboRole.SelectedValue.ToString());
                        com.Parameters.AddWithValue("@lastchangedtimestamp", DateTime.Now);
                        com.Parameters.AddWithValue("@lastchangedusercode", userDTO.userCode);
                        com.Parameters.AddWithValue("@usercode", this.lblUserCode.Text);
                        com.ExecuteNonQuery();
                        MessageBox.Show("User access successfully updated for Username: " + username, "Access Leve"
                            , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                formUserEdit.txtAccessLevel.Text = this.cboRole.GetItemText(this.cboRole.SelectedItem); ;
                this.Dispose();
                //formUserEdit.LoadUserDetails()
            }
        }
    }
}
