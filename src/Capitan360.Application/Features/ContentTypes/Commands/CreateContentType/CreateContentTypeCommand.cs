namespace Capitan360.Application.Features.ContentTypeService.Commands.CreateContentType;

public record CreateContentTypeCommand(
    int CompanyTypeId,
    string ContentTypeName,
    string? ContentTypeDescription,
    bool ContentTypeActive
);