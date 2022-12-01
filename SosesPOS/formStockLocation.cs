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
    public partial class formStockLocation : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        formStockLocationList formStockLocationList = null;
        formUnits formUnits = null;

        public formStockLocation(formStockLocationList formStockLocationList)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            this.formStockLocationList = formStockLocationList;
        }
        
        public formStockLocation(formUnits formUnits)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            this.formUnits = formUnits;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            this.hlblSLID.Text = "";
            this.txtLocationType.Clear();
            this.txtLocationName.Clear();
            this.txtLocationName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this Location?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
                    && "".Equals(hlblSLID.Text) && !String.IsNullOrEmpty(txtLocationName.Text))
                {
                    con.Open();
                    com = new SqlCommand("INSERT INTO tblStockLocation (LocationName, LocationType) " +
                        "VALUES (@locationname, @locationtype)", con);
                    com.Parameters.AddWithValue("@locationname", txtLocationName.Text);
                    com.Parameters.AddWithValue("@locationtype", txtLocationType.Text);
                    com.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("Location has been successfully saved");
                    Clear();
                    formUnits.LoadStockLocationList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Stock Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this Location?", "Update Location", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
                    && !String.IsNullOrEmpty(hlblSLID.Text))
                {
                    con.Open();
                    com = new SqlCommand("update tblStockLocation SET LocationName = @LocationName, LocationType = @locationtype " +
                        "WHERE SLID = @slid", con);
                    com.Parameters.AddWithValue("@LocationName", txtLocationName.Text);
                    com.Parameters.AddWithValue("@locationtype", txtLocationType.Text.Trim());
                    com.Parameters.AddWithValue("@slid", hlblSLID.Text.Trim());
                    com.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("Location has been successfully updated.");
                    Clear();
                    formUnits.LoadStockLocationList();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Stock Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
