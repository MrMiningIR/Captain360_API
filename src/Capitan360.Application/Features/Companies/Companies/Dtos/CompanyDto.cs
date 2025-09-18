using Capitan360.Domain.Entities.Addresses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Application.Features.Companies.Companies.Dtos;

public class CompanyDto
{
    public int Id { get; set; }
    public string Code { get; set; } = default!;
    public string MobileCounter { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int CompanyTypeId { get; set; }
    public string? CompanyTypeName { get; set; }
    public bool IsParentCompany { get; set; }
    public bool Active { get; set; }
    public int CountryId { get; set; }
    public string? CountryName { get; set; }
    public int ProvinceId { get; set; }
    public string? ProvinceName { get; set; }
    public int CityId { get; set; }
    public string? CityName { get; set; }
    public string Description { get; set; } = default!;
}