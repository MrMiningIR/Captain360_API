using Capitan360.Application.Common;
using Capitan360.Application.Features.PackageTypes.Commands.Create;
using Capitan360.Application.Features.PackageTypes.Commands.Delete;
using Capitan360.Application.Features.PackageTypes.Commands.MoveDown;
using Capitan360.Application.Features.PackageTypes.Commands.MoveUp;
using Capitan360.Application.Features.PackageTypes.Commands.Update;
using Capitan360.Application.Features.PackageTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.PackageTypes.Dtos;
using Capitan360.Application.Features.PackageTypes.Queries.GetAll;
using Capitan360.Application.Features.PackageTypes.Queries.GetById;

namespace Capitan360.Application.Features.PackageTypes.Services;

public interface IPackageTypeService
{
    Task<ApiResponse<int>> CreatePackageTypeAsync(CreatePackageTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeletePackageTypeAsync(DeletePackageTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveUpPackageTypeAsync(MoveUpPackageTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveDownPackageTypeAsync(MoveDownPackageTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetPackageTypeActivityStatusAsync(UpdateActiveStatePackageTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PackageTypeDto>> UpdatePackageTypeAsync(UpdatePackageTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<PackageTypeDto>>> GetAllPackageTypesAsync(GetAllPackageTypesQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<PackageTypeDto>> GetPackageTypeByIdAsync(GetPackageTypeByIdQuery query, CancellationToken cancellationToken);
}