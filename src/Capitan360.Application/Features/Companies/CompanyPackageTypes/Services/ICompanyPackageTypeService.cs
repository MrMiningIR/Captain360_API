using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveTypeDown;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveUp;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateNameAndDescription;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetById;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Services;

public interface ICompanyPackageTypeService
{
    Task<ApiResponse<PagedResult<CompanyPackageTypeDto>>> GetAllCompanyPackageTypesByCompanyAsync(
    GetAllCompanyPackageTypesQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveCompanyPackageTypeUpAsync(MoveCompanyPackageTypeUpCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> MoveCompanyPackageTypeDownAsync(MoveCompanyPackageTypeDownCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyPackageTypeDto>> GetCompanyPackageTypeByIdAsync(GetCompanyPackageTypeByIdQuery query,
        CancellationToken cancellationToken);

    Task<ApiResponse<int>> UpdateCompanyPackageTypeNameAsync(UpdateCompanyPackageTypeNameAndDescriptionCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyPackageContentActivityStatusAsync(
        UpdateActiveStateCompanyPackageTypeCommand command,
        CancellationToken cancellationToken);
}