using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyCommissionsRepository
{
    Task<int> CreateCompanyCommissionsAsync(CompanyCommissions companyCommissions, string userId, CancellationToken cancellationToken);
    void Delete(CompanyCommissions companyCommissions);
    Task<IReadOnlyList<CompanyCommissions>> GetAllCompanyCommissions(CancellationToken cancellationToken);
    Task<CompanyCommissions?> GetCompanyCommissionsById(int id, bool tracked, CancellationToken cancellationToken);

    Task<(IReadOnlyList<CompanyCommissions>, int)> GetMatchingAllCompanyCommissions(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);
}

