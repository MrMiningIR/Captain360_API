using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanySmsPatternsRepository
{
    Task<int> CreateCompanySmsPatternsAsync(CompanySmsPatterns companySmsPatterns, CancellationToken cancellationToken);
    Task DeleteCompanySmsPatternsAsync(CompanySmsPatterns companySmsPatterns, string userId);
    Task<IReadOnlyList<CompanySmsPatterns>> GetAllCompanySmsPatternsAsync(CancellationToken cancellationToken);
    Task<CompanySmsPatterns?> GetCompanySmsPatternsByIdAsync(int id, bool tracked, bool loadData,
        CancellationToken cancellationToken);

    Task<(IReadOnlyList<CompanySmsPatterns>, int)> GetMatchingAllCompanySmsPatternsAsync(string? searchPhrase,
        string? sortBy, int companyTypeId, int companyId, bool loadData, int pageNumber, int pageSize,
        SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanySmsPatterns?> GetCompanySmsPatternsByCompanyIdAsync(int companyId, bool tracked,
        bool loadData,
        CancellationToken cancellationToken);

}

