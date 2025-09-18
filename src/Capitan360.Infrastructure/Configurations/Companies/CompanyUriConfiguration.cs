using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class CompanyUriConfiguration : BaseEntityConfiguration<CompanyUri>
{
    public override void Configure(EntityTypeBuilder<CompanyUri> builder)
    {
            base.Configure(builder);
       
        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.CompanyId)
               .IsRequired();

        builder.Property(x => x.Uri)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode()                    
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.Active)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.Captain360Uri)
               .IsRequired()
               .HasColumnType("bit");

        builder.HasOne(x => x.Company)
               .WithMany(c => c.CompanyUris)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
