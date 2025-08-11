namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentTypeName;

public record UpdateCompanyContentTypeNameAndDescriptionCommand(string CompanyContentTypeName, string? CompanyContentTypeDescription)
{
    public int Id { get; set; }

}