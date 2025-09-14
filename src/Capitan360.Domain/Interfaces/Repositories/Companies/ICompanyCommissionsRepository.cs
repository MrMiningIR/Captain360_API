using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Repositories.Companies;

public interface ICompanyCommissionsRepository
{
    Task<int> CreateCompanyCommissionsAsync(CompanyCommissions companyCommissions, CancellationToken cancellationToken);

    Task<CompanyCommissions?> GetCompanyCommissionsByIdAsync(int companyCommissionsId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task DeleteCompanyCommissionsAsync(CompanyCommissions companyCommissions);

    Task<(IReadOnlyList<CompanyCommissions>, int)> GetAllCompanyCommissionsAsync(string? searchPhrase, string? sortBy, int CompanyTypeId, int CompanyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanyCommissions?> GetCompanyCommissionsByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken);
}