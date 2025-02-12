using Capitan360.Domain.Entities.AuthorizationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.AuthorizationConfigs;

internal class GroupPermissionConfigurations : IEntityTypeConfiguration<GroupPermission>
{
   

    public void Configure(EntityTypeBuilder<GroupPermission> builder)
    {
        builder.HasKey(x => new { x.GroupId, x.PermissionId });
   
        builder.HasKey(gp => new { gp.GroupId, gp.PermissionId });

        
        builder.HasOne(gp => gp.Group)
            .WithMany(g => g.GroupPermissions)
            .HasForeignKey(gp => gp.GroupId)
            .OnDelete(DeleteBehavior.NoAction);

      
        builder.HasOne(gp => gp.Permission)
            .WithMany(p => p.GroupPermissions)
            .HasForeignKey(gp => gp.PermissionId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}