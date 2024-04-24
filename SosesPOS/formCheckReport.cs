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
using System.IO;

namespace SosesPOS
{
    public partial class formCheckReport : Form
    {
        DbConnection dbcon = new DbConnection();
        private bool isShiftKeyPressed = false;
        private int lastClickedRowIndex = -1;
        private string bankid = "1";
        public formCheckReport()
        {
            InitializeComponent();
            LoadBankList();
            LoadCheckList(bankid);
        }

        private void LoadBankList()
        {
            List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
            try
            {
                cboBank.Items.Clear();
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "SELECT CheckBankID, BankName, BankShortName " +
                                "FROM tblCheckBank";
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dataSource.Add(new ComboBoxDTO() { Name = reader["BankShortName"].ToString(), Value = reader["CheckBankID"].ToString() });
                            }
                        }
                    }
                }
                dataSource.Add(new ComboBoxDTO() { Name = "Other", Value = "0" });
                cboBank.DataSource = dataSource;
                cboBank.DisplayMember = "Name";
                cboBank.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check Report: LoadCheckList(): " + ex.Message, "Check Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCheckList(string bankId)
        {
            try
            {
                dgvCheckList.Rows.Clear();
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "SELECT CheckDate, CheckNo, PayeeName, CheckAmount, EntryTimestamp, CheckId, Remarks " +
                                "FROM tblCheckIssue WHERE (IsPrinted = '0' OR IsPrinted IS NULL) AND CheckBankID = @checkbankid";
                        com.Parameters.AddWithValue("@checkbankid", bankId);
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            int i = 1;
                            while (reader.Read())
                            {
                                decimal amount = Convert.ToDecimal(reader["CheckAmount"]);
                                string formattedAmount = amount.ToString("C", System.Globalization.CultureInfo.CurrentCulture).Substring(1);
                                dgvCheckList.Rows.Add(i++, 0, Convert.ToDateTime(reader["CheckDate"]).ToString("MM/dd/yyyy")
                                    , reader["CheckNo"].ToString(), reader["Remarks"].ToString().Trim()
                                    , formattedAmount, "CANCEL", reader["CheckId"].ToString());
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
        private void PrintCheck(List<CheckReportDTO> list)
        {
            formPrintCheckReport form = new formPrintCheckReport();
            form.PrintCheck(list);
            this.Focus();
        }

        private void btnBlankCheck_Click(object sender, EventArgs e)
        {
            try
            {
                List<CheckReportDTO> selectedRows = new List<CheckReportDTO>();
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    // Loop through the DataGridView rows
                    for (int i = 0; i < dgvCheckList.Rows.Count; i++)
                    {
                        // Find the checkbox cell in each row
                        DataGridViewCheckBoxCell checkBoxCell = dgvCheckList.Rows[i].Cells["SELECT"] as DataGridViewCheckBoxCell;
                    
                        // Check if the checkbox is checked (true) or not (false)
                        bool isChecked = (checkBoxCell.Value == null) ? false : Convert.ToBoolean(checkBoxCell.Value);

                        // If the checkbox is checked, add the row index to the selectedRows list
                        if (isChecked)
                        {
                            CheckReportDTO dto = new CheckReportDTO();
                            dto.CheckDate = dgvCheckList.Rows[i].Cells["CheckDate"].Value.ToString();
                            dto.CheckNo = dgvCheckList.Rows[i].Cells["CheckNo"].Value.ToString();
                            dto.Payee = dgvCheckList.Rows[i].Cells["Payee"].Value.ToString();
                            dto.Amount = dgvCheckList.Rows[i].Cells["Amount"].Value.ToString();
                            dto.CheckId = dgvCheckList.Rows[i].Cells["CheckId"].Value.ToString();
                            selectedRows.Add(dto);
                        }
                    }

                    // Now you can use the selectedRows list to perform any further actions, e.g., display the selected rows in a MessageBox, etc.
                    if (selectedRows.Count > 0)
                    {
                        PrintCheck(selectedRows);

                        if (MessageBox.Show("Remove from List?", "Check Printer"
                                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            for (int i = 0; i < dgvCheckList.Rows.Count; i++)
                            {
                                // Find the checkbox cell in each row
                                DataGridViewCheckBoxCell checkBoxCell = dgvCheckList.Rows[i].Cells["SELECT"] as DataGridViewCheckBoxCell;

                                // Check if the checkbox is checked (true) or not (false)
                                bool isChecked = (checkBoxCell.Value == null) ? false : Convert.ToBoolean(checkBoxCell.Value);
                                string checkId = dgvCheckList.Rows[i].Cells["CheckId"].Value.ToString();

                                if (isChecked)
                                {
                                    // prompt here
                                    using (SqlCommand com = con.CreateCommand())
                                    {
                                        com.Transaction = transaction;
                                        com.CommandText = "UPDATE tblCheckIssue SET IsPrinted = '1' WHERE CheckId = @checkid";
                                        com.Parameters.AddWithValue("@checkid", checkId);
                                        com.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // No rows are selected, show a message indicating that
                        MessageBox.Show("No rows are selected.");
                    }
                    transaction.Commit();
                }

                LoadCheckList(bankid);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check Report: formWriteCheckList(): " + ex.Message, "Check Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCheckList_KeyUp(object sender, KeyEventArgs e)
        {
            // Detect the Shift key release
            if (e.KeyCode == Keys.ShiftKey)
            {
                isShiftKeyPressed = false;
            }
        }

        private void dgvCheckList_KeyDown(object sender, KeyEventArgs e)
        {
            // Detect the Shift key release
            if (e.KeyCode == Keys.ShiftKey)
            {
                isShiftKeyPressed = true;
            }
        }

        private void dgvCheckList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is a checkbox cell (column with checkbox)
            if (dgvCheckList.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {
                // Check if the shift key is pressed
                if (isShiftKeyPressed && lastClickedRowIndex >= 0)
                {
                    // Get the clicked row index
                    int clickedRowIndex = e.RowIndex;

                    // Determine the range of rows to select
                    int startRowIndex = Math.Min(lastClickedRowIndex, clickedRowIndex);
                    int endRowIndex = Math.Max(lastClickedRowIndex, clickedRowIndex);

                    // Select the rows in the range
                    for (int i = startRowIndex; i <= endRowIndex; i++)
                    {
                        dgvCheckList.Rows[i].Cells[e.ColumnIndex].Value = true;
                    }
                    lastClickedRowIndex = -1;
                }
                else
                {
                    // Toggle the clicked checkbox if shift key is not pressed
                    bool isChecked = Convert.ToBoolean(dgvCheckList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    dgvCheckList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !isChecked;
                    // Update the last clicked row index
                    lastClickedRowIndex = e.RowIndex;
                }

            }

            var senderGrid = (DataGridView)sender;
            string fileName = null;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                string checkId = dgvCheckList.Rows[e.RowIndex].Cells["CheckId"].Value.ToString();
                CheckIssueDTO checkIssueDTO = null;

                try
                {
                    using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                    {
                        con.Open();
                        SqlTransaction transaction = con.BeginTransaction();
                        using (SqlCommand com = con.CreateCommand())
                        {
                            com.Transaction = transaction;
                            com.CommandText = "SELECT * FROM tblCheckIssue WHERE CheckId = @checkid";
                            com.Parameters.AddWithValue("@checkid", checkId);
                            using (SqlDataReader reader = com.ExecuteReader())
                            {
                                if (reader.HasRows && reader.Read())
                                {
                                    checkIssueDTO = new CheckIssueDTO();
                                    checkIssueDTO.PayeeName = reader["PayeeName"].ToString();
                                    checkIssueDTO.CheckNo = Convert.ToInt32(reader["CheckNo"]);
                                    checkIssueDTO.Remarks = reader["Remarks"].ToString();
                                    checkIssueDTO.CheckAmount = Convert.ToDecimal(reader["CheckAmount"]);
                                }
                            }
                        }

                        using (SqlCommand com = con.CreateCommand())
                        {
                            com.Transaction = transaction;
                            com.CommandText = "SELECT ParameterValue FROM tblParameter WHERE ParameterID = @parameterid";
                            com.Parameters.AddWithValue("@parameterid", GlobalConstant.CHECK_CSV_FILE_PARAMETER_ID);
                            using (SqlDataReader reader = com.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    fileName = reader["ParameterValue"].ToString();
                                }
                            }
                        }

                        if (checkIssueDTO != null && !string.IsNullOrEmpty(fileName) && checkIssueDTO.CheckAmount > 0)
                        {
                            string cancelledPayeeName = "CANCELLED - " + checkIssueDTO.PayeeName;
                            using (SqlCommand com = con.CreateCommand())
                            {
                                com.Transaction = transaction;
                                com.CommandText = "UPDATE tblCheckIssue SET Remarks = @payeename, CheckAmount = @checkamount WHERE CheckId = @checkid";
                                com.Parameters.AddWithValue("@payeename", cancelledPayeeName);
                                com.Parameters.AddWithValue("@checkid", checkId);
                                com.Parameters.AddWithValue("@checkamount", 0.00);
                                com.ExecuteNonQuery();
                            }

                            String line = null;
                            if (File.Exists(fileName))
                            {
                                using (StreamReader reader = new StreamReader(fileName))
                                {
                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        if (line.Contains(","))
                                        {
                                            String[] split = line.Split(',');
                                            int fileCheckNo = Convert.ToInt32(split[1].ToString());
                                            if (checkIssueDTO.CheckNo == fileCheckNo)
                                            {
                                                split[3] = Decimal.Negate(checkIssueDTO.CheckAmount).ToString();
                                                split[6] = "CANCELLED";
                                                line = String.Join(",", split);
                                                break;
                                            }
                                        }
                                        //lines.Add(line);
                                    }
                                }
                                if (!string.IsNullOrEmpty(line))
                                {
                                    File.AppendAllText(fileName, line.ToString());
                                }

                                //using (StreamWriter writer = new StreamWriter(fileName))
                                //{
                                //    foreach (String line in lines)
                                //        writer.WriteLine(line);
                                //}
                            }
                        }
                        transaction.Commit();
                        MessageBox.Show("Check is already Cancelled");
                        LoadCheckList(bankid);
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            // Loop through the DataGridView rows
            foreach (DataGridViewRow row in dgvCheckList.Rows)
            {
                // Find the checkbox cell in each row
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["SELECT"] as DataGridViewCheckBoxCell;

                // Set the checkbox value to true (checked)
                checkBoxCell.Value = true;
            }
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            // Loop through the DataGridView rows
            foreach (DataGridViewRow row in dgvCheckList.Rows)
            {
                // Find the checkbox cell in each row
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["SELECT"] as DataGridViewCheckBoxCell;

                // Set the checkbox value to true (checked)
                checkBoxCell.Value = false;
            }
        }

        private void cboBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            ComboBoxDTO dto = (ComboBoxDTO) cb.SelectedItem;
            bankid = dto.Value;
            LoadCheckList(bankid);
        }
    }
}
