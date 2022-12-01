using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.DTO
{
    internal class UOMDTO
    {
        public int uomId { get; set; }
        public string uomCode { get; set; }
        public string uomDescription { get; set; }
        public string uomType { get; set; }
        public int count { get; set; }
    }
}
