using Moq;
using System.Threading.Tasks;
using SimplePayment.Service;
using Xunit;

namespace SimplePayment.Test
{
    public class MerchantServiceTest
    {
        [Fact]
        public async  Task EasyPayPassingTest()
        {
            var payInfo = new MerchantInfoDto();

            var paymentServiceMock = new Mock<IPaymentService>();

            paymentServiceMock.Setup(x => x.EasyPay(It.IsAny<MerchantInfoDto>())).ReturnsAsync(true);

            var service = new DataSourceExtensionService(paymentServiceMock.Object);

            var result = await service.EasyPay(payInfo);

            Assert.True(result);
        }
    }
}
