using Capitan360.Domain.Entities.CompanyInsurances;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyInsurances;

public class CompanyInsuranceChargePaymentConfigurations : BaseEntityConfiguration<CompanyInsuranceChargePayment>
{
    public override void Configure(EntityTypeBuilder<CompanyInsuranceChargePayment> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.CompanyInsuranceChargeId).IsRequired();

        builder.Property(x => x.Diff).HasColumnType("decimal(18,2)").IsRequired();

        builder.Property(x => x.IsPercent);
        builder.Property(x => x.Rate).IsRequired();

        builder.HasOne(x => x.CompanyInsuranceCharge)
            .WithMany(x => x.CompanyInsuranceChargePayments).
            HasForeignKey(x => x.CompanyInsuranceChargeId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}