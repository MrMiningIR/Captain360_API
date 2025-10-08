using Capitan360.Domain.Entities.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Identities;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
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
               .IsRequired()
               .HasMaxLength(11)
               .IsUnicode(false)
               .HasColumnType("varchar(11)");

        builder.Property(x => x.TypeOfFactorInSamanehMoadianId)
               .IsRequired();

        builder.Property(x => x.Tell)
              .IsRequired()
              .HasMaxLength(30)
              .IsUnicode()
              .HasColumnType("nvarchar(30)");

        builder.Property(x => x.NationalCode)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode()
              .HasColumnType("nvarchar(50)");

        builder.Property(x => x.EconomicCode)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode()
              .HasColumnType("nvarchar(50)");

        builder.Property(x => x.NationalId)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode()
              .HasColumnType("nvarchar(50)");

        builder.Property(x => x.RegistrationId)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode()
              .HasColumnType("nvarchar(50)");

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.Credit)
               .IsRequired();

        builder.Property(x => x.HasCredit)
               .IsRequired();

        builder.Property(x => x.Active)
               .IsRequired();

        builder.Property(x => x.Baned)
               .IsRequired();

        builder.Property(x => x.ActivationCode)
              .IsRequired()
              .HasMaxLength(6)
              .IsUnicode(false)
              .HasColumnType("char(6)");

        builder.Property(x => x.ActivationCodeExpireTime)
               .IsRequired();

        builder.Property(x => x.RecoveryPasswordCode)
              .IsRequired()
              .HasMaxLength(6)
              .IsUnicode(false)
              .HasColumnType("char(6)");

        builder.Property(x => x.RecoveryPasswordCodeExpireTime)
               .IsRequired();

        builder.Property(x => x.IsBikeDelivery)
            .IsRequired();

        builder.Property(x => x.LastAccess)
               .IsRequired();

        builder.Property(x => x.ActiveSessionId)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

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