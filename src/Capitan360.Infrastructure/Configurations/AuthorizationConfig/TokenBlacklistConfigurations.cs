using Capitan360.Domain.Entities.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.AuthorizationConfig;

internal class TokenBlacklistConfigurations:IEntityTypeConfiguration<TokenBlacklist>
{
    public void Configure(EntityTypeBuilder<TokenBlacklist> builder)
    {

        builder.HasKey(tb => tb.Id);
        builder.Property(tb => tb.Token)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(tb => tb.ExpiryDate);

        builder.HasOne(tb => tb.User);




    }
}