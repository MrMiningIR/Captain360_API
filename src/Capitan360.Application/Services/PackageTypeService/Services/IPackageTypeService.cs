using Capitan360.Application.Common;
using Capitan360.Application.Services.PackageTypeService.Commands.CreatePackageType;
using Capitan360.Application.Services.PackageTypeService.Commands.DeletePackageType;
using Capitan360.Application.Services.PackageTypeService.Commands.MovePackageTypeDown;
using Capitan360.Application.Services.PackageTypeService.Commands.MovePackageTypeUp;
using Capitan360.Application.Services.PackageTypeService.Commands.UpdateActiveStatePackageType;
using Capitan360.Application.Services.PackageTypeService.Commands.UpdatePackageType;
using Capitan360.Application.Services.PackageTypeService.Dtos;
using Capitan360.Application.Services.PackageTypeService.Queries.GetAllPackageTypes;
using Capitan360.Application.Services.PackageTypeService.Queries.GetPackageTypeById;

namespace Capitan360.Application.Services.PackageTypeService.Services;

public interface IPackageTypeService
{
    Task<ApiResponse<int>> CreatePackageTypeAsync(CreatePackageTypeCommand command,
     CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<PackageTypeDto>>> GetAllPackageTypesAsync(
        GetAllPackageTypesQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<PackageTypeDto>> GetPackageTypeByIdAsync(
        GetPackageTypeByIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeletePackageTypeAsync(DeletePackageTypeCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<PackageTypeDto>> UpdatePackageTypeAsync(UpdatePackageTypeCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<int>> MovePackageTypeUpAsync(MovePackageTypeUpCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<int>> MovePackageTypeDownAsync(
        MovePackageTypeDownCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetPackageTypeActivityStatusAsync(UpdateActiveStatePackageTypeCommand command, CancellationToken cancellationToken);
}