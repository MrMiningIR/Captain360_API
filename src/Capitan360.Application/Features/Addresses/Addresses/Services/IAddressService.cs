using Capitan360.Application.Common;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Create;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Delete;
using Capitan360.Application.Features.Addresses.Addresses.Commands.MoveDown;
using Capitan360.Application.Features.Addresses.Addresses.Commands.MoveUp;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Update;
using Capitan360.Application.Features.Addresses.Addresses.Commands.UpdateActiveState;
using Capitan360.Application.Features.Addresses.Addresses.Dtos;
using Capitan360.Application.Features.Addresses.Addresses.Queries.GetAll;
using Capitan360.Application.Features.Addresses.Addresses.Queries.GetByCompanyId;
using Capitan360.Application.Features.Addresses.Addresses.Queries.GetById;
using Capitan360.Application.Features.Addresses.Addresses.Queries.GetByUserId;

namespace Capitan360.Application.Features.Addresses.Addresses.Services;

public interface IAddressService
{
    Task<ApiResponse<int>> CreateAddressAsync(CreateAddressCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteAddressAsync(DeleteAddressCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveUpAddressAsync(MoveUpAddressCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveDownAddressAsync(MoveDownAddressCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetAddressActivityStatusAsync(UpdateActiveStateAddressCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<AddressDto>>> GetAllAddresssAsync(GetAllAddressQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<IReadOnlyList<AddressDto>>> GetAddressByCompanyIdAsync(GetAddressByCompanyIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<IReadOnlyList<AddressDto>>> GetAddressByUserIdAsync(GetAddressByUserIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<AddressDto>> GetAddressByIdAsync(GetAddressByIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<AddressDto>> UpdateAddressAsync(UpdateAddressCommand command, CancellationToken cancellationToken);
}