using Capitan360.Domain.Entities.Identities;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Identities;

public class PermissionConfigurations : BaseEntityConfiguration<Permission>
{
    public override void Configure(EntityTypeBuilder<Permission> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.DisplayName)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.PermissionCode)
               .IsRequired()
               .HasColumnType("uniqueidentifier");

        builder.Property(x => x.Active)
               .IsRequired();

        builder.Property(x => x.ParentName)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.ParentDisplayName)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.ParentCode)
               .IsRequired()
               .HasColumnType("uniqueidentifier");
    }
}