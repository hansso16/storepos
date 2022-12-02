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
    public partial class formTerminateUser : Form
    {
        DbConnection dbcon = new DbConnection();
        formUserEdit formUserEdit = null;
        UserDTO userDTO = null;
        string username = null;

        public formTerminateUser(formUserEdit formUserEdit, UserDTO userDTO)
        {
            InitializeComponent();
            this.formUserEdit = formUserEdit;
            this.userDTO = userDTO;
            this.lblUserCode.Text = formUserEdit.lblUserCode.Text;
            this.username = formUserEdit.txtUsername.Text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you wish to terminate this user? Username: "+username, "Terminate User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "UPDATE tblUser SET TerminationDate = @terminationdate, " +
                            "LastChangedTimestamp = @lastchangedtimestamp, LastChangedUserCode = @lastchangedusercode " +
                            "WHERE UserCode = @usercode";
                        com.Parameters.AddWithValue("@terminationdate", dtpTerminationDate.Value.ToString("yyyy-MM-dd"));
                        com.Parameters.AddWithValue("@lastchangedtimestamp", DateTime.Now);
                        com.Parameters.AddWithValue("@lastchangedusercode", userDTO.userCode);
                        com.Parameters.AddWithValue("@usercode", this.lblUserCode.Text);
                        com.ExecuteNonQuery();
                        if (dtpTerminationDate.Value <= DateTime.Now)
                        {
                            formUserEdit.txtStatus.Text = "TERMINATED - " + dtpTerminationDate.Value.ToString("MM/dd/yyyy");
                            formUserEdit.btnUpdateTerminationDate.Visible = false;
                            formUserEdit.btnReactivateUser.Visible = true;
                        }
                        MessageBox.Show("User access successfully updated for Username: " + username, "Terminate User"
                            , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                this.Dispose();
            }
        }
    }
}
