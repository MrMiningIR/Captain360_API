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

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(x => x.DisplayName).IsUnicode().HasMaxLength(100).IsRequired(true);
        builder.Property(x => x.Parent).HasMaxLength(100).IsRequired(false);
        builder.Property(x => x.ParentDisplayName).HasMaxLength(100).IsUnicode().IsRequired(false);

        builder.HasMany(p => p.RolePermissions)
            .WithOne(rp => rp.Permission)
            .HasForeignKey(rp => rp.PermissionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.GroupPermissions)
            .WithOne(gp => gp.Permission)
            .HasForeignKey(gp => gp.PermissionId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Property(e => e.PermissionCode).IsRequired();
        builder.Property(e => e.ParentCode).IsRequired();

        builder.HasIndex(e => e.PermissionCode)
                      .HasDatabaseName("IX_Permissions_PermissionCode")
                      .IsUnique().HasFilter("[Deleted] = 0");
        builder.HasIndex(e => e.ParentCode)
                      .HasDatabaseName("IX_Permissions_ParentCode");




    }
}