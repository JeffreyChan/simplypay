using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplePayment.Service;

namespace SimplePayment.Test
{
    public class DataSourceExtensionService
    {
        private readonly IPaymentService _paymentService;

        public DataSourceExtensionService(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        public async Task<bool> EasyPay(MerchantInfoDto merchantDto)
        {
            await _paymentService.EasyPay(merchantDto);
            return true;
        }
    }
}
