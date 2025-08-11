namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentTypeNameAndDescription;

public record UpdateCompanyContentTypeNameAndDescriptionCommand(string CompanyContentTypeName, string? CompanyContentTypeDescription)
{
    public int Id { get; set; }

}