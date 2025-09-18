using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Addresses;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Index.HPRtree;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.Addresses;

public class AddressRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IAddressRepository
{
    public async Task<int> CreateAddressAsync(Address address, CancellationToken cancellationToken)
    {

        dbContext.Addresses.Add(address);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return address.Id;
    }



    public void Delete(Address address)
    {
        dbContext.Entry(address).Property("Deleted").CurrentValue = true;

    }

    public async Task<IReadOnlyList<Address>> GetAllAddresses(CancellationToken cancellationToken)
    {
        return await dbContext.Addresses.ToListAsync(cancellationToken);
    }

    public async Task<Address?> GetAddressById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Addresses.SingleOrDefaultAsync(item => item.Id == id, cancellationToken);
    }



    public async Task<(IReadOnlyList<Address>, int)> GetAllAddresses(string? searchPhrase, int pageSize, int pageNumber, int companyId, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();
        var baseQuery = dbContext.Addresses
            .Where(item => searchPhraseLower == null || item.AddressLine.ToLower().Contains(searchPhraseLower));

        baseQuery = baseQuery.Where(x => x.CompanyId == companyId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<Address, object>>>
            {
                { nameof(Address.AddressLine), item => item.AddressLine },
                { nameof(Address.Order), item => item.Order},
            };

        var selectedColumn = columnsSelector[nameof(Address.Order)];

        if (!string.IsNullOrEmpty(sortBy))
        {
            selectedColumn = columnsSelector[sortBy];
        }

        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var addresses = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (addresses, totalCount);
    }

    public async Task<int> OrderAddress(int commandCompanyId, CancellationToken cancellationToken)
    {
        var maxOrder = await dbContext.Addresses
            .Where(item => item.CompanyId == commandCompanyId)
                        .MaxAsync(item => (int?)item.Order, cancellationToken) ?? 0;
        return maxOrder;
    }

    public async Task MoveAddressUpAsync(int companyId, int addressId, CancellationToken cancellationToken)
    {
        var addresses = await dbContext.Addresses
            .Where(c => c.CompanyId == companyId)
            .OrderBy(item => item.Order)
            .ToListAsync(cancellationToken);

        var currentAddress = addresses.SingleOrDefault(item => item.Id == addressId);

        var currentIndex = addresses.IndexOf(currentAddress!);
        if (currentIndex <= 0)
            return;

        var previousAddress = addresses[currentIndex - 1];

        var currentOrder = currentAddress!.Order;
        currentAddress.Order = previousAddress.Order;
        previousAddress.Order = currentOrder;

        // if we use AsNoTracking or Get Data From Body or anywhere else of Database!
        //dbContext.Update(currentAddress);
        //dbContext.Update(previousAddress);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveAddressDownAsync(int companyId, int addressId, CancellationToken cancellationToken)
    {
        var addresses = await dbContext.Addresses
            .Where(c => c.CompanyId == companyId)
            .OrderBy(item => item.Order)
            .ToListAsync(cancellationToken);

        var currentAddress = addresses.FirstOrDefault(item => item.Id == addressId);

        var currentIndex = addresses.IndexOf(currentAddress!);
        if (currentIndex >= addresses.Count - 1)
            return;

        var nextAddress = addresses[currentIndex + 1];

        var tempOrder = currentAddress!.Order;
        currentAddress.Order = nextAddress.Order;
        nextAddress.Order = tempOrder;

        // if we use AsNoTracking or Get Data From Body or anywhere else of Database!
        //dbContext.Update(currentAddress);
        //dbContext.Update(nextAddress);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<Address>, int)> GetAllAddressesByCompany(string? searchPhrase, int companyId, int active, int pageSize, int pageNumber, string? sortBy,
        SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();

        var baseQuery = dbContext.Addresses.AsNoTracking()
            .Where(item => item.CompanyId == companyId);

        baseQuery = baseQuery.Where(item => searchPhraseLower == null || item.AddressLine.ToLower().Contains(searchPhraseLower));

        if (active == 0)
        {
            baseQuery = baseQuery.Where(x => !x.Active);
        }

        if (active == 1)
        {
            baseQuery = baseQuery.Where(x => x.Active);
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<Address, object>>>
     {
         { nameof(Address.AddressLine), item => item.AddressLine },
         { nameof(Address.Order), item => item.Order },
     };

        var selectedColumn = columnsSelector[nameof(Address.Order)];

        if (!string.IsNullOrEmpty(sortBy))
        {
            selectedColumn = columnsSelector[sortBy];
        }

        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var addresses = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (addresses, totalCount);
    }
}