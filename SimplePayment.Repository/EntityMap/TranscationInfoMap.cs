using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SimplePayment.Repository
{
    public class TranscationInfoMap : EntityTypeConfiguration<TranscationInfo>
    {
        public TranscationInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.TranscationId);

            // Properties
            this.Property(t => t.ReferenceFlag)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TranscationInfo");
            this.Property(t => t.TranscationId).HasColumnName("TranscationId");
            this.Property(t => t.MerchantId).HasColumnName("MerchantId");
            this.Property(t => t.MerchantName).HasColumnName("MerchantName");
            this.Property(t => t.ReferenceFlag).HasColumnName("ReferenceFlag");
            this.Property(t => t.PayAmount).HasColumnName("PayAmount");
            this.Property(t => t.Payway).HasColumnName("Payway");
            this.Property(t => t.PayTime).HasColumnName("PayTime");

            // Relationships
            this.HasRequired(t => t.MerchantInfo)
                .WithMany(t => t.TranscationInfoes)
                .HasForeignKey(d => d.MerchantId);

        }
    }
}
