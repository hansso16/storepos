using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DTO
{
    public class CheckReportDTO
    {
        public string CheckDate { get; set; }
        public string CheckNo { get; set; }
        public string Payee { get; set; }
        public string Amount { get; set; }
        public string CheckId { get; set; }
    }
}
