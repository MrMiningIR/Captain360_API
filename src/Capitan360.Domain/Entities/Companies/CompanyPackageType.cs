using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.DomesticWaybills;
using Capitan360.Domain.Entities.PackageTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyPackageType : BaseEntity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    [ForeignKey(nameof(PackageType))]
    public int PackageTypeId { get; set; }
    public PackageType? PackageType { get; set; }

    public string Name { get; set; } = default!;

    public bool Active { get; set; }

    public int Order { get; set; }

    public string Description { get; set; } = default!;

    public ICollection<DomesticWaybillPackageType> DomesticWaybillPackageTypes { get; set; } = [];
}