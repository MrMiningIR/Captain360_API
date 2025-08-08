using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.PackageEntity;

namespace Capitan360.Domain.Repositories.PackageTypeRepo;

public interface IPackageTypeRepository
{
    Task<int> CreatePackageTypeAsync(PackageType packageType, CancellationToken cancellationToken);

    void Delete(PackageType packageType);

    Task<PackageType?> GetPackageTypeByIdAsync(int packageTypeId, bool tracked, CancellationToken cancellationToken);

    Task<(IReadOnlyList<PackageType>, int)> GetMatchingAllPackageTypesAsync(string? searchPhrase, int companyTypeId, int active,
        int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<bool> CheckExistPackageTypeNameAsync(string packageTypeName, int? currentPackageTypeId, int companyTypeId, CancellationToken cancellationToken);

    Task<int> GetCountPackageTypeAsync(int companyTypeId, CancellationToken cancellationToken);

    Task MovePackageTypeUpAsync(int packageTypeId, CancellationToken cancellationToken);

    Task MovePackageTypeDownAsync(int packageTypeId, CancellationToken cancellationToken);

    Task<List<CompanyPackageTypeTransfer>> GetPackageTypesByCompanyTypeIdAsync(int companyTypeId, CancellationToken cancellationToken);
}