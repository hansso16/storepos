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
    public partial class formProduct : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        DbConnection dbcon = new DbConnection();
        SqlDataReader dr = null;
        formProductList formProductList;
        int productDetailsCounter = 0;

        public formProduct(formProductList formProductList)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            this.formProductList = formProductList;
            productDetailsCounter = 0;
        }

        public void LoadCategory()
        {
            cboCategory.Items.Clear();
            con.Open();
            com = new SqlCommand("SELECT id,category FROM tblCategory order by category", con);
            dr = com.ExecuteReader();
            List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
            while (dr.Read())
            {
                //cboCategory.Items.Add(dr[1].ToString());
                dataSource.Add(new ComboBoxDTO() { Name = dr["category"].ToString(), Value = dr["id"].ToString() });
            }
            dr.Close();
            this.cboCategory.DataSource = dataSource;
            this.cboCategory.DisplayMember = "Name";
            this.cboCategory.ValueMember = "Value";
            con.Close(); 
        }

        public void LoadUOM()
        {
            cboUOM.Items.Clear();
            con.Open();
            com = new SqlCommand("SELECT id,type FROM tblUOM order by id", con);
            dr = com.ExecuteReader();
            List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
            while (dr.Read())
            {
                //cboUOM.Items.Add(dr[1].ToString());
                dataSource.Add(new ComboBoxDTO() { Name = dr["type"].ToString(), Value = dr["id"].ToString() });
            }
            dr.Close();
            this.cboUOM.DataSource = dataSource;
            this.cboUOM.DisplayMember = "Name";
            this.cboUOM.ValueMember = "Value";

            con.Close();
        }

        public void LoadPrice(string pcode)
        {
            productDetailsView.Rows.Clear();
            productDetailsView.Refresh();
            con.Open();
            com = new SqlCommand("SELECT u.id, u.type, u.description, pd.price, pd.startdate, pd.enddate FROM tblProductDetails pd " +
                "INNER JOIN tblUOM u ON u.id = pd.uom " +
                "WHERE pd.pcode = @pcode order by u.type, pd.startdate, pd.enddate asc", con);
            com.Parameters.AddWithValue("@pcode", pcode);
            dr = com.ExecuteReader();
            //int i = 0;
            productDetailsCounter = 0;
            while (dr.Read())
            {
                productDetailsView.Rows.Add(++productDetailsCounter, dr["id"].ToString()
                    , dr["type"].ToString(), dr["description"].ToString()
                    , string.Format("{0:n}", Convert.ToDecimal(dr["price"])), Convert.ToDateTime(dr["startdate"]).ToString("MM/dd/yyyy")
                    , Convert.ToDateTime(dr["enddate"]).ToString("MM/dd/yyyy"));
            }
            dr.Close();
            con.Close();
        }

        public void LoadInventory(string pcode)
        {
            dgvInventory.Rows.Clear();
            dgvInventory.Refresh();
            try
            {
                int count = 1;
                if (!string.IsNullOrWhiteSpace(txtCount.Text))
                {
                    count = Convert.ToInt32(txtCount.Text);
                }
                using (SqlConnection conn = new SqlConnection(dbcon.MyConnection()))
                {
                    conn.Open();
                    string commandText = "SELECT SUM(i.Qty) Qty, i.PCode, sl.SLID, sl.LocationName FROM tblInventory i " +
                        "LEFT JOIN tblStockLocation sl ON i.SLID = sl.SLID " +
                        "WHERE i.PCode = @pcode AND i.Qty != 0 " +
                        "GROUP BY i.PCode, sl.SLID, sl.LocationName";
                    using (SqlCommand scom = new SqlCommand(commandText, conn))
                    {
                        scom.Parameters.AddWithValue("@pcode", pcode);
                        using (SqlDataReader sdr = scom.ExecuteReader())
                        {
                            int i = 0;
                            while (sdr.Read())
                            {
                                int qty = Convert.ToInt32(sdr["Qty"]);
                                StringBuilder sb = new StringBuilder();
                                if (count == 1)
                                {
                                    sb.Append(qty + "pcs");
                                }
                                else if (count > 1)
                                {
                                    sb.Append(qty / count);
                                    sb.Append("whl");
                                    int mod = qty % count;
                                    if (mod > 0)
                                    {
                                        sb.Append("; ");
                                        sb.Append(mod);
                                        sb.Append("pc(s)");
                                    }
                                }
                                string finalQty = sb.ToString();
                                dgvInventory.Rows.Add(++i, sdr["SLID"]
                                    , sdr["LocationName"], finalQty);
                            }
                            sdr.Close();
                        }
                    }
                    conn.Close();
                }
            } catch (Exception ex)
            {
                MessageBox.Show("LoadInventory(pcode): " + ex.Message, "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            //txtPCode.ReadOnly = true;
            txtCount.Clear();
            txtPCode.Clear();
            txtDesc.Clear();
            txtBarcode.Clear();
            cboUOM.Text = "";
            cboCategory.Text = "";
            txtPrice.Clear();
            productDetailsView.Rows.Clear();
            productDetailsView.Refresh();
            dgvInventory.Rows.Clear();
            dgvInventory.Refresh();
            txtPCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this product?", "Register Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();

                    com = new SqlCommand("SELECT 1 FROM tblProduct WHERE pcode = @pcode", con);
                    com.Parameters.AddWithValue("@pcode", txtPCode.Text);
                    dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Product Code already exist. Please choose another.", "", MessageBoxButtons.OK, MessageBoxIcon.Error); // error
                        dr.Close();
                        con.Close();
                        return;
                    }
                    dr.Close();

                    int cboCategoryId = 0;
                    if (!string.IsNullOrEmpty(cboCategory.Text) || !(cboCategory.SelectedIndex == -1))
                    {
                        cboCategoryId = Convert.ToInt32(cboCategory.SelectedValue);
                    }

                    com = new SqlCommand("INSERT INTO tblProduct (pcode, barcode, pdesc, cid, count) VALUES (@pcode, @barcode, @pdesc, @cid, @count)", con);
                    com.Parameters.AddWithValue("@pcode", txtPCode.Text);
                    com.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    com.Parameters.AddWithValue("@pdesc", txtDesc.Text);
                    com.Parameters.AddWithValue("@cid", cboCategoryId);
                    com.Parameters.AddWithValue("@count", txtCount.Text);
                    com.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("Product record has been successfully saved");
                    Clear();
                    formProductList.LoadProductRecords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this product?", "Register Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int cboCategoryId = 0;
                    if (!string.IsNullOrEmpty(cboCategory.Text) || !(cboCategory.SelectedIndex == -1))
                    {
                        cboCategoryId = Convert.ToInt32(cboCategory.SelectedValue);
                    }
                    con.Open();
                    com = new SqlCommand("UPDATE tblProduct SET barcode = @barcode, pdesc = @pdesc, cid = @cid, count = @count where pcode = @pcode", con);
                    com.Parameters.AddWithValue("@pcode", txtPCode.Text);
                    com.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    com.Parameters.AddWithValue("@pdesc", txtDesc.Text);
                    com.Parameters.AddWithValue("@cid", cboCategoryId);
                    com.Parameters.AddWithValue("@count", txtCount.Text);
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product record has been successfully updated");
                    formProductList.LoadProductRecords();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
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

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8 || e.KeyChar == 13 || e.KeyChar == 46)
            {
                // accept backspace and enter and "." (period)
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

        private void btnAddPrice_Click(object sender, EventArgs e)
        {
            // Validate
            if (String.IsNullOrEmpty(cboUOM.Text) || cboUOM.SelectedIndex < 0)
            {
                MessageBox.Show("Invalid Unit. Please try again", "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboUOM.Focus();
                cboUOM.SelectAll();
                return;
            }
            foreach (DataGridViewRow row in productDetailsView.Rows)
            {
                if (row.Cells["id"].Value.ToString().Equals(cboUOM.SelectedValue.ToString()))
                {
                    MessageBox.Show("Price Level already exists", "Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (String.IsNullOrEmpty(txtPCode.Text))
            {
                MessageBox.Show("Invalid Product Code", "Product Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPCode.Focus();
                txtPCode.SelectAll();
                return;
            }
            if (String.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show("Please input PRICE", "Product Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            con.Open();
            // insert to tblProductDetails
            com = new SqlCommand("INSERT INTO tblProductDetails (pcode,uom,price,startdate,enddate) VALUES (@pcode,@uom,@price,@startdate,@enddate)", con);
            com.Parameters.AddWithValue("@pcode", txtPCode.Text);
            com.Parameters.AddWithValue("@uom", cboUOM.SelectedValue.ToString());
            com.Parameters.AddWithValue("@price", txtPrice.Text);
            com.Parameters.AddWithValue("@startdate", DateTime.Today);
            com.Parameters.AddWithValue("@enddate", new DateTime(9999, 12, 31));
            com.ExecuteNonQuery();
            con.Close();
            LoadPrice(txtPCode.Text);
        }

        private void productDetailsView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = productDetailsView.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                formProductDetails form = new formProductDetails(this);
                form.lblPCode.Text = txtPCode.Text;
                form.txtUOM.Text = productDetailsView[2, e.RowIndex].Value.ToString() + " - " + productDetailsView[3, e.RowIndex].Value.ToString();
                form.lblUOMID.Text = productDetailsView[1, e.RowIndex].Value.ToString();
                form.txtPrice.Text = productDetailsView[4, e.RowIndex].Value.ToString();
                form.txtStartDate.Text = productDetailsView[5, e.RowIndex].Value.ToString();
                form.txtEndDate.Text = productDetailsView[6, e.RowIndex].Value.ToString();

                form.ShowDialog();
                form.txtPrice.Focus();
            }
            //else if (colName == "Delete")
            //{
            //    if (MessageBox.Show("Are you sure you want to delete this price?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        con.Open();

            //        com = new SqlCommand("DELETE from tblProductDetails WHERE pcode = @pcode and uom = @uom", con);
            //        com.Parameters.AddWithValue("@pcode", this.txtPCode.Text);
            //        //com.Parameters.AddWithValue("@uom", productDetailsView[2, e.RowIndex].Value.ToString());
            //        com.Parameters.AddWithValue("@uom", productDetailsView["id", e.RowIndex].Value.ToString());
            //        com.ExecuteNonQuery();
            //        con.Close();
            //        MessageBox.Show("Price has been successfully deleted.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        LoadPrice(this.txtPCode.Text);
            //    }
            //}
        }

        private void txtPCode_Leave(object sender, EventArgs e)
        {
            if (!txtPCode.ReadOnly)
            {
                try
                {
                    con.Open();
                    com = new SqlCommand("SELECT COUNT(*) FROM tblProduct WHERE pcode = @pcode", con);
                    com.Parameters.AddWithValue("@pcode", txtPCode.Text);
                    int pcodeExists = (int) com.ExecuteScalar();
                    if (pcodeExists > 0)
                    {
                        MessageBox.Show("Product Code already exists. Please change", "Product Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPCode.Focus();
                        txtPCode.SelectAll();
                    }
                    con.Close();
                } catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message, "Product Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtCount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cboUOM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (String.IsNullOrEmpty(cboUOM.Text) || cboUOM.SelectedIndex < 0)
                {
                    MessageBox.Show("Invalid Unit. Please try again", "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboUOM.Focus();
                    cboUOM.SelectAll();
                    return;
                }
                txtPrice.Focus();
                txtPrice.SelectAll();
            }
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (String.IsNullOrEmpty(txtPrice.Text))
                {
                    MessageBox.Show("Invalid Price. Please try again", "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                    txtPrice.SelectAll();
                    return;
                }
                btnAddPrice_Click(sender, e);
            }
        }
    }
}
