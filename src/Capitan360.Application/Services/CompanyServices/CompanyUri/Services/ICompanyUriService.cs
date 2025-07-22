using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.CreateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.DeleteCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.UpdateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Queries.GetAllCompanyUris;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Queries.GetCompanyUriById;

namespace Capitan360.Application.Services.CompanyServices.CompanyUri.Services;

public interface ICompanyUriService
{
    Task<ApiResponse<int>> CreateCompanyUriAsync(CreateCompanyUriCommand companyUri, CancellationToken cancellationToken);
    Task<ApiResponse<PagedResult<CompanyUriDto>>> GetAllCompanyUris(GetAllCompanyUrisQuery allCompanyUrisQuery, CancellationToken cancellationToken);
    Task<ApiResponse<CompanyUriDto>> GetCompanyUriByIdAsync(GetCompanyUriByIdQuery id, CancellationToken cancellationToken);
    Task<ApiResponse<object>> DeleteCompanyUriAsync(DeleteCompanyUriCommand id, CancellationToken cancellationToken);
    Task<ApiResponse<int>> UpdateCompanyUriAsync(UpdateCompanyUriCommand command, CancellationToken cancellationToken);


}

