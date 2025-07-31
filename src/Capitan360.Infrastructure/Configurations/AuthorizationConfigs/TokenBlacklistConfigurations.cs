using Capitan360.Domain.Entities.UserEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.AuthorizationConfigs;

internal class TokenBlacklistConfigurations : BaseEntityConfiguration<TokenBlacklist>
{
    public override void Configure(EntityTypeBuilder<TokenBlacklist> builder)
    {


        base.Configure(builder);
        builder.Property(tb => tb.Token)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(tb => tb.ExpiryDate);

        builder.HasOne(tb => tb.User);




    }
}