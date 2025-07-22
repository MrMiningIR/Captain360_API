using Capitan360.Domain.Entities.AuthorizationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.AuthorizationConfigs;

public class UserPermissionConfigurations : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder
            .HasKey(x => x.Id);

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