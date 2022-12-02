using SosesPOS.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.Auth
{
    internal class UserService
    {
        public UserDTO retrieverUserDetails(SqlConnection con, string username)
        {
            if (String.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException("Invalid Username. Username is null");
            }

            UserDTO userDTO = null;
            using (SqlCommand com = new SqlCommand("SELECT UserCode, Username, Password, TerminationDate" +
                ", LastChangedTimestamp, LastChangedUserCode, RoleId " +
                "FROM tblUser WHERE Username = @username", con))
            {
                com.Parameters.AddWithValue("@username", username);
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userDTO = new UserDTO();
                        userDTO.userCode = Convert.ToInt32(reader["UserCode"]);
                        userDTO.username = reader["Username"].ToString();
                        userDTO.password = reader["Password"].ToString();
                        userDTO.terminationDate = Convert.ToDateTime(reader["TerminationDate"]);
                        userDTO.lastChangedTimestamp = Convert.ToDateTime(reader["LastChangedTimestamp"]);
                        userDTO.lastChangedUserCode = Convert.ToInt32(reader["LastChangeduserCode"]);
                        userDTO.roleId = Convert.ToInt32(reader["RoleId"]);
                    }
                }
            }
            return userDTO;
        }

        public void updateUserPassword(SqlConnection con, string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Invalid Username/Password.");
            }

            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "UPDATE tblUser SET Password = @password WHERE Username = @username";
                com.Parameters.AddWithValue("@password", password);
                com.Parameters.AddWithValue("@username", username);
                com.ExecuteNonQuery();
            }
        }
    }
}
