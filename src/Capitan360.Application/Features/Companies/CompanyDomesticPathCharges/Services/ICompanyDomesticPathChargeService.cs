using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Dtos;
using Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Dtos;
using Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Queries;
using Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Queries.GetById;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Services;

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