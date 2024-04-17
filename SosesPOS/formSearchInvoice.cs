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
    public partial class formSearchInvoice : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        formPOS formPOS = null;
        public formSearchInvoice(formPOS formPOS)
        {
            InitializeComponent();
            this.KeyPreview = true;
            con = new SqlConnection(dbcon.MyConnection());
            this.formPOS = formPOS;
        }

        private void txtRefNo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void formSearchInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void LoadInvoice(string refNo)
        {
            int invoiceId = 0;
            string orderStatus = null;
            try
            {
                con.Open();
                com = new SqlCommand("SELECT o.OrderId, o.OrderStatus, c.CustomerId, c.CustomerName, c.CustomerCode, c.CustomerAddress" +
                    ", i.InvoiceId, i.ReferenceNo, i.TotalPrice FROM tblOrder o " +
                    "INNER JOIN tblCustomer c ON c.CustomerId = o.CustomerId " +
                    "INNER JOIN tblInvoice i ON o.OrderId = i.OrderId " +
                    "WHERE i.ReferenceNo = @refno", con);
                com.Parameters.AddWithValue("@refno", refNo);
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    formPOS.ClearCart();
                    if (dr.Read())
                    {
                        formPOS.txtTransNo.Text = dr["ReferenceNo"].ToString();
                        formPOS.txtCCode.Text = dr["CustomerCode"].ToString();
                        formPOS.txtCName.Text = dr["CustomerName"].ToString();
                        formPOS.txtCAddress.Text = dr["CustomerAddress"].ToString();
                        formPOS.hlblCustomerId.Text = dr["CustomerId"].ToString();
                        formPOS.lblSubTotal.Text = String.Format("{0:n}", Convert.ToDecimal(dr["TotalPrice"]));
                        invoiceId = Convert.ToInt32(dr["InvoiceId"]);
                        orderStatus = dr["OrderStatus"].ToString();
                        formPOS.hlblInvoiceId.Text = invoiceId.ToString();
                    }
                    dr.Close();

                    com = new SqlCommand("SELECT id.Qty, id.TotalItemPrice, id.PCode, p.pdesc, id.SellingPrice price, u.id, u.type, u.code, id.Location " +
                        "FROM tblInvoiceDetails id " +
                        "INNER JOIN tblProduct p ON p.pcode = id.pcode " +
                        "LEFT JOIN tblUOM u ON u.id = id.uom " +
                        "WHERE id.InvoiceId = @invoiceid", con);
                    com.Parameters.AddWithValue("@invoiceid", invoiceId);
                    dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        int i = 0;
                        while (dr.Read())
                        {
                            string pdesc = null;
                            if ("0".Equals(dr["location"].ToString()))
                            {
                                pdesc = "*" + dr["pdesc"].ToString();
                            }
                            else
                            {
                                pdesc = dr["pdesc"].ToString();
                            }
                            formPOS.cartGridView.Rows.Add(++i, "id"
                                , dr["pcode"].ToString(), pdesc
                                , dr["Qty"].ToString()
                                , dr["id"].ToString(), dr["type"].ToString().ToUpper()
                                , String.Format("{0:n}", Convert.ToDecimal(dr["price"]))
                                , String.Format("{0:n}", Convert.ToDecimal(dr["TotalItemPrice"]))
                                , dr["code"].ToString());
                        }
                        formPOS.i = i;
                    }
                    dr.Close();

                    if (OrderStatusConstant.INV_PRINTED.Equals(orderStatus))
                    {
                        formPOS.txtCCode.ReadOnly = true;
                        formPOS.txtQty.ReadOnly = true;
                        formPOS.cboSearch.Enabled = false;
                        formPOS.cboUOM.Enabled = false;
                        formPOS.cartGridView.Enabled = false;
                        formPOS.txtCName.ReadOnly = true;
                        formPOS.cboLocation.Enabled = false;

                        //btns
                        formPOS.btnSaveAndPrint.Enabled = false;
                        formPOS.btnSave.Enabled = false;
                        formPOS.btnPrint.Enabled = true;
                    }
                    else
                    {
                        if ("0".Equals(formPOS.txtCCode.Text))
                        {
                            formPOS.txtCName.ReadOnly = false;
                        }
                        else
                        {
                            formPOS.txtCName.ReadOnly = true;
                        }
                        formPOS.btnPrint.Enabled = false;
                    }

                    // TODO Get Order Status Description and Display in UI
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("No record found. Please try again.", "Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtRefNo.Focus();
                    this.txtRefNo.SelectAll();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadInvoice(this.txtRefNo.Text);
        }

        public void ViewInvoiceDetails(string refNo)
        {
            LoadInvoice(refNo);
            formPOS.txtCCode.Enabled = false;
            formPOS.cboSearch.Enabled = false;
            formPOS.cboUOM.Enabled = false;
            formPOS.cboLocation.Enabled = false;
            formPOS.txtQty.Enabled = false;

            formPOS.btnNewTrans.Enabled = false;
            formPOS.btnSaveAndPrint.Enabled = false;
            formPOS.btnSave.Enabled = false;
            formPOS.btnLoad.Enabled = false;
            formPOS.btnPrint.Enabled = false;
            formPOS.btnSearchCustomer.Enabled = false;
            formPOS.btnGenerateReport.Enabled = false;
        }
    }
}
