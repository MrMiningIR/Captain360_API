using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveDown;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveUp;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateDescription;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateName;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetByCompanyId;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetById;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Services;

public interface ICompanyPackageTypeService
{
    Task<ApiResponse<int>> MoveUpCompanyPackageTypeAsync(MoveUpCompanyPackageTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveDownCompanyPackageTypeAsync(MoveDownCompanyPackageTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyPackageTypeActivityStatusAsync(UpdateActiveStateCompanyPackageTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> UpdateCompanyPackageTypeNameAsync(UpdateCompanyPackageTypeNameCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyPackageTypeDto>> UpdateCompanyPackageTypeAsync(UpdateCompanyPackageTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyPackageTypeDto>>> GetAllCompanyPackageTypesAsync(GetAllCompanyPackageTypesQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<IReadOnlyList<CompanyPackageTypeDto>>> GetCompanyPackageTypeByCompanyIdAsync(GetCompanyPackageTypeByCompanyIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyPackageTypeDto>> GetCompanyPackageTypeByIdAsync(GetCompanyPackageTypeByIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<int>> UpdateCompanyPackageTypeDescriptionAsync(UpdateCompanyPackageTypeDescriptionCommand command,
    CancellationToken cancellationToken);
}