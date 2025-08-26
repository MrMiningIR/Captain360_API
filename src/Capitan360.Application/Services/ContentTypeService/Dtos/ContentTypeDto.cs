namespace Capitan360.Application.Services.ContentTypeService.Dtos;

public class ContentTypeDto
{
    public int Id { get; set; }
    public int CompanyTypeId { get; set; }
    public string? CompanyTypeName { get; set; }
    public string ContentTypeName { get; set; } = default!;
    public bool ContentTypeActive { get; set; }
    public string? ContentTypeDescription { get; set; }
    public int ContentTypeOrder { get; set; }
}