using Capitan360.Application.Common;
using Capitan360.Application.Features.Addresses.Addresses.Commands.AddNewAddressToCompany;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Delete;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Move;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Update;
using Capitan360.Application.Features.Addresses.Addresses.Dtos;
using Capitan360.Application.Features.Addresses.Addresses.Queries.GetAll;
using Capitan360.Application.Features.Addresses.Addresses.Queries.GetById;

namespace Capitan360.Application.Features.Addresses.Addresses.Services;

public interface IAddressService
{
    // Task<ApiResponse<int>> CreateAddressAsync(CreateAddressCommand address, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<AddressDto>>> GetAllAddressesByCompany(GetAllAddressQuery query,
        CancellationToken cancellationToken);

    Task<ApiResponse<AddressDto>> GetAddressByIdAsync(GetAddressByIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<object>> DeleteAddressAsync(DeleteAddressCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<AddressDto>> UpdateAddressAsync(UpdateAddressCommand command, CancellationToken cancellationToken);

    //Task<ApiResponse<AddressDto>> UpdateCompanyAddressAsync(UpdateCompanyAddressCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> AddNewAddressToCompanyAsync(AddNewAddressToCompanyCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<object>> MoveAddressUpAsync(MoveAddressUpCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<object>> MoveAddressDownAsync(MoveAddressDownCommand command,
        CancellationToken cancellationToken);
}