using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SosesPOS.util;

namespace SosesPOS
{
    public partial class formLiteCheckWriter : Form
    {
        string module = "Lite Check Issue";
        public formLiteCheckWriter()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    this.btnSubmit.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid Amount: " + ex.Message, module, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.txtAmount.Focus();
                    this.txtAmount.SelectAll();
                    this.lblWrittenFigures.Text = "";
                }
            }
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

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                ProcessAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Amount: " + ex.Message, module, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtAmount.Focus();
                this.txtAmount.SelectAll();
                this.lblWrittenFigures.Text = "";
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
                    this.lblWrittenFigures.Text = writtenNumbers;
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ProcessAmount())
                {
                    return;
                }
                if (MessageBox.Show("Print? Changes are irreversible", module
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // PRINT
                    PrintCheck(lblWrittenFigures.Text);

                    MessageBox.Show("Data has been printed.", "Check Writer"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong when saving: " + ex.Message, module, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintCheck(string writtenFigures)
        {
            formLiteCheckPrint form = new formLiteCheckPrint();
            form.PrintCheck(writtenFigures);
            this.Focus();
        }
    }
}
