using Capitan360.Domain.Entities.Users;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Users;

public class UserProfileConfigurations : BaseEntityConfiguration<UserProfile>
{
    public override void Configure(EntityTypeBuilder<UserProfile> builder)
    {


        base.Configure(builder);



        builder.Property(up => up.TelegramPhoneNumber).HasMaxLength(11).IsRequired(false);
        builder.Property(up => up.UserId).HasMaxLength(450).IsRequired(true);
        builder.Property(up => up.TellNumber).HasMaxLength(30).IsRequired(false);

        builder.Property(up => up.NationalCode).HasMaxLength(50).IsRequired(false);
        builder.Property(up => up.EconomicCode).HasMaxLength(50).IsRequired(false);
        builder.Property(up => up.NationalId).HasMaxLength(50).IsRequired(false);
        builder.Property(up => up.RegistrationId).HasMaxLength(50).IsRequired(false);

        builder.Property(up => up.Description).HasMaxLength(1500).IsRequired(false);
        builder.Property(up => up.MoadianFactorType).IsRequired();
        builder.Property(up => up.IsBikeDelivery).HasDefaultValue(false);

        builder.Property(up => up.RecoveryPasswordCode).IsRequired(false);
        builder.Property(up => up.RecoveryPasswordCodeExpireTime);

        // for testing purposes
        builder.Property(uf => uf.Credit).HasColumnType("decimal(18,2)").HasPrecision(18, 2)
            .HasDefaultValue(0);
        builder.Property(uf => uf.HasCredit).HasDefaultValue(false);

        // Navigation Property
        builder.HasOne(up => up.User)
            .WithOne(u => u.Profile)
            .HasForeignKey<UserProfile>(up => up.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
        .HasIndex(uc => new { uc.UserId })
        .HasDatabaseName("IX_UserProfile_Active")
        .IsUnique()
        .HasFilter("[Deleted] = 0");



    }
}