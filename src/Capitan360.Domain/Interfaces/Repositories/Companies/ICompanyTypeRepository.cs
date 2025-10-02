using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.Companies;

public interface ICompanyTypeRepository
{
    Task<bool> CheckExistCompanyTypeNameAsync(string companyTypeName, int? currentCompanyTypeId, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyTypeDisplayNameAsync(string companyDispalyName, int? currentCompanyTypeId, CancellationToken cancellationToken);

    Task<int> CreateCompanyTypeAsync(CompanyType companyType, CancellationToken cancellationToken);

    Task<CompanyType?> GetCompanyTypeByIdAsync(int companyTypeId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task DeleteCompanyTypeAsync(int companyTypeId);

    Task<(IReadOnlyList<CompanyType>, int)> GetAllCompanyTypesAsync(string searchPhrase ,string? sortBy, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);
}


