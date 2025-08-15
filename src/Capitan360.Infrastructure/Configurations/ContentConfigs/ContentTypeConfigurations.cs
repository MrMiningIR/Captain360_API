using Capitan360.Domain.Entities.ContentEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.ContentConfigs
{
    public class ContentTypeConfigurations : BaseEntityConfiguration<ContentType>
    {
        public override void Configure(EntityTypeBuilder<ContentType> builder)
        {

            base.Configure(builder);
            builder.Property(x => x.ContentTypeName).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.ContentTypeDescription).HasMaxLength(500).IsUnicode();
            builder.Property(x => x.ContentTypeOrder).HasDefaultValue(0);
            builder.Property(x => x.ContentTypeActive).HasDefaultValue(true);

            builder.HasIndex(x => x.ContentTypeName);
            builder.HasIndex(x => x.ContentTypeOrder);

        }
    }
}
