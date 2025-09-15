using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

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
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50).IsUnicode();

        builder.Property(x => x.Order).HasDefaultValue(0);
        builder.Property(x => x.Active).HasDefaultValue(true);

        builder.HasOne(ca => ca.Company)
            .WithMany(ca => ca.CompanyPackageTypes)
            .HasForeignKey(ca => ca.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ca => ca.PackageType)
            .WithMany(ca => ca.CompanyPackageTypes)
            .HasForeignKey(ca => ca.PackageTypeId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Property(x => x.Description).HasMaxLength(500).IsUnicode();
    }
}