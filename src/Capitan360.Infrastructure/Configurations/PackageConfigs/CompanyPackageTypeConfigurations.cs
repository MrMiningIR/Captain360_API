using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.PackageConfigs;

public class CompanyPackageTypeConfigurations : BaseEntityConfiguration<CompanyPackageType>
{
    public override void Configure(EntityTypeBuilder<CompanyPackageType> builder)
    {

        base.Configure(builder);
        builder
             .HasIndex(x => new { x.CompanyId, x.PackageTypeId })
             .HasDatabaseName("IX_CompanyPackageType_Active")
             .IsUnique()
             .HasFilter("[Deleted] = 0");
        builder.Property(x => x.CompanyPackageTypeName).IsRequired().HasMaxLength(50).IsUnicode();

        builder.Property(x => x.CompanyPackageTypeOrder).HasDefaultValue(0);
        builder.Property(x => x.CompanyPackageTypeActive).HasDefaultValue(true);

        builder.HasOne(ca => ca.Company)
            .WithMany(ca => ca.CompanyPackageTypes)
            .HasForeignKey(ca => ca.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ca => ca.PackageType)
            .WithMany(ca => ca.CompanyPackageTypes)
            .HasForeignKey(ca => ca.PackageTypeId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Property(x => x.CompanyPackageTypeDescription).HasMaxLength(500).IsUnicode();
    }
}