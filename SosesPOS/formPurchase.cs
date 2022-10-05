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
    public partial class formPurchase : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        public int i = 0;
        public formPurchase()
        {
            InitializeComponent();
            this.KeyPreview = true;
            con = new SqlConnection(dbcon.MyConnection());
            LoadSuggestedProducts();
            LoadStockLocation();
            this.txtVCode.Focus();
            this.txtVCode.SelectAll();
            //GenerateNewTrans();
        }

        private void LoadSuggestedProducts()
        {
            try
            {
                con.Open();
                com = new SqlCommand("select pcode, pdesc from tblProduct", con);
                dr = com.ExecuteReader();
                List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
                while (dr.Read())
                {
                    dataSource.Add(new ComboBoxDTO() { Name = dr["pdesc"].ToString(), Value = dr["pcode"].ToString() });

                }
                cboSearch.DataSource = dataSource;
                cboSearch.DisplayMember = "Name";
                cboSearch.ValueMember = "Value";
                dr.Close();
                con.Close();
            } catch (Exception ex)
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
                MessageBox.Show(ex.Message, "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadStockLocation()
        {
            try
            {
                con.Open();
                com = new SqlCommand("SELECT SLID, LocationName FROM tblStockLocation", con);
                dr = com.ExecuteReader();
                List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
                while (dr.Read())
                {
                    dataSource.Add(new ComboBoxDTO() { Name = dr["LocationName"].ToString(), Value = dr["SLID"].ToString() });

                }
                cboSite.DataSource = dataSource;
                cboSite.DisplayMember = "Name";
                cboSite.ValueMember = "Value";
                dr.Close();
                con.Close();
            } catch (Exception ex)
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
                MessageBox.Show(ex.Message, "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8 || e.KeyChar == 13)
            {
                // accept backspace and enter
            }
            else if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = true;
            }
        }

        private void ClearVendorDetails()
        {
            this.hlblVendorID.Text = "";
            this.txtVCode.Clear();
            this.txtVName.Clear();
            this.txtVAddress.Clear();
            this.txtContactNumber.Clear();
            this.txtContactPerson.Clear();
            this.txtEmailAddress.Clear();
            this.txtVCode.ReadOnly = false;
            this.txtVName.ReadOnly = true;
            this.txtVAddress.ReadOnly = true;
            this.txtContactNumber.ReadOnly = true;
            this.txtContactPerson.ReadOnly = true;
            this.txtEmailAddress.ReadOnly = true;
        }

        private void ClearProductForm()
        {
            this.cboSearch.Enabled = true;
            this.txtQty.ReadOnly = false;
            this.cboUOM.Enabled = true;
            this.txtCost.ReadOnly = false;
            this.cboSite.Enabled = true;

            this.cboSearch.Text = "";
            this.txtQty.Clear();
            this.cboUOM.Text = "";
            this.txtCost.Clear();
            this.cboSite.Text = "";
        }

        private void ClearProductDetails()
        {
            this.txtPCode.Text = "";
            this.txtPDesc.Text = "";
        }

        private void ClearCart()
        {
            this.cartGridView.Enabled = true;
            this.cartGridView.Rows.Clear();
            this.cartGridView.Refresh();
            this.lblSubTotal.Text = "0";
        }

        private void ClearCostHistory()
        {
            // TODO:
            this.dgvCostHistory.Enabled = true;
            this.dgvCostHistory.Rows.Clear();
            this.dgvCostHistory.Refresh();
        }

        private void txtCost_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtVCode_Leave(object sender, EventArgs e)
        {
            try
            {
                //ClearVendorDetails();
                con.Open();
                com = new SqlCommand("SELECT VendorID, VendorName, VendorAddress, ContactPerson, ContactNumber, EmailAddress " +
                    "FROM tblVendor WHERE VendorCode = @vendorcode", con);
                com.Parameters.AddWithValue("@vendorcode", txtVCode.Text.Trim());
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        hlblVendorID.Text = dr["VendorID"].ToString();
                        txtVName.Text = dr["VendorName"].ToString();
                        txtVAddress.Text = dr["VendorAddress"].ToString();
                        txtContactNumber.Text = dr["ContactNumber"].ToString();
                        txtContactPerson.Text = dr["ContactPerson"].ToString();
                        txtEmailAddress.Text = dr["EmailAddress"].ToString();
                    }
                } else
                {
                    MessageBox.Show("Invalid Vendor Code. Please try again.", "Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtVCode.Focus();
                    txtVCode.SelectAll();
                }
                dr.Close();
                con.Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtVCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboSearch.Focus();
            }
        }

        private void cboSearch_Leave(object sender, EventArgs e)
        {
            try
            {
                ClearProductDetails();
                string pcode = null;
                if (cboSearch.SelectedValue == null)
                {
                    pcode = cboSearch.Text;
                }
                else
                {
                    pcode = cboSearch.SelectedValue.ToString();
                }
                con.Open();
                com = new SqlCommand("SELECT pcode, pdesc, count " +
                    "FROM tblProduct " +
                    "WHERE pcode = @pcode", con);
                com.Parameters.AddWithValue("@pcode", pcode);
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        this.txtPCode.Text = dr["pcode"].ToString();
                        this.txtPDesc.Text = dr["pdesc"].ToString();
                        this.txtCount.Text = dr["count"].ToString();
                        
                        cboUOM.Items.Add("BROKEN");
                        if (!String.IsNullOrEmpty(dr["count"].ToString()))
                        {
                            cboUOM.Items.Add("WHOLE");
                        } else
                        {
                            cboUOM.SelectedItem = "BROKEN";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Product Code. Please try again.", "Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboSearch.Focus();
                    cboSearch.SelectAll();
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtQty.Focus();
                txtQty.SelectAll();
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValidateQty())
                {
                    cboUOM.Focus();
                    cboUOM.SelectAll();
                }
            }
        }

        private bool ValidateQty()
        {
            if (String.IsNullOrEmpty(txtQty.Text) || Int32.Parse(txtQty.Text) <= 0)
            {
                MessageBox.Show("Invalid Qty. Please try again", "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtQty.Focus();
                this.txtQty.SelectAll();
                return false;
            }
            return true;
        }

        private void cboUOM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCost.Focus();
                this.txtCost.SelectAll();
            }
        }

        private void txtCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValidateCost())
                {
                    this.cboSite.Focus();
                    this.cboSite.SelectAll();
                }
            }
        }

        private bool ValidateCost()
        {
            if (String.IsNullOrEmpty(txtCost.Text) || Convert.ToDecimal(txtCost.Text) <= 0)
            {
                MessageBox.Show("Invalid Cost. Please try again", "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtCost.Focus();
                this.txtCost.SelectAll();
                return false;
            }
            return true;
        }

        private void cboSite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int qty = 0;
                decimal cost = 0, total = 0, subtotal = 0;

                foreach (DataGridViewRow row in cartGridView.Rows)
                {
                    if (row.Cells["PCODE"].Value.ToString().Equals(txtPCode.Text)
                        && row.Cells["SLID"].Value.ToString().Equals(cboSite.SelectedValue))
                    {
                        MessageBox.Show("Item already exist. Please check"
                            , "Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.cboSearch.Focus();
                        this.cboSearch.SelectAll();
                        return;
                    }
                }

                qty = Convert.ToInt32(txtQty.Text);
                cost = Convert.ToDecimal(txtCost.Text);
                subtotal = Convert.ToDecimal(lblSubTotal.Text);
                
                total = qty * cost;
                subtotal += total;

                this.cartGridView.Rows.Add(++i, txtPCode.Text, txtPDesc.Text
                    , this.txtQty.Text, cboUOM.Text
                    , String.Format("{0:n}", cost), String.Format("{0:n}", total)
                    , cboSite.SelectedValue, cboSite.SelectedText);
                
                this.lblSubTotal.Text = String.Format("{0:n}", subtotal);

                ClearProductDetails();
                ClearProductForm();
                //ClearPriceListView();
                //this.txtSearch.Focus();
                this.cboSearch.Focus();
            }
        }

        private void cartGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = cartGridView.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to remove this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    decimal total = Convert.ToDecimal(cartGridView.Rows[e.RowIndex].Cells["total"].Value);
                    decimal subtotal = Convert.ToDecimal(lblSubTotal.Text);
                    subtotal -= total;
                    lblSubTotal.Text = String.Format("{0:n}", subtotal);
                    cartGridView.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
    }
}
