namespace Capitan360.Application.Services.CompanyServices.Company.Commands.CreateCompany;

public record CreateCompanyCommand(
    string Code,
    string MobileCounter,
    string Name,
    string Description,
    int CompanyTypeId,
    int CountryId,
    int ProvinceId,
    int CityId,
    bool IsParentCompany,
    bool Active);
