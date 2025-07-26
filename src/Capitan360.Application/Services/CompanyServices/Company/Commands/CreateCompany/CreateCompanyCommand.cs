namespace Capitan360.Application.Services.CompanyServices.Company.Commands.CreateCompany;

public record CreateCompanyCommand(string Code, string Phone, string Name, string Description, int CompanyTypeId, int ProvinceId, int CityId)
{
    public string Code { get; } = Code;
    public string PhoneNumber { get; } = Phone;
    public string Name { get; } = Name;
    public int CompanyTypeId { get; } = CompanyTypeId;
    public bool IsParentCompany { get; set; }
    public bool Active { get; set; }
    public string Description { get; } = Description;
    public int CountryId { get; } = 1;
    public int ProvinceId { get; } = ProvinceId;
    public int CityId { get; } = CityId;
};