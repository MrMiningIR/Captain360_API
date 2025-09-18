using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.Companies.Commands.Create;
using Capitan360.Application.Features.Companies.Companies.Commands.Delete;
using Capitan360.Application.Features.Companies.Companies.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.Companies.Commands.Update;
using Capitan360.Application.Features.Companies.Companies.Dtos;
using Capitan360.Application.Features.Companies.Companies.Queries.GetAll;
using Capitan360.Application.Features.Companies.Companies.Queries.GetById;

namespace Capitan360.Application.Features.Companies.Companies.Services;

public interface ICompanyService
{


    Task<ApiResponse<int>> CreateCompanyAsync(CreateCompanyCommand company, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyDto>>> GetAllCompanies(GetAllCompanyQuery allCompanyQuery,
        CancellationToken cancellationToken);

    Task<ApiResponse<CompanyDto>> GetCompanyByIdAsync(GetCompanyByIdQuery id, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanyAsync(DeleteCompanyCommand id, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyDto>> UpdateCompanyAsync(UpdateCompanyCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyActivityStatus(UpdateActiveStateCompanyCommand command, CancellationToken cancellationToken);
}