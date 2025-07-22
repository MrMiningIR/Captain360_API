using Capitan360.Domain.Entities.ContentEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.ContentConfigs
{
    internal class CompanyContentTypeConfigurations : IEntityTypeConfiguration<CompanyContentType>
    {


        public void Configure(EntityTypeBuilder<CompanyContentType> builder)
        {
            builder
                 .HasIndex(x => new { x.CompanyId, x.ContentTypeId })
                 .HasDatabaseName("IX_CompanyContentType_Active")
                 .IsUnique()
                 .HasFilter("[Deleted] = 0");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ContentTypeName).HasMaxLength(50).IsUnicode().IsRequired();

            builder.Property(x => x.OrderContentType);
            builder.Property(x => x.Active);

            builder.HasOne(ca => ca.Company)
                          .WithMany(ca => ca.CompanyContentTypes)
                         .HasForeignKey(ca => ca.CompanyId)
                          .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ca => ca.ContentType)
                    .WithMany(ca => ca.CompanyContentTypes)
                    .HasForeignKey(ca => ca.ContentTypeId)
                    .OnDelete(DeleteBehavior.NoAction);






        }
    }
}
