namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentTypeNameAndDescription;

public record UpdateCompanyContentTypeNameCommand(
   string CompanyContentTypeName)
{
    public int Id { get; set; }
}