using Capitan360.Domain.Entities.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.UserConfig;

internal class UserFinanceConfigurations : IEntityTypeConfiguration<UserFinancialInfo>
{
    public void Configure(EntityTypeBuilder<UserFinancialInfo> builder)
    {

        builder.HasKey(uf => uf.Id);

        // for testing purposes
        builder.Property(uf => uf.Credit).HasColumnType("decimal(18,2)").HasPrecision(18, 2)
            .HasDefaultValue(0);
        builder.Property(uf => uf.HasCredit).HasDefaultValue(false);

        // Navigation Property
        builder.HasOne(uf => uf.User)
            .WithOne(u => u.Financial)
            .HasForeignKey<UserFinancialInfo>(uf => uf.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}