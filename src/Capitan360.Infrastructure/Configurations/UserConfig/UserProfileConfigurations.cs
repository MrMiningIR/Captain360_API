
using Capitan360.Domain.Entities.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.UserConfig;

internal class UserProfileConfigurations : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(up => up.Id);

        builder.Property(up => up.TelegramPhoneNumber).HasMaxLength(11);
        builder.Property(up => up.TellNumber).HasMaxLength(30);

        builder.Property(up => up.NationalCode).HasMaxLength(50);
        builder.Property(up => up.EconomicCode).HasMaxLength(50);
        builder.Property(up => up.NationalId).HasMaxLength(50);
        builder.Property(up => up.RegistrationId).HasMaxLength(50);

        builder.Property(up => up.Description).HasMaxLength(1500);
        builder.Property(up => up.MoadianFactorType);
        builder.Property(up => up.IsBikeDelivery);

        builder.Property(up => up.RecoveryPasswordCode);
        builder.Property(up => up.RecoveryPasswordCodeExpireTime);

        // Navigation Property
        builder.HasOne(up => up.User)
            .WithOne(u => u.Profile)
            .HasForeignKey<UserProfile>(up => up.UserId)
            .OnDelete(DeleteBehavior.NoAction);



    }
}