using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePayment
{
    public class ErrorDataModel
    {
        public string Message { get; set; }
        public string Details { get; set; }
        public string ErrorReference { get; set; }
        public DateTime DateTime { get; set; }
    }
}
