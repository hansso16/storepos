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
    public partial class formWriteCheckList : Form
    {
        DbConnection dbcon = new DbConnection();
        UserDTO user = null;
        public formWriteCheckList(UserDTO user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadVendorList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection())) 
                {
                    con.Open();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "SELECT VendorID, VendorCode, VendorName " +
                            "FROM tblVendor WHERE VendorCode LIKE '%'+@search+'%' " +
                            "OR VendorName LIKE '%'+@search+'%'";
                        com.Parameters.AddWithValue("@search", this.txtSearch.Text);
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            int i = 1;
                            while (reader.Read())
                            {
                                //dgvPayeeList.Rows.Add();
                                dgvVendorList.Rows.Add(i++, reader["VendorID"]
                                    , reader["VendorCode"], reader["VendorName"]
                                    , i);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Write Check List: LoadVendorList(): " + ex.Message, "Write Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadVendorList();
        }

        private void dgvVendorList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dgvVendorList.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                //MessageBox.Show("EDIT");
                decimal a = (decimal)15.25;
                Console.WriteLine(IntegerUtil.NumberToCurrencyText(a, MidpointRounding.AwayFromZero));
                a = (decimal)11.00;
                Console.WriteLine(IntegerUtil.NumberToCurrencyText(a, MidpointRounding.AwayFromZero));
                a = (decimal)123.76;
                Console.WriteLine(IntegerUtil.NumberToCurrencyText(a, MidpointRounding.AwayFromZero));
                a = (decimal)1240.32;
                Console.WriteLine(IntegerUtil.NumberToCurrencyText(a, MidpointRounding.AwayFromZero));
                a = (decimal)46544.0;
                Console.WriteLine(IntegerUtil.NumberToCurrencyText(a, MidpointRounding.AwayFromZero));
                a = (decimal)987766;
                Console.WriteLine(IntegerUtil.NumberToCurrencyText(a, MidpointRounding.AwayFromZero));
                a = (decimal)8888888.88;
                Console.WriteLine(IntegerUtil.NumberToCurrencyText(a, MidpointRounding.AwayFromZero));
                a = (decimal)77777777.77;
                Console.WriteLine(IntegerUtil.NumberToCurrencyText(a, MidpointRounding.AwayFromZero));

                formWriteCheck form = new formWriteCheck(user);
                form.txtPayee.Text = dgvVendorList[3, e.RowIndex].Value.ToString();
                form.dtpCheckDate.Value = DateTime.Now.AddDays(30);
                form.txtAmount.Focus();
                form.ShowDialog();
            }
        }
    }
}
