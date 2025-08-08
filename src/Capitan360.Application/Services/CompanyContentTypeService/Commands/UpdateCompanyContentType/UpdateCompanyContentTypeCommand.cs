namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentType;

public record UpdateCompanyContentTypeCommand(
    int ContentTypeId,
    int CompanyId,
    string CompanyContentTypeName,
    string? CompanyContentTypeDescription,
    bool Active)
{
    public int Id { get; set; }
}
