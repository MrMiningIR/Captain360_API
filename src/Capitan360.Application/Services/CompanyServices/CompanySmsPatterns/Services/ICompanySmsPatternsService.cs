using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.CreateCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.DeleteCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.UpdateCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Queries.GetAllCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Queries.GetCompanySmsPatternsById;

namespace Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Services;

public interface ICompanySmsPatternsService
{
    Task<ApiResponse<int>> CreateCompanySmsPatternsAsync(CreateCompanySmsPatternsCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<PagedResult<CompanySmsPatternsDto>>> GetAllCompanySmsPatternsAsync(GetAllCompanySmsPatternsQuery allCompanySmsPatternsQuery, CancellationToken cancellationToken);
    Task<ApiResponse<CompanySmsPatternsDto>> GetCompanySmsPatternsByIdAsync(GetCompanySmsPatternsByIdQuery getCompanySmsPatternsByIdQuery, CancellationToken cancellationToken);
    Task<ApiResponse<int>> DeleteCompanySmsPatternsAsync(DeleteCompanySmsPatternsCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<CompanySmsPatternsDto>> UpdateCompanySmsPatternsAsync(UpdateCompanySmsPatternsCommand command, CancellationToken cancellationToken);
}