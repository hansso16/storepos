using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DTO
{
    internal class InventoryDTO
    {
        public int inventoryID { get; set; }
        public string pcode { get; set; }
        public int slid { get; set; }
        public int purchaseID { get; set; }
        public int qty { get; set; }
    }
}
