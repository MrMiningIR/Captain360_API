namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentTypeCommand;

public record UpdateCompanyContentTypeCommand(
    string CompanyContentTypeName,
    bool CompanyContentTypeActive,
    string? CompanyContentTypeDescription)
{
    public int Id { get; set; }
}