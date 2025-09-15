using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Repositories.Companies;

public interface ICompanyCommissionsRepository
{
    Task<int> CreateCompanyCommissionsAsync(CompanyCommissions companyCommissions, CancellationToken cancellationToken);

    Task<CompanyCommissions?> GetCompanyCommissionsByIdAsync(int companyCommissionsId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task DeleteCompanyCommissionsAsync(int companyCommissionsId);

    Task<(IReadOnlyList<CompanyCommissions>, int)> GetAllCompanyCommissionsAsync(string? searchPhrase, string? sortBy, int CompanyTypeId, int CompanyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanyCommissions?> GetCompanyCommissionsByCompanyIdAsync(int companyId, bool loadData, bool tracked,  CancellationToken cancellationToken);
}