using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class CompanyDomesticPathStructPricesConfigurations : BaseEntityConfiguration<CompanyDomesticPathStructPrices>
{
    public override void Configure(EntityTypeBuilder<CompanyDomesticPathStructPrices> builder)
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