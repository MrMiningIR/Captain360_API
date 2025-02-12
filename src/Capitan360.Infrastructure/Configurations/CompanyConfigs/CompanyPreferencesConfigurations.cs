using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs
{
    internal class CompanyPreferencesConfigurations: IEntityTypeConfiguration<CompanyPreferences>
    {
        public void Configure(EntityTypeBuilder<CompanyPreferences> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Admin).HasDefaultValue(false);

        }
    }
}
