using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Entities.DomesticWaybillEntity;

public class ManifestFormPeriod : Entity
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
