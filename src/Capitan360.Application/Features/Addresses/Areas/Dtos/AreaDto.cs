using Capitan360.Domain.Entities.Addresses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Application.Features.Addresses.Areas.Dtos;

public class AreaDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string? ParentName { get; set; }
    public short LevelId { get; set; }
    public string PersianName { get; set; } = default!;
    public string EnglishName { get; set; } = default!;
    public string Code { get; set; } = default!;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}


