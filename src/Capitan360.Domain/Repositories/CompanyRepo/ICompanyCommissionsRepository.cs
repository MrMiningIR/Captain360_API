using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyCommissionsRepository
{
    Task<int> CreateCompanyCommissionsAsync(CompanyCommissions companyCommissions, string userId, CancellationToken cancellationToken);

    void Delete(CompanyCommissions companyCommissions);

    Task<CompanyCommissions?> GetCompanyCommissionsByIdAsync(int id, bool tracked, CancellationToken cancellationToken);

    Task<CompanyCommissions?> GetCompanyCommissionsByCompanyIdAsync(int companyId, bool tracked,
        CancellationToken cancellationToken);


    Task<(IReadOnlyList<CompanyCommissions>, int)> GetAllCompanyCommissionsAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);
}