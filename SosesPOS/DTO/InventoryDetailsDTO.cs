using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DTO
{
    internal class InventoryDetailsDTO
    {
        //public int invoiceId;
        //public string pcode;
        //public int uom;
        //public int inventoryId;
        //public int qty;
        //public decimal sellingPrice;
        //public decimal totalItemPrice;
        //public int location;
        public int invoiceId { get; set; }
        public string pcode { get; set; }
        public int uom { get; set; }
        public int inventoryId { get; set; }
        public int qty { get; set; }
        public decimal sellingPrice { get; set; }
        public decimal totalItemPrice { get; set; }
        public int location { get; set; }
    }
}
