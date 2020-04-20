
namespace SimplePayment.Service
{
    public interface ISimplePaymentFacade
    {
        IPaymentService PaymentService { get; set; }
    }

    public class SimplePaymentFacade : ISimplePaymentFacade
    {
        public IPaymentService PaymentService { get; set; }
    }
}
