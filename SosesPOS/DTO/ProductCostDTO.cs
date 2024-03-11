using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DTO
{
    internal class ProductCostDTO
    {
        public string pcode { get; set; }
        public int vendorID { get; set; }
        public decimal cost { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public decimal wholeCost { get; set; }
    }
}
