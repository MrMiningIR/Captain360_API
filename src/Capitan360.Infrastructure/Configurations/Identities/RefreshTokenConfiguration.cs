using Capitan360.Domain.Entities.Identities;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Identities;

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

