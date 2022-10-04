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
    public partial class formProductDetails : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        DbConnection dbcon = new DbConnection();
        formProduct formProduct = null;
        public formProductDetails(formProduct formProduct)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            this.formProduct = formProduct;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                // accept backspace
            }
            else if (e.KeyChar == 46)
            {
                // accept . character
            }
            else if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = true;
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                // accept backspace
            }
            else if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show("Please input PRICE", "Update Price", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (MessageBox.Show("Are you sure you want to update the price?", "Update Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("update tblProductDetails SET price = @price WHERE pcode = @pcode and uom = @uom", con);
                    com.Parameters.AddWithValue("@price", txtPrice.Text);
                    //com.Parameters.AddWithValue("@qty", txtQty.Text);
                    com.Parameters.AddWithValue("@pcode", lblPCode.Text);
                    com.Parameters.AddWithValue("@uom", lblUOMID.Text);
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Price has been successfully updated.");
                    formProduct.LoadPrice(lblPCode.Text);
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
