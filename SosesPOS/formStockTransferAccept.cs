using SosesPOS.DTO;
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
    public partial class formStockTransferAccept : Form
    {
        DbConnection dbcon = new DbConnection();
        UserDTO user = null;
        private string title = "Stock Transfer Accept";
        private int formStatus = 1;
        private int i = 1;
        public formStockTransferAccept(UserDTO user)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.user = user;
        }

        private void ResetButtons()
        {
            btnSet.Enabled = true;
            btnSave.Enabled = false;
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

        private void ClearForm()
        {
            this.cboFrom.Text = "";
            this.cboTo.Text = "";
            this.cboSearch.Text = "";
            this.txtQty.Clear();
            this.txtPCode.Clear();
            this.txtPDesc.Clear();
            this.txtCount.Clear();
            this.txtRefNo.Clear();
            this.txtRefNo.ReadOnly = false;
        }

        private void ResetForm()
        {
            ClearForm();
            ClearInventoryView();
            ClearCart();
            ResetButtons();
            i = 1;
            this.formStatus = 1;
            this.txtRefNo.Focus();
        }

        private void formStockTransferDispatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) // New Transaction
            {
                if (this.btnNewTrans.Enabled)
                {
                    btnNewTrans_Click(sender, e);
                }

            }
            else if (e.KeyCode == Keys.F3) // Save & Print
            {
                if (this.btnSave.Enabled)
                {
                    btnSave_Click(sender, e);
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

        private void btnNewTrans_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset and generate new transaction?", "Stock Transaction", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetForm();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            string refNo = this.txtRefNo.Text;
            if (string.IsNullOrWhiteSpace(refNo) || refNo.Length != 8)
            {
                MessageBox.Show("Invalid Reference Number. Please try again.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRefNo.Focus();
                txtRefNo.SelectAll();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                 
                    int stockTransferId = 0;
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "SELECT st.StockTransferID, slf.SLID FromSLID, slf.LocationName FromLocation, slt.SLID ToSLID, slt.LocationName ToLocation, st.TransferStatus, st.Note " +
                            "FROM tblStockTransfer st " +
                            "INNER JOIN tblStockLocation slf ON slf.SLID = st.FromSite " +
                            "INNER JOIN tblStockLocation slt ON slt.SLID = st.ToSite " +
                            "WHERE st.StockTransferNo = @stocktransferno";
                        com.Parameters.AddWithValue("@stocktransferno", refNo);
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cboFrom.Text = reader["FromLocation"].ToString();
                                cboTo.Text = reader["ToLocation"].ToString();
                                txtNote.Text = reader["Note"].ToString();
                                this.hlblFromSLID.Text = reader["FromSLID"].ToString();
                                this.hlblToSLID.Text = reader["ToSLID"].ToString();
                                stockTransferId = Convert.ToInt32(reader["StockTransferID"]);
                            }
                        }

                    }

                    if (stockTransferId <= 0)
                    {
                        MessageBox.Show("Something went wrong. Please try again.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ResetForm();
                        return;
                    }

                    this.hlblStockTransferID.Text = stockTransferId.ToString();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "SELECT str.PCode, p.pdesc, str.Qty, p.count " +
                            "FROM tblStockTransferRequest str " +
                            "INNER JOIN tblProduct p ON p.pcode = str.PCode " +
                            "WHERE str.StockTransferID = @stocktransferid";
                        com.Parameters.AddWithValue("@stocktransferid", stockTransferId);
                        int counter = 0;
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dgvCart.Rows.Add(++counter, reader["PCode"].ToString(), reader["PDesc"].ToString()
                                    , reader["Qty"].ToString(), reader["count"].ToString());
                            }
                        }
                    }

                    this.btnSave.Enabled = true;
                    this.btnSet.Enabled = false;
                    this.formStatus = 10;
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Setting Data failed: " + ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (formStatus != 10 || string.IsNullOrWhiteSpace(hlblStockTransferID.Text)
                || string.IsNullOrWhiteSpace(hlblToSLID.Text) || string.IsNullOrWhiteSpace(hlblFromSLID.Text))
            {
                MessageBox.Show("Invalid Form. Please try again.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetForm();
                return;
            }

            if (MessageBox.Show("Are you sure to save? Changes are irreversible.", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try 
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();

                    // iterate through cart
                    string stockTransferId = hlblStockTransferID.Text;
                    foreach (DataGridViewRow row in dgvCart.Rows)
                    {
                        string pcode = row.Cells["pcode"].Value.ToString();
                        int qty = Convert.ToInt32(row.Cells["qty"].Value);
                        int count = Convert.ToInt32(row.Cells["count"].Value);
                        int requestQty = qty * count;
                        List<InventoryDTO> posInventoryList = new List<InventoryDTO>();

                        // adjust inventory
                        using (SqlCommand com = new SqlCommand("SELECT  i.InventoryID, i.PurchaseID, i.Qty " +
                        "FROM tblInventory i " +
                        "INNER JOIN tblPurchasing p ON p.PurchaseID = i.PurchaseID " +
                        "WHERE i.PCode = @pcode AND i.SLID = @slid AND i.Qty > 0 " +
                        "ORDER BY p.DateIn, i.InventoryID", con, transaction))
                        {
                            com.Parameters.AddWithValue("@pcode", pcode);
                            com.Parameters.AddWithValue("@slid", hlblFromSLID.Text);
                            using (SqlDataReader reader = com.ExecuteReader())
                            {
                                // put into list
                                InventoryDTO dto = new InventoryDTO();
                                dto.qty = Convert.ToInt32(reader["Qty"]);
                                dto.inventoryID = Convert.ToInt32(reader["InventoryID"]);
                                dto.purchaseID = Convert.ToInt32(reader["PurchaseID"]);
                                posInventoryList.Add(dto);
                            }
                        }

                        // iterate list
                        Queue<InventoryDTO> qtyQueue = new Queue<InventoryDTO>();
                        foreach (InventoryDTO inventoryDTO in posInventoryList)
                        {
                            int qtyCounter = 0;
                            int inventoryQty = inventoryDTO.qty;
                            int inventoryId = inventoryDTO.inventoryID;
                            int purchaseId = inventoryDTO.purchaseID;
                            while (inventoryQty >= count && requestQty > 0) // count
                            {
                                inventoryQty -= count;
                                requestQty -= count;
                                qtyCounter += count;
                            }

                            if (qtyCounter > 0)
                            {
                                //qtyQueue.Enqueue(qtyCounter); should we still use a queue?
                                UpdateInventoryQty(con, transaction, inventoryId, inventoryQty);

                                InventoryDTO dto = new InventoryDTO();
                                dto.purchaseID = purchaseId;
                                dto.qty = qtyCounter;
                                dto.pcode = pcode;
                                dto.slid = Convert.ToInt32(hlblToSLID.Text);
                                qtyQueue.Enqueue(dto);
                            }
                        }

                        //queue check
                        while (qtyQueue.Count > 0)
                        {
                            InventoryDTO dto = qtyQueue.Dequeue();
                            // Save to tblInventory with new SLID
                            SaveInventory(con, transaction, dto);
                        }

                        // save negative; WHAT TO DO WITH NEGATIVE. TODO
                        if (requestQty > 0)
                        {

                        }

                        // save to items accepted to child table
                        using (SqlCommand com = con.CreateCommand())
                        {
                            com.Transaction = transaction;
                            com.CommandText = "INSERT INTO tblStockTransferItems (StockTransferID, PCode, Qty) " +
                                "VALUES (@stocktransferid, @pcode, @qty)";
                            com.Parameters.AddWithValue("@stocktransferid", stockTransferId);
                            com.Parameters.AddWithValue("@pcode", pcode);
                            com.Parameters.AddWithValue("@qty", qty);
                            com.ExecuteNonQuery();
                        }
                    }

                    // Update Stock Transfer parent table
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.Transaction = transaction;
                        com.CommandText = "UPDATE tblStockTransfer SET TransferStatus = @transferstatus" +
                            ", LastChangedTimestamp = @lastchangedtimestamp, LastChangedUserCode = @lastchangedusercode " +
                            "WHERE StockTransferID = @stocktransferid";
                        com.Parameters.AddWithValue("@transferstatus", GlobalConstant.STOCK_TRANSFER_ACCEPTED);
                        com.Parameters.AddWithValue("@lastchangedtimestamp", DateTime.Now);
                        com.Parameters.AddWithValue("@lastchangedusercode", user.userCode);
                        com.Parameters.AddWithValue("@stocktransferid", stockTransferId);
                        com.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Transfer Requested Accepted. System updated.", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Saving Data failed: " + ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateInventoryQty(SqlConnection con, SqlTransaction transaction, int inventoryId, int newInventoryQty)
        {
            try
            {
                using (SqlCommand com = con.CreateCommand())
                {
                    com.Transaction = transaction;
                    com.CommandText = "UPDATE tblInventory SET Qty = @qty " +
                        "WHERE InventoryID = @inventoryid";
                    com.Parameters.AddWithValue("@qty", newInventoryQty);
                    com.Parameters.AddWithValue("@inventoryid", inventoryId);
                    com.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("UpdateInventoryQty failed: " + ex.Message);
            }
        }

        private void SaveInventory(SqlConnection con, SqlTransaction transaction, InventoryDTO inventoryDTO)
        {
            try
            {
                using (SqlCommand com = con.CreateCommand())
                {
                    com.Transaction = transaction;
                    com.CommandText = "INSERT INTO tblInventory (PCode, SLID, PurchaseID, Qty) " +
                        "VALUES (@pcode, @slid, @purchaseid, @qty)";
                    com.Parameters.AddWithValue("@pcode", inventoryDTO.pcode);
                    com.Parameters.AddWithValue("@slid", inventoryDTO.slid);
                    com.Parameters.AddWithValue("@purchaseid", inventoryDTO.purchaseID);
                    com.Parameters.AddWithValue("@qty", inventoryDTO.qty);
                    com.ExecuteNonQuery();
                }
            } 
            catch (Exception ex )
            {
                throw new Exception("Save Inventory to new site failed: " + ex.Message);
            }
        }
    }
}
