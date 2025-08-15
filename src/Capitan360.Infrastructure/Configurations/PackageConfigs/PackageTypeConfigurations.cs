using Capitan360.Domain.Entities.PackageEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.PackageConfigs;


public class PackageTypeConfigurations : BaseEntityConfiguration<PackageType>
{


    public override void Configure(EntityTypeBuilder<PackageType> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.PackageTypeName).IsRequired().HasMaxLength(50).IsUnicode();
        builder.Property(x => x.PackageTypeDescription).HasMaxLength(500).IsUnicode();
        builder.Property(x => x.PackageTypeOrder).HasDefaultValue(0);
        builder.Property(x => x.PackageTypeActive).HasDefaultValue(true);

        builder.HasIndex(x => x.PackageTypeName);
        builder.HasIndex(x => x.PackageTypeOrder);
    }
}
