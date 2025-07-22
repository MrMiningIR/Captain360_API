using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyCommissionsRepository
{
    Task<int> CreateCompanyCommissionsAsync(CompanyCommissions companyCommissions, string userId, CancellationToken cancellationToken);
    void Delete(CompanyCommissions companyCommissions, string userId);
    Task<IReadOnlyList<CompanyCommissions>> GetAllCompanyCommissions(CancellationToken cancellationToken);
    Task<CompanyCommissions?> GetCompanyCommissionsById(int id, CancellationToken cancellationToken);
    CompanyCommissions UpdateShadows(CompanyCommissions companyCommissions, string userId);
    Task<(IReadOnlyList<CompanyCommissions>, int)> GetMatchingAllCompanyCommissions(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);
}

