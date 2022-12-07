using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SosesPOS.util
{
    public class GeneralUtil
    {
        public static bool ValidateComboBox(ComboBox cbo, string title)
        {
            if (string.IsNullOrEmpty(cbo.Text) || cbo.SelectedIndex < 0)
            {
                MessageBox.Show("Invalid input. Please try again", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbo.Focus();
                cbo.SelectAll();
                return false;
            }
            return true;
        }
    }
}
