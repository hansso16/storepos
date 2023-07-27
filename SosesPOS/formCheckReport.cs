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

namespace SosesPOS
{
    public partial class formCheckReport : Form
    {
        DbConnection dbcon = new DbConnection();
        private bool isShiftKeyPressed = false;
        private int lastClickedRowIndex = -1;
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
                        com.CommandText = "SELECT CheckDate, CheckNo, PayeeName, CheckAmount, EntryTimestamp, CheckId " +
                                "FROM tblCheckIssue WHERE IsPrinted = '0'";
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            int i = 1;
                            while (reader.Read())
                            {
                                decimal amount = Convert.ToDecimal(reader["CheckAmount"]);
                                string formattedAmount = amount.ToString("C", System.Globalization.CultureInfo.CurrentCulture).Substring(1);
                                dgvCheckList.Rows.Add(i++, 0, Convert.ToDateTime(reader["CheckDate"]).ToString("MM/dd/yyyy")
                                    , reader["CheckNo"].ToString(), reader["PayeeName"].ToString()
                                    , formattedAmount, reader["CheckId"].ToString());
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

                            using (SqlCommand com = con.CreateCommand())
                            {
                                com.Transaction = transaction;
                                com.CommandText = "UPDATE tblCheckIssue SET IsPrinted = '1' WHERE CheckId = @checkid";
                                com.Parameters.AddWithValue("@checkid", dto.CheckId);
                                com.ExecuteNonQuery();
                            }
                        }
                    }

                    // Now you can use the selectedRows list to perform any further actions, e.g., display the selected rows in a MessageBox, etc.
                    if (selectedRows.Count > 0)
                    {
                        PrintCheck(selectedRows);
                    }
                    else
                    {
                        // No rows are selected, show a message indicating that
                        MessageBox.Show("No rows are selected.");
                    }
                    transaction.Commit();
                }

                LoadCheckList();
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
    }
}
