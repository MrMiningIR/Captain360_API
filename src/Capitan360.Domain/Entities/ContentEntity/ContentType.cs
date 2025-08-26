using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.CompanyContentEntity;
using Capitan360.Domain.Entities.CompanyEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.ContentEntity;

public class ContentType : Entity
{
    [ForeignKey(nameof(CompanyType))]
    public int CompanyTypeId { get; set; }
    public CompanyType? CompanyType { get; set; } 

    public string ContentTypeName { get; set; } = default!;

    public bool ContentTypeActive { get; set; }

    public string? ContentTypeDescription { get; set; }
    public int ContentTypeOrder { get; set; }
    public ICollection<CompanyContentType> CompanyContentTypes { get; set; } = [];
}