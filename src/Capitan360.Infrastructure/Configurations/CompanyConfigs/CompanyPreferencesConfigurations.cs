using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs
{
    internal class CompanyPreferencesConfigurations: IEntityTypeConfiguration<CompanyPreferences>
    {
        public void Configure(EntityTypeBuilder<CompanyPreferences> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Admin).HasDefaultValue(false);
            builder.Property(x => x.Type).IsRequired();
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
