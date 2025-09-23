using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class CompanyPreferencesConfiguration : BaseEntityConfiguration<CompanyPreferences>
{
    public override void Configure(EntityTypeBuilder<CompanyPreferences> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.CompanyId)
               .IsRequired();

        builder.Property(x => x.EconomicCode)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.NationalId)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.RegistrationId)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.CaptainCargoName)
               .IsRequired()
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.CaptainCargoCode)
               .IsRequired()
               .HasMaxLength(10)
               .IsUnicode()
               .HasColumnType("nvarchar(10)");

        builder.Property(x => x.ActiveIssueDomesticWaybill)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ActiveShowInSearchEngine)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ActiveInWebServiceSearchEngine)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ActiveInternationalAirlineCargo)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitStampBillMinWeightIsFixed)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitPackagingMinWeightIsFixed)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitAccumulationMinWeightIsFixed)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitExtraSourceMinWeightIsFixed)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitPricingMinWeightIsFixed)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitRevenue1MinWeightIsFixed)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitRevenue2MinWeightIsFixed)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitRevenue3MinWeightIsFixed)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.Tax)
               .IsRequired()
               .HasColumnType("decimal(5,2)");

        builder.Property(x => x.ExitFareInTax)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitStampBillInTax)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitPackagingInTax)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitAccumulationInTax)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitExtraSourceInTax)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitPricingInTax)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitRevenue1InTax)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitRevenue2InTax)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitRevenue3InTax)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitDistributionInTax)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitExtraDestinationInTax)
               .IsRequired()
               .HasColumnType("bit");

        builder.HasOne(x => x.Company)
               .WithOne(c => c.CompanyPreferences)
               .HasForeignKey<CompanyPreferences>(x => x.CompanyId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
