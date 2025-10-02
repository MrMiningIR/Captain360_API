using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Create;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Delete;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.UpdateActiveState;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Dtos;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Queries.GetAll;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Queries.GetByCompanyId;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Queries.GetById;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Services;

public interface ICompanyDomesticPathService
{
    Task<ApiResponse<int>> CreateCompanyDomesticPathAsync(CreateCompanyDomesticPathCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanyDomesticPathAsync(DeleteCompanyDomesticPathCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyDomesticPathActivityStatusAsync(UpdateActiveStateCompanyDomesticPathCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyDomesticPathDto>> UpdateCompanyDomesticPathAsync(UpdateCompanyDomesticPathCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyDomesticPathDto>>> GetAllCompanyDomesticPathsAsync(GetAllCompanyDomesticPathsQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<IReadOnlyList<CompanyDomesticPathDto>>> GetCompanyDomesticPathByCompanyIdAsync(GetCompanyDomesticPathByCompanyIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyDomesticPathDto>> GetCompanyDomesticPathByIdAsync(GetCompanyDomesticPathByIdQuery query, CancellationToken cancellationToken);
}