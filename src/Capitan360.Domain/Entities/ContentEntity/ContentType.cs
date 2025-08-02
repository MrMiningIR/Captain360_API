using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Entities.ContentEntity;

public class ContentType : Entity
{
    public int CompanyTypeId { get; set; }
    public CompanyType CompanyType { get; set; } = default!;
    public string ContentTypeName { get; set; } = default!;
    public bool Active { get; set; }
    public string? ContentTypeDescription { get; set; }
    public int OrderContentType { get; set; }
    public ICollection<CompanyContentType> CompanyContentTypes { get; set; } = [];
}