using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyUri.Commands.CreateCompanyUri;
using Capitan360.Application.Features.Companies.CompanyUri.Commands.DeleteCompanyUri;
using Capitan360.Application.Features.Companies.CompanyUri.Commands.UpdateActiveStateCompanyUri;
using Capitan360.Application.Features.Companies.CompanyUri.Commands.UpdateCompanyUri;
using Capitan360.Application.Features.Companies.CompanyUri.Dtos;
using Capitan360.Application.Features.Companies.CompanyUri.Queries.GetAllCompanyUris;
using Capitan360.Application.Features.Companies.CompanyUri.Queries.GetCompanyUriById;

namespace Capitan360.Application.Features.Companies.CompanyUri.Services;

public interface ICompanyUriService
{
    Task<ApiResponse<int>> CreateCompanyUriAsync(CreateCompanyUriCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<PagedResult<CompanyUriDto>>> GetAllCompanyUrisAsync(GetAllCompanyUrisQuery allCompanyUrisQuery, CancellationToken cancellationToken);
    Task<ApiResponse<CompanyUriDto>> GetCompanyUriByIdAsync(GetCompanyUriByIdQuery query, CancellationToken cancellationToken);
    Task<ApiResponse<int>> DeleteCompanyUriAsync(DeleteCompanyUriCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<CompanyUriDto>> UpdateCompanyUriAsync(UpdateCompanyUriCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> SetCompanyUriActivityStatusAsync(UpdateActiveStateCompanyUriCommand command, CancellationToken cancellationToken);

}

