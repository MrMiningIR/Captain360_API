using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class CompanyInsuranceChargeConfigurations : BaseEntityConfiguration<CompanyInsuranceCharge>
{
    public override void Configure(EntityTypeBuilder<CompanyInsuranceCharge> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Rate).IsRequired();
        builder.Property(x => x.Value).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(x => x.Settlement).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(x => x.IsPercent);
        builder.Property(x => x.Static);
        builder.Property(x => x.CompanyInsuranceId).IsRequired();




        //  1 to N  : CompanyInsuranceCharge vs  CompanyInsuranceChargePaymentContentTypes
        builder.HasMany(x => x.CompanyInsuranceChargePaymentContentTypes)
            .WithOne(x => x.CompanyInsuranceCharge)
            .HasForeignKey(x => x.CompanyInsuranceChargeId).OnDelete(DeleteBehavior.NoAction);

        // 1 to  1   CompanyInsuranceCharge  vs   CompanyInsuranceChargePayment 
        builder
            .HasMany(p => p.CompanyInsuranceChargePayments)
            .WithOne(p => p.CompanyInsuranceCharge)
            .HasForeignKey(p => p.CompanyInsuranceChargeId)
            .OnDelete(DeleteBehavior.NoAction);



    }
}