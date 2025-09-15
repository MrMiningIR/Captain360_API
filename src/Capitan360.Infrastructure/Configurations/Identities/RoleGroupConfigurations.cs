using Capitan360.Domain.Entities.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Identities;

public class RoleGroupConfigurations : IEntityTypeConfiguration<RoleGroup>
{
    public void Configure(EntityTypeBuilder<RoleGroup> builder)
    {

        builder.HasKey(rg => new { rg.RoleId, rg.GroupId });
        builder.HasOne(rg => rg.Role)
            .WithMany(r => r.RoleGroups)
            .HasForeignKey(rg => rg.RoleId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(rg => rg.Group)
            .WithMany(g => g.RoleGroups)
            .HasForeignKey(rg => rg.GroupId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}