namespace Capitan360.Application.Services.CompanyServices.Company.Dtos;

public class CompanyDto
{
    public int Id { get; set; }
    public string Code { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int CompanyTypeId { get; set; }
    public string CompanyTypeName { get; set; } = default!;
    public bool Admin { get; set; }
    public bool Active { get; set; }
    public string Description { get; set; } = default!;

    public int CountryId { get; set; }
    public int ProvinceId { get; set; }
    public int CityId { get; set; }
}