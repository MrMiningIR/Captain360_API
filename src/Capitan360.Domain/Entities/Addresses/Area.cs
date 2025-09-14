using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Entities.Addresses;

public class Area : BaseEntity
{

    public int? ParentId { get; set; }

    public short LevelId { get; set; }
    public AreaType Level { get; set; }

    public string PersianName { get; set; } = default!;

    public string? EnglishName { get; set; }

    public string Code { get; set; } = default!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Area Parent { get; set; } = null!;
    public virtual ICollection<Area> Children { get; set; } = [];


}
