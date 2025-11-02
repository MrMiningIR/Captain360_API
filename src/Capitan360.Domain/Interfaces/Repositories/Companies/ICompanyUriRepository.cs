using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.Companies;

public interface ICompanyUriRepository
{
    Task<bool> CheckExistCompanyUriUriAsync(string companyUriUri, int? currentCompanyUriId, CancellationToken cancellationToken);
    Task<CompanyUri?> CheckExistCompanyUriByUriAndCompanyId(string companyUriUri, int companyId, CancellationToken cancellationToken);

    Task<int> CreateCompanyUriAsync(CompanyUri companyUri, CancellationToken cancellationToken);

    Task<CompanyUri?> GetCompanyUriByIdAsync(int companyUriId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyUri>?> GetCompanyUriByCompanyIdAsync(int companyUriCompanyId, CancellationToken cancellationToken);

    Task DeleteCompanyUriAsync(int companyUriId);

    Task<(IReadOnlyList<CompanyUri>, int)> GetAllCompanyUrisAsync(string searchPhrase, string? sortBy, int companyId, int active, int captain360Uri, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanyUri?> GetUriByTenant(string tennat, CancellationToken cancellationToken);
}

