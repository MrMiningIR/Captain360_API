namespace Capitan360.Application.Features.Addresses.Areas.Commands.Update;

public record UpdateAreaCommand(
    int Id,
    int? ParentId,
    short? LevelId,
    string? PersianName,
    string? EnglishName,
    string? Code
    //Point? Coordinates
    );