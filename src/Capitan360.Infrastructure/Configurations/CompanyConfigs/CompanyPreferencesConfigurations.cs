using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs
{
    internal class CompanyPreferencesConfigurations : BaseEntityConfiguration<CompanyPreferences>
    {
        public override void Configure(EntityTypeBuilder<CompanyPreferences> builder)
        {
            base.Configure(builder);
            builder.Property(ca => ca.CaptainCargoName).HasMaxLength(30);
            builder.Property(ca => ca.CaptainCargoCode).HasMaxLength(30);
            builder.Property(c => c.EconomicCode).HasMaxLength(50);
            builder.Property(c => c.NationalId).HasMaxLength(50);
            builder.Property(c => c.RegistrationId).HasMaxLength(50);
            builder.Property(c => c.Tax).HasColumnType("decimal(5, 2)");
            builder.Property(x => x.ActiveIssueDomesticWaybill).HasDefaultValue(false);
            builder.Property(x => x.ActiveShowInSearchEngine).HasDefaultValue(false);
            builder.Property(x => x.ActiveInWebServiceSearchEngine).HasDefaultValue(false);
            builder.Property(x => x.ActiveInternationalAirlineCargo).HasDefaultValue(false);
            builder.Property(x => x.ExitStampBillMinWeightIsFixed).HasDefaultValue(false);
            builder.Property(x => x.ExitPackagingMinWeightIsFixed).HasDefaultValue(false);
            builder.Property(x => x.ExitAccumulationMinWeightIsFixed).HasDefaultValue(false);
            builder.Property(x => x.ExitExtraSourceMinWeightIsFixed).HasDefaultValue(false);
            builder.Property(x => x.ExitPricingMinWeightIsFixed).HasDefaultValue(false);
            builder.Property(x => x.ExitRevenue1MinWeightIsFixed).HasDefaultValue(false);
            builder.Property(x => x.ExitRevenue2MinWeightIsFixed).HasDefaultValue(false);
            builder.Property(x => x.ExitRevenue3MinWeightIsFixed).HasDefaultValue(false);
            builder.Property(x => x.ExitFareInTax).HasDefaultValue(false);
            builder.Property(x => x.ExitStampBillInTax).HasDefaultValue(false);
            builder.Property(x => x.ExitPackagingInTax).HasDefaultValue(false);
            builder.Property(x => x.ExitAccumulationInTax).HasDefaultValue(false);
            builder.Property(x => x.ExitExtraSourceInTax).HasDefaultValue(false);
            builder.Property(x => x.ExitPricingInTax).HasDefaultValue(false);
            builder.Property(x => x.ExitRevenue1InTax).HasDefaultValue(false);
            builder.Property(x => x.ExitRevenue2InTax).HasDefaultValue(false);
            builder.Property(x => x.ExitRevenue3InTax).HasDefaultValue(false);
            builder.Property(x => x.ExitDistributionInTax).HasDefaultValue(false);
            builder.Property(x => x.ExitExtraDestinationInTax).HasDefaultValue(false);

            builder.HasOne(x => x.Company)
                .WithOne(x => x.CompanyPreferences)
                .HasForeignKey<CompanyPreferences>(x => x.CompanyId);
        }
    }
}
