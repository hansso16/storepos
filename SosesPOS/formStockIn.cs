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
    public partial class formStockIn : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();

        public formStockIn()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
        }

        public void LoadStockIn()
        {
            int i = 0;
            stockInGridView.Rows.Clear();
            con.Open();
            //com = new SqlCommand("select s.id, s.refno, p.pcode, p.pdesc, p.qty, s.sdate, s.stockinby from tblStockIn s inner join tblProduct p on p.pcode = s.pcode", con);
            com = new SqlCommand("select * from vwStockin where refno = @refno and status = 'Pending'", con);
            com.Parameters.AddWithValue("@refno", txtRefNo.Text);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                stockInGridView.Rows.Add(++i, dr["id"].ToString()
                    , dr["refno"].ToString(), dr["pcode"].ToString()
                    , dr["pdesc"].ToString(), dr["qty"].ToString()
                    , dr["stockinby"].ToString(), dr["sdate"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void stockInGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = stockInGridView.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to remove this record?", "Remove Stock In Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    //com = new SqlCommand("select * from tblProduct where pcode = @pcode", con);
                    //com.Parameters.AddWithValue("@pcode", productListView.Rows[e.RowIndex].Cells[1].Value.ToString());
                    com = new SqlCommand("delete from tblStockIn where id = @id", con);
                    com.Parameters.AddWithValue("@id", stockInGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
                    com.ExecuteNonQuery();
                    con.Close();
                    //MessageBox.Show("Record has been successfully removed.");
                    LoadStockIn();
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            formSearchProductSearchIn form = new formSearchProductSearchIn(this);
            form.LoadProductRecords();
            form.ShowDialog();
        }

        public void Clear()
        {
            txtStockInBy.Clear();
            txtRefNo.Clear();
            dtStockInDate.Value = DateTime.Now;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (stockInGridView.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure you want to save this records?", "Stock In Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                    {
                        for (int i = 0; i < stockInGridView.RowCount; i++)
                        {
                            // update product qty
                            con.Open();
                            com = new SqlCommand("update tblProduct set qty = qty + @qty where pcode = @pcode", con);
                            com.Parameters.AddWithValue("@pcode", stockInGridView.Rows[i].Cells[3].Value.ToString());
                            com.Parameters.AddWithValue("@qty", stockInGridView.Rows[i].Cells[5].Value.ToString());
                            com.ExecuteNonQuery();
                            // stockInGridView.Rows[i].Cells[5].Value.ToString()
                            con.Close();

                            // update tblstockin qty
                            con.Open();
                            com = new SqlCommand("update tblStockIn set qty = qty + @qty, status = 'Completed' where id = @id", con);
                            com.Parameters.AddWithValue("@id", stockInGridView.Rows[i].Cells[1].Value.ToString());
                            com.Parameters.AddWithValue("@qty", stockInGridView.Rows[i].Cells[5].Value.ToString());
                            com.ExecuteNonQuery();
                            con.Close();
                        }
                        Clear();
                        LoadStockIn();
                    }
                }
            } catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void LoackStockInHistory()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            con.Open();
            //com = new SqlCommand("select s.id, s.refno, p.pcode, p.pdesc, p.qty, s.sdate, s.stockinby from tblStockIn s inner join tblProduct p on p.pcode = s.pcode", con);
            com = new SqlCommand("select * from vwStockin where status = 'Completed'", con);
            com.Parameters.AddWithValue("@refno", txtRefNo.Text);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(++i, dr["id"].ToString()
                    , dr["refno"].ToString(), dr["pcode"].ToString()
                    , dr["pdesc"].ToString(), dr["qty"].ToString()
                    , dr["stockinby"].ToString(), dr["sdate"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            con.Open();
            com = new SqlCommand("select * from vwStockin where sdate between @from and @to and status = 'Completed'", con);
            com.Parameters.AddWithValue("@from", sDateFrom.Value.ToString("MM/dd/yyyy HH:mm:ss"));
            com.Parameters.AddWithValue("@to", sDateTo.Value.ToString("MM/dd/yyyy HH:mm:ss"));
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(++i, dr["id"].ToString()
                    , dr["refno"].ToString(), dr["pcode"].ToString()
                    , dr["pdesc"].ToString(), dr["qty"].ToString()
                    , dr["sdate"].ToString(), dr["stockinby"].ToString());
            }
            dr.Close();
            con.Close();
        }
    }
}
