namespace Capitan360.Application.Services.AddressService.Dtos;

public class AreaDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public short LevelId { get; set; }
    public string PersianName { get; set; } = default!;
    public string? EnglishName { get; set; }
    public string Code { get; set; } = default!;
    // public Point? Coordinates { get; set; }
}