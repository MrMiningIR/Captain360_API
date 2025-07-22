using Capitan360.Domain.Entities.ContentEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.ContentConfigs
{
    public class ContentTypeConfigurations : IEntityTypeConfiguration<ContentType>
    {
        public void Configure(EntityTypeBuilder<ContentType> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.ContentTypeName).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(x => x.ContentTypeDescription).IsRequired().HasMaxLength(100).IsUnicode();
            builder.Property(x => x.OrderContentType).HasDefaultValue(0);
            builder.Property(x => x.Active).HasDefaultValue(true);

            builder.HasIndex(x => x.ContentTypeName);
            builder.HasIndex(x => x.OrderContentType);

        }
    }
}
