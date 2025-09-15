using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class CompanyAddressConfigurations : BaseEntityConfiguration<CompanyAddress>
{
    public override void Configure(EntityTypeBuilder<CompanyAddress> builder)
    {
    }
}