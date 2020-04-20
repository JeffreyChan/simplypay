using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplePayment.Repository;

namespace SimplePayment.Service
{
    public interface IPaymentService : IEntityService<TranscationInfo>
    { 
        Task<bool> EasyPay(MerchantInfoDto merchantDto);
    }

    public class PaymentService : EntityService<TranscationInfo>, IPaymentService
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly ITranscationRepository _transcationRepository;

        public PaymentService(IUnitOfWork unitOfWork, 
            ITranscationRepository transcationRepository,IMerchantRepository merchantRepository)
            : base(unitOfWork, transcationRepository)
        {
            this._merchantRepository = merchantRepository;
            this._transcationRepository = transcationRepository;
        }

        public async Task<bool> EasyPay(MerchantInfoDto merchantDto)
        {
            var merchantDb = await this._merchantRepository.Get(x =>
                x.MerchantName.Equals(merchantDto.MerchantName, StringComparison.CurrentCultureIgnoreCase));

            if (merchantDb == null) throw new RuntimeException("Could not find business information");

            
            var referenceFlag = await this._transcationRepository.Get(x =>
                x.ReferenceFlag.Equals(merchantDto.ReferenceFlag, StringComparison.InvariantCultureIgnoreCase));

            if (referenceFlag != null) throw new RuntimeException("You have duplicate reference, please try it again");

            using (var scope = TransactionUtilities.CreateTransactionScopeAsync())
            {
                var transaction = new TranscationInfo
                {
                    MerchantId = merchantDb.Id,
                    MerchantName = merchantDb.MerchantName,
                    ReferenceFlag = merchantDto.ReferenceFlag,
                    PayAmount = merchantDto.PayAmount,
                    Payway = (Payway)merchantDto.Payway,
                    PayTime = DateTime.Now
                };

                await AddOrUpdate(transaction);

                scope.Complete();
            }

            return true;
        }
    }
}
