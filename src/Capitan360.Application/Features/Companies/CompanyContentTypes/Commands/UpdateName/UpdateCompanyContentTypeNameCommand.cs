namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateName;

public record UpdateCompanyContentTypeNameCommand(
    string Name)
{
    public int Id { get; set; } = 0;
}