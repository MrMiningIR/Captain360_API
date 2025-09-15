using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Entities.Users;
using Capitan360.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Users;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.ToTable(ConstantNames.UserTableName);
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                name: "CK_User_PhoneNumber_Length",
                sql: "LEN(PhoneNumber) = 11");
        });
        builder.HasIndex(u => u.PhoneNumber).IsUnique();



        builder.Property(u => u.FullName).IsRequired(true).HasMaxLength(100);
        builder.Property(u => u.CapitanCargoCode).IsUnicode(false).IsRequired().HasMaxLength(50);
        // builder.Property(u => u.LastAccess).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate(); 
        builder.Property(u => u.LastAccess);

        // for testing purposes
        builder.Property(u => u.Active).HasDefaultValue(true);
        builder.Property(u => u.ActivationCode).IsRequired(false).HasMaxLength(6);
        builder.Property(u => u.ActiveSessionId).IsRequired(false).HasMaxLength(36);
        builder.Property(u => u.CapitanCargoCode).IsRequired(false);
        builder.Property(u => u.PhoneNumber).IsRequired(true).HasMaxLength(11);
        builder.Property(u => u.Email).IsRequired(false);
        builder.Property(x => x.UserKind).HasDefaultValue(UserKind.Normal).HasConversion<int>();


        builder.HasMany(u => u.Roles)
         .WithMany()
         .UsingEntity<IdentityUserRole<string>>(
             j => j.HasOne<Role>().WithMany().HasForeignKey(ur => ur.RoleId),
             j => j.HasOne<User>().WithMany().HasForeignKey(ur => ur.UserId),
             j =>
             {
                 j.ToTable("AspNetUserRoles");
                 j.HasKey(ur => new { ur.UserId, ur.RoleId });
             });


        // Navigation Properties
        //builder.HasOne(u => u.Profile).WithOne(p => p.User).HasForeignKey<UserProfile>(p => p.UserId)
        //    .OnDelete(DeleteBehavior.NoAction);
        //builder.HasOne(u => u.Company).WithOne(c => c.User).HasForeignKey<Company>(c => c.UserId);

        builder.HasMany(u => u.UserGroups).WithOne(ug => ug.User).HasForeignKey(ug => ug.UserId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasMany(u => u.UserCompanies).WithOne(uc => uc.User).HasForeignKey(uc => uc.UserId);

        builder.Property(x => x.ConcurrencyToken).IsRowVersion();



    }
}