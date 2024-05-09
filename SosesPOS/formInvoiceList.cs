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
using SosesPOS.DTO;
using SosesPOS.util;

namespace SosesPOS
{
    public partial class formInvoiceList : Form
    {
        DbConnection dbcon = new DbConnection();
        UserDTO user = null;
        public formInvoiceList(UserDTO user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadUserList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
                    dataSource.Add(new ComboBoxDTO() { Name = "ALL", Value = "0" });
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "select UserCode, Username from tblUser WHERE TerminationDate = '9999-12-31'";
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    dataSource.Add(new ComboBoxDTO() { Name = dr["Username"].ToString(), Value = dr["UserCode"].ToString() });
                                }
                                cboUserList.DataSource = dataSource;
                                cboUserList.DisplayMember = "Name";
                                cboUserList.ValueMember = "Value";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Customer Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadInvoiceList()
        {
            try
            {
                dgvInvoiceList.Rows.Clear();
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "SELECT i.ReferenceNo, c.CustomerName, i.ProcessTimestamp, i.TotalPrice, u.Username " +
                            "FROM tblInvoice i " +
                            "INNER JOIN tblOrder o ON i.OrderId = o.OrderId " +
                            "INNER JOIN tblCustomer c ON c.CustomerId = o.CustomerId " +
                            "LEFT JOIN tblUser u ON u.UserCode = i.UserCode " +
                            "WHERE c.CustomerName LIKE '%'+@search+'%' " +
                                "AND CONVERT(DATE, i.ProcessTimestamp) BETWEEN @before AND @to ";

                        string usercode = cboUserList.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(usercode) && !"0".Equals(usercode))
                        {
                            com.CommandText += "AND i.UserCode = @usercode ";
                        }
                        com.CommandText += "ORDER BY i.ProcessTimestamp ASC";
                        com.Parameters.AddWithValue("@search", this.txtSearch.Text);
                        com.Parameters.AddWithValue("@before", dtpFrom.Value.Date);
                        com.Parameters.AddWithValue("@to", dtpTo.Value.Date);
                        if (!string.IsNullOrEmpty(usercode) && !"0".Equals(usercode))
                        {
                            com.Parameters.AddWithValue("@usercode", usercode);
                        }
                        Console.WriteLine(com.CommandText);
                        decimal totalAmount = 0;
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            int i = 1;
                            while (reader.Read())
                            {
                                //dgvPayeeList.Rows.Add();
                                dgvInvoiceList.Rows.Add(i++, reader["ReferenceNo"]
                                    , reader["CustomerName"], Convert.ToDateTime(reader["ProcessTimestamp"]).ToString("MM/dd/yyyy")
                                    , reader["Username"].ToString(), string.Format("{0:N}", Convert.ToDecimal(reader["TotalPrice"])));
                                totalAmount += Convert.ToDecimal(reader["TotalPrice"]);
                            }
                        }
                        lblTotalAmount.Text = "Total Amount: " + string.Format("{0:N}", totalAmount);
                    }
                }
            } 
            catch (Exception ex) 
            {
                MessageBox.Show("Invoice List: LoadInvoiceList(): " + ex.Message, "Invoice List", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadInvoiceList();
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadInvoiceList();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            LoadInvoiceList();
        }

        private void dgvInvoiceList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dgvInvoiceList.Columns[e.ColumnIndex].Name;
            if (e.RowIndex >= 0)
            {
                if (colName == "refno")
                {
                    // open invoice details
                    string refNo = dgvInvoiceList[e.ColumnIndex, e.RowIndex].Value.ToString();
                    formPOS formPOS = new formPOS(user);
                    formSearchInvoice formSearchInvoice = new formSearchInvoice(formPOS);
                    formSearchInvoice.ViewInvoiceDetails(refNo);
                    formSearchInvoice.Dispose();
                    formPOS.ShowDialog();
                }
            }
        }

        private void cboUserList_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            LoadInvoiceList();
        }
    }
}
