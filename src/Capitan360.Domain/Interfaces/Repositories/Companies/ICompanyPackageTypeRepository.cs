using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.PackageTypes;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.Companies;

public interface ICompanyPackageTypeRepository
{
    Task CreateCompanyPackageTypesAsync(List<int> companiesId, PackageType packageType, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyPackageTypeNameAsync(string companyPackageTypeName, int companyId, int? currentCompanyPackageTypeId, CancellationToken cancellationToken);

    Task<int> GetCountCompanyPackageTypeAsync(int companyId, CancellationToken cancellationToken);

    Task<CompanyPackageType?> GetCompanyPackageTypeByIdAsync(int companyPackageTypeId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyPackageType>?> GetCompanyPackageTypeByCompanyIdAsync(int companyId, CancellationToken cancellationToken);

    Task DeleteCompanyPackageTypeAsync(int cmpanyPackageTypeId, CancellationToken cancellationToken);

    Task MoveCompanyPackageTypeUpAsync(int companyPackageTypeId, CancellationToken cancellationToken);

    Task MoveCompanyPackageTypeDownAsync(int companyPackageTypeId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<CompanyPackageType>, int)> GetAllCompanyPackageTypesAsync(string searchPhrase, string? sortBy, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task AddPackageTypesToCompanyPackageTypeAsync(List<CompanyPackageTypeTransfer> relatedPackageTypes, int companyId, CancellationToken cancellationToken);
}