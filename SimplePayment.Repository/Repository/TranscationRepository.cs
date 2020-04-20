namespace SimplePayment.Repository
{
    public interface ITranscationRepository : IGenericRepository<TranscationInfo>
    {
        
    }

    public class TranscationRepository : GenericRepository<TranscationInfo>, ITranscationRepository
    {
        public TranscationRepository(SimplePaymentContext context)
            : base(context)
        {
        }
    }
}
