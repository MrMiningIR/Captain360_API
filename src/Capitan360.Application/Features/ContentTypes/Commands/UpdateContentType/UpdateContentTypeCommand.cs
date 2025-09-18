namespace Capitan360.Application.Features.ContentTypeService.Commands.Update;

public record UpdateContentTypeCommand(
    string ContentTypeName,
    string? ContentTypeDescription,
    bool ContentTypeActive
)
{
    public int Id { get; set; }
};