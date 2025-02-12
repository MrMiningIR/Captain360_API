using Capitan360.Domain.Entities.AuthorizationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.AuthorizationConfigs;

internal class PermissionConfigurations : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(p => p.RolePermissions)
            .WithOne(rp => rp.Permission)
            .HasForeignKey(rp => rp.PermissionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.GroupPermissions)
            .WithOne(gp => gp.Permission)
            .HasForeignKey(gp => gp.PermissionId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}