namespace Capitan360.Application.Services.CompanyContentTypeService.Dtos;

public class CompanyContentTypeDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; } 
    public int ContentTypeId { get; set; }
    public string? ContentTypeName { get; set; }
    public string CompanyContentTypeName { get; set; } = default!;
    public bool CompanyContentTypeActive { get; set; }
    public int CompanyContentTypeOrder { get; set; }
    public string? CompanyContentTypeDescription { get; set; }
}