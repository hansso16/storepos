using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DTO
{
    public class CheckIssueDTO
    {
        public int CheckId { get; set; }
        public int CheckNo { get; set; }
        public DateTime CheckDate { get; set; }
        public decimal CheckAmount { get; set; }
        public int CheckBankID { get; set; }
        public string PayeeCode { get; set; }
        public string PayeeName { get; set; }
        public string Remarks { get; set; }
        public int Computer { get; set; }
        public int Retain { get; set; }
        public DateTime EntryTimestamp { get; set; }
        public DateTime LastChangedUser { get; set; }

    }
}
