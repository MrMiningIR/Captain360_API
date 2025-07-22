using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

public class CompanyDomesticPathChargeContentTypeConfigurations:IEntityTypeConfiguration<CompanyDomesticPathChargeContentType>
{
    public void Configure(EntityTypeBuilder<CompanyDomesticPathChargeContentType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.WeightType).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.CompanyDomesticPathChargeId).IsRequired();
        builder.Property(x => x.CompanyDomesticPathId).IsRequired();

        builder.HasOne(x => x.ContentType)
            .WithMany().HasForeignKey(x => x.ContentTypeId);

        builder.HasOne(x => x.CompanyDomesticPathCharge)
            .WithMany(x => x.CompanyDomesticPathChargeContentTypes)
            .HasForeignKey(x => x.CompanyDomesticPathChargeId)
            .OnDelete(DeleteBehavior.NoAction);

        ;
        builder.HasOne(x => x.CompanyDomesticPaths)
            .WithMany(x => x.CompanyDomesticPathChargeContentTypes)
            .HasForeignKey(x => x.CompanyDomesticPathId)
            .OnDelete(DeleteBehavior.NoAction)

            ;
    }
}