namespace Capitan360.Application.Features.Companies.Companies.Commands.Create;

public record CreateCompanyCommand(
    string Code, 
    string MobileCounter, 
    string Name, 
    int CompanyTypeId,
    bool IsParentCompany,
    bool Active,
    int CountryId,
    int ProvinceId, 
    int CityId,
    string Description);