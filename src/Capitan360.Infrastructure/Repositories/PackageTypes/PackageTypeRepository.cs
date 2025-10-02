using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.PackageTypes;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.PackageTypes;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.PackageTypes;

public class PackageTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IPackageTypeRepository
{
    public async Task<bool> CheckExistPackageTypeNameAsync(string packageTypeName, int companyTypeId, int? currentPackageTypeId,  CancellationToken cancellationToken)
    {
        return await dbContext.PackageTypes.AnyAsync(item => item.Name.ToLower() == packageTypeName.Trim().ToLower() && item.CompanyTypeId == companyTypeId && (currentPackageTypeId == null || item.Id != currentPackageTypeId), cancellationToken);
    }

    public async Task<int> GetCountPackageTypeAsync(int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.PackageTypes.CountAsync(item => item.CompanyTypeId == companyTypeId, cancellationToken: cancellationToken);
    }

    public async Task<int> CreatePackageTypeAsync(PackageType packageType, CancellationToken cancellationToken)
    {
        dbContext.PackageTypes.Add(packageType);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return packageType.Id;
    }

    public async Task<PackageType?> GetPackageTypeByIdAsync(int packageTypeId, bool loadData, bool tracked,  CancellationToken cancellationToken)
    {
        IQueryable<PackageType> query = dbContext.PackageTypes;

        if (loadData)
            query = query.Include(item => item.CompanyType);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == packageTypeId, cancellationToken);
    }

    public async Task DeletePackageTypeAsync(int packageTypeId)
    {
        await Task.Yield();
    }

    public async Task MovePackageTypeUpAsync(int packageTypeId, CancellationToken cancellationToken)
    {
        var currentPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(item => item.Id == packageTypeId, cancellationToken: cancellationToken);
        if (currentPackageType == null)
            return;

        var nextPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(item => item.CompanyTypeId == currentPackageType.CompanyTypeId && item.Order == currentPackageType.Order - 1, cancellationToken: cancellationToken);
        if (nextPackageType == null)
            return;

        nextPackageType.Order++;
        currentPackageType.Order--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MovePackageTypeDownAsync(int packageTypeId, CancellationToken cancellationToken)
    {
        var currentPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(item => item.Id == packageTypeId, cancellationToken: cancellationToken);
        if (currentPackageType == null)
            return;

        var nextPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(item => item.CompanyTypeId == currentPackageType.CompanyTypeId && item.Order == currentPackageType.Order + 1, cancellationToken: cancellationToken);
        if (nextPackageType == null)
            return;

        nextPackageType.Order--;
        currentPackageType.Order++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<PackageType>, int)> GetAllPackageTypesAsync(string searchPhrase, string? sortBy, int companyTypeId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();
        var baseQuery = dbContext.PackageTypes.AsNoTracking()
                                              .Where(item => searchPhrase == null || item.Name.ToLower().Contains(searchPhrase));

        if (loadData)
            baseQuery = baseQuery.Include(item => item.CompanyType);

        if (companyTypeId != 0)
            baseQuery = baseQuery.Where(item => item.CompanyTypeId == companyTypeId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<PackageType, object>>>
        {
            { nameof(PackageType.Order), item => item.Order}
        };

        sortBy ??= nameof(PackageType.Order);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var packageTypes = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (packageTypes, totalCount);
    }

    public async Task<List<CompanyPackageTypeTransfer>> GetPackageTypesByCompanyTypeIdAsync(int companyTypeId, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext.PackageTypes.Where(item => item.CompanyTypeId == companyTypeId);

        return await baseQuery.Select(item => new CompanyPackageTypeTransfer()
        {
            Id = item.Id,
            Active = item.Active,
            Order = item.Order,
            Name = item.Name,
            Description = item.Description,
        }).ToListAsync(cancellationToken);
    }
}