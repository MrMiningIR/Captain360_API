using Capitan360.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Identities;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => new { x.PhoneNumber, x.CompanyId })
            .IsUnique();

        builder.Property(x => x.NameFamily)
              .IsRequired()
              .HasMaxLength(100)
              .IsUnicode()
              .HasColumnType("nvarchar(100)");

        builder.Property(x => x.AccountCodeInDesktopCaptainCargo)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.MobileTelegram)

               .HasMaxLength(11)
               .IsUnicode(false)
               .HasColumnType("varchar(11)");

        builder.Property(x => x.TypeOfFactorInSamanehMoadianId)
               .IsRequired();

    builder.Property(x => x.Tell)

              .HasMaxLength(30)
              .IsUnicode()
              .HasColumnType("nvarchar(30)");

        builder.Property(x => x.NationalCode)

              .HasMaxLength(50)
              .IsUnicode()
              .HasColumnType("nvarchar(50)");

        builder.Property(x => x.EconomicCode)

              .HasMaxLength(50)
              .IsUnicode()
              .HasColumnType("nvarchar(50)");

        builder.Property(x => x.NationalId)

              .HasMaxLength(50)
              .IsUnicode()
              .HasColumnType("nvarchar(50)");

        builder.Property(x => x.RegistrationId)

              .HasMaxLength(50)
              .IsUnicode()
              .HasColumnType("nvarchar(50)");

        builder.Property(x => x.Description)

               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.Credit);

        builder.Property(x => x.HasCredit);

        builder.Property(x => x.Active);

        builder.Property(x => x.Baned);

        builder.Property(x => x.ActivationCode)
            .HasMaxLength(6)
              .IsUnicode(false)
              .HasColumnType("char(6)");

        builder.Property(x => x.ActivationCodeExpireTime);

        builder.Property(x => x.RecoveryPasswordCode)
            .HasMaxLength(6)
              .IsUnicode(false)
              .HasColumnType("char(6)");

        builder.Property(x => x.RecoveryPasswordCodeExpireTime);

        builder.Property(x => x.IsBikeDelivery);

        builder.Property(x => x.LastAccess);

        builder.Property(x => x.ActiveSessionId)

               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

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

        builder.Property(x => x.PermissionVersion)
               .IsRequired()
               .HasMaxLength(36)
               .HasColumnType("nvarchar(36)");

        builder.HasOne(x => x.Company)
               .WithMany(c => c.CompanyUsers)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}