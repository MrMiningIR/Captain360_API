using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class CompanyDomesticPathStructPriceMunicipalAreasConfigurations : BaseEntityConfiguration<CompanyDomesticPathStructPriceMunicipalArea>
{
    public override void Configure(EntityTypeBuilder<CompanyDomesticPathStructPriceMunicipalArea> builder)
    {
        base.Configure(builder);
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