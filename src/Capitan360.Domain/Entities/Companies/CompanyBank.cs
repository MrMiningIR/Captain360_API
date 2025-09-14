using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyBank : BaseEntity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public bool Active { get; set; }

    public int Order { get; set; }
}
