using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Domain.Entities.ManifestForms;

public class ManifestFormPeriod : BaseEntity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    public string Code { get; set; } = default!;

    public long StartNumber { get; set; }

    public long EndNumber { get; set; }

    public bool Active { get; set; }

    public string Description { get; set; } = default!;

    public ICollection<ManifestForm> ManifestForms { get; set; } = [];
}
