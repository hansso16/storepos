using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SosesPOS.DTO;

namespace SosesPOS.DAO
{
    internal class ProductDetailsDAO
    {
        DbConnection dbcon = new DbConnection();
        SqlConnection con = null;
        SqlTransaction transaction = null;

        public ProductDetailsDAO(SqlConnection con, SqlTransaction transaction)
        {
            this.con = con;
            this.transaction = transaction;
        }

        public ProductDetailsDTO getProductDetailsByPcode(string pcode)
        {
            ProductDetailsDTO dto = null;
            try
            {
                using (SqlCommand tmpCom = new SqlCommand("SELECT pcode, uom, price, startdate, enddate " +
                    "FROM tblProductDetails " +
                    "WHERE pcode = @pcode", con, transaction))
                {
                    tmpCom.Parameters.AddWithValue("@pcode", pcode);
                    using (SqlDataReader reader = tmpCom.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                dto = new ProductDetailsDTO();
                                dto.pcode = reader["PCode"].ToString();
                                dto.uom = Convert.ToInt32(reader["uom"].ToString());
                                dto.price = Convert.ToDecimal(reader["price"].ToString());
                                dto.startDate = Convert.ToDateTime(reader["startdate"]);
                                dto.endDate = Convert.ToDateTime(reader["enddate"]);
                            }
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ProductDetailsDAO - getProductDetailsByPcode(pcode): " + ex.Message);
            }
            return dto;
        }
    }
}
