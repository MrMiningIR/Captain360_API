namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateDescription;

public record UpdateCompanyContentTypeDescriptionCommand(
   string CompanyContentTypeDescription)
{
    public int Id { get; set; }
}