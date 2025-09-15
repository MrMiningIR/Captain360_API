using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies
{
    public class CompanyUriConfigurations : BaseEntityConfiguration<CompanyUri>
    {
        public override void Configure(EntityTypeBuilder<CompanyUri> builder)
        {

            base.Configure(builder);
            builder.Property(ur => ur.Uri).IsRequired().HasMaxLength(100);
            builder.Property(ur => ur.Description).HasMaxLength(500).IsUnicode();
            builder.Property(ur => ur.Active).IsRequired().HasDefaultValue(true);
            builder.Property(ur => ur.Captain360Uri);

            builder.HasOne(ur => ur.Company)
                .WithMany(c => c.CompanyUris)
                .HasForeignKey(ur => ur.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);




        }
    }
}
