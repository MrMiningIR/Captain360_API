using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeDown;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeUp;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateActiveStateCompanyPackageType;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageType;
using Capitan360.Application.Services.CompanyPackageTypeService.Dtos;
using Capitan360.Application.Services.CompanyPackageTypeService.Queries.GetAllCompanyPackageTypes;
using Capitan360.Application.Services.CompanyPackageTypeService.Queries.GetCompanyPackageTypeById;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Services;

public interface ICompanyPackageTypeService
{
    Task<ApiResponse<int>> UpdateCompanyPackageTypeAsync(UpdateCompanyPackageTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyPackageTypeDto>>> GetAllCompanyPackageTypesByCompany(
        GetAllCompanyPackageTypesQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<object>> MoveCompanyPackageTypeUpAsync(MoveCompanyPackageTypeUpCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<object>> MoveCompanyPackageTypeDownAsync(MoveCompanyPackageTypeDownCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyPackageTypeDto>> GetCompanyPackageTypeByIdAsync(GetCompanyPackageTypeByIdQuery getCompanyPackageTypeByIdQuery, CancellationToken cancellationToken);

    Task<ApiResponse<int>> UpdateCompanyPackageTypeNameAsync(UpdateCompanyPackageTypeNameCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> SetCompanyPackageContentActivityStatus(UpdateActiveStateCompanyPackageTypeCommand command, CancellationToken cancellationToken);
}