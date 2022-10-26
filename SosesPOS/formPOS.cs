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
    public partial class formPOS : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        public int i = 0;
        //formStockIn formStockIn = null;

        public formPOS()
        {
            InitializeComponent();
            this.KeyPreview = true;
            con = new SqlConnection(dbcon.MyConnection());
            //this.txtSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
            //this.txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            LoadSuggestedProducts();
            GenerateNewTrans();
            LoadLocation();
        }

        private void LoadLocation()
        {
            List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
            dataSource.Add(new ComboBoxDTO() { Name = "Warehouse", Value = "0" });
            dataSource.Add(new ComboBoxDTO() { Name = "Store", Value = "1" });
            cboLocation.DataSource = dataSource;
            cboLocation.DisplayMember = "Name";
            cboLocation.ValueMember = "Value";
        }

        private void LoadProductLocation(string pcode)
        {
            con.Open();
            com = new SqlCommand("SELECT sl.SLID, sl.LocationName, sl.LocationType " +
                "FROM tblInventory i INNER JOIN tblStockLocation sl ON sl.SLID = i.SLID " +
                "WHERE PCode = @pcode AND Qty > 0 " +
                "GROUP BY sl.SLID, sl.LocationName, sl.LocationType", con);
            com.Parameters.AddWithValue("@pcode", pcode);
            dr = com.ExecuteReader();
            List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
            //AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                //MessageBox.Show("HERE");
                dataSource.Add(new ComboBoxDTO() { Name = dr["LocationName"].ToString(), Value = dr["SLID"].ToString() });
                //collection.Add(dr["pdesc"].ToString());
            }
            cboSearch.DataSource = dataSource;
            cboSearch.DisplayMember = "Name";
            cboSearch.ValueMember = "Value";
            //cboTest.AutoCompleteCustomSource = collection;
            dr.Close();
            con.Close();
        }

        private void LoadSuggestedProducts()
        {
            //con.Open();
            //com = new SqlCommand("select pcode, pdesc from tblProduct", con);
            ////com.Parameters.AddWithValue("@pdesc", this.txtSearch.Text);
            //dr = com.ExecuteReader();
            //AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            //while (dr.Read())
            //{
            //    collection.Add(dr["pdesc"].ToString());
            //    collection.Add(dr["pcode"].ToString() + " - " +dr["pdesc"].ToString());
            //}

            //this.txtSearch.AutoCompleteCustomSource = collection;

            //dr.Close();
            //con.Close();

            con.Open();
            com = new SqlCommand("select * from tblProduct", con);
            dr = com.ExecuteReader();
            List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
            //AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                //MessageBox.Show("HERE");
                dataSource.Add(new ComboBoxDTO() { Name = dr["pdesc"].ToString(), Value = dr["pcode"].ToString() });
                //collection.Add(dr["pdesc"].ToString());
            }
            cboSearch.DataSource = dataSource;
            cboSearch.DisplayMember = "Name";
            cboSearch.ValueMember = "Value";
            //cboTest.AutoCompleteCustomSource = collection;
            dr.Close();
            con.Close();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClearProductDetails();
                ClearPriceListView();
                con.Open();
                com = new SqlCommand("select pcode, pdesc from tblProduct " +
                    "where pdesc = @pdesc", con);
                com.Parameters.AddWithValue("@pdesc", txtSearch.Text);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    this.txtPCode.Text = dr["pcode"].ToString();
                    this.txtPDesc.Text = dr["pdesc"].ToString();
                }
                dr.Close();

                com = new SqlCommand("SELECT u.id, u.type, u.description, u.code, pd.price FROM tblProductDetails pd " +
                    "INNER JOIN tblUOM u ON pd.UOM = u.ID " +
                    "WHERE pd.pcode = @pcode ", con);
                com.Parameters.AddWithValue("@pcode", txtPCode.Text);
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
                    int i = 0;
                    while (dr.Read())
                    {
                        MessageBox.Show(dr["code"].ToString());
                        string uom = dr["type"].ToString().ToUpper(); //+ " - " + dr["description"].ToString();
                        priceListView.Rows.Add(++i, dr["id"].ToString(), uom
                            , dr["price"].ToString(), "", "1234");
                        dataSource.Add(new ComboBoxDTO() { Name = uom, Value = dr["id"].ToString() });
                    }
                    this.cboUOM.DataSource = dataSource;
                    this.cboUOM.DisplayMember = "Name";
                    this.cboUOM.ValueMember = "Value";
                }
                dr.Close();
                con.Close();

                cboUOM.Focus();
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (String.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("Invalid Qty. Please try again", "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Int32.Parse(txtQty.Text) <= 0)
                {
                    MessageBox.Show("Invalid Qty. Not enough stock.");
                    this.txtQty.Focus();
                    this.txtQty.SelectAll();
                }
                else
                {
                    int qty = 0;
                    decimal price = 0, total = 0, subtotal = 0;

                    // Check for duplicates
                    foreach (DataGridViewRow row in cartGridView.Rows)
                    {
                        if (row.Cells["PCODE"].Value.ToString().Equals(txtPCode.Text)
                            && row.Cells["UOMID"].Value.ToString().Equals(cboUOM.SelectedValue))
                        {
                            MessageBox.Show("Item already exist in invoice. Please check"
                                , "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.cboSearch.Focus();
                            this.cboSearch.SelectAll();
                            return;
                        }
                    }

                    // Compute Price
                    foreach (DataGridViewRow row in priceListView.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(cboUOM.SelectedValue.ToString())) 
                        {
                            price = decimal.Parse(row.Cells[3].Value.ToString());
                            qty = Int32.Parse(txtQty.Text);
                            total = price * qty;

                        }
                    }

                    // Check Location Type
                    string pdesc = null, locationType = null;
                    con.Open();
                    com = new SqlCommand("SELECT LocationType from tblStockLocation WHERE SLID = @slid", con);
                    com.Parameters.AddWithValue("@slid", cboLocation.SelectedValue);
                    dr = com.ExecuteReader();
                    if(dr.Read())
                    {
                        locationType = dr["LocationType"].ToString();
                        if ("0".Equals(locationType))
                        {
                            pdesc = "*"+txtPDesc.Text;
                        } else
                        {
                            pdesc = txtPDesc.Text;
                        }
                    }
                    dr.Close();
                    con.Close(); 
                    // Add to Cart
                    this.cartGridView.Rows.Add(++i, "id", txtPCode.Text, pdesc
                        , qty, cboUOM.SelectedValue.ToString(), cboUOM.Text
                        , String.Format("{0:n}", price), String.Format("{0:n}", total)
                        , locationType, cboLocation.SelectedValue, txtCount.Text);

                    subtotal = Convert.ToDecimal(lblSubTotal.Text);
                    subtotal += total;
                    this.lblSubTotal.Text = String.Format("{0:n}", subtotal);
                    
                    ClearProductDetails();
                    ClearProductForm();
                    ClearPriceListView();
                    //this.txtSearch.Focus();
                    this.cboSearch.Focus();
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ClearProductDetails()
        {
            this.txtPCode.Clear();
            this.txtPDesc.Clear();
            this.txtCount.Clear();
        }

        private void ClearProductForm()
        {
            this.cboSearch.Enabled = true;
            this.cboUOM.Enabled = true;
            this.txtQty.ReadOnly = false;
            this.txtQty.Clear();
            //this.txtSearch.Clear();
            this.cboSearch.Text = "";
            this.cboUOM.DataSource = null;
            this.cboUOM.Items.Clear();
            this.cboUOM.Text = "";
            this.cboLocation.Enabled = true;
            this.cboLocation.DataSource = null;
            this.cboLocation.Items.Clear();
            this.cboLocation.Text = "";
        }

        private void ClearCustomerDetails()
        {
            this.txtCCode.ReadOnly = false;
            this.hlblCustomerId.Text = "";
            this.txtCCode.Clear();
            this.txtCName.Clear();
            this.txtCAddress.Clear();
            this.txtCCode.ReadOnly = false;
            this.txtCName.ReadOnly = true;
            this.txtCAddress.ReadOnly = true;
            this.txtOpenBalance.Clear();
        }

        private void ClearPriceListView()
        {
            this.priceListView.Rows.Clear();
            this.priceListView.Refresh();
            this.dgvInventory.Rows.Clear();
            this.dgvInventory.Refresh();
        }

        public void ClearCart()
        {
            this.cartGridView.Enabled = true;
            this.cartGridView.Rows.Clear();
            this.cartGridView.Refresh();
            this.lblSubTotal.Text = "0";
        }

        private void formPOS_KeyDown(object sender, KeyEventArgs e)
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
                    btnSaveAndPrint_Click(sender, e);
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
                    btnLoad_Click(sender, e);
                }
            }
            else if (e.KeyCode == Keys.F5) // Print 
            {
                if (this.btnPrint.Enabled)
                {
                    btnPrint_Click(sender, e);
                }
            }
            else if (e.KeyCode == Keys.F6) // Search Customer
            {
                btnSearchCustomer_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F10) // Close
            {
                this.Dispose();
            }
        }

        private void txtCCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                con.Open();
                com = new SqlCommand("SELECT c.CustomerId, c.CustomerName, c.CustomerAddress, cc.OpenBalance from tblCustomer c " +
                    "INNER JOIN tblCustomerCollection cc ON cc.CustomerId = c.CustomerId " +
                    "where c.CustomerCode = @customercode", con);
                com.Parameters.AddWithValue("@customercode", txtCCode.Text.Trim());
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    hlblCustomerId.Text = dr["CustomerId"].ToString();
                    txtCName.Text = dr["CustomerName"].ToString();
                    txtCAddress.Text = dr["CustomerAddress"].ToString();
                    txtOpenBalance.Text = String.Format("{0:n}", dr["OpenBalance"]);
                    //txtCCode.ReadOnly = true;
                }
                dr.Close();
                con.Close();

                if (txtCCode.Text.Equals("0"))
                {
                    txtCName.ReadOnly = false;
                    txtCName.Focus();
                } else
                {
                    txtCName.ReadOnly = true;
                    //txtSearch.Focus();
                    cboSearch.Focus();
                    cboSearch.SelectAll();
                }
                
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
                cboLocation.Focus();
                cboLocation.SelectAll();
                //foreach (DataGridViewRow row in priceListView.Rows)
                //{
                //    if (row.Cells["ID"].Value.ToString().Equals(cboUOM.SelectedValue))
                //    {
                //        if ("0".Equals(row.Cells["CODE"].Value.ToString()))
                //        {
                //            cboLocation.Enabled = true;
                //            cboLocation.SelectedIndex = 0;
                //            cboLocation.Focus();
                //            cboLocation.SelectAll();
                //        } else
                //        {
                //            cboLocation.SelectedIndex = cboLocation.FindStringExact("Store");
                //            cboLocation.Enabled = false;
                //            txtQty.Focus();
                //            txtQty.SelectAll();
                //        }
                //    }
                //}
            }
        }

        // F3 SAVE
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(hlblInvoiceId.Text))
            {
                // Save Invoice - Insert
                if (SaveInvoice())
                {
                    MessageBox.Show("Invoice saved successfully.  Ref No: " + txtTransNo.Text
                        , "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } 
            else
            {
                // Update Invoice - Update
                if (UpdateInvoice())
                {
                    MessageBox.Show("Invoice updated successfully.  Ref No: " + txtTransNo.Text
                        , "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (MessageBox.Show("Create new invoice?", "Sales Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetInvoiceForm();
            }
        }

        private bool UpdateInvoice()
        {
            if (String.IsNullOrEmpty(txtCCode.Text) || cartGridView.Rows.Count == 0 || String.IsNullOrEmpty(hlblInvoiceId.Text))
            {
                MessageBox.Show("Invalid Invoice Details. Please check again.", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            int orderId = 0, invoiceId = Convert.ToInt32(hlblInvoiceId.Text);
            decimal totalPrice = 0;

            if (MessageBox.Show("Are you sure you want to save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                com.Transaction = transaction;

                try
                {
                    // update totalPrice in tblInvoice
                    com = new SqlCommand("UPDATE tblInvoice SET TotalPrice = @totalprice " +
                        "OUTPUT inserted.OrderId " +
                        "WHERE ReferenceNo = @refno", con, transaction);
                    com.Parameters.AddWithValue("@refno", txtTransNo.Text);
                    com.Parameters.AddWithValue("@totalprice", Convert.ToDecimal(lblSubTotal.Text)); 
                    orderId = Convert.ToInt32(com.ExecuteScalar().ToString());
                    //invoiceId = Int32.Parse(com.ExecuteScalar().ToString());
                    
                    // update customerId in tblOrder
                    com = new SqlCommand("UPDATE tblOrder SET CustomerId = @customerid, LastUpdatedTimestamp = @lastupdatedtimestamp " +
                        "WHERE OrderId = @orderid", con, transaction);
                    com.Parameters.AddWithValue("@customerid", hlblCustomerId.Text);
                    com.Parameters.AddWithValue("@lastupdatedtimestamp", DateTime.Now);
                    com.Parameters.AddWithValue("@orderid", orderId);
                    com.ExecuteNonQuery();


                    com = new SqlCommand("DELETE FROM tblInvoiceDetails " +
                        "WHERE InvoiceId = @invoiceid", con, transaction);
                    com.Parameters.AddWithValue("@invoiceid", invoiceId);
                    com.ExecuteNonQuery();

                    foreach (DataGridViewRow row in cartGridView.Rows)
                    {
                        Queue<int> queue = new Queue<int>();
                        totalPrice += Decimal.Parse(row.Cells["total"].Value.ToString());

                        // Get Inventory Qty List
                        com = new SqlCommand("SELECT  i.InventoryID, i.Qty " +
                            "FROM tblInventory i INNER JOIN tblStockLocation sl ON sl.SLID = i.SLID " +
                            "INNER JOIN tblPurchasing p ON p.PurchaseID = i.PurchaseID " +
                            "WHERE i.PCode = @pcode AND i.SLID = @slid " +
                            "ORDER BY p.DateIn, i.InventoryID", con, transaction);
                        com.Parameters.AddWithValue("@pcode", "");
                        com.Parameters.AddWithValue("@slid", "");
                        dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            // Add to Queue
                            queue.Enqueue(dr.GetInt32(0));

                            // Identify next inventory
                            int invQty = dr.GetInt32(1);
                            int saleQty = 1;//Convert.ToInt32(row.Cells["qty"].Value);
                            int count = Convert.ToInt32(txtCount.Text);
                            if (String.IsNullOrEmpty(txtCount.Text) && count > 1)
                            {
                                saleQty = saleQty * count;
                            }
                            if (invQty > saleQty)
                            {
                                break;
                            }
                        }
                        dr.Close();


                        // Save to tblInvoiceDetails
                        while (queue.Count > 0)
                        {
                            int invID = queue.Dequeue();
                            com = new SqlCommand("INSERT INTO tblInvoiceDetails (InvoiceId, PCode, UOM, Qty, SellingPrice, TotalItemPrice, Location, InventoryID) " +
                                "VALUES (@invoiceid, @pcode, @uom, @qty, @sellingprice, @totalitemprice, @location, @inventoryid)", con, transaction);
                            com.Parameters.AddWithValue("@invoiceid", invoiceId);
                            com.Parameters.AddWithValue("@pcode", row.Cells["pcode"].Value.ToString());
                            com.Parameters.AddWithValue("@uom", row.Cells["uomid"].Value.ToString());
                            com.Parameters.AddWithValue("@qty", row.Cells["qty"].Value.ToString());
                            com.Parameters.AddWithValue("@totalitemprice", Convert.ToDecimal(row.Cells["total"].Value.ToString()));
                            com.Parameters.AddWithValue("@sellingprice", Convert.ToDecimal(row.Cells["price"].Value.ToString()));
                            com.Parameters.AddWithValue("@location", row.Cells["location"].Value.ToString());
                            com.Parameters.AddWithValue("@inventoryid", invID);
                            com.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    con.Close();
                    MessageBox.Show(ex.Message, "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                transaction.Commit();
                con.Close();
                return true;
            }
            return false;
        }

        private bool SaveInvoice()
        {
            if (String.IsNullOrEmpty(txtCCode.Text) || cartGridView.Rows.Count == 0)
            {
                MessageBox.Show("Invalid Invoice Details. Please check again.", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int orderId = 0, invoiceId = 0;
            decimal totalPrice = 0;

            if (MessageBox.Show("Are you sure you want to save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                com.Transaction = transaction;

                try
                {
                    //Save to tblOrder
                    com = new SqlCommand("INSERT INTO tblOrder (CustomerId, OrderStatus, EntryTimestamp, LastUpdatedTimestamp) " +
                        "OUTPUT inserted.OrderId " +
                        "VALUES (@customerid, @orderstatus, @entrytimestamp, @lastupdatedtimestamp)", con, transaction);
                    com.Parameters.AddWithValue("@customerid", hlblCustomerId.Text);
                    com.Parameters.AddWithValue("@orderstatus", OrderStatusConstant.INV_CREATED);
                    com.Parameters.AddWithValue("@entrytimestamp", DateTime.Now);
                    com.Parameters.AddWithValue("@lastupdatedtimestamp", DateTime.Now);
                    orderId = Convert.ToInt32(com.ExecuteScalar());
                    //orderId = Int32.Parse(com.ExecuteScalar().ToString());

                    // Save to tblInvoice
                    com = new SqlCommand("INSERT INTO tblInvoice (OrderId, ReferenceNo, TotalPrice, EntryTimestamp) " +
                        "OUTPUT inserted.InvoiceId " +
                        "VALUES (@orderid, @refno, @totalprice, @entrytimestamp)", con, transaction);
                    com.Parameters.AddWithValue("@orderid", orderId);
                    com.Parameters.AddWithValue("@refno", txtTransNo.Text);
                    com.Parameters.AddWithValue("@totalprice", Convert.ToDecimal(lblSubTotal.Text));
                    com.Parameters.AddWithValue("@entrytimestamp", DateTime.Now);
                    invoiceId = Convert.ToInt32(com.ExecuteScalar().ToString());
                    //invoiceId = Int32.Parse(com.ExecuteScalar().ToString());


                    // Get Inventory Qty List
                    foreach (DataGridViewRow row in cartGridView.Rows)
                    {
                        Queue<int> queue = new Queue<int>();
                        Queue<int> qtyQueue = new Queue<int>();
                        totalPrice += Decimal.Parse(row.Cells["total"].Value.ToString());

                        using (SqlConnection drCon = new SqlConnection(dbcon.MyConnection()))
                        {
                            drCon.Open();
                            using (SqlCommand tmpCom = new SqlCommand("SELECT  i.InventoryID, i.Qty " +
                            "FROM tblInventory i INNER JOIN tblStockLocation sl ON sl.SLID = i.SLID " +
                            "INNER JOIN tblPurchasing p ON p.PurchaseID = i.PurchaseID " +
                            "WHERE i.PCode = @pcode AND i.SLID = @slid AND i.Qty > 0 " +
                            "ORDER BY p.DateIn, i.InventoryID", drCon))
                            {
                                tmpCom.Parameters.AddWithValue("@pcode", row.Cells["pcode"].Value.ToString());
                                tmpCom.Parameters.AddWithValue("@slid", row.Cells["slid"].Value.ToString());
                                Console.WriteLine(tmpCom.CommandText);
                                int invoiceQty = Convert.ToInt32(row.Cells["qty"].Value);
                                int count = 0;
                                if (!String.IsNullOrEmpty(row.Cells["count"].Value.ToString()))
                                {
                                    count = Convert.ToInt32(row.Cells["count"].Value);
                                }
                                if (!String.IsNullOrEmpty(txtCount.Text) && count > 1)
                                {
                                    invoiceQty = invoiceQty * count;
                                }
                                using (SqlDataReader reader = tmpCom.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            // Add to Queue
                                            int inventoryId = reader.GetInt32(0);
                                            int inventoryQty = reader.GetInt32(1);
                                            queue.Enqueue(inventoryId);
                                            // Identify next inventory
                                            if (inventoryQty > invoiceQty)
                                            {
                                                // update inventory qty: inventoryQty - invoiceQty
                                                int newInventoryQty = inventoryQty - invoiceQty;
                                                qtyQueue.Enqueue(invoiceQty);
                                                invoiceQty = 0;
                                                UpdateInventoryQty(transaction, inventoryId, newInventoryQty);
                                                break;
                                            }
                                            else
                                            {
                                                // update inventory qty: invoiceQty - inventoryQty
                                                invoiceQty = invoiceQty - inventoryQty;
                                                qtyQueue.Enqueue(inventoryQty);
                                                inventoryQty = 0;
                                                UpdateInventoryQty(transaction, inventoryId, inventoryQty);
                                            }
                                        }

                                        while (queue.Count > 0 && qtyQueue.Count > 0)
                                        {
                                            int invID = queue.Dequeue();
                                            int invQty = qtyQueue.Dequeue();
                                            // Save to tblInvoiceDetails With Inventory Reserved
                                            SaveInvoiceDetails(invoiceId, transaction, row, invID, invQty);
                                        }

                                        if (invoiceQty > 0)
                                        {
                                            invoiceQty = -invoiceQty;
                                            int invId = InsertNegativeInventory(transaction, row, invoiceQty);

                                            // Save to tblInvoiceDetails without Inventory
                                            invoiceQty = -invoiceQty;
                                            SaveInvoiceDetails(invoiceId, transaction, row, invId, invoiceQty);
                                        }
                                    }
                                    else
                                    {
                                        invoiceQty = -invoiceQty;
                                        int invId = InsertNegativeInventory(transaction, row, invoiceQty);

                                        // Save to tblInvoiceDetails without Inventory
                                        invoiceQty = -invoiceQty;
                                        SaveInvoiceDetails(invoiceId, transaction, row, invId, invoiceQty);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    con.Close();
                    MessageBox.Show(ex.Message, "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                transaction.Commit();
                con.Close();
                return true;
            } else
            {
                return false;
            }
        }

        private int InsertNegativeInventory(SqlTransaction transaction, DataGridViewRow row, int invoiceQty)
        {
            try
            {
                com = new SqlCommand("INSERT INTO tblInventory (pcode, qty) " +
                    "OUTPUT inserted.InventoryID " +
                    "VALUES (@pcode, @qty)", con, transaction);
                com.Parameters.AddWithValue("@pcode", row.Cells["pcode"].Value.ToString());
                com.Parameters.AddWithValue("@qty", invoiceQty);
                int invId = Convert.ToInt32(com.ExecuteScalar());
                return invId;
            } catch (Exception ex)
            {
                throw new Exception("InsertNegativeInventory failed: " + ex.Message);
            }
        }

        private void UpdateInventoryQty(SqlTransaction transaction, int inventoryId, int newInventoryQty)
        {
            try
            {
                com = new SqlCommand("UPDATE tblInventory SET Qty = @qty " +
                    "WHERE InventoryID = @inventoryid", con, transaction);
                com.Parameters.AddWithValue("@qty", newInventoryQty);
                com.Parameters.AddWithValue("@inventoryid", inventoryId);
                com.ExecuteNonQuery();
            } catch (Exception ex)
            {
                throw new Exception("UpdateInventoryQty failed: " + ex.Message);
            }
}

        private void SaveInvoiceDetails(int invoiceId, SqlTransaction transaction, DataGridViewRow row, int inventoryID, int qty)
        {
            try
            {
                com = new SqlCommand("INSERT INTO tblInvoiceDetails (InvoiceId, PCode, UOM, Qty, SellingPrice, TotalItemPrice, Location, InventoryID) " +
                                                    "VALUES (@invoiceid, @pcode, @uom, @qty, @sellingprice, @totalitemprice, @location, @inventoryid)", con, transaction);
                com.Parameters.AddWithValue("@invoiceid", invoiceId);
                com.Parameters.AddWithValue("@pcode", row.Cells["pcode"].Value.ToString());
                com.Parameters.AddWithValue("@uom", row.Cells["uomid"].Value.ToString());
                //com.Parameters.AddWithValue("@qty", row.Cells["qty"].Value.ToString());
                com.Parameters.AddWithValue("@qty", qty);
                decimal price = Convert.ToDecimal(row.Cells["price"].Value.ToString());
                com.Parameters.AddWithValue("@sellingprice", price);
                com.Parameters.AddWithValue("@totalitemprice", price * qty);
                com.Parameters.AddWithValue("@location", row.Cells["location"].Value.ToString());
                com.Parameters.AddWithValue("@inventoryid", inventoryID);
                com.ExecuteNonQuery();
            } catch (Exception ex)
            {
                throw new Exception("SaveInvoiceDetails failed: " + ex.Message);
            }
        }

        // F1 New Trans
        private void btnNewTrans_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("F1 New Transaction");
            if (MessageBox.Show("Generate New Transaction?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetInvoiceForm();
            }
        }

        private void ResetInvoiceForm()
        {
            GenerateNewTrans();

            ClearProductDetails();
            ClearProductForm();
            ClearCustomerDetails();
            ClearPriceListView();
            ClearCart();
            ResetButtons();

            i = 0;
            txtCCode.Focus();
            txtCCode.SelectAll();
            hlblInvoiceId.Text = null;
        }

        private void ResetButtons()
        {
            this.btnSaveAndPrint.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnPrint.Enabled = false;
        }

        private void GenerateNewTrans()
        {
            string refNo = DateTime.Now.ToString("yyMMdd");
            con.Open();
            com = new SqlCommand("SELECT NEXT VALUE FOR sqx_inv_ref_no", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                refNo += dr[0].ToString();
            }
            con.Close();
            txtTransNo.Text = refNo;
        }

        // F6 Search Product
        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            //txtSearch.Focus();
            //txtSearch.SelectAll();
            cboSearch.Focus();
            cboSearch.SelectAll();
        }

        private void cartGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = cartGridView.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                formQty form = new formQty(this, e.RowIndex);
                form.ShowDialog();
            }
            else if (colName == "Delete")
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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                formSearchInvoice form = new formSearchInvoice(this);
                form.ShowDialog();
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Search Customer
        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            formSearchCustomer form = new formSearchCustomer();
            form.ShowDialog();
        }

        private void btnSaveAndPrint_Click(object sender, EventArgs e)
        {
            string refno = txtTransNo.Text;
            string invoiceId = hlblInvoiceId.Text;
            bool isSuccessTrans = false;
            if (String.IsNullOrEmpty(invoiceId))
            {
                // Save Invoice
                isSuccessTrans = SaveInvoice();
            } else
            {
                // Update Invoice
                isSuccessTrans = UpdateInvoice();
            }

            if (isSuccessTrans)
            {
                try
                {
                    // Print Invoice
                    PrintInvoice(refno);
                    
                    // Set Order to Printed
                    setOrderStatusPrinted(refno);

                    // Adjust Inv based on Inv
                    // NOTE: Moved to Sales Invoice
                    //AdjustInventory(refno);

                    // Update A/R
                    updateCustomerCollection();

                    // Reset form
                    ResetInvoiceForm();
                } catch (Exception ex)
                {
                    if (con != null && (con.State == ConnectionState.Open || con.State == ConnectionState.Broken))
                    {
                        con.Close();
                    }
                    MessageBox.Show(ex.Message, "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AdjustInventory(string refno)
        {
            try
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                com.Transaction = transaction;

                using (SqlConnection drCon = new SqlConnection(dbcon.MyConnection()))
                {
                    drCon.Open();
                    using (SqlCommand tmpCom = new SqlCommand("Select InventoryID, Qty " +
                            "FROM tblInvoice i INNER JOIN tblInvoiceDetails id ON i.InvoiceID = id.InvoiceID " +
                            "WHERE i.ReferenceNo = @refno", drCon))
                    {
                        tmpCom.Parameters.AddWithValue("@refno", refno);
                        dr = com.ExecuteReader();
                        using (SqlDataReader reader = tmpCom.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //com = new SqlCommand("UPDATE", con, transaction);
                            }
                        }
                    }
                }
                con.Close();
            } 
            catch (Exception ex)
            {
                if (con != null && (con.State == ConnectionState.Open || con.State == ConnectionState.Broken))
                {
                    con.Close();
                }
                MessageBox.Show(ex.Message, "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateCustomerCollection()
        {
            string customerId = hlblCustomerId.Text;
            decimal total = Convert.ToDecimal(lblSubTotal.Text);
            if (customerId == null)
            {
                throw new Exception("Customer ID is missing/null");
            } else if (total <= 0)
            {
                throw new Exception("Invalid Sub-Total: " + total);
            }

            try
            {
                con.Open();

                com = new SqlCommand("UPDATE tblCustomerCollection SET OpenBalance += @openbalance " +
                    "WHERE CustomerId = @customerid", con);
                com.Parameters.AddWithValue("@customerid", customerId);
                com.Parameters.AddWithValue("@openbalance", total);
                com.ExecuteNonQuery();

                con.Close();
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void setOrderStatusPrinted(string refno)
        {
            int orderId = 0;
            decimal openBalance = Convert.ToDecimal(txtOpenBalance.Text);
            decimal totalPrice = Convert.ToDecimal(lblSubTotal.Text);
            decimal endingBalance = openBalance + totalPrice;
            try
            {
                con.Open();
                com = new SqlCommand("SELECT o.OrderId FROM tblOrder o " +
                    "INNER JOIN tblInvoice i ON i.OrderId = o.OrderId " +
                    "WHERE i.ReferenceNo = @refno", con);
                com.Parameters.AddWithValue("@refno", refno);
                dr = com.ExecuteReader();
                if (dr.HasRows && dr.Read())
                {
                    orderId = Convert.ToInt32(dr["OrderId"].ToString());
                }
                dr.Close();

                if (orderId > 0)
                {
                    com = new SqlCommand("UPDATE tblOrder SET OrderStatus = @orderstatus, LastUpdatedTimestamp = @lastupdatedtimestamp " +
                        "WHERE OrderId = @orderid", con);
                    com.Parameters.AddWithValue("@orderid", orderId);
                    com.Parameters.AddWithValue("@orderstatus", OrderStatusConstant.INV_PRINTED);
                    com.Parameters.AddWithValue("@lastupdatedtimestamp", DateTime.Now);
                    com.ExecuteNonQuery();

                    com = new SqlCommand("Update tblInvoice SET ProcessTimestamp = @processtimestamp, RunningBalance = @runningbalance " +
                        "WHERE ReferenceNo = @refno", con);
                    com.Parameters.AddWithValue("@refno", refno);
                    com.Parameters.AddWithValue("@processtimestamp", DateTime.Now);
                    com.Parameters.AddWithValue("@runningbalance", endingBalance);
                    com.ExecuteNonQuery();
                }
                con.Close();
                //MessageBox.Show("Invoice saved successfully.  Ref No: " + refno, "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //dr.Close();
                con.Close();
                throw new Exception(ex.Message);
            }
        }

        private static void PrintInvoice(string refno)
        {
            formInvoiceReceipt form = new formInvoiceReceipt();
            form.LoadReport(refno);
        }

        private void cboSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClearProductDetails();
                ClearPriceListView();
                string pcode = null;
                if (cboSearch.SelectedValue == null)
                {
                    pcode = cboSearch.Text;
                } else
                {
                    pcode = cboSearch.SelectedValue.ToString();
                }
                con.Open();
                com = new SqlCommand("select pcode, pdesc, count from tblProduct " +
                    "where pcode = @pcode", con);
                com.Parameters.AddWithValue("@pcode", pcode);
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        this.txtPCode.Text = dr["pcode"].ToString();
                        this.txtPDesc.Text = dr["pdesc"].ToString();
                        this.txtCount.Text = dr["count"].ToString();
                    }
                } else
                {
                    dr.Close();
                    con.Close();
                    MessageBox.Show("Invalid Product Code. Please try again", "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboSearch.Focus();
                    cboSearch.SelectAll();
                    return;
                }
                dr.Close();

                // Load Product Location - cboLocation
                com = new SqlCommand("SELECT sl.SLID, sl.LocationName " +
                "FROM tblInventory i INNER JOIN tblStockLocation sl ON sl.SLID = i.SLID " +
                "WHERE PCode = @pcode AND Qty > 0 " +
                "GROUP BY sl.SLID, sl.LocationName", con);
                com.Parameters.AddWithValue("@pcode", pcode);
                dr = com.ExecuteReader();
                List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataSource.Add(new ComboBoxDTO() { Name = dr["LocationName"].ToString(), Value = dr["SLID"].ToString() }); 
                    }
                } else
                {
                    dataSource.Add(new ComboBoxDTO() { Name = "STORE", Value = "1" });// default
                }
                cboLocation.DataSource = dataSource;
                cboLocation.DisplayMember = "Name";
                cboLocation.ValueMember = "Value";
                dr.Close();

                // Load Inventory Information // PROBLEM HERE - dgvInventory
                com = new SqlCommand("SELECT sl.SLID, sl.LocationName, sl.LocationType, i.PCode, SUM(i.Qty) QTY " +
                    "FROM tblInventory i INNER JOIN tblPurchasing p ON p.PurchaseID = i.PurchaseID " +
                    "INNER JOIN tblStockLocation sl ON sl.SLID = i.SLID " +
                    "WHERE i.PCode = @pcode and Qty > 0 " + // get rin ba ng utang?
                    "GROUP BY sl.SLID, sl.LocationName, sl.LocationType, i.PCode", con);
                com.Parameters.AddWithValue("@pcode", pcode);
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    int count = 1;
                    if (!String.IsNullOrEmpty(txtCount.Text))
                    {
                        count = Convert.ToInt32(txtCount.Text);
                    }
                    while (dr.Read())
                    {
                        int qty = Convert.ToInt32(dr["QTY"]);
                        int div = qty;
                        if (qty > 0)
                        {
                            div = qty / count;
                        }

                        string finalQty = null;
                        if (count == 1 || qty <= 0)
                        {
                            finalQty = div + "pcs";
                        } else if (count > 1)
                        {
                            int mod = qty % count;
                            finalQty = div + "whl " + mod + "pc";
                        }


                        dgvInventory.Rows.Add(dr["SLID"].ToString(), dr["LocationType"].ToString()
                            , dr["LocationName"].ToString(), finalQty);
                    }
                }
                dr.Close();

                // Load PriceList and UOM
                com = new SqlCommand("SELECT u.id, u.type, u.description, u.code, pd.price FROM tblProductDetails pd " +
                    "INNER JOIN tblUOM u ON pd.UOM = u.ID " +
                    "WHERE pd.pcode = @pcode ", con);
                com.Parameters.AddWithValue("@pcode", pcode);
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    List<ComboBoxDTO> uomDataSource = new List<ComboBoxDTO>();
                    int i = 0;
                    while (dr.Read())
                    {
                        string uom = dr["type"].ToString().ToUpper(); //+ " - " + dr["description"].ToString();
                        priceListView.Rows.Add(++i, dr["id"].ToString(), uom
                            , String.Format("{0:n}", Convert.ToDecimal(dr["price"])), "", dr["code"].ToString());
                        uomDataSource.Add(new ComboBoxDTO() { Name = uom, Value = dr["id"].ToString() });
                    }
                    this.cboUOM.DataSource = uomDataSource;
                    this.cboUOM.DisplayMember = "Name";
                    this.cboUOM.ValueMember = "Value";
                }
                dr.Close();
                con.Close();

                cboUOM.Focus();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string refno = this.txtTransNo.Text;
            PrintInvoice(refno);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (String.IsNullOrEmpty(cboLocation.Text) || cboLocation.SelectedIndex < 0)
                {
                    MessageBox.Show("Invalid Location. Please try again", "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboUOM.Focus();
                    cboUOM.SelectAll();
                    return;
                }
                txtQty.Focus();
                txtQty.SelectAll();
            }
        }

        private void cartGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void cartGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //decimal price = decimal.Parse(row.Cells[3].Value.ToString());
            //int qty = Int32.Parse(txtQty.Text);
            //decimal total = price * qty;
            //String.Format("{0:n}", price), String.Format("{0:n}", total)
            decimal price = 0, total = 0, subtotal = 0;
            int qty = 0;

            try
            {
                if (cartGridView.Columns[e.ColumnIndex].Name == "qty")
                {
                    using (DataGridViewRow row = cartGridView.Rows[e.RowIndex])
                    {
                        qty = Convert.ToInt32(row.Cells["qty"].Value);
                        price = Convert.ToDecimal(row.Cells["price"].Value);
                        total = qty * price;
                        row.Cells["total"].Value = String.Format("{0:n}", total);
                    }
                }
                else if (cartGridView.Columns[e.ColumnIndex].Name == "price")
                {
                    using (DataGridViewRow row = cartGridView.Rows[e.RowIndex])
                    {
                        qty = Convert.ToInt32(row.Cells["qty"].Value);
                        price = Convert.ToDecimal(row.Cells["price"].Value);
                        total = price * qty;
                        row.Cells["price"].Value = String.Format("{0:n}", price);
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
                        row.Cells["price"].Value = String.Format("{0:n}", price);
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
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
