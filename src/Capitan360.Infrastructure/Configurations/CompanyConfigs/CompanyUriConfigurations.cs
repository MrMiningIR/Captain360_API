using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs
{
    internal class CompanyUriConfigurations : BaseEntityConfiguration<CompanyUri>
    {
        public override void Configure(EntityTypeBuilder<CompanyUri> builder)
        {

            base.Configure(builder);
            builder.Property(ur => ur.Uri).IsRequired().HasMaxLength(100);
            builder.Property(ur => ur.Description).HasMaxLength(500).IsUnicode();
            builder.Property(ur => ur.Active).IsRequired().HasDefaultValue(true);

            builder.HasOne(ur => ur.Company)
                .WithMany(c => c.CompanyUris)
                .HasForeignKey(ur => ur.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);




        }
    }
}
