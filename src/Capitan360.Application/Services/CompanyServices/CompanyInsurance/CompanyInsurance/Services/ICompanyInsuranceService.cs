using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.CreateCompanyInsurance;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.DeleteCompanyInsurance;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.UpdateCompanyInsurance;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Queries.GetAllCompanyInsurances;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.Dtos;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Services;

public interface ICompanyInsuranceService
{
    Task<ApiResponse<int>> CreateCompanyInsuranceAsync(CreateCompanyInsuranceCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<PagedResult<CompanyInsuranceDto>>> GetAllCompanyInsurances(GetAllCompanyInsurancesQuery query, CancellationToken cancellationToken);
    Task<ApiResponse<CompanyInsuranceDto>> GetCompanyInsuranceByIdAsync(GetCompanyInsuranceByIdQuery query, CancellationToken cancellationToken);
    Task<ApiResponse<object>> DeleteCompanyInsuranceAsync(DeleteCompanyInsuranceCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> UpdateCompanyInsuranceAsync(UpdateCompanyInsuranceCommand command, CancellationToken cancellationToken);
}