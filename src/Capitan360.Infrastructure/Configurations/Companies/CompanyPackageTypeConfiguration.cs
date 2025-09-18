using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class CompanyPackageTypeConfiguration : BaseEntityConfiguration<CompanyPackageType>
{
    public override void Configure(EntityTypeBuilder<CompanyPackageType> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.Active)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.Order)
               .IsRequired();

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PackageTypeId)
               .IsRequired();

        builder.HasOne(x => x.Company)
               .WithMany(c => c.CompanyPackageTypes)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.PackageType)
               .WithMany(p => p.CompanyPackageTypes)
               .HasForeignKey(x => x.PackageTypeId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
