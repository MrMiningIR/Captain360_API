using Capitan360.Domain.Entities.Users;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Users;

public class TokenBlacklistConfigurations : BaseEntityConfiguration<TokenBlacklist>
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