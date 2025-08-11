using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeDown;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeUp;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateActiveStateCompanyContentType;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentTypeNameAndDescription;
using Capitan360.Application.Services.CompanyContentTypeService.Dtos;
using Capitan360.Application.Services.CompanyContentTypeService.Queries.GetAllCompanyContentTypes;
using Capitan360.Application.Services.CompanyContentTypeService.Queries.GetCompanyContentTypeById;

namespace Capitan360.Application.Services.CompanyContentTypeService.Services;

public interface ICompanyContentTypeService
{

    Task<ApiResponse<PagedResult<CompanyContentTypeDto>>> GetAllCompanyContentTypesByCompany(
        GetAllCompanyContentTypesQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveContentTypeUpAsync(MoveCompanyContentTypeUpCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveContentTypeDownAsync(MoveCompanyContentTypeDownCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<CompanyContentTypeDto>> GetCompanyContentTypeByIdAsync(GetCompanyContentTypeByIdQuery getCompanyContentTypeByIdQuery, CancellationToken cancellationToken);

    Task<ApiResponse<int>> UpdateCompanyContentTypeNameAndDescriptionAsync(UpdateCompanyContentTypeNameAndDescriptionCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> SetCompanyContentActivityStatus(UpdateActiveStateCompanyContentTypeCommand command,
        CancellationToken cancellationToken);
}