using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

public class CompanyInsuranceChargePaymentContentTypeConfigurations:IEntityTypeConfiguration<CompanyInsuranceChargePaymentContentType>
{
    public void Configure(EntityTypeBuilder<CompanyInsuranceChargePaymentContentType> builder)
    {
        builder.HasKey(x => x.Id);
       builder.Property(x => x.RateSettlement).HasColumnType("decimal(18,2)").IsRequired();
       builder.Property(x => x.RateDiff).HasColumnType("decimal(18,2)").IsRequired();
       builder.Property(x => x.IsPercentDiff);
       builder.Property(x => x.IsPercentRateSettlement);
       builder.Property(x => x.ContentId).IsRequired();
       builder.Property(x => x.CompanyInsuranceChargeId).IsRequired();
       builder.Property(x => x.Rate).IsRequired();


       builder.HasOne(x => x.CompanyInsuranceCharge)
           .WithMany(x => x.CompanyInsuranceChargePaymentContentTypes)
           .HasForeignKey(x => x.CompanyInsuranceChargeId).OnDelete(DeleteBehavior.NoAction);;


        //builder.HasOne(x => x.ContentType)
        //    .WithOne()
        //    .HasForeignKey<CompanyInsuranceChargePaymentContentType>(x => x.ContentId)
        //    .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne(x => x.ContentType)
    .WithMany().HasForeignKey(x => x.ContentId).OnDelete(DeleteBehavior.NoAction); 
    }
}