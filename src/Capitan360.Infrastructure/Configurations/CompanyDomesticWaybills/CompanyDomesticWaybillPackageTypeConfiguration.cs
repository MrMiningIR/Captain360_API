using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Enums;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyDomesticWaybills;

public class CompanyDomesticWaybillPackageTypeConfiguration : BaseEntityConfiguration<CompanyDomesticWaybillPackageType>
{
    public override void Configure(EntityTypeBuilder<CompanyDomesticWaybillPackageType> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.CompanyDomesticWaybillId)
               .IsRequired();

        builder.Property(x => x.CompanyPackageTypeId)
               .IsRequired();

        builder.Property(x => x.CompanyContentTypeId)
               .IsRequired();

        builder.Property(x => x.UserInsertedContentName)
               .IsRequired(false)
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.GrossWeight)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

        builder.Property(x => x.DimensionalWeight)
               .IsRequired(false)
               .HasColumnType("decimal(10,2)");

        builder.Property(x => x.DeclaredValue)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.Dimensions)
               .IsRequired(false)
               .HasColumnType("char(14)")
               .IsUnicode(false);

        builder.Property(x => x.CountDimension)
               .IsRequired();

        builder.HasOne(x => x.CompanyDomesticWaybill)
               .WithMany(wb => wb.CompanyDomesticWaybillPackageTypes)
               .HasForeignKey(x => x.CompanyDomesticWaybillId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyPackageType)
               .WithMany(cpt => cpt.CompanyDomesticWaybillPackageTypes)
               .HasForeignKey(x => x.CompanyPackageTypeId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyContentType)
               .WithMany(cct => cct.CompanyDomesticWaybillPackageTypes)
               .HasForeignKey(x => x.CompanyContentTypeId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}