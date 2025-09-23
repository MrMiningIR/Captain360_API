using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Entities.CompanyManifestForms;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Application.Features.Addresses.Areas.Dtos;

public class AreaDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string? ParentName { get; set; }
    public short LevelId { get; set; }
    public string? LevelName { get; set; }
    public string PersianName { get; set; } = default!;
    public string EnglishName { get; set; } = default!;
    public string Code { get; set; } = default!;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}
