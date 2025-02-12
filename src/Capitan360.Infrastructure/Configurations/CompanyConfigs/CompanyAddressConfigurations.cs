using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

internal class CompanyAddressConfigurations : IEntityTypeConfiguration<CompanyAddress>
{
    public void Configure(EntityTypeBuilder<CompanyAddress> builder)
    {

        builder.HasKey(ca => ca.Id);

        builder.Property(ca => ca.Active).HasDefaultValue(true);
        builder.Property(ca => ca.OrderAddress).IsRequired();
        builder.Property(ca => ca.CaptainCargoName).HasMaxLength(30);
        builder.Property(ca => ca.CaptainCargoCode).HasMaxLength(30);


        builder.HasOne(ca => ca.Company)
               .WithMany(ca => ca.CompanyAddresses)
               .HasForeignKey(ca => ca.CompanyId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ca=>ca.Address)
                .WithMany(ca=>ca.CompanyAddresses)
                .HasForeignKey(ca => ca.AddressId)
                .OnDelete(DeleteBehavior.NoAction);


    }
}