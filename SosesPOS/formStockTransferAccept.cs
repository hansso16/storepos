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
                        com.CommandText = "SELECT st.StockTransferID, slf.LocationName FromLocation, slt.LocationName ToLocation, st.TransferStatus, st.Note " +
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
            if (formStatus != 10 || string.IsNullOrWhiteSpace(hlblStockTransferID.Text))
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

                    //TODO: iterate through cart and adjust inventory


                    // Update Stock Transfer
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.Transaction = transaction;
                        com.CommandText = "UPDATE tblStockTransfer SET TransferStatus = @transferstatus" +
                            ", LastChangedTimestamp = @lastchangedtimestamp, LastChangedUserCode = @lastchangedusercode " +
                            "WHERE StockTransferID = @stocktransferid";
                        com.Parameters.AddWithValue("@transferstatus", GlobalConstant.STOCK_TRANSFER_ACCEPTED);
                        com.Parameters.AddWithValue("@lastchangedtimestamp", DateTime.Now);
                        com.Parameters.AddWithValue("@lastchangedusercode", user.userCode);
                        com.Parameters.AddWithValue("@stocktransferid", hlblStockTransferID.Text);
                        com.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Saving Data failed: " + ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
