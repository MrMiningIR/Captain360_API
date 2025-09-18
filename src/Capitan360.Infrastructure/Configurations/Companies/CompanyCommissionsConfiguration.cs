using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class CompanyCommissionsConfiguration : BaseEntityConfiguration<CompanyCommissions>
{
    public override void Configure(EntityTypeBuilder<CompanyCommissions> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.CompanyId)
               .IsRequired();

        builder.Property(x => x.CommissionFromCaptainCargoWebSite)
               .IsRequired()
               .HasColumnType("bigint");

        builder.Property(x => x.CommissionFromCompanyWebSite)
               .IsRequired()
               .HasColumnType("bigint");

        builder.Property(x => x.CommissionFromCaptainCargoWebService)
               .IsRequired()
               .HasColumnType("bigint");

        builder.Property(x => x.CommissionFromCompanyWebService)
               .IsRequired()
               .HasColumnType("bigint");

        builder.Property(x => x.CommissionFromCaptainCargoPanel)
               .IsRequired()
               .HasColumnType("bigint");

        builder.Property(x => x.CommissionFromCaptainCargoDesktop)
               .IsRequired()
               .HasColumnType("bigint");

        builder.HasOne(x => x.Company)
               .WithOne(c => c.CompanyCommissions)
               .HasForeignKey<CompanyCommissions>(x => x.CompanyId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}