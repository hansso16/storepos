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
    public partial class formCheckMonitor : Form
    {
        UserDTO user = null;
        DbConnection dbcon = new DbConnection();
        public formCheckMonitor()
        {
            InitializeComponent();
        }

        public formCheckMonitor(UserDTO user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCheckList()
        {
            dgvCheckList.Rows.Clear();

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "SELECT cp.CustomerPaymentID, cp.CustomerId, c.CustomerName, cp.BankBranch, cp.CheckNo, cp.CheckDate, cp.Amount, cp.CheckStatus, b.BankName " +
                            "FROM tblCustomerPayment cp INNER JOIN tblCustomer c ON c.CustomerId = cp.CustomerId " +
                            "INNER JOIN tblCustomerCollection cc ON cc.CustomerId = cp.CustomerId " +
                            "LEFT JOIN tblBank b ON b.BankId = cp.CheckBank " +
                            "WHERE cp.CheckStatus IN ('00') AND c.CustomerName LIKE '%'+@customername+'%' " +
                            "AND DATEADD(DAY, 1, cp.CheckDate) <= GETDATE() " +
                            "ORDER BY cp.CheckDate ASC";
                        com.Parameters.AddWithValue("@customername", txtSearch.Text);
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // derive checked status

                                dgvCheckList.Rows.Add(false, dr["CustomerPaymentID"].ToString(), dr["CustomerID"].ToString(), dr["CustomerName"].ToString()
                                    , dr["BankName"].ToString() + " - " + dr["BankBranch"].ToString(), dr["CheckNo"].ToString()
                                    , Convert.ToDateTime(dr["CheckDate"]).ToString("MM/dd/yyyy")
                                    , Convert.ToDouble(dr["Amount"]).ToString("N2"), dr["CheckStatus"].ToString());
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Please contact your system administrator: " + ex.Message, "Check Monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dgvCheckList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dgvCheckList.Columns[e.ColumnIndex].Name;
            if (colName == "CheckBox")
            {
                DataGridViewCheckBoxCell chbox = dgvCheckList[0, e.RowIndex] as DataGridViewCheckBoxCell;
                if (chbox.TrueValue == chbox.Value)
                {
                    chbox.Value = chbox.FalseValue;
                } else
                {
                    chbox.Value = chbox.TrueValue;
                }

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    foreach (DataGridViewRow row in dgvCheckList.Rows)
                    {
                        DataGridViewCheckBoxCell checkbox = row.Cells[0] as DataGridViewCheckBoxCell;
                        if (checkbox.TrueValue == checkbox.Value)
                        {
                            string customerPaymentId = row.Cells[1].Value.ToString();
                            string customerId = row.Cells[2].Value.ToString();
                            decimal paymentAmount = Convert.ToDecimal(row.Cells[7].Value);

                            // Update Open Balance - extract to method
                            using (SqlCommand com = con.CreateCommand())
                            {
                                com.Transaction = transaction;
                                com.CommandText = "UPDATE tblCustomerCollection SET OpenBalance -= @openbalance " +
                                    "WHERE CustomerId = @customerid";
                                com.Parameters.AddWithValue("@openbalance", paymentAmount);
                                com.Parameters.AddWithValue("@customerid", customerId);

                                com.ExecuteNonQuery();
                            }

                            // Update Check Status - extract to method; overload check status
                            using (SqlCommand com = con.CreateCommand())
                            {
                                com.Transaction = transaction;
                                com.CommandText = "UPDATE tblCustomerPayment SET CheckStatus = @checkstatus WHERE CustomerPaymentId = @customerpaymentid";
                                com.Parameters.AddWithValue("@checkstatus", GlobalConstant.CHECK_STATUS_CLRD);
                                com.Parameters.AddWithValue("@customerpaymentid", customerPaymentId);

                                com.ExecuteNonQuery();
                            }
                        }
                    }
                    transaction.Commit();
                    LoadCheckList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong when updating. Please contact your system administrator: " + ex.Message, "Check Monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
