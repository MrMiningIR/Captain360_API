using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

public class CompanyDomesticPathsConfigurations : BaseEntityConfiguration<CompanyDomesticPaths>
{
    public override void Configure(EntityTypeBuilder<CompanyDomesticPaths> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Description).IsRequired().IsUnicode().HasMaxLength(500);
        builder.Property(x => x.DescriptionForSearch).IsRequired().IsUnicode().HasMaxLength(500);

        builder.HasOne(x => x.Company)
            .WithMany(x => x.CompanyDomesticPaths)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.Property(x => x.Active).HasDefaultValue(false);
        builder.Property(x => x.ExitStampBillMinWeightIsFixed).HasDefaultValue(false);
        builder.Property(x => x.ExitPackagingMinWeightIsFixed).HasDefaultValue(false);
        builder.Property(x => x.ExitAccumulationMinWeightIsFixed).HasDefaultValue(false);
        builder.Property(x => x.ExitExtraSourceMinWeightIsFixed).HasDefaultValue(false);
        builder.Property(x => x.ExitPricingMinWeightIsFixed).HasDefaultValue(false);
        builder.Property(x => x.ExitRevenue1MinWeightIsFixed).HasDefaultValue(false);
        builder.Property(x => x.ExitRevenue2MinWeightIsFixed).HasDefaultValue(false);
        builder.Property(x => x.ExitRevenue3MinWeightIsFixed).HasDefaultValue(false);
        builder.Property(x => x.ExitDistributionMinWeightIsFixed).HasDefaultValue(false);
        builder.Property(x => x.ExitExtraDestinationMinWeightIsFixed).HasDefaultValue(false);


        builder.Property(x => x.EntranceFeeWeight).HasColumnType("decimal(10, 2)").HasDefaultValue(0);
        builder.Property(x => x.EntranceFee).HasDefaultValue(0);
        builder.Property(x => x.EntranceFeeType).IsRequired();




        builder.HasOne(a => a.SourceCountry)
            .WithMany()
            .HasForeignKey(a => a.SourceCountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.SourceProvince)
            .WithMany()
            .HasForeignKey(a => a.SourceProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.SourceCity)
            .WithMany()
            .HasForeignKey(a => a.SourceCityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.DestinationCountry)
            .WithMany()
            .HasForeignKey(a => a.DestinationCountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.DestinationProvince)
            .WithMany()
            .HasForeignKey(a => a.DestinationProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.DestinationCity)
            .WithMany()
            .HasForeignKey(a => a.DestinationCityId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}