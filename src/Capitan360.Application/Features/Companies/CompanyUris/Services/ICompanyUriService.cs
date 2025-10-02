using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.UpdateCaptain360UriState;
using Capitan360.Application.Features.Companies.CompanyUris.Dtos;
using Capitan360.Application.Features.Companies.CompanyUris.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyUris.Queries.GetByCompanyId;
using Capitan360.Application.Features.Companies.CompanyUris.Queries.GetById;

namespace Capitan360.Application.Features.Companies.CompanyUris.Services;

public interface ICompanyUriService
{
    Task<ApiResponse<int>> CreateCompanyUriAsync(CreateCompanyUriCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanyUriAsync(DeleteCompanyUriCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyUriActivityStatusAsync(UpdateActiveStateCompanyUriCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyUriCaptain360UriStatusAsync(UpdateCaptain360UriStateCompanyUriCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyUriDto>> UpdateCompanyUriAsync(UpdateCompanyUriCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyUriDto>>> GetAllCompanyUrisAsync(GetAllCompanyUrisQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<IReadOnlyList<CompanyUriDto>>> GetCompanyUriByCompanyIdAsync(GetCompanyUriByCompanyIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyUriDto>> GetCompanyUriByIdAsync(GetCompanyUriByIdQuery query, CancellationToken cancellationToken);
}