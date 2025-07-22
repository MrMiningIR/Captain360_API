using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.CreateCompanyInsuranceCharge;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.DeleteCompanyInsuranceCharge;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.UpdateCompanyInsuranceCharge;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Queries.GetAllCompanyInsuranceCharges;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Queries.GetCompanyInsuranceChargeById;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.Dtos;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Services;

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