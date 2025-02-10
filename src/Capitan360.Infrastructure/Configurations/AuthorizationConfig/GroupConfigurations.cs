using Capitan360.Domain.Entities.AuthorizationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.AuthorizationConfig;

internal class GroupConfigurations:IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.HasMany(x => x.GroupPermissions).WithOne(x => x.Group).HasForeignKey(x => x.GroupId);
         builder.HasMany(x => x.UserGroups).WithOne(x => x.Group).HasForeignKey(x => x.GroupId);
    }
}