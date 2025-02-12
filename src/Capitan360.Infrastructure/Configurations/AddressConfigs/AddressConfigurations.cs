using Capitan360.Domain.Entities.AddressEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.AddressConfigs;

internal class AddressConfigurations : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {

        builder.HasKey(a => a.Id);

        builder.Property(a => a.AddressLine).IsRequired().HasMaxLength(500).IsUnicode();
        builder.Property(a => a.Mobile).HasMaxLength(11).IsUnicode(false);
        builder.Property(a => a.Tel1).HasMaxLength(11).IsUnicode(false);
        builder.Property(a => a.Tel2).HasMaxLength(11).IsUnicode(false);
        builder.Property(a => a.Zipcode).HasMaxLength(5).IsUnicode(false);
        builder.Property(a => a.Description).HasMaxLength(500).IsUnicode();
        builder.Property(a => a.Coordinates).HasColumnType("geography");

        builder.HasOne(a => a.Country)
            .WithMany()
            .HasForeignKey(a => a.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Province)
            .WithMany()
            .HasForeignKey(a => a.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.City)
            .WithMany()
            .HasForeignKey(a => a.CityId)
            .OnDelete(DeleteBehavior.Restrict);



    }
}