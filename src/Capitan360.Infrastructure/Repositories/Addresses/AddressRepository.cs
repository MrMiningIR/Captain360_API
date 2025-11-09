using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Addresses;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
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

    public async Task<int> GetCountAddressOfUserAsync(string userId, CancellationToken cancellationToken)
    {
        return await dbContext.Addresses.CountAsync(item => !item.CompanyId.HasValue && item.UserId != null && item.UserId.ToLower() == userId.Trim().ToLower(), cancellationToken);
    }

    public async Task<int> GetCountAddressOfCompanyIdAsync(int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.Addresses.CountAsync(item => item.CompanyId.HasValue && item.UserId == null && item.CompanyId == companyId, cancellationToken);
    }

    public async Task<Address?> GetAddressByIdAsync(int addressId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<Address> query = dbContext.Addresses;

        if (loadData)
        {
            query = query.Include(item => item.Company);
            query = query.Include(item => item.User);
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == addressId, cancellationToken);
    }

    public async Task<IReadOnlyList<Address>?> GetAddressByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        IQueryable<Address> query = dbContext.Addresses.Include(item => item.Company)
                                                       .AsNoTracking();

        return await query.Where(item => !item.CompanyId.HasValue && item.UserId != null && item.UserId.ToLower() == userId.Trim().ToLower())
                          .OrderBy(item => item.Order)
                          .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Address>?> GetAddressByCompanyIdAsync(int companyId, CancellationToken cancellationToken)
    {
        IQueryable<Address> query = dbContext.Addresses.Include(item => item.User)
                                                       .AsNoTracking();

        return await query.Where(item => item.CompanyId.HasValue && item.UserId == null && item.CompanyId == companyId)
                          .OrderBy(item => item.Order)
                          .ToListAsync(cancellationToken);
    }

    public Task DeleteAddressAsync(int addressId, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task MoveAddressUpAsync(int addressId, CancellationToken cancellationToken)
    {
        var currentAddress = await dbContext.Addresses.SingleOrDefaultAsync(item => item.Id == addressId, cancellationToken);
        if (currentAddress == null)
            return;
        if (currentAddress.UserId != null)
        {
            var nextAddress = await dbContext.Addresses.SingleOrDefaultAsync(item => !item.CompanyId.HasValue && item.UserId != null && item.UserId.ToLower() == currentAddress!.UserId.Trim().ToLower() && item.Order == currentAddress.Order - 1, cancellationToken);
            if (nextAddress == null)
                return;

            nextAddress.Order++;
            currentAddress.Order--;
        }
        else if (currentAddress.CompanyId != null)
        {
            var nextAddress = await dbContext.Addresses.SingleOrDefaultAsync(item => item.CompanyId.HasValue && item.UserId == null && item.CompanyId == currentAddress!.CompanyId && item.Order == currentAddress.Order - 1, cancellationToken);
            if (nextAddress == null)
                return;

            nextAddress.Order++;
            currentAddress.Order--;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveAddressDownAsync(int addressId, CancellationToken cancellationToken)
    {
        var currentAddress = await dbContext.Addresses.SingleOrDefaultAsync(item => item.Id == addressId, cancellationToken);
        if (currentAddress == null)
            return;

        if (currentAddress.UserId != null)
        {
            var nextAddress = await dbContext.Addresses.SingleOrDefaultAsync(item => !item.CompanyId.HasValue && item.UserId != null && item.UserId.ToLower() == currentAddress!.UserId.Trim().ToLower() && item.Order == currentAddress.Order + 1, cancellationToken);
            if (nextAddress == null)
                return;

            nextAddress.Order--;
            currentAddress.Order++;
        }
        else if (currentAddress.CompanyId != null)
        {
            var nextAddress = await dbContext.Addresses.SingleOrDefaultAsync(item => item.CompanyId.HasValue && item.UserId == null && item.CompanyId == currentAddress!.CompanyId && item.Order == currentAddress.Order + 1, cancellationToken);
            if (nextAddress == null)
                return;

            nextAddress.Order--;
            currentAddress.Order++;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<Address>, int)> GetAllAddresssesAsync(string searchPhrase, string? sortBy, int? companyId, string? userId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();
        var baseQuery = dbContext.Addresses.Where(item => item.AddressLine.ToLower().Contains(searchPhrase) || item.Mobile.ToLower().Contains(searchPhrase) ||
                                                          item.Tel1.ToLower().Contains(searchPhrase) || item.Tel2.ToLower().Contains(searchPhrase) || item.Zipcode.ToLower().Contains(searchPhrase) ||
                                                          item.Description.ToLower().Contains(searchPhrase));


        baseQuery = baseQuery.Where(x => x.IsCompanyAddress);
        if (companyId != null)
            baseQuery = baseQuery.Where(x => x.CompanyId == companyId);

        if (userId != null)
            baseQuery = baseQuery.Where(x => x.UserId!.ToLower() == userId.Trim().ToLower());

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
}