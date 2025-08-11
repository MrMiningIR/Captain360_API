using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Entities.ContentEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.CompanyContentEntity;

public class CompanyContentType : Entity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }

    [ForeignKey(nameof(ContentType))]
    public int ContentTypeId { get; set; }

    public string CompanyContentTypeName { get; set; } = default!;

    public bool Active { get; set; }

    public int OrderCompanyContentType { get; set; }
    public string? CompanyContentTypeDescription { get; set; }
    public Company Company { get; set; } = default!;
    public ContentType ContentType { get; set; } = default!;
}