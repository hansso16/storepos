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
    public partial class formCheckReport : Form
    {
        DbConnection dbcon = new DbConnection();
        public formCheckReport()
        {
            InitializeComponent();
            LoadCheckList();
        }

        private void LoadCheckList()
        {
            try
            {
                dgvCheckList.Rows.Clear();
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "SELECT CheckDate, CheckNo, PayeeName, CheckAmount, EntryTimestamp " +
                                "FROM tblCheckIssue WHERE CAST(EntryTimestamp AS DATE) = CAST(GETDATE() AS DATE)";
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            int i = 1;
                            while (reader.Read())
                            {
                                decimal amount = Convert.ToDecimal(reader["CheckAmount"]);
                                string formattedAmount = amount.ToString("C", System.Globalization.CultureInfo.CurrentCulture).Substring(1);
                                dgvCheckList.Rows.Add(i++, Convert.ToDateTime(reader["CheckDate"]).ToString("MM/dd/yyyy")
                                    , reader["CheckNo"].ToString(), reader["PayeeName"].ToString()
                                    , formattedAmount);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check Report: LoadCheckList(): " + ex.Message, "Check Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void PrintCheck()
        {
            formPrintCheckReport form = new formPrintCheckReport();
            form.PrintCheck();
            this.Focus();
        }

        private void btnBlankCheck_Click(object sender, EventArgs e)
        {
            try
            {
                PrintCheck();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check Report: formWriteCheckList(): " + ex.Message, "Check Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
