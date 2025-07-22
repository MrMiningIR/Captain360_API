namespace Capitan360.Application.Services.AddressService.Commands.AddNewAddressToCompany;

public record AddNewAddressToCompanyCommand(

    string AddressLine,
    string? Mobile,
    string? Tel1,
    string? Tel2,
    string? Zipcode,
    string? Description,
    double? Latitude,
    double? Longitude,
    int Active,
    int UserCompanyTypeId



)
{
    public int CompanyId { get; set; }
};