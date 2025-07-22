using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyUriRepo;

public interface ICompanyUriRepository
{
    Task<int> CreateCompanyUriAsync(CompanyUri companyUri, CancellationToken cancellationToken);
    void Delete(CompanyUri companyUri);

    Task<CompanyUri?> GetCompanyUriById(int id, CancellationToken cancellationToken);

    Task<(IReadOnlyList<CompanyUri>, int)> GetMatchingAllCompanyUris(string? searchPhrase, int companyId, int active, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<bool> CheckExistUri(string uri, int companyId, CancellationToken cancellationToken);
}

