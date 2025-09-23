using Capitan360.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Domain.Entities.CompanyManifestForms;

public class CompanyManifestFormPeriod : BaseEntity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    public string Code { get; set; } = default!;

    public long StartNumber { get; set; }

    public long EndNumber { get; set; }

    public bool Active { get; set; }

    public string Description { get; set; } = default!;

    public ICollection<CompanyManifestForm> CompanyManifestForms { get; set; } = [];
}
