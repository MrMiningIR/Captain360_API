using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

public class CompanyCommissionsConfigurations : BaseEntityConfiguration<CompanyCommissions>
{
    public override void Configure(EntityTypeBuilder<CompanyCommissions> builder)
    {
        base.Configure(builder);

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