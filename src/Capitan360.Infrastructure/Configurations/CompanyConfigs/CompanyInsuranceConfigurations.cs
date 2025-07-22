using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

public class CompanyInsuranceConfigurations : IEntityTypeConfiguration<CompanyInsurance>
{
    public void Configure(EntityTypeBuilder<CompanyInsurance> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
        builder.Property(x => x.CompanyId).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Tax).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(x => x.Scale).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(x => x.Active);
        builder.Property(x => x.CompanyTypeId).IsRequired();


        // 1 to n
        builder.HasOne(x => x.Company)
            .WithMany(x => x.CompanyInsurances)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);



        //  1 to 0 : we don't need to set anything in  CompanyType
        builder.HasOne(x => x.CompanyType)
            .WithMany()
            .HasForeignKey(x => x.CompanyTypeId)
            .OnDelete(DeleteBehavior.NoAction);


        // One to n :  CompanyInsurance Vs CompanyInsuranceCharge
        builder
            .HasMany(p => p.CompanyInsuranceCharges)
            .WithOne(p => p.CompanyInsurance)
            .HasForeignKey(p => p.CompanyInsuranceId).IsRequired(false);




    }
}