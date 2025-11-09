namespace Capitan360.Application.Features.Addresses.Addresses.Dtos;

public class AddressDto
{
    public int Id { get; set; }
    public int? CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public string? UserId { get; set; }
    public string? UserNameFamily { get; set; }
    public int CountryId { get; set; }
    public string? CountryName { get; set; }
    public int ProvinceId { get; set; }
    public string? ProvinceName { get; set; }
    public int CityId { get; set; }
    public string? CityName { get; set; }
    public int MunicipalAreaId { get; set; }
    public string? MunicipalAreaName { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string AddressLine { get; set; } = default!;
    public string Mobile { get; set; } = default!;
    public string Tel1 { get; set; } = default!;
    public string Tel2 { get; set; } = default!;
    public string Zipcode { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool Active { get; set; }
    public int Order { get; set; }
    public bool IsCompanyAddress { get; set; }
}