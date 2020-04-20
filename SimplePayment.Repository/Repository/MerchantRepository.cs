using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePayment.Repository
{
    public interface IMerchantRepository : IGenericRepository<MerchantInfo>
    {
    }

    public class MerchantRepository : GenericRepository<MerchantInfo>, IMerchantRepository
    {
        public MerchantRepository(SimplePaymentContext context)
            : base(context)
        {
        }
    }
}
