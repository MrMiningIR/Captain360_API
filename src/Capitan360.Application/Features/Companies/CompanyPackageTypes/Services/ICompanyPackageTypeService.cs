using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveDown;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveUp;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetById;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Services;

public interface ICompanyPackageTypeService
{
    Task<ApiResponse<PagedResult<CompanyPackageTypeDto>>> GetAllCompanyPackageTypesByCompanyAsync(
    GetAllCompanyPackageTypesQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveCompanyPackageTypeUpAsync(MoveUpCompanyPackageTypeCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> MoveCompanyPackageTypeDownAsync(MoveDownCompanyPackageTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyPackageTypeDto>> GetCompanyPackageTypeByIdAsync(GetCompanyPackageTypeByIdQuery query,
        CancellationToken cancellationToken);

    //Task<ApiResponse<int>> UpdateCompanyPackageTypeNameAsync(UpdateCompanyPackageTypeNameAndDescriptionCommand command,
    //    CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyPackageContentActivityStatusAsync(
        UpdateActiveStateCompanyPackageTypeCommand command,
        CancellationToken cancellationToken);
}