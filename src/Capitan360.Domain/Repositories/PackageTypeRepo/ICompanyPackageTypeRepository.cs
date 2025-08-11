using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.CompanyPackageEntity;
using Capitan360.Domain.Entities.PackageEntity;

namespace Capitan360.Domain.Repositories.PackageTypeRepo;

public interface ICompanyPackageTypeRepository
{
    Task<(IReadOnlyList<CompanyPackageType>, int)> GetAllCompanyPackageTypesAsync(string? searchPhrase, int companyId, int active, int pageSize,
        int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<int> InsertCompanyPackageTypeForCompanyAsync(CompanyPackageType companyPackageType, CancellationToken ct);
    Task<int> UpdateCompanyPackageTypeForCompanyAsync(CompanyPackageType companyPackageType, CancellationToken ct);
    Task<bool> CheckExistCompanyPackageTypeNameAsync(string companyPackageTypeName, int? currentCompanyPackageTypeId, int companyId, CancellationToken cancellationToken);
    Task MoveCompanyPackageTypeUpAsync(int companyPackageTypeId, CancellationToken cancellationToken);
    Task MoveCompanyPackageTypeDownAsync(int companyPackageTypeId, CancellationToken cancellationToken);
    Task<int> GetCountCompanyPackageTypeAsync(int companyId, CancellationToken cancellationToken);
    Task CreateCompanyPackageTypesAsync(List<int> companiesId, PackageType packageType, CancellationToken cancellationToken);
    Task AddPackageTypesToCompanyPackageTypeAsync(List<CompanyPackageTypeTransfer> relatedPackageTypes, int companyId, CancellationToken cancellationToken);
    Task DeleteAllPackagesByCompanyIdAsync(int companyId, CancellationToken cancellationToken);
    Task<bool> CheckExistAnyItemAsync(int companyId, CancellationToken cancellationToken);
    Task<CompanyPackageType?> GetCompanyPackageTypeByIdAsync(int companyPackageTypeId, bool tracked, CancellationToken cancellationToken);
}