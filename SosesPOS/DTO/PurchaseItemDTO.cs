using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DTO
{
    public class PurchaseItemDTO
    {
        public string productCode { get; set; }
        public string productDescription { get; set; }
        public string count { get; set; }
        public int qty { get; set; }
        public int bal { get; set; }
        public decimal cost { get; set; }
        public decimal freight { get; set; }
        public decimal totalCost { get; set; }
    }
}
