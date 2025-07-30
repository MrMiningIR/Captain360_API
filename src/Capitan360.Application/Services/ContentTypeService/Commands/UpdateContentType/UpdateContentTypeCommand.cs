namespace Capitan360.Application.Services.ContentTypeService.Commands.UpdateContentType;

public record UpdateContentTypeCommand(
    int CompanyTypeId,
    string ContentTypeName,
    string ContentTypeDescription,
    bool Active
   )
{
    public int Id { get; set; }
};