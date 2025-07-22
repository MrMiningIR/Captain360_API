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
    Task<int> CreateCompanySmsPatternsAsync(CreateCompanySmsPatternsCommand command, CancellationToken cancellationToken);
    Task<PagedResult<CompanySmsPatternsDto>> GetAllCompanySmsPatterns(GetAllCompanySmsPatternsQuery query, CancellationToken cancellationToken);
    Task<CompanySmsPatternsDto> GetCompanySmsPatternsByIdAsync(GetCompanySmsPatternsByIdQuery id, CancellationToken cancellationToken);
    Task DeleteCompanySmsPatternsAsync(DeleteCompanySmsPatternsCommand id, CancellationToken cancellationToken);
    Task UpdateCompanySmsPatternsAsync(UpdateCompanySmsPatternsCommand command, CancellationToken cancellationToken);
}