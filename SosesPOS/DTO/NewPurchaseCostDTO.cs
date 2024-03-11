using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DTO
{
    public class NewPurchaseCostDTO
    {
        public string productCode { get; set; }
        public string productDescription { get; set; }
        public decimal oldCost { get; set; }
        public decimal newCost { get; set; }
        public decimal oldPrice { get; set; }
    }
}
