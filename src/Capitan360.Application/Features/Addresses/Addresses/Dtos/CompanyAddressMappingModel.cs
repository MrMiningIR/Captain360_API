namespace Capitan360.Application.Features.Addresses.Addresses.Dtos;

public record CompanyAddressMappingModel(
    int AddressId,
    int CompanyId,
    bool Active,
    int OrderAddress);