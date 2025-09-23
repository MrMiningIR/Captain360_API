using Capitan360.Domain.Entities.CompanyInsurances;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyInsurances;

public class CompanyInsuranceConfiguration : BaseEntityConfiguration<CompanyInsurance>
{
    public override void Configure(EntityTypeBuilder<CompanyInsurance> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.CompanyId)
               .IsRequired();

        builder.Property(x => x.Code)
               .IsRequired()
               .HasMaxLength(10)
               .IsUnicode()
               .HasColumnType("nvarchar(10)");

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.CaptainCargoCode)
               .IsRequired()
               .HasMaxLength(10)
               .IsUnicode()
               .HasColumnType("nvarchar(10)");

        builder.Property(x => x.Tax)
               .IsRequired()
               .HasColumnType("decimal(5,2)");

        builder.Property(x => x.Scale)
               .IsRequired()
               .HasColumnType("bigint");

        builder.Property(x => x.Active)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.HasOne(x => x.Company)
               .WithMany(c => c.CompanyInsurances)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}