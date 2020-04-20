using System;

namespace SimplePayment
{
    public partial class TranscationInfo : Entity<int>
    {
        public override int Id
        {
            get { return TranscationId; }
        }
        public int TranscationId { get; set; }
        public int MerchantId { get; set; }

        public string MerchantName { get; set; }
        public string ReferenceFlag { get; set; }
        public decimal PayAmount { get; set; }
        public Payway Payway { get; set; }
        public DateTime PayTime { get; set; }
        public virtual MerchantInfo MerchantInfo { get; set; }
    }
}