using System.Collections.Generic;

namespace SimplePayment.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SimplePayment.Repository.SimplePaymentContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SimplePayment.Repository.SimplePaymentContext context)
        {
            var listMerchant = new List<MerchantInfo>()
            {
                new MerchantInfo()
                {
                    MerchantName = "alipay"
                },
                new MerchantInfo()
                {
                    MerchantName = "qqpay"
                },
                new MerchantInfo()
                {
                    MerchantName = "weixinpay"
                },
            };

            listMerchant.ForEach(x => context.MerchantInfo.AddOrUpdate(x));

            context.SaveChanges();
        }
    }
}
