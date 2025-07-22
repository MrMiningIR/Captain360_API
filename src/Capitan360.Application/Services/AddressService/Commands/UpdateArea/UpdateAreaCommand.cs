namespace Capitan360.Application.Services.AddressService.Commands.UpdateArea;

public record UpdateAreaCommand(
    int Id,
    int? ParentId,
    short? LevelId,
    string? PersianName,
    string? EnglishName,
    string? Code
    //Point? Coordinates
    );