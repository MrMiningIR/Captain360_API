using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyCommissionsRepository
{
    Task<int> CreateCompanyCommissionsAsync(CompanyCommissions companyCommissions, CancellationToken cancellationToken);

    Task DeleteCompanyCommissionsAsync(CompanyCommissions companyCommissions);

    Task<CompanyCommissions?> GetCompanyCommissionsByIdAsync(int id, bool tracked, bool loadData,
        CancellationToken cancellationToken);

    Task<CompanyCommissions?> GetCompanyCommissionsByCompanyIdAsync(int companyId, bool tracked,
        bool loadData,
        CancellationToken cancellationToken);


    Task<(IReadOnlyList<CompanyCommissions>, int)> GetMatchingAllCompanyCommissionsAsync(string? searchPhrase, string? sortBy,
        int CompanyTypeId, int CompanyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection,
        CancellationToken cancellationToken);
}