using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

internal class CompanyConfigurations : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Code).IsRequired().HasMaxLength(50);
        builder.Property(c => c.EconomicCode).IsRequired().HasMaxLength(50);
        builder.Property(c => c.NationalId).IsRequired().HasMaxLength(50);
        builder.Property(c => c.RegistrationId).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Tax).HasColumnType("decimal(5, 2)");
        builder.Property(c => c.Active).HasDefaultValue(true);
        builder.Property(c => c.Description).HasMaxLength(500).IsUnicode();




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







    }
}