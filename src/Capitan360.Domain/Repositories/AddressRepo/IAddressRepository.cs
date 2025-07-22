
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.AddressEntity;

namespace Capitan360.Domain.Repositories.AddressRepo;

public interface IAddressRepository
{
    Task<int> CreateAddressAsync(Address address, CancellationToken cancellationToken);
    void Delete(Address address);
    Task<IReadOnlyList<Address>> GetAllAddresses(CancellationToken cancellationToken);
    Task<Address?> GetAddressById(int id, CancellationToken cancellationToken);

    Task<(IReadOnlyList<Address>, int)> GetMatchingAllAddresses(string? searchPhrase, int pageSize, int pageNumber, int companyId, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);
    Task<(IReadOnlyList<Address>, int)> GetMatchingAllAddressesByCompany(string? searchPhrase, int companyId,
        int active, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection,
        CancellationToken cancellationToken);
    Task<int> OrderAddress(int commandCompanyId, CancellationToken cancellationToken);


    Task MoveAddressUpAsync(int companyId, int addressId, CancellationToken cancellationToken);
    Task MoveAddressDownAsync(int companyId, int addressId, CancellationToken cancellationToken);


}

