namespace Capitan360.Application.Features.ContentTypes.Commands.Update;

public record UpdateContentTypeCommand(
    string Name,
    string Description,
    bool Active)
{
    public int Id { get; set; }
};