using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SosesPOS
{
    public partial class formQty : Form
    {
        formPOS formPOS = null;
        int rowIndex = 0;
        public formQty(formPOS formPOS, int rowIndex)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.formPOS = formPOS;
            this.rowIndex = rowIndex;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                // accept backspace
            }
            else if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtQty.Text))
            {
                MessageBox.Show("Please input Qty", "Edit Qty", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            int qty = Convert.ToInt32(txtQty.Text);
            decimal price = Convert.ToDecimal(formPOS.cartGridView.Rows[rowIndex].Cells["price"].Value);
            decimal total = qty * price;
            formPOS.cartGridView.Rows[rowIndex].Cells["qty"].Value = qty;
            formPOS.cartGridView.Rows[rowIndex].Cells["total"].Value = total;

            //decimal subtotal = Convert.ToDecimal(formPOS.lblSubTotal.Text);
            decimal subtotal = 0;
            foreach (DataGridViewRow row in formPOS.cartGridView.Rows)
            {
                subtotal += Convert.ToDecimal(row.Cells["total"].Value);
            }
            formPOS.lblSubTotal.Text = String.Format("{0:n}", subtotal);

            this.Dispose();
        }

        private void formQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave_Click(sender, e);
            }
        }
    }
}
