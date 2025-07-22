namespace Capitan360.Application.Services.CompanyContentTypeService.Dtos;

public class CompanyContentTypeDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int ContentTypeId { get; set; }
    public string ContentTypeName { get; set; } = default!;
    public string NewContentTypeName { get; set; } = default!;
    public bool Active { get; set; }
    public int OrderContentType { get; set; }
}