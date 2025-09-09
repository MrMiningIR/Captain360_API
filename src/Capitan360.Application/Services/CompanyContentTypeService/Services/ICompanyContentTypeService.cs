using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeDown;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeUp;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateActiveStateCompanyContentType;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentTypeCommand;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentTypeNameAndDescription;
using Capitan360.Application.Services.CompanyContentTypeService.Dtos;
using Capitan360.Application.Services.CompanyContentTypeService.Queries.GetAllCompanyContentTypes;
using Capitan360.Application.Services.CompanyContentTypeService.Queries.GetCompanyContentTypeById;

namespace Capitan360.Application.Services.CompanyContentTypeService.Services;

public interface ICompanyContentTypeService
{

    Task<ApiResponse<PagedResult<CompanyContentTypeDto>>> GetAllCompanyContentTypesByCompanyAsync(
        GetAllCompanyContentTypesQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveCompanyContentTypeUpAsync(MoveCompanyContentTypeUpCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveContentTypeDownAsync(MoveCompanyContentTypeDownCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<CompanyContentTypeDto>> GetCompanyContentTypeByIdAsync(GetCompanyContentTypeByIdQuery getCompanyContentTypeByIdQuery, CancellationToken cancellationToken);

    Task<ApiResponse<int>> UpdateCompanyContentTypeNameAsync(UpdateCompanyContentTypeNameCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> SetCompanyContentContentActivityStatusAsync(UpdateActiveStateCompanyContentTypeCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<CompanyContentTypeDto>> UpdateCompanyContentTypeAsync(
        UpdateCompanyContentTypeCommand command, CancellationToken cancellationToken);
}