namespace Capitan360.Application.Features.ContentTypes.Commands.Create;

public record CreateContentTypeCommand(
    int CompanyTypeId,
    string Name,
    string Description,
    bool Active);