using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyInsurances.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyInsurances.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyInsurances.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyInsurances.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyInsurances.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyInsurances.Queries.GetByCompanyId;
using Capitan360.Application.Features.Companies.CompanyInsurances.Dtos;
using Capitan360.Application.Features.Companies.CompanyInsurances.Queries.GetById;

namespace Capitan360.Application.Features.Companies.CompanyInsurances.Services;

public interface ICompanyInsuranceService
{
    Task<ApiResponse<int>> CreateCompanyInsuranceAsync(CreateCompanyInsuranceCommand createCompanyInsuranceCommand, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanyInsuranceAsync(DeleteCompanyInsuranceCommand deleteCompanyInsuranceCommand, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyInsuranceActivityStatusAsync(UpdateActiveStateCompanyInsuranceCommand updateActiveStateCompanyInsuranceCommand, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyInsuranceDto>> UpdateCompanyInsuranceAsync(UpdateCompanyInsuranceCommand updateCompanyInsuranceCommand, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyInsuranceDto>>> GetAllCompanyInsurancesAsync(GetAllCompanyInsurancesQuery getAllCompanyInsurancesQuery, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyInsuranceDto>> GetCompanyInsuranceByCompanyIdAsync(GetCompanyInsuranceByCompanyIdQuery getCompanyInsuranceByCompanyIdQuery, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyInsuranceDto>> GetCompanyInsuranceByIdAsync(GetCompanyInsuranceByIdQuery getCompanyInsuranceByIdQuery, CancellationToken cancellationToken);
}