using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Dtos;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Queries.GetByCompanyId;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Queries.GetById;

namespace Capitan360.Application.Features.Companies.CompanyCommissionses.Services;

public interface ICompanyCommissionsService
{
    Task<ApiResponse<int>> CreateCompanyCommissionsAsync(CreateCompanyCommissionsCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanyCommissionsAsync(DeleteCompanyCommissionsCommand command, CancellationToken cancellationToken);
    
    Task<ApiResponse<CompanyCommissionsDto>> UpdateCompanyCommissionsAsync(UpdateCompanyCommissionsCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyCommissionsDto>>> GetAllCompanyCommissionsAsync(GetAllCompanyCommissionsQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyCommissionsDto>> GetCompanyCommissionsByCompanyIdAsync(GetCompanyCommissionsByCompanyIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyCommissionsDto>> GetCompanyCommissionsByIdAsync(GetCompanyCommissionsByIdQuery query, CancellationToken cancellationToken);
}