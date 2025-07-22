using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

public class CompanyDomesticPathStructPricesConfigurations : IEntityTypeConfiguration<CompanyDomesticPathStructPrices>
{
    public void Configure(EntityTypeBuilder<CompanyDomesticPathStructPrices> builder)
    {
        builder.HasKey(x => x.Id);
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