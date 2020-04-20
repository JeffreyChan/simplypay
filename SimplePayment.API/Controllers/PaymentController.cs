using System;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using NLog;
using SimplePayment.Service;

namespace SimplePayment.API
{
    /// <summary>  
    /// Payment  
    /// </summary>  
    [RoutePrefix("api/simple")]
    public class PaymentController : ApiController
    {
        protected readonly ISimplePaymentFacade _facade;

        private readonly ILogger _logger  = LogManager.GetCurrentClassLogger();
        public PaymentController(ISimplePaymentFacade facade)
        {
            this._facade = facade;
        }

        /// <summary>  
        /// Process payment  
        /// </summary>   
        /// <returns code="200"></returns>  
        [HttpPost]
        [Route("pay")]
        public async Task<bool> SendVerificationCode(MerchantInfoDto dto)
        { 
            var requestJson = JsonConvert.SerializeObject(dto);
            _logger.Info("log payment request");
            _logger.Info(requestJson);

            if (string.IsNullOrWhiteSpace(dto.MerchantName)) throw new RuntimeException("You merchant name is required.");

            if (string.IsNullOrWhiteSpace(dto.ReferenceFlag)) throw new RuntimeException("You reference flag is required.");

            if (dto.PayAmount <= 0) throw new RuntimeException("The amount must be greater than 0");
           
            if (!Enum.IsDefined(typeof(Payway), dto.Payway))
            {
                throw new RuntimeException("Only support credit card or bank");
            }

            await _facade.PaymentService.EasyPay(dto);
            return true;
        }
    }
}
