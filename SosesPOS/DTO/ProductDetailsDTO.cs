using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DTO
{
    public class ProductDetailsDTO
    {
        public string pcode { get; set; }
        public int uom { get; set; }
        public decimal price { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
