using Capitan360.Domain.Entities.UserEntity;
using Capitan360.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.UserConfigs;

internal class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
       
        builder.ToTable(StringValues.UserTableName);
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                name: "CK_User_PhoneNumber_Length",
                sql: "LEN(PhoneNumber) = 11");
        });
        builder.HasIndex(u => u.PhoneNumber).IsUnique();



        builder.Property(u => u.FullName).IsRequired(true).HasMaxLength(100);
        // builder.Property(u => u.LastAccess).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate(); 
        builder.Property(u => u.LastAccess);

        // for testing purposes
        builder.Property(u => u.ActivationCode).IsRequired(false).HasMaxLength(6);
        builder.Property(u => u.ActiveSessionId).IsRequired(false).HasMaxLength(36);
        builder.Property(u => u.PhoneNumber).IsRequired(true).HasMaxLength(11);
        builder.Property(u => u.Email).IsRequired(false);
       

      




        // Navigation Properties
        builder.HasOne(u => u.UserProfile).WithOne(p => p.User).HasForeignKey<UserProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        //builder.HasOne(u => u.Company).WithOne(c => c.User).HasForeignKey<Company>(c => c.UserId);

        builder.HasMany(u => u.UserGroups).WithOne(ug => ug.User).HasForeignKey(ug => ug.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(u => u.UserCompanies).WithOne(uc => uc.User).HasForeignKey(uc => uc.UserId);



    }
}