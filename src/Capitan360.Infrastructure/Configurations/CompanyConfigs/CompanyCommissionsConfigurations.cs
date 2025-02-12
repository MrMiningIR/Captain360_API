using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

internal class CompanyCommissionsConfigurations:IEntityTypeConfiguration<CompanyCommissions>
{
    public void Configure(EntityTypeBuilder<CompanyCommissions> builder)
    {
        
    }
}