namespace Capitan360.Application.Services.ContentTypeService.Commands.CreateContentType;

public record CreateContentTypeCommand(
    int CompanyTypeId,
    string ContentTypeName,
    string ContentTypeDescription,
    bool Active
    );