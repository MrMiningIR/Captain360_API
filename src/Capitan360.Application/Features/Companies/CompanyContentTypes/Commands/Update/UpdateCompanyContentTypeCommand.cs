namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.Update;

public record UpdateCompanyContentTypeCommand(
    string Name,
    bool Active,
    string Description)
{
    public int Id { get; set; }
}