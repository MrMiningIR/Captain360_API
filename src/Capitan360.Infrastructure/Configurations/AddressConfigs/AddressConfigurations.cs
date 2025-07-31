using Capitan360.Domain.Entities.AddressEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.AddressConfigs;

internal class AddressConfigurations : BaseEntityConfiguration<Address>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        base.Configure(builder);
        builder.HasKey(a => a.Id);

        builder.Property(a => a.AddressLine).IsRequired().HasMaxLength(200).IsUnicode();
        builder.Property(a => a.Mobile).HasMaxLength(11).IsUnicode(false);
        builder.Property(a => a.Tel1).HasMaxLength(11).IsUnicode(false);
        builder.Property(a => a.Tel2).HasMaxLength(11).IsUnicode(false);
        builder.Property(a => a.Zipcode).HasMaxLength(10).IsUnicode(false);
        builder.Property(a => a.Description).HasMaxLength(200).IsUnicode();
        builder.Property(a => a.Active).HasDefaultValue(false);
        builder.Property(a => a.OrderAddress).HasDefaultValue(0);
        builder.Property(a => a.Latitude).HasDefaultValue(0);
        builder.Property(a => a.Longitude).HasDefaultValue(0);

        builder.HasOne(a => a.Company)
            .WithMany(c => c.Addresses)
            .HasForeignKey(a => a.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);




    }
}