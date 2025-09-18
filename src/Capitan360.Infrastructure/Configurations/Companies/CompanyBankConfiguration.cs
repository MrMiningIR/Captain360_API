using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class CompanyBankConfiguration : BaseEntityConfiguration<CompanyBank>
{
    public override void Configure(EntityTypeBuilder<CompanyBank> builder)
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

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.Active)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.Order)
               .IsRequired();

        builder.HasOne(x => x.Company)
               .WithMany(c => c.CompanyBanks)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
