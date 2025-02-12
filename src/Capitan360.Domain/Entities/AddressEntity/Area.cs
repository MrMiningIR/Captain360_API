using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Capitan360.Domain.Abstractions;
using NetTopologySuite.Geometries;

namespace Capitan360.Domain.Entities.AddressEntity;

public class Area : Entity
{

    public int? ParentId { get; set; }

    public short LevelId { get; set; }

    public string PersianName { get; set; } = default!;

    public string? EnglishName { get; set; }

    public string Code { get; set; } = default!;
    public Point? Coordinates { get; set; }

    public  Area Parent { get; set; } = null!;
    public virtual ICollection<Area> Children { get; set; } = [];


}
