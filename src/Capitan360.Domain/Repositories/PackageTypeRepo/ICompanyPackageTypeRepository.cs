using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.PackageEntity;

namespace Capitan360.Domain.Repositories.PackageRepo;

public interface ICompanyPackageTypeRepository
{

    Task<(IReadOnlyList<CompanyPackageType>, int)> GetCompanyPackageTypes(string? searchPhrase,
    int companyId, int active, int pageSize,
    int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<int> UpdateCompanyPackageTypeForCompany(
      CompanyPackageType companyContentType,

        CancellationToken ct);
    Task<int> InsertCompanyPackageTypeForCompany(
        CompanyPackageType companyContentType,

        CancellationToken ct);


    Task<CompanyPackageType?> CheckExistCompanyPackageTypeName(int commandCompanyId, int commandContentTypeId, CancellationToken cancellationToken);

    Task MoveCompanyPackageTypeUpAsync(int companyId, int contentTypeId, CancellationToken cancellationToken);
    Task MoveCompanyPackageTypeDownAsync(int companyId, int contentTypeId, CancellationToken cancellationToken);

    Task<int> GetCountCompanyPackageType(int companyId, CancellationToken cancellationToken);


    Task CreateCompanyPackageTypes(List<int> getEligibleCommandlines, PackageType packageType,
        CancellationToken cancellationToken);


    Task AddPackageTypesToCompanyPackageType(List<CompanyPackageTypeTransfer> relatedPackageTypes, int companyId, CancellationToken cancellationToken);
    Task DeleteAllPackagesByCompanyId(int companyId, CancellationToken cancellationToken);
    Task<bool> CheckExistAnyItem(int companyId, CancellationToken cancellationToken);
    Task<CompanyPackageType?> GetCompanyPackageTypeById(int id, CancellationToken cancellationToken,
        bool tracking = false);
}