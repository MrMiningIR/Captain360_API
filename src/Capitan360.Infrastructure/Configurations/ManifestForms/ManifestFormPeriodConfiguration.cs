using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.ManifestForms;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.ManifestForms;

public class ManifestFormPeriodConfiguration : BaseEntityConfiguration<ManifestFormPeriod>
{
    public override void Configure(EntityTypeBuilder<ManifestFormPeriod> builder)
    {
        base.Configure(builder);

    }
}
