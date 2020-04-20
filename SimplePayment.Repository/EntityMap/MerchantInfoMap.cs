using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SimplePayment.Repository
{
    public class MerchantInfoMap : EntityTypeConfiguration<MerchantInfo>
    {
        public MerchantInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.MerchantId);

            // Properties
            this.Property(t => t.MerchantName)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("MerchantInfo");
            this.Property(t => t.MerchantId).HasColumnName("MerchantId");
            this.Property(t => t.MerchantName).HasColumnName("MerchantName");
        }
    }
}
