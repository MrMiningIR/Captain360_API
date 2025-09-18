using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class CompanyConfigurations : BaseEntityConfiguration<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
               .IsRequired()
               .HasMaxLength(10)
               .IsUnicode()
               .HasColumnType("nvarchar(10)");

        builder.Property(x => x.MobileCounter)
               .IsRequired()
               .HasMaxLength(11)
               .IsUnicode()
               .HasColumnType("nvarchar(11)");

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.IsParentCompany)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.Active)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.CompanyTypeId)
               .IsRequired();

        builder.Property(x => x.CountryId)
               .IsRequired();

        builder.Property(x => x.ProvinceId)
               .IsRequired();

        builder.Property(x => x.CityId)
               .IsRequired();

        builder.HasOne(x => x.CompanyType)
               .WithMany(ct => ct.Companies)
               .HasForeignKey(x => x.CompanyTypeId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Country)
               .WithMany(a => a.CompanyCountries)
               .HasForeignKey(x => x.CountryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Province)
               .WithMany(a => a.CompanyProvinces)
               .HasForeignKey(x => x.ProvinceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.City)
               .WithMany(a => a.CompanyCities)
               .HasForeignKey(x => x.CityId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}