using System.Linq.Expressions;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyTypeRepository
{

    Task<int> CreateCompanyTypeAsync(CompanyType companyType, string userId, CancellationToken cancellationToken);
    void Delete(CompanyType companyType, string userId);
    Task<IReadOnlyList<CompanyType>> GetAllCompanyTypes(CancellationToken cancellationToken);
    Task<CompanyType?> GetCompanyTypeById(int id, CancellationToken cancellationToken);
    CompanyType UpdateShadows(CompanyType companyType, string userId);
    Task<(IReadOnlyList<CompanyType>, int)> GetMatchingAllCompanyTypes(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);
}


