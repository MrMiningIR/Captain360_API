using Capitan360.Domain.Entities.Users;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Users;

public class UserPermissionVersionControlConfigurations : BaseEntityConfiguration<UserPermissionVersionControl>
{
    public override void Configure(EntityTypeBuilder<UserPermissionVersionControl> builder)
    {
        base.Configure(builder);


        builder.HasOne(uc => uc.User)
            .WithOne(u => u.UserPermissionVersionControl)
            .HasForeignKey<UserPermissionVersionControl>(uc => uc.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Property(x => x.PermissionVersion).IsRequired()
            .HasMaxLength(36);

        builder
            .HasIndex(uc => uc.UserId)
            .IsUnique();


        builder
.HasIndex(uc => new { uc.UserId, uc.PermissionVersion })
.HasDatabaseName("IX_UserPermissionVersionControl_Active")
.IsUnique()
.HasFilter("[Deleted] = 0");
    }
}