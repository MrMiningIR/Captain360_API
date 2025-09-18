namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.Update;

public record UpdateCompanyContentTypeCommand(
    string CompanyContentTypeName,
    bool CompanyContentTypeActive,
    string? CompanyContentTypeDescription)
{
    public int Id { get; set; }
}