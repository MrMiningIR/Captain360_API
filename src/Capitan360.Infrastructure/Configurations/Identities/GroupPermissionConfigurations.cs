using Capitan360.Domain.Entities.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Identities;

public class GroupPermissionConfigurations : IEntityTypeConfiguration<GroupPermission>
{


    public void Configure(EntityTypeBuilder<GroupPermission> builder)
    {


        builder.HasKey(gp => new { gp.GroupId, gp.PermissionId });


        builder.HasOne(gp => gp.Group)
            .WithMany(g => g.GroupPermissions)
            .HasForeignKey(gp => gp.GroupId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne(gp => gp.Permission)
            .WithMany(p => p.GroupPermissions)
            .HasForeignKey(gp => gp.PermissionId)
            .OnDelete(DeleteBehavior.NoAction);


        builder
.HasIndex(uc => new { uc.GroupId, uc.PermissionId })
.HasDatabaseName("IX_GroupPermission_Active")
.IsUnique()
.HasFilter("[Deleted] = 0");

    }
}