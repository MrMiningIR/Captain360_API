using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.Companies;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Capitan360.Domain.Entities.DomesticWaybills;

public class DomesticWaybillPeriod : BaseEntity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    public string Code { get; set; } = default!;

    public long StartNumber { get; set; }

    public long EndNumber { get; set; }

    public bool Active { get; set; }

    public string? Description { get; set; }
}
