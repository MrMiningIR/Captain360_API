using Capitan360.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Identities;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //builder.HasIndex(x => new { x.PhoneNumber, x.CompanyId })
        //    .IsUnique();

        // Composite unique index for multi-tenant username (NormalizedUserName + CompanyId)
        // Note: The existing UserNameIndex unique constraint should be removed in migration
        builder.HasIndex(x => new { x.NormalizedUserName, x.CompanyId })
            .IsUnique()
            .HasFilter("[NormalizedUserName] IS NOT NULL");

        builder.Property(x => x.NameFamily)
              .IsRequired()
              .HasMaxLength(100)
              .IsUnicode()
              .HasColumnType("nvarchar(100)");

        builder.Property(x => x.AccountCodeInDesktopCaptainCargo)
                .HasDefaultValue("")
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.MobileTelegram)
            .HasDefaultValue("")
               .HasMaxLength(11)
               .IsUnicode(false)
               .HasColumnType("varchar(11)");

        builder.Property(x => x.TypeOfFactorInSamanehMoadianId)
               .IsRequired();

        builder.Property(x => x.Tell)
             .HasDefaultValue("")
              .HasMaxLength(30)
              .IsUnicode()
              .HasColumnType("nvarchar(30)");

        builder.Property(x => x.NationalCode)
             .HasDefaultValue("")
              .HasMaxLength(50)
              .IsUnicode()
              .HasColumnType("nvarchar(50)");

        builder.Property(x => x.EconomicCode)
             .HasDefaultValue("")
              .HasMaxLength(50)
              .IsUnicode()
              .HasColumnType("nvarchar(50)");

        builder.Property(x => x.NationalId)
             .HasDefaultValue("")
              .HasMaxLength(50)
              .IsUnicode()
              .HasColumnType("nvarchar(50)");

        builder.Property(x => x.RegistrationId)
             .HasDefaultValue("")
              .HasMaxLength(50)
              .IsUnicode()
              .HasColumnType("nvarchar(50)");

        builder.Property(x => x.Description)
             .HasDefaultValue("")
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
             .HasDefaultValue("")
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