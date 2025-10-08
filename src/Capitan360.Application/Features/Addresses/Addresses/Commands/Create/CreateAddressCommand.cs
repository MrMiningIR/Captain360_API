namespace Capitan360.Application.Features.Addresses.Addresses.Commands.Create;

public record CreateAddressCommand(
    int? CompanyId,
    string? UserId,
    int CountryId,
    int ProvinceId,
    int CityId,
    int MunicipalAreaId,
    decimal Latitude,
    decimal Longitude,
    string AddressLine,
    string Mobile,
    string Tel1,
    string Tel2,
    string Zipcode,
    string Description,
    bool Active);