using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Repositories.Companies;

public interface ICompanyDomesticPathChargeRepository
{
    Task<List<int>> CreateByListAsync(List<CompanyDomesticPathCharge> items, CancellationToken cancellationToken);

    Task DeleteByListAsync(List<int> priceIds, CancellationToken cancellationToken);
    Task<List<int>> UpdateByListAsync(List<CompanyDomesticPathCharge> prices, CancellationToken cancellationToken);
    void Delete(CompanyDomesticPathCharge price);
    Task<(IReadOnlyList<CompanyDomesticPathCharge>, int)> GetAllCompanyDomesticPathCharge(
        string? searchPhrase, int companyDomesticPathId, int pageSize, int pageNumber, string? sortBy,
        SortDirection sortDirection, CancellationToken cancellationToken);
    Task<bool> CheckExistPrice(int companyDomesticPathId, int weight, PathStructType pathStructType, WeightType weightType, CancellationToken cancellationToken);
    Task<int> CreateAsync(CompanyDomesticPathCharge price, CancellationToken cancellationToken);
    Task<CompanyDomesticPathCharge?> GetCompanyCompanyDomesticPathChargeById(int id, CancellationToken cancellationToken);
    Task<List<CompanyDomesticPathCharge>> GetExistingStructPaths(List<int> ids, CancellationToken cancellationToken);
}