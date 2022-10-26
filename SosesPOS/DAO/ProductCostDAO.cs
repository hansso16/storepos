using SosesPOS.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DAO
{
    internal class ProductCostDAO
    {
        DbConnection dbcon = new DbConnection();
        SqlConnection con = null;
        SqlTransaction transaction = null;

        public ProductCostDAO(SqlConnection con, SqlTransaction transaction)
        {
            this.con = con;
            this.transaction = transaction;
        }

        public ProductCostDTO retrieveProductCostByPCodeAndVendorID(string pcode, string vendorid)
        {
            ProductCostDTO dto = null;
            try
            {
                using (SqlCommand tmpCom = new SqlCommand("SELECT PCode, VendorID, Cost, StartDate, EndDate " +
                    "FROM tblProductCost " +
                    "WHERE PCode = @pcode AND VendorID = @vendorid AND EndDate = '9999-12-31'", con, transaction))
                {
                    tmpCom.Parameters.AddWithValue("@pcode", pcode);
                    tmpCom.Parameters.AddWithValue("@vendorid", vendorid);
                    Console.WriteLine(tmpCom.CommandText);
                    using (SqlDataReader reader = tmpCom.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                dto = new ProductCostDTO();
                                dto.pcode = reader["PCode"].ToString();
                                dto.vendorID = Convert.ToInt32(reader["VendorID"]);
                                dto.cost = Convert.ToDecimal(reader["Cost"].ToString());
                                dto.startDate = Convert.ToDateTime(reader["StartDate"]);
                                dto.endDate = Convert.ToDateTime(reader["EndDate"]);
                            }
                        }
                        reader.Close();
                    }
                }
            } catch (Exception ex)
            {
                throw new Exception("ProductCostDAO - retrieveProductCostByPCodeAndVendorID(pcode,vendorid): " + ex.Message);
            }
            return dto;
        }
    }
}
