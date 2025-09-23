using Capitan360.Domain.Entities.CompanyManifestForms;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyManifestForms;

public class CompanyManifestFormPeriodConfiguration : BaseEntityConfiguration<CompanyManifestFormPeriod>
{
    public override void Configure(EntityTypeBuilder<CompanyManifestFormPeriod> builder)
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

        builder.Property(x => x.StartNumber)
               .IsRequired()
               .HasColumnType("bigint");

        builder.Property(x => x.EndNumber)
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
               .WithMany(c => c.CompanyManifestFormPeriods)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
