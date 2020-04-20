using System.Collections.Generic;

namespace SimplePayment
{
    public partial class MerchantInfo : Entity<int>
    {
        public override int Id
        {
            get { return MerchantId; }
        }
        public MerchantInfo()
        {
            this.TranscationInfoes = new List<TranscationInfo>();
        }

        public int MerchantId { get; set; }
        public string MerchantName { get; set; }
        public virtual ICollection<TranscationInfo> TranscationInfoes { get; set; }
    }
}
