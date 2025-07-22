namespace Capitan360.Application.Services.AddressService.Dtos;

public record CompanyAddressMappingModel(
    int AddressId,
    int CompanyId,
    bool Active,
    int OrderAddress);