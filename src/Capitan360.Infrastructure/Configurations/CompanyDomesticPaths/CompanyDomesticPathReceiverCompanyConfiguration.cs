using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Capitan360.Domain.Entities.CompanyDomesticPaths;

namespace Capitan360.Infrastructure.Configurations.CompanyDomesticPaths;

public class CompanyDomesticPathReceiverCompanyConfiguration : BaseEntityConfiguration<CompanyDomesticPathReceiverCompany>
{
    public override void Configure(EntityTypeBuilder<CompanyDomesticPathReceiverCompany> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.ReceiverCompanyUserInsertedCode)
               .IsRequired(false)
               .HasMaxLength(10)
               .IsUnicode()
               .HasColumnType("nvarchar(10)");

        builder.Property(x => x.ReceiverCompanyUserInsertedName)
               .IsRequired(false)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.ReceiverCompanyUserInsertedTelephone)
               .IsRequired(false)
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.ReceiverCompanyUserInsertedAddress)
               .IsRequired(false)
               .HasMaxLength(200)
               .IsUnicode()
               .HasColumnType("nvarchar(200)");

        builder.Property(x => x.DescriptionForPrint1)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.DescriptionForPrint2)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.DescriptionForPrint3)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.HasOne(e => e.CompanyDomesticPath)
               .WithMany(p => p.CompanyDomesticPathReceiverCompanies) 
               .HasForeignKey(e => e.CompanyDomesticPathId)
               .OnDelete(DeleteBehavior.NoAction); 

        builder.HasOne(e => e.MunicipalArea)
               .WithMany(a => a.CompanyDomesticPathReceiverCompanies)
               .HasForeignKey(e => e.MunicipalAreaId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.ReceiverCompany)
               .WithMany(c => c.CompanyDomesticPathReceiverCompanies)
               .HasForeignKey(e => e.ReceiverCompanyId)
               .OnDelete(DeleteBehavior.NoAction); 
    }
}
