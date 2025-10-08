namespace Capitan360.Application.Features.Addresses.Areas.Commands.Create;

public record CreateAreaCommand(
    int? ParentId,
    short LevelId,
    string PersianName,
    string EnglishName,
    string Code,
    decimal Latitude,
    decimal Longitude);