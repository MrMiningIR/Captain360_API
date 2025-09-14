using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.ContentEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyContentType : Entity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    [ForeignKey(nameof(ContentType))]
    public int ContentTypeId { get; set; }
    public ContentType? ContentType { get; set; }

    public string CompanyContentTypeName { get; set; } = default!;

    public bool CompanyContentTypeActive { get; set; }

    public int CompanyContentTypeOrder { get; set; }

    public string? CompanyContentTypeDescription { get; set; }
}