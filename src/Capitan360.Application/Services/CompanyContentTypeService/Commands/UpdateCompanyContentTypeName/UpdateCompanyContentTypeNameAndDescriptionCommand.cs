namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentTypeName;

public record UpdateCompanyContentTypeNameAndDescriptionCommand(string ContentTypeName, string? ContentTypeDescription)
{
    public int Id { get; set; }

}