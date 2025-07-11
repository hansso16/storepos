﻿using CsvHelper;
using SosesPOS.DTO;
using SosesPOS.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SosesPOS
{
    public partial class formWriteCheck : Form
    {
        DbConnection dbcon = new DbConnection();
        UserDTO user = null;
        string module = "Check Writer";
        formWriteCheckList formWriteCheckList = null;
        public formWriteCheck(UserDTO user, formWriteCheckList formWriteCheckList)
        {
            InitializeComponent();
            this.user = user;
            this.KeyPreview = true;
            this.formWriteCheckList = formWriteCheckList;
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    RetrieveCheckNoByBank(con, rbBDO);
                    this.txtBankType.Text = "1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Write Check: formWriteCheck(): " + ex.Message, "Write Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.txtBankType.Focus();
            this.txtBankType.SelectAll();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8 || e.KeyChar == 13 || e.KeyChar == 46)
            {
                // accept backspace and enter and "." (period)
            }
            else if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = true;
            }
        }

        private void txtPayee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtPayee.Text.Trim()))
                {
                    MessageBox.Show("Invalid Payee Information", module, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPayee.Focus();
                    txtPayee.SelectAll();
                }
                txtAmount.Focus();
                txtAmount.SelectAll();
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    btnSubmit_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid Amount: " + ex.Message, module, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.txtAmount.Focus();
                    this.txtAmount.SelectAll();
                    this.lblWrittenInteger.Text = "";
                }
            }
        }

        private bool ProcessAmount()
        {
            if (ValidateCost())
            {
                decimal amount = Convert.ToDecimal(txtAmount.Text);
                amount = IntegerUtil.Normalize(amount);
                string strAmount = $"{amount:n}";
                this.txtAmount.Text = strAmount;
                if (!amount.Equals(decimal.Zero))
                {
                    string writtenNumbers = IntegerUtil.NumberToCurrencyText(amount, MidpointRounding.AwayFromZero);
                    this.lblWrittenInteger.Text = writtenNumbers;
                }
                return true;
            }
            return false;
        }

        private bool ValidateCost()
        {
            if (String.IsNullOrEmpty(txtAmount.Text) || Convert.ToDecimal(txtAmount.Text) < 0)
            {
                MessageBox.Show("Invalid Check Amount. Please try again", module, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtAmount.Focus();
                this.txtAmount.SelectAll();
                return false;
            }
            return true;
        }

        private void txtPayee_Leave(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtPayee.Text.Trim()))
            //{
            //    MessageBox.Show("Invalid Payee Information", module, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtPayee.Focus();
            //    txtPayee.SelectAll();
            //}
            //txtAmount.Focus();
            //txtAmount.SelectAll();
        }

        private bool ValidateCheckDetails()
        {
            if ((String.IsNullOrEmpty(txtCheckNo.Text) || Convert.ToDecimal(txtAmount.Text) < 0))
            {
                MessageBox.Show("Invalid Check Number. Please try again", module, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtCheckNo.Focus();
                this.txtCheckNo.SelectAll();
                return false;
            }
            else if (String.IsNullOrEmpty(txtPayee.Text))
            {
                MessageBox.Show("Invalid Payee. Please try again", module, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtPayee.Focus();
                this.txtPayee.SelectAll();
                return false;
            } else if (String.IsNullOrEmpty(dtpCheckDate.Value.ToString()))
            {
                MessageBox.Show("Invalid Check Date. Please try again", module, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.dtpCheckDate.Focus();
                return false;
            }
            return true;
        }

        private bool ValidateCheck()
        {
            if (!ProcessAmount() || !ValidateCheckDetails())
            { 
                return false;
            }
            if (string.IsNullOrEmpty(txtPayee.Text.Trim()))
            {
                MessageBox.Show("Invalid Payee Information", module, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPayee.Focus();
                txtPayee.SelectAll();
                return false;
            }
            return true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateCheck())
                {
                    return;
                }

                if (MessageBox.Show("Yes - Save and Print\nNo - Cancel", module
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // if other, we only need to print
                        if (rbOther.Checked)
                        {
                            //hlblCheckBankID.Text = "0";
                            if (String.IsNullOrEmpty(cboCategory.Text) || cboCategory.SelectedIndex < 0)
                            {
                                cboCategory.SelectedValue = "PERSONAL";
                            }
                            //PrintCheck(dtpCheckDate.Value.ToString("MM/dd/yyyy"), txtAmount.Text, txtPayee.Text);
                            //this.Dispose();
                            //return;
                        }

                        if ((String.IsNullOrEmpty(cboCategory.Text) || cboCategory.SelectedIndex < 0) && !rbOther.Checked)
                        {
                            MessageBox.Show("Invalid Category.", "Check Writer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cboCategory.Focus();
                            return;
                        }
                        if ((String.IsNullOrEmpty(txtVendorShortName.Text)) && !rbOther.Checked)
                        {
                            MessageBox.Show("Invalid Vendor.", "Check Writer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtVendorShortName.Focus();
                            txtVendorShortName.SelectAll();
                            return;
                        }

                        using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                        {
                            con.Open();
                            SqlTransaction transaction = con.BeginTransaction();
                            string fileName = null;
                            int checkBankID = Convert.ToInt32(hlblCheckBankID.Text);
                            int checkNo = String.IsNullOrEmpty(txtCheckNo.Text) ? 0 : Convert.ToInt32(txtCheckNo.Text) + 1;
                            decimal decAmount = Convert.ToDecimal(txtAmount.Text);

                            // retrieve file path and name
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

                            if (string.IsNullOrEmpty(fileName))
                            {
                                MessageBox.Show("fileName is null: btnSubmit_Click(): ", "Write Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                                return;
                            }

                            // Set up data
                            CheckCSVDTO csvDTO = new CheckCSVDTO();
                            csvDTO.Bank = hlblBankShortName.Text;
                            csvDTO.CheckNo = txtCheckNo.Text;
                            csvDTO.CheckDate = dtpCheckDate.Value.ToString("MM/dd/yyyy");
                            csvDTO.CheckAmount = decAmount.ToString();
                            csvDTO.VendorCode = hlblPayeeCode.Text;
                            csvDTO.VendorShortName = txtVendorShortName.Text;
                            csvDTO.VendorFullName = txtPayee.Text;
                            csvDTO.Computer = "1";
                            csvDTO.Retain = decAmount.Equals(decimal.Zero) ? "1" : "0";
                            if (rbOther.Checked)
                            {
                                csvDTO.Category = "PERSONAL";
                            } else
                            {
                                csvDTO.Category = cboCategory.SelectedValue.ToString();
                            }

                            // Save to CheckIssue table
                            using (SqlCommand com = con.CreateCommand())
                            {
                                com.Transaction = transaction;
                                com.CommandText = "INSERT INTO tblCheckIssue (CheckNo, CheckDate, CheckAmount, CheckBankID" +
                                    ", PayeeCode, PayeeName, Computer, Retain, EntryTimestamp, LastChangedUser, IsPrinted, Remarks) " +
                                    "VALUES (@checkno, @checkdate, @checkamount, @checkbankid, @payeecode, @payeename" +
                                    ", @computer, @retain, @entrytimestamp, @lastchangeduser, @isprinted, @remarks)";
                                com.Parameters.AddWithValue("@checkno", csvDTO.CheckNo);
                                com.Parameters.AddWithValue("@checkdate", csvDTO.CheckDate);
                                com.Parameters.AddWithValue("@checkamount", decAmount);
                                com.Parameters.AddWithValue("@checkbankid", checkBankID);
                                com.Parameters.AddWithValue("@payeecode", csvDTO.VendorCode);
                                com.Parameters.AddWithValue("@payeename", csvDTO.VendorFullName);
                                com.Parameters.AddWithValue("@computer", csvDTO.Computer);
                                com.Parameters.AddWithValue("@retain", csvDTO.Retain);
                                com.Parameters.AddWithValue("@entrytimestamp", DateTime.Now);
                                com.Parameters.AddWithValue("@lastchangeduser", this.user.userCode);
                                com.Parameters.AddWithValue("@isprinted", "0");
                                com.Parameters.AddWithValue("@remarks", csvDTO.VendorShortName);
                                com.ExecuteNonQuery();
                            }

                            // Update Check No/Increment by 1
                            using (SqlCommand com = con.CreateCommand())
                            {
                                com.Transaction = transaction;
                                com.CommandText = "UPDATE tblCheckBank SET CheckNo = @checkno WHERE CheckBankID = @checkbankid";
                                com.Parameters.AddWithValue("@checkno", checkNo);
                                com.Parameters.AddWithValue("@checkbankid", checkBankID);
                                com.ExecuteNonQuery();
                            }

                            // if 'other'/personal check, no need to put into csv file
                            if (!rbOther.Checked)
                            {
                                // Save to file
                                ProcessCSVFile(fileName, csvDTO);
                            }

                            // PRINT
                            if (!rbSS.Checked)
                            {
                                PrintCheck(Convert.ToDateTime(dtpCheckDate.Value), txtAmount.Text, txtPayee.Text);
                            }

                            //MessageBox.Show("The data has been successfully saved.", "Check Writer"
                            //   , MessageBoxButtons.OK, MessageBoxIcon.Information);

                            transaction.Commit();
                            this.Dispose();
                            formWriteCheckList.txtSearch.Clear();
                            formWriteCheckList.txtSearch.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Write Check: btnSubmit_Click(): " + ex.Message, "Write Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                } else
                {
                    btnCancel_Click(sender, e);
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong when saving: " + ex.Message, module, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbBDO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBDO.Checked)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                    {
                        con.Open();
                        RetrieveCheckNoByBank(con, rbBDO);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Write Check: rbBDO_CheckedChanged(): " + ex.Message, "Write Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        private void rbSS_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSS.Checked)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                    {
                        con.Open();
                        RetrieveCheckNoByBank(con, rbSS);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Write Check: rbSS_CheckedChanged(): " + ex.Message, "Write Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void rbOther_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOther.Checked)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                    {
                        con.Open();
                        RetrieveCheckNoByBank(con, rbOther);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Write Check: rbOther_CheckedChanged(): " + ex.Message, "Write Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RetrieveCheckNoByBank(SqlConnection con, RadioButton rb)
        {
            using (SqlCommand com = con.CreateCommand())
            {
                com.CommandText = "SELECT CheckBankID, CheckNo, BankShortName FROM tblCheckBank WHERE BankShortName = @bankshortname";
                com.Parameters.AddWithValue("@bankshortname", rb.Text);
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtCheckNo.Text = reader["CheckNo"].ToString();
                        hlblCheckBankID.Text = reader["CheckBankID"].ToString();
                        hlblBankShortName.Text = reader["BankShortName"].ToString();
                    }
                    else
                    {
                        txtCheckNo.Clear();
                    }
                }
            }
        }

        public void LoadCategory(string categoryId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.MyConnection()))
                {
                    con.Open();
                    List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
                    using (SqlCommand com = con.CreateCommand())
                    {
                        com.CommandText = "SELECT id, category FROM tblCategory";
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dataSource.Add(new ComboBoxDTO() { Name = reader["category"].ToString(), Value = reader["id"].ToString() });
                                //collection.Add(dr["pdesc"].ToString());
                            }
                            cboCategory.DataSource = dataSource;
                            cboCategory.DisplayMember = "Name";
                            cboCategory.ValueMember = "Value";
                            cboCategory.SelectedValue = categoryId;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Write Check: formWriteCheck(): " + ex.Message, "Write Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                //ProcessAmount();
                decimal amount = 0;
                if (!String.IsNullOrEmpty(this.txtAmount.Text) && Decimal.TryParse(this.txtAmount.Text, out amount) && amount > 0)
                {
                    //decimal amount = Convert.ToDecimal(txtAmount.Text);
                    amount = IntegerUtil.Normalize(amount);
                    string strAmount = $"{amount:n}";
                    this.txtAmount.Text = strAmount;
                    if (!amount.Equals(decimal.Zero))
                    {
                        string writtenNumbers = IntegerUtil.NumberToCurrencyText(amount, MidpointRounding.AwayFromZero);
                        this.lblWrittenInteger.Text = writtenNumbers;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Amount: " + ex.Message, module, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtAmount.Focus();
                this.txtAmount.SelectAll();
                this.lblWrittenInteger.Text = "";
            }
        }

        private void ProcessCSVFile(string fileName, CheckCSVDTO csv)
        {
            StringBuilder output = new StringBuilder();

            if ("SuySing".Equals(csv.Bank))
            {
                csv.Bank = "BDO";
            }

            //string newLine = string.Format("{0}, {1}, {2}, {3}", "", "", "", 01);
            String[] newLine = { csv.Bank.ToString(), csv.CheckNo, csv.CheckDate, csv.CheckAmount, csv.VendorCode
                    , "\"" + csv.VendorShortName + "\"", "\"" + csv.VendorFullName + "\"", csv.Category, csv.Computer, csv.Retain };
            output.AppendLine(string.Join(GlobalConstant.COMMA_SEPARATOR, newLine));

            try
            {
                //Console.WriteLine(output.ToString());
                if (!File.Exists(fileName))
                {
                    //File.Create(fileName);
                    File.WriteAllText(fileName, output.ToString());
                }
                else
                {
                    File.AppendAllText(fileName, output.ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Data could not be written to the CSV file: " + ex.Message);
            }
        }

        private void txtCheckNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8 || e.KeyChar == 13)
            {
                // accept backspace and enter
            }
            else if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = true;
            }
        }

        private void PrintCheck(DateTime checkDate, string checkAmount, string payee)
        {
            formCheckPrint form = new formCheckPrint();
            form.PrintCheck(checkDate, checkAmount, payee);
            this.Focus();
        }

        private void txtCheckNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((rbBDO.Checked || rbSS.Checked) && string.IsNullOrEmpty(txtCheckNo.Text.Trim()))
                {
                    MessageBox.Show("Invalid Check No", module, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCheckNo.Focus();
                    txtCheckNo.SelectAll();
                    return;
                }
                this.txtAmount.Focus();
                this.txtAmount.SelectAll();
                //this.dtpCheckDate.Focus();
                //this.dtpCheckDate.SelectAll();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.txtCheckNo.Focus();
            this.txtCheckNo.SelectAll();
            //this.txtPayee.Clear();
            this.txtAmount.Clear();
            this.lblWrittenInteger.Text = "";
        }

        private void formWriteCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void dtpCheckDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtPayee.Focus();
                //txtPayee.SelectAll();
                txtAmount.Focus();
                txtAmount.SelectAll();
            }
        }

        private void txtBankType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8 || e.KeyChar == 49 || e.KeyChar == 50 || e.KeyChar == 51)
            {
                // accept backspace, 1, 2 and 3
            }
            else if (e.KeyChar == 13) // enter
            {
                if (string.IsNullOrEmpty(txtBankType.Text))
                {
                    MessageBox.Show("Invalid Bank Type");
                    return;
                }
                if (txtBankType.Text.Equals("1"))
                {
                    rbBDO.Checked = true;
                } 
                else if (txtBankType.Text.Equals("2"))
                {
                    rbSS.Checked = true;
                }
                else if (txtBankType.Text.Equals("3"))
                {
                    rbOther.Checked = true;
                }
                txtCheckNo.Focus();
                txtCheckNo.SelectAll();
            }
            else if (e.KeyChar <= 48 || e.KeyChar >= 52)
            {
                e.Handled = true;
            }
        }
    }
}
