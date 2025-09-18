namespace Capitan360.Application.Features.Addresses.Areas.Commands.Create;

public record CreateAreaCommand(
    int? ParentId,
    short LevelId,
    string PersianName,
    string? EnglishName,
    string Code

    )
{
    public int? ParentId { get; } = ParentId;
    public short LevelId { get; } = LevelId;
    public string PersianName { get; } = PersianName;
    public string? EnglishName { get; } = EnglishName;
    public string Code { get; } = Code;
}