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
    public partial class formBillingPOS : formPOS
    {
        DbConnection dbcon = new DbConnection();
        public formBillingPOS()
        {
            InitializeComponent();
            this.KeyPreview = true;
            LoadSuggestedProducts();
            GenerateNewTrans();
            LoadLocation();
            this.btnGenerateReport.Visible = true;
        }

        protected override void btnSaveAndPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("OVERRIDDEN");
            string refno = txtTransNo.Text;
            string invoiceId = hlblInvoiceId.Text;
            bool isSuccessTrans = false;

            try
            {
                if (String.IsNullOrEmpty(invoiceId))
                {
                    // Save Invoice
                    isSuccessTrans = SaveInvoice();
                }
                else
                {
                    // Update Invoice
                    isSuccessTrans = UpdateInvoice();
                }

                if (isSuccessTrans)
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
                }
            }
            catch (Exception ex)
            {
                //if (con != null && (con.State == ConnectionState.Open || con.State == ConnectionState.Broken))
                //{
                //    con.Close();
                //}
                MessageBox.Show(ex.Message, "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    using (SqlCommand com = new SqlCommand("SELECT o.OrderId FROM tblOrder o " +
                        "INNER JOIN tblInvoice i ON i.OrderId = o.OrderId " +
                        "WHERE i.ReferenceNo = @refno", con, transaction))
                    {
                        com.Parameters.AddWithValue("@refno", refno);
                        using (SqlDataReader sdr = com.ExecuteReader())
                        {
                            if (sdr.HasRows && sdr.Read())
                            {
                                orderId = Convert.ToInt32(sdr["OrderId"].ToString());
                            }
                        }
                    }
                    if (orderId > 0)
                    {
                        using (SqlCommand com = new SqlCommand("UPDATE tblOrder SET OrderStatus = @orderstatus" +
                            ", LastUpdatedTimestamp = @lastupdatedtimestamp " +
                            "WHERE OrderId = @orderid", con, transaction))
                        {
                            com.Parameters.AddWithValue("@orderid", orderId);
                            com.Parameters.AddWithValue("@orderstatus", OrderStatusConstant.INV_PRINTED);
                            com.Parameters.AddWithValue("@lastupdatedtimestamp", DateTime.Now);
                            com.ExecuteNonQuery();
                        }

                        using (SqlCommand com = new SqlCommand("Update tblInvoice SET ProcessTimestamp = @processtimestamp" +
                            ", RunningBalance = @runningbalance " +
                            "WHERE ReferenceNo = @refno", con, transaction))
                        {
                            com.Parameters.AddWithValue("@refno", refno);
                            com.Parameters.AddWithValue("@processtimestamp", DateTime.Now);
                            com.Parameters.AddWithValue("@runningbalance", endingBalance);
                            com.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("setOrderStatusPrinted: " + ex.Message);
            }
        }

        protected override void btnGenerateReport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GENERATE THE REPORT");
        }
    }
}
