using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePayment.Repository
{
    public partial class SimplePaymentContext : DbContext
    {
        //static SimplePaymentContext()
        //{
        //    Database.SetInitializer<SimplePaymentContext>(null);
        //}

        public SimplePaymentContext()
            : base("SimplePaymentContext")
        {
        }

        public DbSet<MerchantInfo> MerchantInfo { get; set; }
        public DbSet<TranscationInfo> TranscationInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MerchantInfoMap());
            modelBuilder.Configurations.Add(new TranscationInfoMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
