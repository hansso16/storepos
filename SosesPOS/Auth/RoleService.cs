using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SosesPOS.DTO;

namespace SosesPOS.Auth
{
    internal class RoleService
    {

        public RoleDTO retrieverRoleDetails(SqlConnection con, UserDTO userDTO)
        {
            if (userDTO == null || userDTO.roleId == 0)
            {
                throw new Exception("Invalid UserDTO.");
            }

            RoleDTO role = null;
            using (SqlCommand com = new SqlCommand("SELECT RoleId, RoleCode, RoleName, AccessLevel " +
                "FROM tblRole WHERE RoleId = @roleid", con))
            {
                com.Parameters.AddWithValue("@roleid", userDTO.roleId);
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        role = new RoleDTO();
                        role.roleId = Convert.ToInt32(reader["RoleId"]);
                        role.roleCode = reader["RoleCode"].ToString();
                        role.roleName = reader["RoleName"].ToString();
                        role.accessLevel = Convert.ToInt32(reader["AccessLevel"]);
                    }
                }
            }
            return role;
        }

        public List<RoleDTO> retrieveRoleByAccessLevel(SqlConnection con, RoleDTO roleDTO)
        {
            List<RoleDTO> roles = new List<RoleDTO>();
            using (SqlCommand com = con.CreateCommand())
            {
                com.CommandText = "SELECT RoleId, RoleCode, RoleName " +
                    "FROM tblRole WHERE AccessLevel <= @accesslevel " +
                    "ORDER BY AccessLevel ASC";
                com.Parameters.AddWithValue("@accesslevel", roleDTO.accessLevel);

                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RoleDTO dto = new RoleDTO();
                        dto.roleId = Convert.ToInt32(reader["RoleId"]);
                        dto.roleCode = reader["RoleCode"].ToString();
                        dto.roleName = reader["RoleName"].ToString();
                        roles.Add(dto);
                    }
                }

            }
            return roles;
        }
    }
}
