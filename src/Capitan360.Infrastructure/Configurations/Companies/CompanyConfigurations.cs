using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

public class CompanyConfigurations : BaseEntityConfiguration<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.MobileCounter).IsRequired().HasMaxLength(11);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Code).IsRequired().HasMaxLength(20);
        builder.Property(x => x.CompanyTypeId).IsRequired();
        builder.Property(c => c.Active).HasDefaultValue(true);
        builder.Property(c => c.Description).HasMaxLength(500).IsUnicode();
        builder.Property(x => x.IsParentCompany).HasDefaultValue(false);

        builder.HasMany(c => c.UserCompanies)
            .WithOne(uc => uc.Company)
            .HasForeignKey(uc => uc.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(c => c.CompanyUris)
            .WithOne(ur => ur.Company)
            .HasForeignKey(ur => ur.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(c => c.CompanyCommissions)
            .WithOne(c => c.Company)
            .HasForeignKey<CompanyCommissions>(c => c.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(c => c.CompanyPreferences)
            .WithOne(c => c.Company)
            .HasForeignKey<CompanyPreferences>(c => c.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(c => c.CompanySmsPatterns)
            .WithOne(c => c.Company)
            .HasForeignKey<CompanySmsPatterns>(c => c.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

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