using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS.util
{
    internal class GlobalConstant
    {
        public static readonly string STORE_CODE = "1";
        public static readonly string WH_CODE = "0";

        public static readonly string WHOLE_UOM_CODE = "0";
        public static readonly string BROKEN_UOM_CODE = "1";

        public static readonly string STOCK_TRANSFER_REQUESTED = "01";
        public static readonly string STOCK_TRANSFER_DISPATCHED = "15";
        public static readonly string STOCK_TRANSFER_ACCEPTED = "20";

        public static readonly string COMMA_SEPARATOR = ",";
        public static readonly string CHECK_CSV_FILE_PARAMETER_ID = "1";
    }
}
