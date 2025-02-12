using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs
{
    internal class CompanyUriConfigurations:IEntityTypeConfiguration<CompanyUri>
    {
        public void Configure(EntityTypeBuilder<CompanyUri> builder)
        {
           
            builder.HasKey(ur => ur.Id);
            builder.Property(ur => ur.Uri).IsRequired().HasMaxLength(50);
            builder.Property(ur => ur.Description).HasMaxLength(500);
            builder.Property(ur => ur.IsActive).IsRequired().HasDefaultValue(true);

            builder.HasOne(ur => ur.Company)
                .WithMany(c => c.CompanyUris)
                .HasForeignKey(ur => ur.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);




        }
    }
}
