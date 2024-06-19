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
    public partial class formCustomerModule : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();

        string customerId = null;
        public formCustomerModule(string customerId)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            this.customerId = customerId;
        }

        public void LoadCustomerProfile()
        {
            try
            {
                con.Open();
                com = new SqlCommand("SELECT c.CustomerId, c.CustomerCode, c.CustomerName, c.CustomerAddress, cc.OpenBalance " +
                    "FROM tblCustomer c " +
                    "INNER JOIN tblCustomerCollection cc ON c.CustomerId = cc.CustomerId " +
                    "WHERE c.CustomerId = @customerid", con);
                com.Parameters.AddWithValue("@customerid", customerId);
                dr = com.ExecuteReader();

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        int i = 0;
                        //lblCustomerCode.Text = dr["CustomerCode"].ToString();
                        //lblCustomerName.Text = dr["CustomerName"].ToString();
                        //lblCustomerAddress.Text = dr["CustomerAddress"].ToString();
                        //lblOpenBalance.Text = String.Format("{0:n}", Convert.ToDecimal(dr["OpenBalance"].ToString()));
                        //tlblCustomerCode.Text = dr["CustomerCode"].ToString();

                        dgvCustomerProfile.Rows.Add(dr["CustomerCode"].ToString()
                            , dr["CustomerName"].ToString(), dr["CustomerAddress"].ToString()
                            , String.Format("{0:n}", Convert.ToDecimal(dr["OpenBalance"].ToString())));
                    }
                }
                dr.Close();

                com = new SqlCommand("SELECT * " +
                    "FROM (" +
                        "SELECT 'INVOICE' as TYPE, i.ReferenceNo RefNo, i.ProcessTimestamp, i.TotalPrice as DEBIT, 0 as CREDIT, i.RunningBalance " +
                        "FROM tblOrder o INNER JOIN tblInvoice i ON o.OrderId = i.OrderId " +
                        "WHERE o.CustomerId = @customerid AND o.OrderStatus in ('15','16','20') " +
                        "UNION ALL " +
                        "SELECT 'PAYMENT' as TYPE, CASE WHEN 'CHECK' = cp.Type THEN CONCAT(b.BankName, ' ', cp.BankBranch, ' - ', cp.CheckNo) ELSE 'CASH' END as RefNo" +
                            ", cp.ProcessTimestamp, CASE WHEN cp.Amount < 0 THEN ABS(cp.Amount) ELSE 0 END as DEBIT" +
                            ", CASE WHEN cp.Amount >= 0 THEN cp.Amount ELSE 0 END as CREDIT, cp.RunningBalance " +
                        "FROM tblCustomerPayment cp " +
                        "LEFT JOIN tblBank b ON b.BankId = cp.CheckBank " +
                        "WHERE cp.CustomerId = @customerid " +
                        "UNION ALL " +
                        "SELECT CASE WHEN 'C' = m.MemoType THEN 'CREDIT MEMO' ELSE 'DEBIT MEMO' END as TYPE, m.ReferenceNo as RefNo, m.ProcessTimestamp," +
                            " CASE WHEN 'D' = m.MemoType THEN m.Amount ELSE '0' END as DEBIT, CASE WHEN 'C' = m.MemoType THEN m.Amount ELSE '0' END as CREDIT," +
                            " m.RunningBalance " +
                        "FROM tblMemo m WHERE m.CustomerId = @customerid) as t " +
                    "WHERE t.ProcessTimestamp is not null " +
                    "ORDER BY t.ProcessTimestamp", con);
                com.Parameters.AddWithValue("@customerid", customerId);
                dr = com.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string debit = dr["DEBIT"].ToString();
                        string credit = dr["CREDIT"].ToString();
                        dgvTransactions.Rows.Add(dr["TYPE"].ToString()
                            , String.IsNullOrWhiteSpace(dr["RefNo"].ToString()) ? "CASH": dr["RefNo"].ToString()
                            , Convert.ToDateTime(dr["ProcessTimestamp"]).ToString("MM/dd/yyyy")
                            , "0.00".Equals(debit) ? "" : String.Format("{0:n}", Convert.ToDecimal(dr["DEBIT"]))
                            , "0.00".Equals(credit) ? "" : String.Format("{0:n}", Convert.ToDecimal(dr["CREDIT"]))
                            , String.Format("{0:n}", Convert.ToDecimal(dr["RunningBalance"])));
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
