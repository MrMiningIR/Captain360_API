using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyType : BaseEntity
{
    public string TypeName { get; set; } = default!;

    public string DisplayName { get; set; } = default!;

    public string Description { get; set; } = default!;
}