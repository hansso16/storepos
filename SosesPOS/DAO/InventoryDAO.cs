using SosesPOS.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DAO
{
    internal class InventoryDAO
    {
        DbConnection dbcon = new DbConnection();
        SqlConnection con = null;
        SqlTransaction transaction = null;
        public InventoryDAO(SqlConnection con, SqlTransaction transaction)
        {
            this.con = con;
            this.transaction = transaction;
        }

        public List<InventoryDTO> retrieveNegativeInventoryByPCode(string pcode)
        {
            List<InventoryDTO> inventoryList = null;
            try
            {
                using (SqlCommand tempCom = new SqlCommand("SELECT PCode, InventoryID, Qty " +
                            "FROM tblInventory " +
                            "WHERE PCode = @pcode AND Qty < 0", con, transaction))
                {
                    tempCom.Parameters.AddWithValue("@pcode", pcode);
                    using (SqlDataReader reader = tempCom.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            inventoryList = new List<InventoryDTO>();
                            while (reader.Read())
                            {
                                InventoryDTO inventoryDTO = new InventoryDTO();
                                inventoryDTO.pcode = reader["PCode"].ToString();
                                inventoryDTO.inventoryID = Convert.ToInt32(reader["InventoryID"]);
                                inventoryDTO.qty = Convert.ToInt32(reader["Qty"].ToString());
                                inventoryList.Add(inventoryDTO);
                            }
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("InventoryDAO - retrieveNegativeInventoryByPCode(pcode): " + ex.Message);
            }
            return inventoryList;
        }

        public int retrieveTotalQtyByPCode(string pcode)
        {
            int totalQty = 0;
            try
            {
                using (SqlCommand tempCom = new SqlCommand("SELECT SUM(Qty) TOTAL_QTY " +
                            "FROM tblInventory " +
                            "WHERE PCode = @pcode AND Qty > 0", con, transaction))
                {
                    tempCom.Parameters.AddWithValue("@pcode", pcode);
                    using (SqlDataReader reader = tempCom.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            totalQty = Convert.ToInt32(reader["TOTAL_QTY"]);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("InventoryDAO - retrieveTotalQtyByPCode(pcode): " + ex.Message);
            }
            return totalQty;
        }
    }
}
