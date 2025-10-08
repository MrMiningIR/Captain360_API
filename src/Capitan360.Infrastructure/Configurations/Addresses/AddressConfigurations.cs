using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.Identities;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Addresses;

public class AddressConfigurations : BaseEntityConfiguration<Address>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.CompanyId)
               .IsRequired(false);

        builder.Property(x => x.UserId)
               .IsRequired(false)
               .HasMaxLength(450)
               .IsUnicode()
               .HasColumnType("nvarchar(450)");

        builder.Property(x => x.CountryId)
               .IsRequired();

        builder.Property(x => x.ProvinceId)
               .IsRequired();

        builder.Property(x => x.CityId)
               .IsRequired();

        builder.Property(x => x.MunicipalAreaId)
               .IsRequired();

        builder.Property(x => x.AddressLine)
               .IsRequired()
               .HasMaxLength(1000)
               .IsUnicode()
               .HasColumnType("nvarchar(1000)");

    builder.Property(x => x.Mobile)
              .IsRequired()
              .HasMaxLength(30)
              .IsUnicode(false)
              .HasColumnType("varchar(30)");

        builder.Property(x => x.Tel1)
               .IsRequired()
               .HasMaxLength(30)
               .IsUnicode(false)
               .HasColumnType("varchar(30)");

        builder.Property(x => x.Tel2)
               .IsRequired()
               .HasMaxLength(30)
               .IsUnicode(false)
               .HasColumnType("varchar(30)");

        builder.Property(x => x.Zipcode)
               .IsRequired()
               .HasMaxLength(30)
               .IsUnicode(false)
               .HasColumnType("varchar(30)");

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.Latitude)
               .IsRequired()
               .HasColumnType("decimal(9,6)");

        builder.Property(x => x.Longitude)
               .IsRequired()
               .HasColumnType("decimal(9,6)");

        builder.Property(x => x.Active)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.Order)
               .IsRequired();

        builder.HasOne(x => x.Company)
               .WithMany(c => c.Addresses)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.User)
               .WithMany(c => c.Addresses)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Country)
               .WithMany(co => co.AddressCountries)
               .HasForeignKey(x => x.CountryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Province)
               .WithMany(co => co.AddressProvinces)
               .HasForeignKey(x => x.ProvinceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.City)
                   .WithMany(co => co.AddressCities)
                   .HasForeignKey(x => x.CityId)
                   .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.MunicipalArea)
               .WithMany(co => co.AddressMunicipalAreas)
               .HasForeignKey(x => x.MunicipalAreaId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}