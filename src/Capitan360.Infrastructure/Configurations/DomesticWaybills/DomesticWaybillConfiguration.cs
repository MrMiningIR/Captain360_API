using Capitan360.Domain.Entities.DomesticWaybills;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.DomesticWaybills;

public class DomesticWaybillConfiguration : BaseEntityConfiguration<DomesticWaybill>
{
    public override void Configure(EntityTypeBuilder<DomesticWaybill> builder)
    {
        base.Configure(builder);

    }
}
