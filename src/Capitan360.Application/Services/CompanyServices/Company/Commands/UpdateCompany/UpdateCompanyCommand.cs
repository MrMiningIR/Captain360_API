namespace Capitan360.Application.Services.CompanyServices.Company.Commands.UpdateCompany;

public record UpdateCompanyCommand(

    int UserCompanyTypeId,
    string Code,//
    string PhoneNumber,
    string Name,
    string Description,
    int CompanyTypeId,
    bool IsParentCompany,
    bool Active,
    int CountryId,//
    int CityId,//
    int ProvinceId,//
    bool UpdateRelatedThings = false
        )
{
    public int Id { get; set; }
}