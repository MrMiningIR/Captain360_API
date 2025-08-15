namespace Capitan360.Application.Services.CompanyContentTypeService.Dtos;

public class CompanyContentTypeDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int ContentTypeId { get; set; }
    public string ContentTypeName { get; set; } = default!;
    public string CompanyContentTypeName { get; set; } = default!;
    public string? CompanyContentTypeDescription { get; set; }
    public bool CompanyContentTypeActive { get; set; }
    public int CompanyContentTypeOrder { get; set; }
}