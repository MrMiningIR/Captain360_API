using Capitan360.Domain.Entities.DomesticWaybills;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.DomesticWaybills;

public class DomesticWaybillPackageTypeConfiguration : BaseEntityConfiguration<DomesticWaybillPackageType>
{
    public override void Configure(EntityTypeBuilder<DomesticWaybillPackageType> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.DomesticWaybillId)
               .IsRequired();

        builder.Property(x => x.CompanyPackageTypeId)
               .IsRequired();

        builder.Property(x => x.CompanyContentTypeId)
               .IsRequired();

        builder.Property(x => x.UserInsertedContentName)
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.GrossWeight)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

        builder.Property(x => x.ChargeableWeight)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

        builder.Property(x => x.DeclaredValue)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.Dimensions)
               .HasColumnType("char(14)")
               .IsUnicode(false);

        builder.Property(x => x.CountDimension)
               .IsRequired();

        builder.HasOne(x => x.DomesticWaybill)
               .WithMany(wb => wb.DomesticWaybillPackageTypes)
               .HasForeignKey(x => x.DomesticWaybillId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyPackageType)
               .WithMany(cpt => cpt.DomesticWaybillPackageTypes)
               .HasForeignKey(x => x.CompanyPackageTypeId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyContentType)
               .WithMany(cct => cct.DomesticWaybillPackageTypes)
               .HasForeignKey(x => x.CompanyContentTypeId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}