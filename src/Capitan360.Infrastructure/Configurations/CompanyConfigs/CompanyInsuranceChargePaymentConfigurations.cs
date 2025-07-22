using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

public class CompanyInsuranceChargePaymentConfigurations : IEntityTypeConfiguration<CompanyInsuranceChargePayment>
{
    public void Configure(EntityTypeBuilder<CompanyInsuranceChargePayment> builder)
    {
        builder.HasKey(x => x.Id);
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