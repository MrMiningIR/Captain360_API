using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.PackageTypes;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Repositories.PackageTypes;

public interface IPackageTypeRepository
{
    Task<bool> CheckExistPackageTypeNameAsync(string packageTypeName, int? currentPackageTypeId, int companyTypeId, CancellationToken cancellationToken);

    Task<int> GetCountPackageTypeAsync(int companyTypeId, CancellationToken cancellationToken);

    Task<int> CreatePackageTypeAsync(PackageType packageType, CancellationToken cancellationToken);

    Task<PackageType?> GetPackageTypeByIdAsync(int packageTypeId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task DeletePackageTypeAsync(PackageType packageType);

    Task MovePackageTypeUpAsync(int packageTypeId, CancellationToken cancellationToken);

    Task MovePackageTypeDownAsync(int packageTypeId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<PackageType>, int)> GetAllPackageTypesAsync(string? searchPhrase, string? sortBy, int companyTypeId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<List<CompanyPackageTypeTransfer>> GetPackageTypesByCompanyTypeIdAsync(int CompanyTypeId, bool tracked, bool loadData, CancellationToken cancellationToken);
}