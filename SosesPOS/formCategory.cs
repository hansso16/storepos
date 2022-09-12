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

namespace SosesPOS
{
    public partial class formCategory : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        DbConnection dbcon = new DbConnection();
        formCategoryList formCategoryList;

        public formCategory(formCategoryList formCategoryList)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            this.formCategoryList = formCategoryList;
        }
        private void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtCategoryName.Clear();
            txtCategoryName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this category?", "Register Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("INSERT INTO tblCategory (Category) VALUES (@category)", con);
                    com.Parameters.AddWithValue("@category", txtCategoryName.Text);
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Brand record has been successfully saved");
                    Clear();
                    formCategoryList.LoadCategoryRecords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this category?", "Update Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("update tblCategory SET category = @category WHERE ID = @id", con);
                    com.Parameters.AddWithValue("@category", txtCategoryName.Text.Trim());
                    com.Parameters.AddWithValue("@id", lblCategoryId.Text);
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Brand hass been successfully updated.");
                    Clear();
                    formCategoryList.LoadCategoryRecords();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
