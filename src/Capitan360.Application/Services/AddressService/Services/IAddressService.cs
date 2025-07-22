using Capitan360.Application.Common;
using Capitan360.Application.Services.AddressService.Commands.AddNewAddressToCompany;
using Capitan360.Application.Services.AddressService.Commands.DeleteAddress;
using Capitan360.Application.Services.AddressService.Commands.MoveAddress;
using Capitan360.Application.Services.AddressService.Commands.UpdateAddress;
using Capitan360.Application.Services.AddressService.Dtos;
using Capitan360.Application.Services.AddressService.Queries.GetAddressById;
using Capitan360.Application.Services.AddressService.Queries.GetAllAddresses;

namespace Capitan360.Application.Services.AddressService.Services;

public interface IAddressService
{
    // Task<ApiResponse<int>> CreateAddressAsync(CreateAddressCommand address, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<AddressDto>>> GetAllAddressesByCompany(GetAllAddressQuery allAddressQuery,
        CancellationToken cancellationToken);

    Task<ApiResponse<AddressDto>> GetAddressByIdAsync(GetAddressByIdQuery id, CancellationToken cancellationToken);

    Task<ApiResponse<object>> DeleteAddressAsync(DeleteAddressCommand id, CancellationToken cancellationToken);

    Task<ApiResponse<AddressDto>> UpdateAddressAsync(UpdateAddressCommand command, CancellationToken cancellationToken);

    //Task<ApiResponse<AddressDto>> UpdateCompanyAddressAsync(UpdateCompanyAddressCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> AddNewAddressToCompanyAsync(AddNewAddressToCompanyCommand addNewAddressToCompanyCommand,
        CancellationToken cancellationToken);

    Task<ApiResponse<object>> MoveAddressUpAsync(MoveAddressUpCommand moveAddressUpCommand,
        CancellationToken cancellationToken);

    Task<ApiResponse<object>> MoveAddressDownAsync(MoveAddressDownCommand moveAddressDownCommand,
        CancellationToken cancellationToken);
}