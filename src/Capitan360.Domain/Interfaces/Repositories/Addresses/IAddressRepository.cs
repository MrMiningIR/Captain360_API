using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.Addresses;

public interface IAddressRepository
{
    Task<int> CreateAddressAsync(Address address, CancellationToken cancellationToken);
    void Delete(Address address);
    Task<IReadOnlyList<Address>> GetAllAddresses(CancellationToken cancellationToken);
    Task<Address?> GetAddressById(int id, CancellationToken cancellationToken);

    Task<(IReadOnlyList<Address>, int)> GetAllAddresses(string searchPhrase, int pageSize, int pageNumber, int companyId, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);
    Task<(IReadOnlyList<Address>, int)> GetAllAddressesByCompany(string searchPhrase, int companyId,
        int active, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection,
        CancellationToken cancellationToken);
    Task<int> OrderAddress(int commandCompanyId, CancellationToken cancellationToken);


    Task MoveAddressUpAsync(int companyId, int addressId, CancellationToken cancellationToken);
    Task MoveAddressDownAsync(int companyId, int addressId, CancellationToken cancellationToken);


}

