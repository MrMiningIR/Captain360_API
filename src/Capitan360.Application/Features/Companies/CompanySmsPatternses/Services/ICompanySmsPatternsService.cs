using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Commands.Create;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Commands.Update;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Dtos;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Queries.GetByCompanyId;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Queries.GetById;

namespace Capitan360.Application.Features.Companies.CompanySmsPatternses.Services;

public interface ICompanySmsPatternsService
{
    Task<ApiResponse<int>> CreateCompanySmsPatternsAsync(CreateCompanySmsPatternsCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanySmsPatternsAsync(DeleteCompanySmsPatternsCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<CompanySmsPatternsDto>> UpdateCompanySmsPatternsAsync(UpdateCompanySmsPatternsCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanySmsPatternsDto>>> GetAllCompanySmsPatternsAsync(GetAllCompanySmsPatternsQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<CompanySmsPatternsDto>> GetCompanySmsPatternsByCompanyIdAsync(GetCompanySmsPatternsByCompanyIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<CompanySmsPatternsDto>> GetCompanySmsPatternsByIdAsync(GetCompanySmsPatternsByIdQuery query, CancellationToken cancellationToken);
}