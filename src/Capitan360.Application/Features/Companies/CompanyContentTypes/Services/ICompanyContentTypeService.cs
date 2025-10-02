using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveDown;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveUp;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateName;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetByCompanyId;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetById;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Services;

public interface ICompanyContentTypeService
{
    Task<ApiResponse<int>> MoveUpCompanyContentTypeAsync(MoveUpCompanyContentTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveDownCompanyContentTypeAsync(MoveDownCompanyContentTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyContentTypeActivityStatusAsync(UpdateActiveStateCompanyContentTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> UpdateCompanyContentTypeNameAsync(UpdateCompanyContentTypeNameCommand command, CancellationToken cancellationToken);
 
    Task<ApiResponse<CompanyContentTypeDto>> UpdateCompanyContentTypeAsync(UpdateCompanyContentTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyContentTypeDto>>> GetAllCompanyContentTypesAsync(GetAllCompanyContentTypesQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<IReadOnlyList<CompanyContentTypeDto>>> GetCompanyContentTypeByCompanyIdAsync(GetCompanyContentTypeByCompanyIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyContentTypeDto>> GetCompanyContentTypeByIdAsync(GetCompanyContentTypeByIdQuery query, CancellationToken cancellationToken);
}