using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.PackageEntity;

namespace Capitan360.Domain.Repositories.PackageRepo;

public interface IPackageTypeRepository
{
    Task<int> CreatePackageTypeAsync(PackageType packageType, CancellationToken cancellationToken);
    void Delete(PackageType packageType);
    Task<PackageType?> GetPackageTypeById(int id, CancellationToken cancellationToken);
    Task<(IReadOnlyList<PackageType>, int)> GetMatchingAllPackageTypes(string? searchPhrase, int companyTypeId, int active, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);
    Task<bool> CheckExistPackageTypeName(string packageTypeName, int companyTypeId, CancellationToken cancellationToken);


    Task<int> OrderPackageType(int companyTypeId, CancellationToken cancellationToken);
    Task MovePackageTypeUpAsync(int companyTypeId, int packageTypeId, CancellationToken cancellationToken);
    Task MovePackageTypeDownAsync(int companyTypeId, int packageTypeId, CancellationToken cancellationToken);
    Task<List<CompanyPackageTypeTransfer>> GetPackageTypesByCompanyTypeId(int companyTypeId,
        CancellationToken cancellationToken);
}