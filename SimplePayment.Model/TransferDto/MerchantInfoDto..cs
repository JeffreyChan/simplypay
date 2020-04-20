using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePayment
{
    public class MerchantInfoDto
    {
        public string MerchantName { get; set; }
        public string ReferenceFlag { get; set; }
        public decimal PayAmount { get; set; }

        public byte Payway { get; set; }
    }
}
