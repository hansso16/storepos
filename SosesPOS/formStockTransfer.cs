using SosesPOS.util;
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
    public partial class formStockTransfer : Form
    {
        DbConnection dbcon = new DbConnection();
        private string title = "Stock Transfer";
        private int formStatus = 1;
        private int i = 1;
        public formStockTransfer()
        {
            InitializeComponent();
            this.KeyPreview = true;
            LoadSuggestedProducts();
            LoadStockLocation();
        }

        protected void LoadSuggestedProducts()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "SELECT pcode, pdesc FROM tblProduct";
                        List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dataSource.Add(new ComboBoxDTO() { Name = reader["pdesc"].ToString(), Value = reader["pcode"].ToString() });
                            }
                        }
                        cboSearch.DataSource = dataSource;
                        cboSearch.DisplayMember = "Name";
                        cboSearch.ValueMember = "Value";
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Loading Products failed: " + ex.Message, "Stock Transfer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStockLocation()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open(); 
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "SELECT SLID, LocationName, LocationType FROM tblStockLocation";
                        List < ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
                        List<ComboBoxDTO> toDataSource = new List<ComboBoxDTO>();
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dataSource.Add(new ComboBoxDTO() { Name = reader["LocationName"].ToString(), Value = reader["SLID"].ToString() });
                                toDataSource.Add(new ComboBoxDTO() { Name = reader["LocationName"].ToString(), Value = reader["SLID"].ToString() });
                            }
                        }
                        cboFrom.DataSource = dataSource;
                        cboFrom.DisplayMember = "Name";
                        cboFrom.ValueMember = "Value";
                        cboTo.DataSource = toDataSource;
                        cboTo.DisplayMember = "Name";
                        cboTo.ValueMember = "Value";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Stock Location/Site failed: " + ex.Message, "Stock Transfer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetButtons()
        {
            btnSet.Enabled = true;
            btnSave.Enabled = false;
            btnSaveAndPrint.Enabled = false;
            btnPrint.Enabled = false;
        }

        private void ClearProductDetails()
        {
            this.txtPCode.Clear();
            this.txtPDesc.Clear();
            this.txtCount.Clear();
        }

        private void ClearInventoryView()
        {
            this.dgvFromInv.Rows.Clear();
            this.dgvFromInv.Refresh();
            this.dgvToInv.Rows.Clear();
            this.dgvToInv.Refresh();
        }

        private void ClearCart()
        {
            this.dgvCart.Rows.Clear();
            this.dgvCart.Refresh();
        }

        private void ClearStockLocation()
        {
            this.cboFrom.Text = "";
            this.cboFrom.Enabled = true;
            this.cboTo.Text = "";
            this.cboTo.Enabled = true;
        }

        private void DisableProductForm()
        {
            cboSearch.Enabled = false;
            txtQty.ReadOnly = true;
        }

        private void formStockTransfer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) // New Transaction
            {
                if (this.btnNewTrans.Enabled)
                {
                    btnNewTrans_Click(sender, e);
                }

            }
            else if (e.KeyCode == Keys.F2) // Save & Print
            {
                if (this.btnSaveAndPrint.Enabled)
                {
                    //btnSaveAndPrint_Click(sender, e);
                }
            }
            else if (e.KeyCode == Keys.F6) // Set Site
            {
                if (this.btnSet.Enabled)
                {
                    btnSet_Click(sender, e);
                }
            }
            else if (e.KeyCode == Keys.F10) // Close
            {
                if (this.btnClose.Enabled)
                {
                    btnClose_Click(sender, e);
                }
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

        private void cboFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (GeneralUtil.ValidateComboBox(cboFrom, title))
                {
                    cboTo.Focus();
                    cboTo.SelectAll();
                }
            }
        }

        private void cboTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (GeneralUtil.ValidateComboBox(cboTo, title))
                {
                    cboSearch.Focus();
                    cboSearch.SelectAll();
                }
            }
        }

        private void cboSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!GeneralUtil.ValidateComboBox(cboTo, title) || !GeneralUtil.ValidateComboBox(cboFrom, title)
                    || string.IsNullOrEmpty(cboSearch.Text))
                {
                    return;
                }

                ClearProductDetails();
                ClearInventoryView();
                string fromSLID = cboFrom.SelectedValue.ToString();
                string toSLID = cboTo.SelectedValue.ToString();
                string pcode = null;
                if (cboSearch.SelectedValue == null)
                {
                    pcode = cboSearch.Text;
                }
                else
                {
                    pcode = cboSearch.SelectedValue.ToString();
                }
                try
                {
                    using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                    {
                        con.Open();
                        using (SqlCommand com = con.CreateCommand())
                        {
                            com.CommandText = "SELECT pcode, pdesc, count FROM tblProduct " +
                                "WHERE pcode = @pcode";
                            com.Parameters.AddWithValue("@pcode", pcode);
                            using (SqlDataReader reader = com.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    this.txtPCode.Text = reader["pcode"].ToString();
                                    this.txtPDesc.Text = reader["pdesc"].ToString();
                                    this.txtCount.Text = reader["count"].ToString();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid Product Code. Please try again", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    cboSearch.Focus();
                                    cboSearch.SelectAll();
                                    return;
                                }
                            }
                        }

                        using (SqlCommand com = con.CreateCommand())
                        {
                            com.CommandText = "SELECT sl.SLID, sl.LocationName, SUM(i.Qty) QTY " +
                            "FROM tblInventory i INNER JOIN tblPurchasing p ON p.PurchaseID = i.PurchaseID " +
                            "INNER JOIN tblStockLocation sl ON sl.SLID = i.SLID " +
                            "WHERE i.PCode = @pcode AND sl.SLID = @slid and Qty > 0 " + // get rin ba ng utang?
                            "GROUP BY sl.SLID, sl.LocationName";
                            com.Parameters.AddWithValue("@pcode", pcode);
                            com.Parameters.AddWithValue("@slid", fromSLID);
                            int count = 1;
                            if (!String.IsNullOrEmpty(txtCount.Text))
                            {
                                count = Convert.ToInt32(txtCount.Text);
                            }
                            using (SqlDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string finalQty = null;
                                    int qty = Convert.ToInt32(reader["QTY"]);
                                    if (count == 1)
                                    {
                                        finalQty = qty + "pcs";
                                    } else
                                    {
                                        int div = qty / count;
                                        int mod = qty % count;
                                        finalQty = div + "whl " + mod + "pc";

                                    }
                                    dgvFromInv.Rows.Add(reader["SLID"].ToString(), reader["LocationName"].ToString()
                                        , finalQty);
                                }
                            }

                            com.Parameters.Clear();
                            com.Parameters.AddWithValue("@pcode", pcode);
                            com.Parameters.AddWithValue("@slid", toSLID);
                            using (SqlDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string finalQty = null;
                                    int qty = Convert.ToInt32(reader["QTY"]);
                                    if (count == 1)
                                    {
                                        finalQty = qty + "pcs";
                                    }
                                    else
                                    {
                                        int div = qty / count;
                                        int mod = qty % count;
                                        finalQty = div + "whl " + mod + "pc";

                                    }
                                    dgvToInv.Rows.Add(reader["SLID"].ToString(), reader["LocationName"].ToString()
                                        , finalQty);
                                }
                            }
                        }
                    }
                    txtQty.Clear();
                    txtQty.Focus();
                    txtQty.SelectAll();
                } catch (Exception ex)
                {
                    MessageBox.Show("Loading Stock Location/Site failed: " + ex.Message, "Stock Transfer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtPCode.Text) || string.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("Invalid Product Code. Please try again.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (DataGridViewRow row in dgvCart.Rows)
                {
                    if (row.Cells["PCODE"].Value.ToString().Equals(txtPCode.Text))
                    {
                        MessageBox.Show("Item already exist. Please check"
                            , title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.cboSearch.Focus();
                        this.cboSearch.SelectAll();
                        return;
                    }
                }
                dgvCart.Rows.Add(i++, txtPCode.Text, txtPDesc.Text, txtQty.Text);
                txtQty.Clear();
                cboSearch.Focus();
                cboSearch.SelectAll();
            }
        }

        private void btnNewTrans_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset and generate new transaction?", "Stock Transaction", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetForm();
            }
        }

        private void ResetForm()
        {
            ClearStockLocation();
            DisableProductForm();
            ClearProductDetails();
            ClearInventoryView();
            ClearCart();
            ResetButtons();
            i = 1;
            this.cboFrom.Focus();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (!GeneralUtil.ValidateComboBox(cboTo, title) || !GeneralUtil.ValidateComboBox(cboFrom, title)
                || cboTo.SelectedValue.ToString().Equals(cboFrom.SelectedValue.ToString()))
            {
                MessageBox.Show(cboFrom.SelectedValue.ToString());
                MessageBox.Show(cboTo.SelectedValue.ToString());
                MessageBox.Show("Invalid Site. Please try again.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboFrom.Focus();
                cboFrom.SelectAll();
                return;
            }

            cboTo.Enabled = false;
            cboFrom.Enabled = false;

            cboSearch.Enabled = true;
            txtQty.ReadOnly = false;

            btnSet.Enabled = false;
            btnSave.Enabled = true;
            btnSaveAndPrint.Enabled = true;

            this.cboSearch.Focus();
            this.cboSearch.SelectAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dgvCart.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to remove '" + dgvCart.Rows[e.RowIndex].Cells["pdesc"].Value + "'from the list?"
                    , "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dgvCart.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void dgvCart_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(dgvCart_KeyPress);
            if (dgvCart.CurrentCell.OwningColumn == dgvCart.Columns["qty"])
            //if (dgvCart.Columns["dgvReturnQTY"].Index == dgvReturnQTY.Index)
            {
                TextBox tbt = e.Control as TextBox;
                if (tbt != null)
                {
                    tbt.KeyPress += new KeyPressEventHandler(dgvCart_KeyPress);
                }
            }
        }

        private void dgvCart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvCart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //string qty = dgvCart.Rows[e.RowIndex].Cells["pdesc"].Value;
                if (dgvCart.Rows[e.RowIndex].Cells["qty"].Value != null)
                {
                    string qty = dgvCart.Rows[e.RowIndex].Cells["qty"].Value.ToString();
                    if (string.IsNullOrWhiteSpace(qty) || "0".Equals(qty))
                    {
                        dgvCart.Rows.RemoveAt(e.RowIndex);
                    }
                } else
                {
                    dgvCart.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
    }
}
