using Capitan360.Domain.Entities.AuthorizationEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.AuthorizationConfigs;

internal class GroupConfigurations : BaseEntityConfiguration<Group>
{
    public override void Configure(EntityTypeBuilder<Group> builder)
    {

        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.HasMany(x => x.GroupPermissions).WithOne(x => x.Group).HasForeignKey(x => x.GroupId);
        builder.HasMany(x => x.UserGroups).WithOne(x => x.Group).HasForeignKey(x => x.GroupId);
    }
}