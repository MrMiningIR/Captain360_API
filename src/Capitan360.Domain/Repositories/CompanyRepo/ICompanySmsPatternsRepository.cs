using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanySmsPatternsRepository
{
    Task<int> CreateCompanySmsPatternsAsync(CompanySmsPatterns companySmsPatterns, CancellationToken cancellationToken);
    void Delete(CompanySmsPatterns companySmsPatterns, string userId);
    Task<IReadOnlyList<CompanySmsPatterns>> GetAllCompanySmsPatternsAsync(CancellationToken cancellationToken);
    Task<CompanySmsPatterns?> GetCompanySmsPatternsByIdAsync(int id, bool tracked, CancellationToken cancellationToken);

    Task<(IReadOnlyList<CompanySmsPatterns>, int)> GetAllCompanySmsPatterns(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanySmsPatterns?> GetCompanySmsPatternsByCompanyIdAsync(int companyId, bool tracked,
        CancellationToken cancellationToken);

}

