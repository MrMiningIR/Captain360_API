using Capitan360.Domain.Entities.PackageTypes;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.PackageTypes;

public class PackageTypeConfiguration : BaseEntityConfiguration<PackageType>
{
    public override void Configure(EntityTypeBuilder<PackageType> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.CompanyTypeId)
               .IsRequired();

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

        builder.HasOne(x => x.CompanyType)
               .WithMany(ct => ct.PackageTypes)
               .HasForeignKey(x => x.CompanyTypeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}