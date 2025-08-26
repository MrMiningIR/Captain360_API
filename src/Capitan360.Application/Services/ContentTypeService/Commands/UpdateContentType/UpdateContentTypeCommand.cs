namespace Capitan360.Application.Services.ContentTypeService.Commands.UpdateContentType;

public record UpdateContentTypeCommand(
    string ContentTypeName,
    string? ContentTypeDescription,
    bool ContentTypeActive
)
{
    public int Id { get; set; }
};