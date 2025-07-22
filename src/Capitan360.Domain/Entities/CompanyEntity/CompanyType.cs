using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyType : Entity
{
    public string TypeName { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string? Description { get; set; }

}