using Capitan360.Domain.Entities.DomesticWaybills;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.DomesticWaybills;

public class DomesticWaybillPackageTypeConfiguration : BaseEntityConfiguration<DomesticWaybillPackageType>
{
    public override void Configure(EntityTypeBuilder<DomesticWaybillPackageType> builder)
    {
        base.Configure(builder);

    }
}
