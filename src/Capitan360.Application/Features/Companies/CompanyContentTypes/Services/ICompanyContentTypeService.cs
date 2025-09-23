using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveDown;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveUp;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetById;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Services;

public interface ICompanyContentTypeService
{

    Task<ApiResponse<PagedResult<CompanyContentTypeDto>>> GetAllCompanyContentTypesByCompanyAsync(
        GetAllCompanyContentTypesQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveCompanyContentTypeUpAsync(MoveUpCompanyContentTypeCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveContentTypeDownAsync(MoveDownCompanyContentTypeCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<CompanyContentTypeDto>> GetCompanyContentTypeByIdAsync(GetCompanyContentTypeByIdQuery getCompanyContentTypeByIdQuery, CancellationToken cancellationToken);

    //Task<ApiResponse<int>> UpdateCompanyContentTypeNameAsync(UpdateCompanyContentTypeNameCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> SetCompanyContentContentActivityStatusAsync(UpdateActiveStateCompanyContentTypeCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<CompanyContentTypeDto>> UpdateCompanyContentTypeAsync(
        UpdateCompanyContentTypeCommand command, CancellationToken cancellationToken);
}