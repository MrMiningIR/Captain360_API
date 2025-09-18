using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyInsuranceCharges.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyInsuranceCharges.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyInsuranceCharges.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyInsuranceCharges.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyInsuranceCharges.Queries.GetById;
using Capitan360.Application.Features.Companies.CompanyInsurances.Dtos;

namespace Capitan360.Application.Features.Companies.CompanyInsuranceCharges.Services;

public interface ICompanyInsuranceChargeService
{
    Task<ApiResponse<List<int>>> CreateCompanyInsuranceChargeAsync(CreateCompanyInsuranceChargeListCommand? command,
        CancellationToken cancellationToken);
    Task<ApiResponse<PagedResult<CompanyInsuranceChargeDto>>> GetAllCompanyInsuranceCharges(GetAllCompanyInsuranceChargesQuery query, CancellationToken cancellationToken);
    Task<ApiResponse<CompanyInsuranceChargeDto>> GetCompanyInsuranceChargeByIdAsync(GetCompanyInsuranceChargeByIdQuery query, CancellationToken cancellationToken);
    Task<ApiResponse<object>> DeleteCompanyInsuranceChargeAsync(DeleteCompanyInsuranceChargeCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<List<int>>> UpdateCompanyInsuranceChargeAsync(UpdateCompanyInsuranceChargeListCommand? command, CancellationToken cancellationToken);

    Task<ApiResponse<List<CompanyInsuranceChargeService.CompanyInsuranceChargeTableDataDto>>> GetTableDataAAsync(
        CompanyInsuranceChargeService.GetCompanyInsuranceChargeTableDataQuery query,
        CancellationToken cancellationToken);
}