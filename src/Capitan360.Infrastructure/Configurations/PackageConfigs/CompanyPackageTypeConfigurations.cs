using Capitan360.Domain.Entities.PackageEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.PackageConfigs;

public class CompanyPackageTypeConfigurations : IEntityTypeConfiguration<CompanyPackageType>
{
    public void Configure(EntityTypeBuilder<CompanyPackageType> builder)
    {

        builder
             .HasIndex(x => new { x.CompanyId, x.PackageTypeId })
             .HasDatabaseName("IX_CompanyPackageType_Active")
             .IsUnique()
             .HasFilter("[Deleted] = 0");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PackageTypeName).IsRequired().HasMaxLength(50).IsUnicode();

        builder.Property(x => x.OrderPackageType).HasDefaultValue(0);
        builder.Property(x => x.Active).HasDefaultValue(true);

        builder.HasOne(ca => ca.Company)
            .WithMany(ca => ca.CompanyPackageTypes)
            .HasForeignKey(ca => ca.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ca => ca.PackageType)
            .WithMany(ca => ca.CompanyPackageTypes)
            .HasForeignKey(ca => ca.PackageTypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}