using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DTO
{
    public class RoleDTO
    {
        public int roleId { get; set; }
        public string roleCode { get; set; }
        public string roleName { get; set; }
        public int accessLevel { get; set; }
    }
}
