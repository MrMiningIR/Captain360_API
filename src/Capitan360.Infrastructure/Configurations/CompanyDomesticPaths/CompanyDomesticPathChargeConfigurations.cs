using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyDomesticPaths;

public class CompanyDomesticPathChargeConfigurations : BaseEntityConfiguration<CompanyDomesticPathCharge>
{
    public override void Configure(EntityTypeBuilder<CompanyDomesticPathCharge> builder)
    {

        base.Configure(builder);
        builder.Property(x => x.Weight).IsRequired();
        builder.Property(x => x.WeightType).IsRequired();
        builder.Property(x => x.PriceDirect).IsRequired();
        builder.Property(x => x.ContentTypeChargeBaseNormal).HasDefaultValue(false);
        builder.Property(x => x.ContentTypeId).HasDefaultValue(0);

        builder.HasOne(x => x.CompanyDomesticPaths)
            .WithMany(x => x.CompanyDomesticPathCharges)
            .HasForeignKey(x => x.CompanyDomesticPathId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}