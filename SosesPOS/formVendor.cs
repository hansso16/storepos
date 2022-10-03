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
    public partial class formVendor : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        formVendorList formVendorList = null;
        public formVendor(formVendorList formVendorList)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            this.formVendorList = formVendorList;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            this.hlblVendorID.Text = "";
            this.txtAddress.Clear();
            this.txtName.Clear();
            this.txtContactNumber.Clear();
            this.txtContactPerson.Clear();
            this.txtEmailAddress.Clear();
            this.txtVCode.Clear();
            this.txtVCode.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this vendor?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
                    && "".Equals(hlblVendorID.Text))
                {
                    con.Open();

                    com = new SqlCommand("SELECT 1 FROM tblVendor WHERE VendorCode = @ccode", con);
                    com.Parameters.AddWithValue("@ccode", txtVCode.Text);
                    dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Vendor Code already exist. Please choose another.", "", MessageBoxButtons.OK, MessageBoxIcon.Error); // error
                        dr.Close();
                        con.Close();
                        return;
                    }
                    dr.Close();


                    com = new SqlCommand("INSERT INTO tblVendor (VendorCode, VendorName, VendorAddress, ContactPerson, ContactNumber, EmailAddress) " +
                        "OUTPUT inserted.VendorID " +
                        "VALUES (@vcode, @vname, @vaddress, @contactperson, @contactnumber, @email)", con);
                    com.Parameters.AddWithValue("@vcode", txtVCode.Text);
                    com.Parameters.AddWithValue("@vname", txtName.Text);
                    com.Parameters.AddWithValue("@vaddress", txtAddress.Text);
                    com.Parameters.AddWithValue("@contactperson", txtContactPerson.Text);
                    com.Parameters.AddWithValue("@contactnumber", txtContactNumber.Text);
                    com.Parameters.AddWithValue("@email", txtEmailAddress.Text);
                    int vid = Convert.ToInt32(com.ExecuteScalar());

                    con.Close();
                    MessageBox.Show("Vendor record has been successfully saved");
                    Clear();
                    formVendorList.LoadVendorList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Vendor Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this vendor?", "Update Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
                    && !String.IsNullOrEmpty(hlblVendorID.Text))
                {
                    con.Open();
                    com = new SqlCommand("update tblVendor SET VendorCode = @vcode, VendorName = @vname, VendorAddress = @vaddress " +
                        ", ContactPerson = @contactperson, ContactNumber = @contactnumber, EmailAddress = @email " +
                        "WHERE VendorID = @vid", con);
                    com.Parameters.AddWithValue("@vid", hlblVendorID.Text);
                    com.Parameters.AddWithValue("@vcode", txtVCode.Text.Trim());
                    com.Parameters.AddWithValue("@vname", txtName.Text.Trim());
                    com.Parameters.AddWithValue("@vaddress", txtAddress.Text.Trim());
                    com.Parameters.AddWithValue("@contactperson", txtContactPerson.Text.Trim());
                    com.Parameters.AddWithValue("@contactnumber", txtContactNumber.Text.Trim());
                    com.Parameters.AddWithValue("@email", txtEmailAddress.Text.Trim());
                    com.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("Vendor has been successfully updated.");
                    Clear();
                    formVendorList.LoadVendorList();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Vendor Module", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
