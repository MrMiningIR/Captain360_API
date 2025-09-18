using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.Addresses.Addresses.Commands.Create;

public record CreateAddressCommand(

    string AddressLine,
    string? Mobile,
    string? Tel1,
    string? Tel2,
    string? Zipcode,
    string? Description,
    AddressType AddressType

    );