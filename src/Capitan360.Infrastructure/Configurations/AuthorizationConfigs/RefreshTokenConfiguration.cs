using Capitan360.Domain.Entities.AuthorizationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.AuthorizationConfigs;

public class RefreshTokenConfiguration : BaseEntityConfiguration<RefreshToken>

{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);
        builder.HasIndex(e => e.Token).IsUnique();

        builder.HasOne(rt => rt.User) // RefreshToken has one User
            .WithMany()            // User can have many RefreshTokens
            .HasForeignKey(rt => rt.UserId) // Foreign key is UserId
            .OnDelete(DeleteBehavior.Cascade);
    }
}

