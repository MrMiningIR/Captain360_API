using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.CreateCompanyDomesticPath;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.DeleteCompanyDomesticPath;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.UpdateCompanyDomesticPath;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Queries.GetAllCompanyDomesticPaths;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Queries.GetCompanyDomesticPathById;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Services;

public interface ICompanyDomesticPathsService
{
    Task<ApiResponse<int>> CreateCompanyDomesticPathAsync(CreateCompanyDomesticPathCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<PagedResult<CompanyDomesticPathDto>>> GetAllCompanyDomesticPaths(GetAllCompanyDomesticPathsQuery query, CancellationToken cancellationToken);
    Task<ApiResponse<CompanyDomesticPathDto>> GetCompanyDomesticPathByIdAsync(GetCompanyDomesticPathByIdQuery query, CancellationToken cancellationToken);
    Task<ApiResponse<object>> DeleteCompanyDomesticPathAsync(DeleteCompanyDomesticPathCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> UpdateCompanyDomesticPathAsync(UpdateCompanyDomesticPathCommand command,
        CancellationToken cancellationToken);
}