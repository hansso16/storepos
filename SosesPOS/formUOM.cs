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
    public partial class formUOM : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        DbConnection dbcon = new DbConnection();
        SqlDataReader dr = null;
        formUOMList fromUOMList;
        public formUOM(formUOMList formUOMList)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            this.fromUOMList = formUOMList;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void formBrand_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this UOM?", "", MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("SELECT 1 FROM tblUOM WHERE type = @type", con);
                    com.Parameters.AddWithValue("@type", txtUOM.Text);
                    dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Unit of Measurement already exists. Please choose another.", "", MessageBoxButtons.OK, MessageBoxIcon.Error); // error
                        dr.Close();
                        con.Close();
                        return;
                    }
                    dr.Close();

                    com = new SqlCommand("INSERT INTO tblUOM (type, description, code) VALUES (@type, @description, @code)", con);
                    com.Parameters.AddWithValue("@type", txtUOM.Text);
                    com.Parameters.AddWithValue("@description", txtDescription.Text);
                    com.Parameters.AddWithValue("@code", GetUOMType());
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("UOM record has been successfully saved");
                    Clear();
                    fromUOMList.LoadUOMRecords();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            lblUOMId.Text = "";
            txtDescription.Clear();
            txtUOM.Clear();
            txtUOM.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this UOM?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("update tblUOM SET type = @type, description = @description, code = @code WHERE ID = @id", con);
                    com.Parameters.AddWithValue("@type", txtUOM.Text.Trim());
                    com.Parameters.AddWithValue("@description", txtDescription.Text.Trim());
                    com.Parameters.AddWithValue("@id", lblUOMId.Text);
                    com.Parameters.AddWithValue("@code", GetUOMType());
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("UOM hass been successfully updated.");
                    Clear();
                    fromUOMList.LoadUOMRecords();
                    this.Dispose();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetUOMType()
        {
            string code = null;
            if (rbtnWhl.Checked)
            {
                code = "0";
            }
            else if (rbtnBkn.Checked)
            {
                code = "1";
            }

            return code;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
