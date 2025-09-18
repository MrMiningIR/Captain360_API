namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateNameAndDescription;

public record UpdateCompanyContentTypeNameCommand(
   string CompanyContentTypeName)
{
    public int Id { get; set; }
}