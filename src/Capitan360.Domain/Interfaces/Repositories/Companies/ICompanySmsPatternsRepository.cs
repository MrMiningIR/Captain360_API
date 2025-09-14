using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Repositories.Companies;

public interface ICompanySmsPatternsRepository
{
    Task<int> CreateCompanySmsPatternsAsync(CompanySmsPatterns companySmsPatterns, CancellationToken cancellationToken);

    Task<CompanySmsPatterns?> GetCompanySmsPatternsByIdAsync(int companySmsPatternsId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task DeleteCompanySmsPatternsAsync(CompanySmsPatterns companySmsPatterns);

    Task<(IReadOnlyList<CompanySmsPatterns>, int)> GetAllCompanySmsPatternsAsync(string? searchPhrase, string? sortBy, int CompanyTypeId, int CompanyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanySmsPatterns?> GetCompanySmsPatternsByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken);
}

