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
                        com.CommandText = "SELECT CheckDate, CheckNo, PayeeName, CheckAmount, EntryTimestamp, CheckId, isExported, Remarks " +
                                "FROM tblCheckIssue WHERE (IsPrinted = '0' OR IsPrinted IS NULL) AND CheckBankID = @checkbankid ORDER BY CheckNo ASC";
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
                                    , formattedAmount, reader["isExported"].ToString(), "CANCEL", reader["CheckId"].ToString());
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
            // EDIT
            //if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
            //    e.RowIndex >= 0 && senderGrid.Columns[e.ColumnIndex].Name.Equals("colEdit"))
            //{
            //    MessageBox.Show("asdf");
            //}

            // CANCEL
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
            e.RowIndex >= 0 && senderGrid.Columns[e.ColumnIndex].Name.Equals("colCancel"))
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
                            StringBuilder output = new StringBuilder();
                            if (File.Exists(fileName))
                            {
                                using (StreamReader reader = new StreamReader(fileName))
                                {
                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        if (line.Contains(","))
                                        {
                                            //String[] split = line.Split(',');
                                            String[] split = ParseCsvLine(line);
                                            int fileCheckNo = Convert.ToInt32(split[1].ToString());
                                            if (checkIssueDTO.CheckNo == fileCheckNo)
                                            {
                                                split[3] = Decimal.Negate(checkIssueDTO.CheckAmount).ToString();
                                                split[6] = "CANCELLED";
                                                //line = String.Join(",", split);
                                                output.AppendLine(string.Join(GlobalConstant.COMMA_SEPARATOR, split));
                                                break;
                                            }
                                        }
                                        //lines.Add(line);
                                    }
                                }
                                if (output != null && output.Length >= 0)
                                {
                                    File.AppendAllText(fileName, output.ToString());
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

        string[] ParseCsvLine(string line)
        {
            bool inQuotes = false;
            var result = new List<string>();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char currentChar = line[i];

                if (currentChar == '\"')
                {
                    sb.Append(currentChar);
                    inQuotes = !inQuotes;  // Toggle the quote mode
                }
                else if (currentChar == ',' && !inQuotes)
                {
                    // Field ends if we hit a comma and we are not inside quotes
                    result.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    // Add the current character to the current value
                    sb.Append(currentChar);
                }
            }

            // Add the last value
            result.Add(sb.ToString());

            return result.ToArray();
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

        private void dgvCheckList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore the header row
            if (e.RowIndex < 0 || isReverting) return;

            DataGridViewRow row = dgvCheckList.Rows[e.RowIndex];
            string columnName = dgvCheckList.Columns[e.ColumnIndex].Name;

            // We only want to update the DB if they edit specific columns
            if (columnName == "CheckNo")
            {
                string checkIssueID = row.Cells["CheckId"].Value.ToString();
                string newValue = row.Cells[e.ColumnIndex].Value.ToString();

                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    string query = "";
                    SqlCommand com = con.CreateCommand();

                    if (columnName == "CheckNo")
                    {
                        query = "UPDATE tblCheckIssue SET CheckNo = @checkno WHERE CheckId = @id";
                    }

                    if (!string.IsNullOrEmpty(query))
                    {
                        com.CommandText = query;
                        com.Parameters.AddWithValue("checkno", newValue);
                        com.Parameters.AddWithValue("@id", checkIssueID);

                        try
                        {
                            con.Open();
                            com.ExecuteNonQuery();
                        } catch (Exception ex)
                        {
                            MessageBox.Show("Error updating record: " + ex.Message, "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            RevertCell(row, e.ColumnIndex);
                        }
                    }
                }

            }
            
        }

        private string previousCellValue = "";
        private bool isReverting = false;
        private void dgvCheckList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // 1. Check the hidden IsExported column for this specific row
            bool isExported = false;

            // Safely check the value, treating 1, "True", or true as exported
            var exportedValue = dgvCheckList.Rows[e.RowIndex].Cells["colIsExported"].Value;
            if (exportedValue != null && (exportedValue.ToString() == "1" || exportedValue.ToString().ToLower() == "true"))
            {
                isExported = true;
            }

            // 2. If it is already exported, block the edit
            if (isExported)
            {
                e.Cancel = true; // This forces the cell to remain read-only
                MessageBox.Show("This check has already been exported to the file and cannot be edited.", "Edit Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Safely grab the current value
            previousCellValue = dgvCheckList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";
        }

        // A helper method to handle the revert process safely
        private void RevertCell(DataGridViewRow row, int columnIndex)
        {
            isReverting = true; // Turn ON the flag
            row.Cells[columnIndex].Value = previousCellValue; // Put the old value back
            isReverting = false; // Turn OFF the flag
        }

        private void btnExportToCSV_Click(object sender, EventArgs e)
        {
            // Confirm with the user before generating
            if (MessageBox.Show("Are you sure you want to export all pending checks to the bank file?", "Confirm Export", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    string fileName = "";

                    // 1. Fetch the CSV File Path from Parameters
                    using (SqlCommand com = new SqlCommand("SELECT ParameterValue FROM tblParameter WHERE ParameterID = @parameterid", con))
                    {
                        com.Parameters.AddWithValue("@parameterid", GlobalConstant.CHECK_CSV_FILE_PARAMETER_ID);
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                fileName = reader["ParameterValue"].ToString();
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(fileName))
                    {
                        MessageBox.Show("Export file path is missing in the database parameters.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 2. Fetch all Unexported Checks 
                    // We use a JOIN to get the Bank Short Name, assuming you have a tblCheckBank table
                    string selectQuery = @"
                                        SELECT 
                                            i.CheckID, 
                                            b.BankShortName, 
                                            i.CheckNo, 
                                            i.CheckDate, 
                                            i.CheckAmount, 
                                            i.PayeeCode, 
                                            i.Remarks AS VendorShortName, 
                                            i.PayeeName,
                                            i.Computer,
                                            i.Retain,
                                            i.Category
                                        FROM tblCheckIssue i
                                        LEFT JOIN tblCheckBank b ON i.CheckBankID = b.CheckBankID
                                        WHERE i.IsExported = 0 AND b.CheckBankId != '3'";

                    DataTable dtUnexported = new DataTable();
                    using (SqlCommand com = new SqlCommand(selectQuery, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(dtUnexported);
                        }
                    }

                    if (dtUnexported.Rows.Count == 0)
                    {
                        MessageBox.Show("There are no new checks waiting to be exported.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // 3. Write data to the CSV File
                    // 'true' in StreamWriter means it will APPEND to the file. Change to 'false' if you want it to overwrite and create a fresh file every time.
                    using (StreamWriter sw = new StreamWriter(fileName, true))
                    {
                        foreach (DataRow row in dtUnexported.Rows)
                        {
                            // Clean up fields to prevent commas in names from breaking the CSV layout
                            string bank = row["BankShortName"].ToString();
                            string checkNo = row["CheckNo"].ToString();
                            string checkDate = Convert.ToDateTime(row["CheckDate"]).ToString("MM/dd/yyyy");
                            string amount = row["CheckAmount"].ToString();
                            string vendorCode = row["PayeeCode"].ToString();
                            string vendorShortName = row["VendorShortName"].ToString();
                            string vendorFullName = row["PayeeName"].ToString();
                            string computer = row["Computer"].ToString();
                            string retain = row["Retain"].ToString();
                            string category = row["Category"].ToString();


                            // Note: In your original code you also had 'Category', but I noticed it wasn't saved in the tblCheckIssue INSERT statement. 
                            // If your bank requires it, you can append a default value here, or add Category to your table.

                            // Build the comma-separated line (adjust the order if your bank expects it differently)
                            String[] newLine = {
                                bank,
                                checkNo,
                                checkDate,
                                amount,
                                vendorCode,
                                "\"" + vendorShortName + "\"",
                                "\"" + vendorFullName + "\"",
                                category,
                                computer,
                                retain
                            };

                            sw.WriteLine(string.Join(GlobalConstant.COMMA_SEPARATOR, newLine));
                        }
                    }

                    // 4. Mark these specific checks as Exported in the database
                    using (SqlCommand com = new SqlCommand("UPDATE tblCheckIssue SET IsExported = 1 WHERE IsExported = 0", con))
                    {
                        com.ExecuteNonQuery();
                    }

                    // 5. Success
                    MessageBox.Show($"Successfully exported {dtUnexported.Rows.Count} check(s) to the file.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optional: Refresh your DataGridView here so the user sees the updated status
                    LoadCheckList(bankid);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during export: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
