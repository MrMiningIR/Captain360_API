using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.DomesticWaybills;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.DomesticWaybills;

public class DomesticWaybillPeriodConfiguration : BaseEntityConfiguration<DomesticWaybillPeriod>
{
    public override void Configure(EntityTypeBuilder<DomesticWaybillPeriod> builder)
    {
        base.Configure(builder);

    }
}