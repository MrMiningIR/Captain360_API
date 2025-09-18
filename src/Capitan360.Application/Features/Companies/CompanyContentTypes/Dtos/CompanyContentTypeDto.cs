namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Dtos;

public class CompanyContentTypeDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public int ContentTypeId { get; set; }
    public string? ContentTypeName { get; set; }
    public string Name { get; set; } = default!;
    public bool Active { get; set; }
    public int Order { get; set; }
    public string Description { get; set; } = default!;
}