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
    public partial class formSearchProductSearchIn : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        formStockIn formStockIn = null;
        public formSearchProductSearchIn(formStockIn formStockIn)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            this.formStockIn = formStockIn;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadProductRecords()
        {
            int i = 0;
            productListView.Rows.Clear();
            con.Open();
            com = new SqlCommand("select pcode, pdesc, qty from tblProduct " +
                "where pdesc like '%" + txtSearch.Text + "%'", con);
            //com = new SqlCommand("select pcode, pdesc, qty from tblProduct " +
            //    "where pdesc like '%@pdesc%'", con);
            //com.Parameters.AddWithValue("@pdesc", txtSearch.Text);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                productListView.Rows.Add(++i, dr["pcode"].ToString()
                    , dr["pdesc"].ToString(), dr["qty"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void productListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = productListView.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                if (formStockIn.txtRefNo.Text == String.Empty)
                {
                    MessageBox.Show("Please enter Reference No");
                    formStockIn.txtRefNo.Focus();
                    return;
                }
                if (formStockIn.txtStockInBy.Text == String.Empty)
                {
                    MessageBox.Show("Please enter Stock In By");
                    formStockIn.txtStockInBy.Focus();
                    return;
                }
                if (MessageBox.Show("Add this item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    //com = new SqlCommand("select * from tblProduct where pcode = @pcode", con);
                    //com.Parameters.AddWithValue("@pcode", productListView.Rows[e.RowIndex].Cells[1].Value.ToString());
                    com = new SqlCommand("insert into tblStockIn (refno,pcode,sdate,stockinby) values (@refno, @pcode, @sdate, @stockinby)", con);
                    com.Parameters.AddWithValue("@refno", formStockIn.txtRefNo.Text);
                    com.Parameters.AddWithValue("@pcode", productListView.Rows[e.RowIndex].Cells[1].Value.ToString());
                    com.Parameters.AddWithValue("@sdate", formStockIn.dtStockInDate.Value);
                    com.Parameters.AddWithValue("@stockinby", formStockIn.txtStockInBy.Text);
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully added!");
                    formStockIn.LoadStockIn();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProductRecords();
        }
    }
}
