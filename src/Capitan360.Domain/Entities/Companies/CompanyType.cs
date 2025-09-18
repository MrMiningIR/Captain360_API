using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.ContentTypes;
using Capitan360.Domain.Entities.PackageTypes;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyType : BaseEntity
{
    public string TypeName { get; set; } = default!;

    public string DisplayName { get; set; } = default!;

    public string Description { get; set; } = default!;

    public ICollection<PackageType> PackageTypes { get; set; } = [];

    public ICollection<ContentType> ContentTypes { get; set; } = [];

    public ICollection<Company> Companies { get; set; } = [];
}