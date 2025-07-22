using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyUri: Entity
{
   
    public string Uri { get; set; } = default!;
    public string?  Description { get; set; }

    public bool IsActive { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; } = default!;
}