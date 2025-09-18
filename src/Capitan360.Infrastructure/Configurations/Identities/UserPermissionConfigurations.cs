using Capitan360.Domain.Entities.Identities;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Identities;

public class UserPermissionConfigurations : BaseEntityConfiguration<UserPermission>
{
    public override void Configure(EntityTypeBuilder<UserPermission> builder)
    {

        base.Configure(builder);

        builder
            .HasOne(up => up.User)
            .WithMany(u => u.UserPermissions)
            .HasForeignKey(up => up.UserId);

        builder
            .HasOne(up => up.Permission)
            .WithMany(p => p.UserPermissions)
            .HasForeignKey(up => up.PermissionId);


        builder
.HasIndex(uc => new { uc.UserId, uc.PermissionId })
.HasDatabaseName("IX_UserPermission_Active")
.IsUnique()
.HasFilter("[Deleted] = 0");
    }
}