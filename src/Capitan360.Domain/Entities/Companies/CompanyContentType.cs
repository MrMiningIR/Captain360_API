using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Entities.ContentTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyContentType : BaseEntity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    [ForeignKey(nameof(ContentType))]
    public int ContentTypeId { get; set; }
    public ContentType? ContentType { get; set; }

    public string Name { get; set; } = default!;

    public bool Active { get; set; }

    public int Order { get; set; }

    public string Description { get; set; } = default!;

    public ICollection<CompanyDomesticWaybillPackageType> CompanyDomesticWaybillPackageTypes { get; set; } = [];
}