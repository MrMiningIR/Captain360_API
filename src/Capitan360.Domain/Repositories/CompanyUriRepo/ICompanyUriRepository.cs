using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyUriRepo;

public interface ICompanyUriRepository
{
    Task<int> CreateCompanyUriAsync(CompanyUri companyUri, CancellationToken cancellationToken);
    Task<bool> CheckExistCompanyUriUriAsync(string companyUriUri, int? currentCompanyUriId,
        CancellationToken cancellationToken);
    Task DeleteCompanyUriAsync(CompanyUri companyUri);

    Task<CompanyUri?> GetCompanyUriByIdAsync(int companyUriId, bool tracked, bool loadData,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyUri>?> GetCompanyUriByCompanyIdAsync(int companyUriCompanyId, bool tracked, bool loadData,
        CancellationToken cancellationToken);
    Task<(IReadOnlyList<CompanyUri>, int)> GetAllCompanyUrisAsync(string? searchPhrase, string? sortBy, int companyId,
        int active, int captain360Uri, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection,
        CancellationToken cancellationToken);
}

