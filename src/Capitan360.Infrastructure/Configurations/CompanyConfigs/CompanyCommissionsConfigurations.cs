using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

internal class CompanyCommissionsConfigurations:IEntityTypeConfiguration<CompanyCommissions>
{
    public void Configure(EntityTypeBuilder<CompanyCommissions> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CommissionFromCaptainCargoWebSite).HasColumnType("decimal(18,2)");
        builder.Property(x => x.CommissionFromCompanyWebSite).HasColumnType("decimal(18,2)");
        builder.Property(x => x.CommissionFromCaptainCargoWebService).HasColumnType("decimal(18,2)");
        builder.Property(x => x.CommissionFromCompanyWebService).HasColumnType("decimal(18,2)");
        builder.Property(x => x.CommissionFromCaptainCargoPanel).HasColumnType("decimal(18,2)");
        builder.Property(x => x.CommissionFromCaptainCargoDesktop).HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.Company)
            .WithOne(x => x.CompanyCommissions)
            .HasForeignKey<CompanyCommissions>(x => x.CompanyId);

    }
}