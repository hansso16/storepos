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
using SosesPOS.util;
namespace SosesPOS
{
    public partial class formTest : Form
    {
        SqlConnection con = null;
        SqlCommand com = null;
        SqlDataReader dr = null;
        DbConnection dbcon = new DbConnection();
        public formTest()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.MyConnection());
            LoadProductRecords();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadProductRecords()
        {
            con.Open();
            com = new SqlCommand("select * from tblProduct", con);
            dr = com.ExecuteReader();
            List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
            //AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                //MessageBox.Show("HERE");
                dataSource.Add(new ComboBoxDTO() { Name = dr["pdesc"].ToString(), Value = dr["pcode"].ToString() });
                //collection.Add(dr["pdesc"].ToString());
            }
            cboTest.DataSource = dataSource;
            cboTest.DisplayMember = "Name";
            cboTest.ValueMember = "Value";
            //cboTest.AutoCompleteCustomSource = collection;
            dr.Close();
            con.Close();
        }

        private void cboTest_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cboTest_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboTest_KeyPress(object sender, KeyPressEventArgs e)
        {
            //cboTest.DataSource = null;
            //cboTest.Items.Clear();
            //con.Open();
            //com = new SqlCommand("select * from tblProduct where pdesc like '%'+@pdesc+'%'", con);
            //com.Parameters.AddWithValue("@pdesc", cboTest.Text);
            //dr = com.ExecuteReader();
            //List<ComboBoxDTO> dataSource = new List<ComboBoxDTO>();
            ////AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            //while (dr.Read())
            //{
            //    //MessageBox.Show("HERE");
            //    dataSource.Add(new ComboBoxDTO() { Name = dr["pdesc"].ToString(), Value = dr["pcode"].ToString() });
            //    //collection.Add(dr["pdesc"].ToString());
            //}
            //cboTest.DataSource = dataSource;
            //cboTest.DisplayMember = "Name";
            //cboTest.ValueMember = "Value";
            ////cboTest.AutoCompleteCustomSource = collection;
            //dr.Close();
            //con.Close();
        }
    }
}
