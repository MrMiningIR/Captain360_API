
using Capitan360.Domain.Entities.UserEntity;
using Capitan360.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.UserConfig;

internal class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(StringValues.UserTableName);


        builder.Property(u => u.FullName).IsRequired(false).HasMaxLength(100);
        builder.Property(u => u.CapitanCargoCode).IsUnicode(false).IsRequired().HasMaxLength(50);
        builder.Property(u => u.LastAccess).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate(); 

        // for testing purposes
        builder.Property(u => u.Active).HasDefaultValue(true);
        builder.Property(u => u.ActivationCode).IsRequired(false).HasMaxLength(6);




        // Navigation Properties
        builder.HasOne(u => u.Profile).WithOne(p => p.User).HasForeignKey<UserProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        //builder.HasOne(u => u.Company).WithOne(c => c.User).HasForeignKey<Company>(c => c.UserId);
        builder.HasOne(u => u.Financial).WithOne(f => f.User).HasForeignKey<UserFinancialInfo>(f => f.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(u => u.UserGroups).WithOne(ug => ug.User).HasForeignKey(ug => ug.UserId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasMany(u => u.UserCompanies).WithOne(uc => uc.User).HasForeignKey(uc => uc.UserId);



    }
}