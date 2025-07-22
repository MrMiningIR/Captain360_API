using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.AddressService.Commands.UpdateCompanyAddress;

public record UpdateCompanyAddressCommand(
    int AddressId,

    string? AddressLine,
    string? Mobile,
    string? Tel1,
    string? Tel2,
    string? Zipcode,
    string? Description,
    double Latitude,
    double Longitude,
    bool Active,
    AddressType? AddressType

)
{
}