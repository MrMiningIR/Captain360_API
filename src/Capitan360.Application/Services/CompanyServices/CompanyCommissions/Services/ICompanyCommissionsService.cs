using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.CreateCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.DeleteCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.UpdateCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetAllCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetCompanyCommissionsById;

namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.Services;

public interface ICompanyCommissionsService
{
    Task<ApiResponse<int>> CreateCompanyCommissionsAsync(CreateCompanyCommissionsCommand companyCommissions, CancellationToken cancellationToken);
    Task<ApiResponse<PagedResult<CompanyCommissionsDto>>> GetAllCompanyCommissions(GetAllCompanyCommissionsQuery allCompanyCommissionsQuery, CancellationToken cancellationToken);
    Task<ApiResponse<CompanyCommissionsDto>> GetCompanyCommissionsByIdAsync(GetCompanyCommissionsByIdQuery id, CancellationToken cancellationToken);
    Task<ApiResponse<int>> DeleteCompanyCommissionsAsync(DeleteCompanyCommissionsCommand id, CancellationToken cancellationToken);
    Task<ApiResponse<int>> UpdateCompanyCommissionsAsync(UpdateCompanyCommissionsCommand command, CancellationToken cancellationToken);
}