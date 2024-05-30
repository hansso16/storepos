using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DTO
{
    public class UserDTO
    {
        public int userCode { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public DateTime terminationDate { get; set; }
        public DateTime lastChangedTimestamp { get; set; }
        public int lastChangedUserCode { get; set; }
        public int roleId { get; set; }
        public RoleDTO role { get; set; }
        public char invoiceType { get; set; }
    }
}
