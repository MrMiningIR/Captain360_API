using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyDomesticPaths;

public class CompanyDomesticPathStructPricesConfigurations : BaseEntityConfiguration<CompanyDomesticPathStructPrice>
{
    public override void Configure(EntityTypeBuilder<CompanyDomesticPathStructPrice> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Weight).IsRequired();
        builder.Property(x => x.PathStructType).IsRequired();
        builder.Property(x => x.WeightType).IsRequired();
        builder.Property(x => x.MunicipalAreaId).HasDefaultValue(0);

        builder.HasOne(x => x.CompanyDomesticPaths)
            .WithMany(x => x.CompanyDomesticPathStructPrices)
            .HasForeignKey(x => x.CompanyDomesticPathId)
            .OnDelete(DeleteBehavior.NoAction)
            ;

    }
}