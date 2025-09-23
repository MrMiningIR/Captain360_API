using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyDomesticPaths;

public class CompanyDomesticPathsConfiguration : BaseEntityConfiguration<CompanyDomesticPath>
{
    public override void Configure(EntityTypeBuilder<CompanyDomesticPath> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.CompanyId)
               .IsRequired();

        builder.Property(x => x.SourceCountryId)
               .IsRequired();

        builder.Property(x => x.SourceProvinceId)
               .IsRequired();

        builder.Property(x => x.SourceCityId)
               .IsRequired();

        builder.Property(x => x.DestinationCountryId)
               .IsRequired();

        builder.Property(x => x.DestinationProvinceId)
               .IsRequired();

        builder.Property(x => x.DestinationCityId)
               .IsRequired();

        builder.Property(x => x.Active)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.DescriptionForSearch)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.EntranceFee)
               .IsRequired()
               .HasColumnType("bigint");

        builder.Property(x => x.EntranceFeeWeight)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

        builder.Property(x => x.EntranceFeeType)
               .IsRequired()
               .HasColumnType("smallint");

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

        builder.Property(x => x.ExitDistributionMinWeightIsFixed)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.ExitExtraDestinationMinWeightIsFixed)
               .IsRequired()
               .HasColumnType("bit");

        builder.HasOne(x => x.Company)
               .WithMany(c => c.CompanyDomesticPaths)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceCountry)
               .WithMany(a => a.CompanyDomesticPathSourceCountries)
               .HasForeignKey(x => x.SourceCountryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceProvince)
               .WithMany(a => a.CompanyDomesticPathSourceProvinces)
               .HasForeignKey(x => x.SourceProvinceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceCity)
               .WithMany(a => a.CompanyDomesticPathSourceCities)
               .HasForeignKey(x => x.SourceCityId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationCountry)
               .WithMany(a => a.CompanyDomesticPathDestinationCountries)
               .HasForeignKey(x => x.DestinationCountryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationProvince)
               .WithMany(a => a.CompanyDomesticPathDestinationProvinces)
               .HasForeignKey(x => x.DestinationProvinceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationCity)
               .WithMany(a => a.CompanyDomesticPathDestinationCities)
               .HasForeignKey(x => x.DestinationCityId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}