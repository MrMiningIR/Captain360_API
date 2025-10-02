using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.PackageTypes;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.PackageTypes;

public interface IPackageTypeRepository
{
    Task<bool> CheckExistPackageTypeNameAsync(string packageTypeName, int companyTypeId, int? currentPackageTypeId, CancellationToken cancellationToken);

    Task<int> GetCountPackageTypeAsync(int companyTypeId, CancellationToken cancellationToken);

    Task<int> CreatePackageTypeAsync(PackageType packageType, CancellationToken cancellationToken);

    Task<PackageType?> GetPackageTypeByIdAsync(int packageTypeId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task DeletePackageTypeAsync(int packageTypeId);

    Task MovePackageTypeUpAsync(int packageTypeId, CancellationToken cancellationToken);

    Task MovePackageTypeDownAsync(int packageTypeId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<PackageType>, int)> GetAllPackageTypesAsync(string searchPhrase, string? sortBy, int companyTypeId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<List<CompanyPackageTypeTransfer>> GetPackageTypesByCompanyTypeIdAsync(int CompanyTypeId, CancellationToken cancellationToken);
}