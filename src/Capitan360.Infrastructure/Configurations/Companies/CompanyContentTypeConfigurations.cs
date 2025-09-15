using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class CompanyContentTypeConfigurations : BaseEntityConfiguration<CompanyContentType>
{
    public override void Configure(EntityTypeBuilder<CompanyContentType> builder)
    {
        base.Configure(builder);
        builder
             .HasIndex(x => new { x.CompanyId, x.ContentTypeId })
             .HasDatabaseName("IX_CompanyContentType_Active")
             .IsUnique()
             .HasFilter("[Deleted] = 0");

        builder.Property(x => x.Name).HasMaxLength(50).IsUnicode().IsRequired();

        builder.Property(x => x.Order);
        builder.Property(x => x.Active);

        builder.HasOne(ca => ca.Company)
                      .WithMany(ca => ca.CompanyContentTypes)
                     .HasForeignKey(ca => ca.CompanyId)
                      .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ca => ca.ContentType)
                .WithMany(ca => ca.CompanyContentTypes)
                .HasForeignKey(ca => ca.ContentTypeId)
                .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.Description).HasMaxLength(500).IsUnicode();
    }
}