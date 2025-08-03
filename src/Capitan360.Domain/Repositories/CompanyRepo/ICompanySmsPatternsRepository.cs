using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanySmsPatternsRepository
{
    Task<int> CreateCompanySmsPatternsAsync(CompanySmsPatterns companySmsPatterns, CancellationToken cancellationToken);
    void Delete(CompanySmsPatterns companySmsPatterns, string userId);
    Task<IReadOnlyList<CompanySmsPatterns>> GetAllCompanySmsPatterns(CancellationToken cancellationToken);
    Task<CompanySmsPatterns?> GetCompanySmsPatternsById(int id, bool tracked, CancellationToken cancellationToken);

    Task<(IReadOnlyList<CompanySmsPatterns>, int)> GetMatchingAllCompanySmsPatterns(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);
}

