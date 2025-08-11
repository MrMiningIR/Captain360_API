using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeDown;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeUp;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateActiveStateCompanyPackageType;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageTypeNameAndDescription;
using Capitan360.Application.Services.CompanyPackageTypeService.Dtos;
using Capitan360.Application.Services.CompanyPackageTypeService.Queries.GetAllCompanyPackageTypes;
using Capitan360.Application.Services.CompanyPackageTypeService.Queries.GetCompanyPackageTypeById;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Services;

public interface ICompanyPackageTypeService
{
    Task<ApiResponse<PagedResult<CompanyPackageTypeDto>>> GetAllCompanyPackageTypesByCompanyAsync(
    GetAllCompanyPackageTypesQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveCompanyPackageTypeUpAsync(MoveCompanyPackageTypeUpCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> MoveCompanyPackageTypeDownAsync(MoveCompanyPackageTypeDownCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyPackageTypeDto>> GetCompanyPackageTypeByIdAsync(GetCompanyPackageTypeByIdQuery query,
        CancellationToken cancellationToken);

    Task<ApiResponse<int>> UpdateCompanyPackageTypeNameAndDescriptionAsync(UpdateCompanyPackageTypeNameAndDescriptionCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyPackageContentActivityStatusAsync(
        UpdateActiveStateCompanyPackageTypeCommand command,
        CancellationToken cancellationToken);
}