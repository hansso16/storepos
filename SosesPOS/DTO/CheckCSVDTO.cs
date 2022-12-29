using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DTO
{
    internal class CheckCSVDTO
    {
        public string Bank { get; set; }
        public string CheckNo { get; set; }
        public string CheckDate { get; set; }
        public string CheckAmount { get; set; }
        public string VendorCode { get; set; }
        public string VendorShortName { get; set; }
        public string VendorFullName { get; set; }
        public string Category{ get; set; }
        public string Computer { get; set; }
        public string Retain { get; set; }
    }
}
