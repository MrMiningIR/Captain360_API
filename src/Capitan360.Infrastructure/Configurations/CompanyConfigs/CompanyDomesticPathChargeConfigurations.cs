using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

public class CompanyDomesticPathChargeConfigurations : IEntityTypeConfiguration<CompanyDomesticPathCharge>
{
    public void Configure(EntityTypeBuilder<CompanyDomesticPathCharge> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Weight).IsRequired();
        builder.Property(x => x.WeightType).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.ContentTypeChargeBaseNormal).HasDefaultValue(false);
        builder.Property(x => x.ContentTypeId).HasDefaultValue(0);

        builder.HasOne(x => x.CompanyDomesticPaths)
            .WithMany(x => x.CompanyDomesticPathCharges)
            .HasForeignKey(x => x.CompanyDomesticPathId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}