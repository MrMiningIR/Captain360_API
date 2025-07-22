using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

public class CompanyDomesticPathStructPriceMunicipalAreasConfigurations : IEntityTypeConfiguration<CompanyDomesticPathStructPriceMunicipalAreas>
{
    public void Configure(EntityTypeBuilder<CompanyDomesticPathStructPriceMunicipalAreas> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.WeightType).IsRequired();
        builder.Property(x => x.PathStructType).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Static).HasDefaultValue(false);
        builder.HasOne(x => x.Area)
            .WithMany().HasForeignKey(x => x.MunicipalAreaId);

        builder.HasOne(x => x.CompanyDomesticPathStructPrice)
            .WithMany(x => x.CompanyDomesticPathStructPriceMunicipalAreas)
            .HasForeignKey(x => x.CompanyDomesticPathStructPriceId)
            .OnDelete(DeleteBehavior.NoAction);

        ;
        builder.HasOne(x => x.CompanyDomesticPaths)
            .WithMany(x => x.CompanyDomesticPathStructPriceMunicipalAreas)
            .HasForeignKey(x => x.CompanyDomesticPathId)
            .OnDelete(DeleteBehavior.NoAction)

            ;


    }
}