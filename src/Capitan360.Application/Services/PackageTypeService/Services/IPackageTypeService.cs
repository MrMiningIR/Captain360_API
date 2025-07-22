using Capitan360.Application.Common;
using Capitan360.Application.Services.PackageTypeService.Commands.CreatePackageType;
using Capitan360.Application.Services.PackageTypeService.Commands.DeletePackageType;
using Capitan360.Application.Services.PackageTypeService.Commands.MovePackageTypeDown;
using Capitan360.Application.Services.PackageTypeService.Commands.MovePackageTypeUp;
using Capitan360.Application.Services.PackageTypeService.Commands.UpdatePackageType;
using Capitan360.Application.Services.PackageTypeService.Dtos;
using Capitan360.Application.Services.PackageTypeService.Queries.GetAllPackageTypes;
using Capitan360.Application.Services.PackageTypeService.Queries.GetPackageTypeById;

namespace Capitan360.Application.Services.PackageTypeService.Services;

public interface IPackageTypeService
{
    Task<ApiResponse<int>> CreatePackageTypeAsync(CreatePackageTypeCommand packageType, CancellationToken cancellationToken);
    Task<ApiResponse<PagedResult<PackageTypeDto>>> GetAllPackageTypes(GetAllPackageTypesQuery allPackageTypesQuery, CancellationToken cancellationToken);
    Task<ApiResponse<PackageTypeDto>> GetPackageTypeByIdAsync(GetPackageTypeByIdQuery id, CancellationToken cancellationToken);
    Task<ApiResponse<object>> DeletePackageTypeAsync(DeletePackageTypeCommand id, CancellationToken cancellationToken);
    Task<ApiResponse<PackageTypeDto>> UpdatePackageTypeAsync(UpdatePackageTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<object>> MovePackageTypeUpAsync(MovePackageTypeUpCommand movePackageTypeUpCommand, CancellationToken cancellationToken);
    Task<ApiResponse<object>> MovePackageTypeDownAsync(MovePackageTypeDownCommand movePackageTypeDownCommand, CancellationToken cancellationToken);
}