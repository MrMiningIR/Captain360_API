using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyUri : BaseEntity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    public string Uri { get; set; } = default!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public bool Captain360Uri { get; set; }
}