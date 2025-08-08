using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyUriRepo;

public interface ICompanyUriRepository
{
    Task<int> CreateCompanyUriAsync(CompanyUri companyUri, CancellationToken cancellationToken);
    Task<bool> CheckExistUriAsync(string uri, CancellationToken cancellationToken);
    void Delete(CompanyUri companyUri);
    Task<CompanyUri?> GetCompanyUriByIdAsync(int companyUriId, bool tracked, CancellationToken cancellationToken);
    Task<(IReadOnlyList<CompanyUri>, int)> GetAllCompanyUrisAsync(string? searchPhrase, int companyId, int active, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);
}

