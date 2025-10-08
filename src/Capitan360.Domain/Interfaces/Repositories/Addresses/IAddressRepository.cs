using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.Addresses;

public interface IAddressRepository
{
    Task<int> CreateAddressAsync(Address address, CancellationToken cancellationToken);
 
    Task<int> GetCountAddressOfUserAsync(string userId, CancellationToken cancellationToken);

    Task<int> GetCountAddressOfCompanyIdAsync(int companyId, CancellationToken cancellationToken);

    Task<Address?> GetAddressByIdAsync(int addressId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task<IReadOnlyList<Address>?> GetAddressByCompanyIdAsync(int companyId, CancellationToken cancellationToken);

    Task<IReadOnlyList<Address>?> GetAddressByUserIdAsync(string userId, CancellationToken cancellationToken);

    Task DeleteAddressAsync(int addressId, CancellationToken cancellationToken);

    Task MoveAddressUpAsync(int addressId, CancellationToken cancellationToken);

    Task MoveAddressDownAsync(int addressId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<Address>, int)> GetAllAddresssesAsync(string searchPhrase, string? sortBy, int? companyId, string? userId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);
}