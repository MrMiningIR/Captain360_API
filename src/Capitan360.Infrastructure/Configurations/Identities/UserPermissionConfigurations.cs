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

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
               .IsRequired();

        builder.Property(x => x.PermissionId)
               .IsRequired();

        builder.HasOne(x => x.User)
               .WithMany(c => c.UserPermissions)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Permission)
               .WithMany(c => c.UserPermissions)
               .HasForeignKey(x => x.PermissionId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}