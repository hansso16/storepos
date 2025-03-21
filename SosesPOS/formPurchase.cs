﻿using System;
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
using SosesPOS.DTO;
using SosesPOS.DAO;

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
            this.txtVendorRefNo.Clear();
            this.txtVendorRefNo.ReadOnly = false;
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
            //this.cboSite.Text = "";
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
                txtVendorRefNo.Focus();
            }
        }

        private void cboSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    ClearProductDetails();
                    string pcode = null;
                    int count = 1;
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

                            cboUOM.Items.Clear();
                            if (!String.IsNullOrEmpty(dr["count"].ToString()))
                            {
                                cboUOM.Items.Add("WHOLE");
                                cboUOM.Items.Add("BROKEN");
                                cboUOM.SelectedItem = "WHOLE";
                                count = Convert.ToInt32(dr["count"]);
                            }
                            else
                            {
                                cboUOM.Items.Add("BROKEN");
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

                    dgvCostHistory.Rows.Clear();
                    com = new SqlCommand("SELECT Cost, StartDate from tblProductCost " +
                        "WHERE PCode = @pcode AND VendorID = @vendorid ORDER BY StartDate", con);
                    com.Parameters.AddWithValue("@pcode", pcode);
                    com.Parameters.AddWithValue("@vendorid", hlblVendorID.Text);
                    dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        int i = 0;
                        while (dr.Read())
                        {
                            dgvCostHistory.Rows.Add(++i, String.Format("{0:n}", dr["Cost"])
                                , String.Format("{0:n}", Convert.ToDecimal(dr["Cost"]) * count)
                                , Convert.ToDateTime(dr["StartDate"]).ToString("MM/dd/yyyy"));
                        }
                    }
                    dr.Close();
                    con.Close();
                    txtQty.Focus();
                    txtQty.SelectAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                if (String.IsNullOrEmpty(cboSite.Text) || cboSite.SelectedIndex < 0)
                {
                    MessageBox.Show("Invalid Site. Please try again", "Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboSite.Focus();
                    cboSite.SelectAll();
                    return;
                }

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
                string count = String.IsNullOrEmpty(txtCount.Text)? "0":txtCount.Text;

                this.cartGridView.Rows.Add(++i, txtPCode.Text, txtPDesc.Text
                    , this.txtQty.Text, cboUOM.Text
                    , String.Format("{0:n}", cost), String.Format("{0:n}", total)
                    , cboSite.SelectedValue, cboSite.SelectedText
                    , count);
                
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

        private void cartGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal price = 0, total = 0, subtotal = 0;
            int qty = 0;

            try
            {
                if (cartGridView.Columns[e.ColumnIndex].Name == "qty")
                {
                    using (DataGridViewRow row = cartGridView.Rows[e.RowIndex])
                    {
                        qty = Convert.ToInt32(row.Cells["qty"].Value);
                        price = Convert.ToDecimal(row.Cells["cost"].Value);
                        total = qty * price;
                        row.Cells["total"].Value = String.Format("{0:n}", total);
                    }
                }
                else if (cartGridView.Columns[e.ColumnIndex].Name == "cost")
                {
                    using (DataGridViewRow row = cartGridView.Rows[e.RowIndex])
                    {
                        qty = Convert.ToInt32(row.Cells["qty"].Value);
                        price = Convert.ToDecimal(row.Cells["cost"].Value);
                        total = price * qty;
                        row.Cells["cost"].Value = String.Format("{0:n}", price);
                        row.Cells["total"].Value = String.Format("{0:n}", total);
                    }
                }
                else if (cartGridView.Columns[e.ColumnIndex].Name == "total")
                {
                    using (DataGridViewRow row = cartGridView.Rows[e.RowIndex])
                    {
                        total = Convert.ToDecimal(row.Cells["total"].Value);
                        qty = Convert.ToInt32(row.Cells["qty"].Value);
                        price = total / qty;
                        row.Cells["cost"].Value = String.Format("{0:n}", price);
                        row.Cells["total"].Value = String.Format("{0:n}", total);
                    }
                }

                foreach (DataGridViewRow row in cartGridView.Rows)
                {
                    subtotal += Convert.ToDecimal(row.Cells["total"].Value);
                }
                lblSubTotal.Text = String.Format("{0:n}", subtotal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formPurchase_KeyDown(object sender, KeyEventArgs e)
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
            else if (e.KeyCode == Keys.F3) // Save
            {
                if (this.btnSave.Enabled)
                {
                    btnSave_Click(sender, e);
                }
            }
            else if (e.KeyCode == Keys.F4) // Load
            {
                if (this.btnLoad.Enabled)
                {
                    //btnLoad_Click(sender, e);
                }
            }
            else if (e.KeyCode == Keys.F5) // Print 
            {
                if (this.btnPrint.Enabled)
                {
                    //btnPrint_Click(sender, e);
                }
            }
            else if (e.KeyCode == Keys.F6) // Search Product
            {
                btnSearchVendor_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F7) // Search Customer
            {
                //btnSearchCustomer_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F10) // Close
            {
                this.Dispose();
            }
        }

        private void btnSearchVendor_Click(object sender, EventArgs e)
        {
            formSearchVendor form = new formSearchVendor();
            form.ShowDialog();
        }

        private void btnNewTrans_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Generate New Transaction?", "Purchasing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearVendorDetails();
                ClearProductForm();
                ClearProductDetails();
                ClearCostHistory();
                ClearCart();
                this.txtVCode.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PurchaseReportDTO purchaseReportDTO = new PurchaseReportDTO();
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            com.Transaction = transaction;
            try
            {
                string vendorRefNo = String.IsNullOrWhiteSpace(txtVendorRefNo.Text)? null : txtVendorRefNo.Text;
                purchaseReportDTO.vendorCode = txtVName.Text;
                purchaseReportDTO.vendorRefNo = vendorRefNo;
                purchaseReportDTO.freight = "0.00";
                purchaseReportDTO.site = cartGridView.Rows[0].Cells[8].Value.ToString();
                purchaseReportDTO.entryTimestamp = DateTime.Now;
                purchaseReportDTO.totalCost = Convert.ToDecimal(lblSubTotal.Text);
                com = new SqlCommand("INSERT INTO tblPurchasing (ReferenceNo, VendorID, VendorReferenceNo, DateIn, Status) " +
                    "OUTPUT inserted.PurchaseID " +
                    "VALUES (@refno, @vendorid, @vendorrefno, @datein, @status)", con, transaction);
                com.Parameters.AddWithValue("@refno", DBNull.Value);
                com.Parameters.AddWithValue("@vendorid", hlblVendorID.Text);
                if (vendorRefNo == null) 
                {
                    com.Parameters.AddWithValue("@vendorrefno", DBNull.Value);
                } else
                {
                    com.Parameters.AddWithValue("@vendorrefno", vendorRefNo);
                }
                com.Parameters.AddWithValue("@datein", DateTime.Today);
                com.Parameters.AddWithValue("@status", "RECORDED");
                int purchaseId = Convert.ToInt32(com.ExecuteScalar());
                com.Dispose();

                int qtyCounter = 0;
                List<PurchaseItemDTO> list = new List<PurchaseItemDTO>();
                List<NewPurchaseCostDTO> newPurchaseCostList = new List<NewPurchaseCostDTO>();
                foreach (DataGridViewRow row in cartGridView.Rows)
                {
                    string pcode = row.Cells["pcode"].Value.ToString();
                    decimal cost = Convert.ToDecimal(row.Cells["cost"].Value);
                    string unit = row.Cells["uom"].Value.ToString();
                    decimal costPerU = cost;
                    int count = Convert.ToInt32(row.Cells["count"].Value);
                    int purchaseQty = Convert.ToInt32(row.Cells["qty"].Value);
                    decimal totalCost = purchaseQty * cost;
                    qtyCounter += purchaseQty;
                    PurchaseItemDTO item = new PurchaseItemDTO();
                    item.productCode = pcode;
                    item.productDescription = row.Cells["pdesc"].Value.ToString();
                    item.count = count.ToString();
                    item.freight = 0;
                    item.qty = purchaseQty;
                    item.cost = cost;
                    item.totalCost = totalCost;

                    // insert into purchase detail.
                    com = new SqlCommand("INSERT INTO tblPurchaseDetails (PurchaseID, PCode, UOM, SLID, Qty, Cost, Total) " +
                        "VALUES (@purchaseid, @pcode, @uom, @slid, @qty, @cost, @total)", con, transaction);
                    com.Parameters.AddWithValue("@purchaseid", purchaseId);
                    com.Parameters.AddWithValue("@pcode", pcode);
                    com.Parameters.AddWithValue("@uom", row.Cells["uom"].Value);
                    com.Parameters.AddWithValue("@slid", row.Cells["slid"].Value);
                    com.Parameters.AddWithValue("@qty", purchaseQty);
                    com.Parameters.AddWithValue("@cost", cost);
                    com.Parameters.AddWithValue("@total", totalCost);
                    com.ExecuteNonQuery();


                    // saving per piece unit only
                    if (count > 0)
                    {
                        costPerU = cost / count;
                        //costPerU.ToString(0.00);
                        costPerU = decimal.Round(costPerU, 2, MidpointRounding.AwayFromZero);
                        purchaseQty = purchaseQty * count; // convet qty to pcs
                    }

                    ProductCostDAO productCostDAO = new ProductCostDAO(con, transaction);
                    ProductCostDTO productCostDTO = productCostDAO.retrieveProductCostByPCodeAndVendorID(pcode, hlblVendorID.Text);
                    if (productCostDTO != null)
                    {
                        // If there is a change in cost - update old cost and insert new cost
                        if (costPerU != productCostDTO.cost)
                        {
                            // Update EndDate to Today
                            UpdateExistingProductCost(transaction, row, productCostDTO);

                            // Insert New Cost with Active EndDate
                            InsertNewProductCost(transaction, row, costPerU, cost);

                            // Add to List
                            NewPurchaseCostDTO newPurchaseCostDTO = new NewPurchaseCostDTO();
                            newPurchaseCostDTO.productCode = pcode;
                            newPurchaseCostDTO.productDescription = item.productDescription;
                            newPurchaseCostDTO.oldCost = productCostDTO.wholeCost;
                            newPurchaseCostDTO.oldPrice = 0;
                            newPurchaseCostDTO.newCost = cost;

                            // Get Price of product
                            ProductDetailsDAO productDetailsDAO = new ProductDetailsDAO(con, transaction);
                            ProductDetailsDTO productDetailsdto = productDetailsDAO.getProductDetailsByPcode(pcode);
                            if (productDetailsdto != null)
                            {
                            newPurchaseCostDTO.oldPrice = productDetailsdto.price;
                            }
                            newPurchaseCostList.Add(newPurchaseCostDTO);
                        }
                    } else
                    {
                        InsertNewProductCost(transaction, row, costPerU, cost);
                    }

                    InventoryDAO inventoryDAO = new InventoryDAO(con, transaction);
                    int invCounter = inventoryDAO.retrieveTotalQtyByPCode(pcode); // get stock balance of item
                    item.bal = invCounter / count;

                    List<InventoryDTO> inventoryList = inventoryDAO.retrieveNegativeInventoryByPCode(pcode);
                    if (inventoryList != null && inventoryList.Count > 0)
                    {
                        foreach (InventoryDTO inventoryDTO in inventoryList) if (purchaseQty > 0)
                        {
                            int inventoryID = inventoryDTO.inventoryID;
                            int inventoryQty = inventoryDTO.qty;
                            int newInvQty = 0;

                            inventoryQty = System.Math.Abs(inventoryQty);
                            if (purchaseQty >= inventoryQty) // if new stock is higher
                            {
                                purchaseQty = purchaseQty - inventoryQty;

                                // update inventory to 0
                                UpdateInventory(transaction, purchaseId, inventoryID, 0);
                            }
                            else // if negative is higher
                            {
                                newInvQty = purchaseQty - inventoryQty;


                                // update inventory to 0 
                                UpdateInventory(transaction, purchaseId, inventoryID, 0);

                                // Excess Negative Qty
                                // Insert Negative Inventory
                                int newInvID = SaveNewInventory(transaction, 0, row, newInvQty);

                                // Get Invoice Details
                                InventoryDetailsDTO inventoryDetailsDTO = new InventoryDetailsDTO();
                                SqlCommand idCom = new SqlCommand("SELECT InvoiceId, PCode, UOM, InventoryID, Qty, SellingPrice, Location " +
                                    "FROM tblInvoiceDetails id " +
                                    "WHERE InventoryID = @inventoryid", con, transaction);
                                idCom.Parameters.AddWithValue("@inventoryid", inventoryID);
                                dr = idCom.ExecuteReader();
                                if (dr.HasRows)
                                {
                                    newInvQty = System.Math.Abs(newInvQty);
                                    if (dr.Read())
                                    {
                                        inventoryDetailsDTO.invoiceId = Convert.ToInt32(dr["InvoiceID"]);
                                        inventoryDetailsDTO.pcode = dr["PCode"].ToString();
                                        inventoryDetailsDTO.uom = Convert.ToInt32(dr["UOM"]);
                                        inventoryDetailsDTO.qty = Convert.ToInt32(dr["Qty"]);
                                        //decimal price = Convert.ToDecimal(dr["SellingPrice"]);
                                        inventoryDetailsDTO.sellingPrice = Convert.ToDecimal(dr["SellingPrice"]);
                                        //inventoryDetailsDTO.totalItemPrice = qty * price);
                                        inventoryDetailsDTO.location = Convert.ToInt32(dr["Location"]);
                                        inventoryDetailsDTO.inventoryId = newInvID;

                                    }
                                }
                                dr.Close();
                                // Update Invoice Details Qty
                                com = new SqlCommand("UPDATE tblInvoiceDetails SET Qty = @qty, TotalItemPrice = @totalitemprice " +
                                    "WHERE InvoiceId = @invoiceid and InventoryID = @inventoryid", con, transaction);
                                com.Parameters.AddWithValue("@qty", purchaseQty);
                                com.Parameters.AddWithValue("@totalitemprice", inventoryQty * inventoryDetailsDTO.sellingPrice);
                                com.Parameters.AddWithValue("@invoiceid", inventoryDetailsDTO.invoiceId);
                                com.Parameters.AddWithValue("@inventoryid", inventoryID);
                                com.ExecuteNonQuery();


                                // Insert New InvoiceDetails Qty
                                com = new SqlCommand("INSERT INTO tblInvoiceDetails (InvoiceId, PCode, UOM, Qty, SellingPrice, TotalItemPrice, Location, InventoryID) " +
                                    "VALUES (@invoiceid, @pcode, @uom, @qty, @sellingprice, @totalitemprice, @location, @inventoryid)", con, transaction);
                                com.Parameters.AddWithValue("@invoiceid", inventoryDetailsDTO.invoiceId);
                                com.Parameters.AddWithValue("@pcode", inventoryDetailsDTO.pcode);
                                com.Parameters.AddWithValue("@uom", inventoryDetailsDTO.uom);
                                com.Parameters.AddWithValue("@qty", newInvQty);
                                com.Parameters.AddWithValue("@sellingprice", inventoryDetailsDTO.sellingPrice);
                                com.Parameters.AddWithValue("@totalitemprice", inventoryDetailsDTO.sellingPrice * newInvQty);
                                com.Parameters.AddWithValue("@location", inventoryDetailsDTO.location);
                                com.Parameters.AddWithValue("@inventoryid", inventoryDetailsDTO.inventoryId);
                                com.ExecuteNonQuery();

                                purchaseQty = 0;
                            }
                        }

                        if (purchaseQty > 0) // insert positive inventory qty
                        {
                            SaveNewInventory(transaction, purchaseId, row, purchaseQty);
                        }
                    } else
                    {
                        SaveNewInventory(transaction, purchaseId, row, purchaseQty);
                    }
                    list.Add(item);
                }

                purchaseReportDTO.totalQty = qtyCounter;
                purchaseReportDTO.purchaseItemDTO = list;
                purchaseReportDTO.newPurchaseCostDTO = newPurchaseCostList;
                transaction.Commit();

                // Print Transaction
                PrintPurchaseReport(purchaseReportDTO);

                MessageBox.Show("Purchase Order saved successfully.  Vendor Ref No: " + txtVendorRefNo.Text, "Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNewTrans_Click(sender, e);
            } catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.Message, "Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally
            {
                con.Close();
                //con.Dispose();
            }
        }

        private void UpdateInventory(SqlTransaction transaction, int purchaseId, int inventoryID, int newInvQty)
        {
            com = new SqlCommand("UPDATE tblInventory SET SLID = @slid, PurchaseID = @purchaseid, Qty = @qty " +
                                                        "WHERE InventoryID = @inventoryid", con, transaction);
            com.Parameters.AddWithValue("@slid", cboSite.SelectedValue.ToString());
            com.Parameters.AddWithValue("@purchaseid", purchaseId);
            com.Parameters.AddWithValue("@qty", newInvQty);
            com.Parameters.AddWithValue("@inventoryid", inventoryID);
            com.ExecuteNonQuery();
        }

        private int SaveNewInventory(SqlTransaction transaction, int purchaseId, DataGridViewRow row, int qty)
        {
            com = new SqlCommand("INSERT INTO tblInventory (SLID, PCode, PurchaseID, Qty) " +
                "OUTPUT inserted.InventoryID VALUES (@slid, @pcode, @purchaseid, @qty)", con, transaction);
            com.Parameters.AddWithValue("@pcode", row.Cells["pcode"].Value.ToString());
            if (purchaseId > 0)
            {
                com.Parameters.AddWithValue("@slid", cboSite.SelectedValue.ToString());
                com.Parameters.AddWithValue("@purchaseid", purchaseId);
            } else
            {
                com.Parameters.AddWithValue("@slid", DBNull.Value);
                com.Parameters.AddWithValue("@purchaseid", DBNull.Value);
            }
            com.Parameters.AddWithValue("@qty", qty);
            return Convert.ToInt32(com.ExecuteScalar());
        }

        private void UpdateExistingProductCost(SqlTransaction transaction, DataGridViewRow row, ProductCostDTO productCostDTO)
        {
            try
            {
                using (SqlCommand tmpCommand = new SqlCommand("UPDATE tblProductCost SET EndDate = @newenddate " +
                    "WHERE PCode = @pcode AND VendorID = @vendorid AND EndDate = @oldenddate", con, transaction))
                {
                    tmpCommand.Parameters.AddWithValue("@pcode", row.Cells["pcode"].Value.ToString());
                    tmpCommand.Parameters.AddWithValue("@vendorid", hlblVendorID.Text);
                    tmpCommand.Parameters.AddWithValue("@newenddate", DateTime.Today);
                    tmpCommand.Parameters.AddWithValue("@oldenddate", productCostDTO.endDate);
                    tmpCommand.ExecuteNonQuery();
                }

            } catch (Exception ex)
            {
                throw new Exception("Fail to update existing tblProductCost: " + ex.Message);
            }
        }

        private void InsertNewProductCost(SqlTransaction transaction, DataGridViewRow row, decimal cost, decimal wholeCost)
        {
            try
            {
                using (SqlCommand tmpCommand = new SqlCommand("INSERT INTO tblProductCost (PCode, VendorID, Cost, StartDate, EndDate, WholeCost) " +
                    "VALUES (@pcode, @vendorid, @cost, @startdate, @enddate, @wholecost)", con, transaction))
                {
                    tmpCommand.Parameters.AddWithValue("@pcode", row.Cells["pcode"].Value.ToString());
                    tmpCommand.Parameters.AddWithValue("@vendorid", hlblVendorID.Text);
                    tmpCommand.Parameters.AddWithValue("@cost", cost);
                    tmpCommand.Parameters.AddWithValue("@startdate", DateTime.Today);
                    tmpCommand.Parameters.AddWithValue("@enddate", new DateTime(9999, 12, 31));
                    tmpCommand.Parameters.AddWithValue("@wholecost", wholeCost);
                    tmpCommand.ExecuteNonQuery();
                }

            } catch (Exception ex)
            {
                throw new Exception("Fail to insert into tblProductCost: " +ex.Message);
            }
        }

        private void txtVendorRefNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboSearch.Focus();
            }
        }

        private void PrintPurchaseReport(PurchaseReportDTO dto)
        {
            formPurchaseReportPrint form = new formPurchaseReportPrint();
            form.LoadReport(dto);
            this.Focus();
        }
    }
}
