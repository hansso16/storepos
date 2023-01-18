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

            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            //com.Transaction = transaction;
            try
            {
                if (MessageBox.Show("Are you sure you want to update the price?", "Update Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    com = new SqlCommand("update tblProductDetails SET enddate = @newenddate WHERE pcode = @pcode and uom = @uom and enddate = @enddate", con, transaction);
                    com.Parameters.AddWithValue("@newenddate", DateTime.Today);
                    com.Parameters.AddWithValue("@pcode", lblPCode.Text);
                    com.Parameters.AddWithValue("@uom", lblUOMID.Text);
                    com.Parameters.AddWithValue("@enddate", txtEndDate.Text);
                    com.ExecuteNonQuery();

                    decimal price = Convert.ToDecimal(txtPrice.Text);

                    com = new SqlCommand("INSERT INTO tblProductDetails (pcode, uom, price, startdate, enddate) " +
                        "VALUES (@pcode, @uom, @price, @startdate, @enddate)", con, transaction);
                    com.Parameters.AddWithValue("@pcode", lblPCode.Text);
                    com.Parameters.AddWithValue("@uom", lblUOMID.Text);
                    com.Parameters.AddWithValue("@price", price);
                    com.Parameters.AddWithValue("@startdate", DateTime.Today);
                    com.Parameters.AddWithValue("@enddate", new DateTime(9999, 12, 31));
                    com.ExecuteNonQuery();

                    transaction.Commit();
                    con.Close();
                    MessageBox.Show("Price has been successfully updated.");
                    formProduct.LoadPrice(lblPCode.Text);
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
