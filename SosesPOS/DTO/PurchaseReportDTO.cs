using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DTO
{
    public class PurchaseReportDTO
    {
        public string vendorCode { get; set; }
        public string vendorRefNo { get; set; }
        public int totalQty { get; set; }
        public decimal totalCost { get; set; }
        public DateTime entryTimestamp { get; set; }
        public string site { get; set; }
        public string freight { get; set; }
        public List<PurchaseItemDTO> purchaseItemDTO { get; set; }

        public List<NewPurchaseCostDTO> newPurchaseCostDTO { get; set; }
    }
}
