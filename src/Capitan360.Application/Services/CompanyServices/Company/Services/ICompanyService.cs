using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.Company.Commands.CreateCompany;
using Capitan360.Application.Services.CompanyServices.Company.Commands.DeleteCompany;
using Capitan360.Application.Services.CompanyServices.Company.Commands.UpdateActiveStateCompany;
using Capitan360.Application.Services.CompanyServices.Company.Commands.UpdateCompany;
using Capitan360.Application.Services.CompanyServices.Company.Dtos;
using Capitan360.Application.Services.CompanyServices.Company.Queries.GetAllCompanies;
using Capitan360.Application.Services.CompanyServices.Company.Queries.GetCompanyById;

namespace Capitan360.Application.Services.CompanyServices.Company.Services;

public interface ICompanyService
{
    Task<ApiResponse<int>> CreateCompanyAsync(CreateCompanyCommand company, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyDto>>> GetAllCompanies(GetAllCompanyQuery allCompanyQuery,
        CancellationToken cancellationToken);

    Task<ApiResponse<CompanyDto>> GetCompanyByIdAsync(GetCompanyByIdQuery id, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanyAsync(DeleteCompanyCommand id, CancellationToken cancellationToken);

    Task<ApiResponse<int>> UpdateCompanyAsync(UpdateCompanyCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyActivityStatus(UpdateActiveStateCompanyCommand command, CancellationToken cancellationToken);
}