using Capitan360.Domain.Entities.ContentTypes;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.ContentConfigs
{
    public class ContentTypeConfigurations : BaseEntityConfiguration<ContentType>
    {
        public override void Configure(EntityTypeBuilder<ContentType> builder)
        {

            base.Configure(builder);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.Description).HasMaxLength(500).IsUnicode();
            builder.Property(x => x.Order).HasDefaultValue(0);
            builder.Property(x => x.Active).HasDefaultValue(true);

            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.Order);

        }
    }
}
