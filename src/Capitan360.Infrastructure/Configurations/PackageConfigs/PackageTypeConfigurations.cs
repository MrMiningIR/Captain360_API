using Capitan360.Domain.Entities.PackageEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Configurations.PackageConfigs;


public class PackageTypeConfigurations : IEntityTypeConfiguration<PackageType>
{


    public void Configure(EntityTypeBuilder<PackageType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PackageTypeName).IsRequired().HasMaxLength(50).IsUnicode();
        builder.Property(x => x.PackageTypeDescription).IsRequired().HasMaxLength(50).IsUnicode();
        builder.Property(x => x.OrderPackageType).HasDefaultValue(0);
        builder.Property(x => x.Active).HasDefaultValue(true);

        builder.HasIndex(x => x.PackageTypeName);
        builder.HasIndex(x => x.OrderPackageType);
    }
}
