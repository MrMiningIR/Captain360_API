using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.Companies;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.ContentTypes;

public class ContentType : BaseEntity
{
    [ForeignKey(nameof(CompanyType))]
    public int CompanyTypeId { get; set; }
    public CompanyType? CompanyType { get; set; } 

    public string Name { get; set; } = default!;

    public bool Active { get; set; }

    public string Description { get; set; } = default!;

    public int Order { get; set; }

    public ICollection<CompanyContentType> CompanyContentTypes { get; set; } = [];
}