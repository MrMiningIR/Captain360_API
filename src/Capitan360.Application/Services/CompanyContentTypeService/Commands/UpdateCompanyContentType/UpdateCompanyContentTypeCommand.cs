namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentType;

public record UpdateCompanyContentTypeCommand
{
    public int Id { get; set; }
    public int ContentTypeId { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyContentTypeName { get; set; }
    public bool? Active { get; set; }
}