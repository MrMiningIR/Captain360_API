using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Commands.CreateCompanyDomesticPathCharge;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Commands.UpdateCompanyDomesticPathCharge;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Queries.GetAllCompanyDomesticPathCharge;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Create;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Delete;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Queries;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Services;

public interface ICompanyDomesticPathChargeService
{

    Task<ApiResponse<List<int>>> CreateCompanyDomesticPathCharge(CreateCompanyDomesticPathChargeCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<List<int>>> UpdateCompanyDomesticPathCharge(UpdateCompanyDomesticPathChargeCommand command, CancellationToken cancellationToken);
    //  Task<ApiResponse<List<int>>> UpdateCompanyDomesticPathStructPriceAsync(UpdateCompanyDomesticPathStructPriceCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> CreateCompanyDomesticPathStructPriceAsync(CreateCompanyDomesticPathStructPriceCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<PagedResult<CompanyDomesticPathChargeDto>>> GetAllCompanyDomesticPathCharge(GetAllCompanyDomesticPathChargeQuery query, CancellationToken cancellationToken);
    Task<ApiResponse<CompanyDomesticPathStructPriceDto>> GetCompanyDomesticPathStructPriceByIdAsync(GetCompanyDomesticPathStructPriceByIdQuery query, CancellationToken cancellationToken);
    Task<ApiResponse<object>> DeleteCompanyDomesticPathStructPriceAsync(DeleteCompanyDomesticPathStructPriceCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<List<CompanyDomesticPathChargeService.PathChargeTableDataDto>>> GetTableDataAAsync(
        CompanyDomesticPathChargeService.GetCompanyDomesticPathChargeTableDataQuery query,
        CancellationToken cancellationToken);

}


//-----------------------

// Create | SubItem

#region MyRegion

#endregion



// Update

// Update| SubItem