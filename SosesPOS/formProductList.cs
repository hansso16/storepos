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
using SosesPOS.util;
namespace SosesPOS
{
    public partial class formProductList : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();

        public formProductList()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
        }

        public void LoadProductRecords()
        {
            int i = 0;
            productListView.Rows.Clear();
            con.Open();
            com = new SqlCommand("select p.pcode, p.barcode, p.pdesc, p.count, c.category from tblProduct p " +
                "left join tblCategory c on c.id = p.cid " +
                "where p.pdesc like '%'+@search+'%' or p.pcode like '%'+@search+'%' " +
                "order by p.pdesc asc", con);
            com.Parameters.AddWithValue("@search", txtSearch.Text);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                productListView.Rows.Add(++i, dr["pcode"].ToString()
                    , dr["barcode"].ToString(), dr["pdesc"].ToString()
                    , dr["count"].ToString(), dr["category"].ToString());
            }
            dr.Close();
            con.Close();
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            formProduct formProduct = new formProduct(this);
            formProduct.btnSave.Enabled = true;
            formProduct.btnUpdate.Enabled = false;
            formProduct.LoadUOM();
            formProduct.LoadCategory();
            formProduct.ShowDialog();
            formProduct.txtPCode.Focus();
        }

        private void brandListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = productListView.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                formProduct form = new formProduct(this);
                String pcode = productListView[1, e.RowIndex].Value.ToString();
                form.txtPCode.Text = pcode;
                form.txtBarcode.Text = productListView[2, e.RowIndex].Value.ToString();
                form.txtDesc.Text = productListView[3, e.RowIndex].Value.ToString();
                form.txtCount.Text = productListView[4, e.RowIndex].Value.ToString();
                form.LoadCategory();
                form.cboCategory.SelectedIndex = form.cboCategory.FindStringExact(productListView[5, e.RowIndex].Value.ToString());

                form.btnSave.Enabled = false;
                form.btnUpdate.Enabled = true;
                form.txtPCode.ReadOnly = true;
                form.LoadPrice(pcode);
                form.LoadUOM();
                form.LoadInventory(pcode);
                form.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this brand?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();

                    com = new SqlCommand("DELETE from tblProductDetails WHERE pcode = @pcode", con);
                    com.Parameters.AddWithValue("@pcode", productListView[1, e.RowIndex].Value.ToString());
                    com.ExecuteNonQuery();

                    com = new SqlCommand("DELETE from tblProduct WHERE pcode = @pcode", con);
                    com.Parameters.AddWithValue("@pcode", productListView[1, e.RowIndex].Value.ToString());
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been successfully deleted.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProductRecords();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProductRecords();
        }
    }
}
