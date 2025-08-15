using Capitan360.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyUri : Entity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }

    public Company Company { get; set; } = default!;
    public string Uri { get; set; } = default!;
    public string? Description { get; set; }

    public bool Active { get; set; }
}