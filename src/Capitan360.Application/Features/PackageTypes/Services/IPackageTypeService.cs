using Capitan360.Application.Common;
using Capitan360.Application.Features.PackageTypeService.Commands.CreatePackageType;
using Capitan360.Application.Features.PackageTypeService.Commands.DeletePackageType;
using Capitan360.Application.Features.PackageTypeService.Commands.MoveTypeDown;
using Capitan360.Application.Features.PackageTypeService.Commands.MoveUp;
using Capitan360.Application.Features.PackageTypeService.Commands.UpdateActiveState;
using Capitan360.Application.Features.PackageTypeService.Commands.Update;
using Capitan360.Application.Features.PackageTypeService.Queries.GetAll;
using Capitan360.Application.Features.PackageTypeService.Queries.GetById;
using Capitan360.Application.Features.PackageTypes.Dtos;

namespace Capitan360.Application.Features.PackageTypeService.Services;

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