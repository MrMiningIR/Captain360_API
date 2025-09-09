using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.PackageEntity;
using Capitan360.Domain.Repositories.PackageTypeRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.PackageTypeImpl;



public class PackageTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IPackageTypeRepository
{
    public async Task<int> CreatePackageTypeAsync(PackageType packageType, CancellationToken cancellationToken)//ch**
    {
        dbContext.PackageTypes.Add(packageType);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return packageType.Id;
    }

    public void Delete(PackageType packageType)
    {
        dbContext.Entry(packageType).Property("Deleted").CurrentValue = true;
    }

    public async Task<PackageType?> GetPackageTypeByIdAsync(int packageTypeId, bool tracked, bool loadData, CancellationToken cancellationToken) //ch**
    {
        IQueryable<PackageType> query = dbContext.PackageTypes;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.CompanyType);

        return await query.SingleOrDefaultAsync(a => a.Id == packageTypeId, cancellationToken);
    }

    public async Task<(IReadOnlyList<PackageType>, int)> GetMatchingAllPackageTypesAsync(string? searchPhrase, int companyTypeId, int active, bool loadData,//ch**
        int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.PackageTypes.AsNoTracking()
                                              .Where(pt => searchPhraseLower == null || pt.PackageTypeName.ToLower().Contains(searchPhraseLower));
        if (loadData)
            baseQuery = baseQuery.Include(a => a.CompanyType);



        if (companyTypeId > 1)
            baseQuery = baseQuery.Where(pt => pt.CompanyTypeId == companyTypeId);

        baseQuery = active switch
        {
            1 => baseQuery.Where(a => a.PackageTypeActive),
            0 => baseQuery.Where(a => !a.PackageTypeActive),
            _ => baseQuery
        };

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<PackageType, object>>>
        {
             { nameof(PackageType.PackageTypeOrder), pt => pt.PackageTypeOrder}
        };

        sortBy ??= nameof(PackageType.PackageTypeOrder);

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

    public async Task<bool> CheckExistPackageTypeNameAsync(string packageTypeName, int? currentPackageTypeId, int companyTypeId, CancellationToken cancellationToken)//ch**
    {
        return await dbContext.PackageTypes.AnyAsync(pt => pt.CompanyTypeId == companyTypeId && (currentPackageTypeId == null || pt.Id != currentPackageTypeId) && pt.PackageTypeName.ToLower() == packageTypeName.ToLower().Trim(), cancellationToken);
    }

    public async Task<int> GetCountPackageTypeAsync(int companyTypeId, CancellationToken cancellationToken)//ch**
    {
        return await dbContext.PackageTypes
            .CountAsync(pt => pt.CompanyTypeId == companyTypeId, cancellationToken: cancellationToken);
    }

    public async Task MovePackageTypeUpAsync(int packageTypeId, CancellationToken cancellationToken) //ch**
    {
        var currentPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(p => p.Id == packageTypeId, cancellationToken: cancellationToken);
        var nextPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(p => p.CompanyTypeId == currentPackageType.CompanyTypeId && p.PackageTypeOrder == currentPackageType.PackageTypeOrder - 1, cancellationToken: cancellationToken);

        if (nextPackageType != null)
            nextPackageType.PackageTypeOrder++;

        if (currentPackageType != null)
            currentPackageType.PackageTypeOrder--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MovePackageTypeDownAsync(int packageTypeId, CancellationToken cancellationToken) //ch**
    {
        var currentPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(p => p.Id == packageTypeId, cancellationToken: cancellationToken);
        var nextPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(p => p.CompanyTypeId == currentPackageType.CompanyTypeId && p.PackageTypeOrder == currentPackageType.PackageTypeOrder + 1, cancellationToken: cancellationToken);

        if (nextPackageType != null)
            nextPackageType.PackageTypeOrder--;

        if (currentPackageType != null)
            currentPackageType.PackageTypeOrder++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CompanyPackageTypeTransfer>> GetPackageTypesByCompanyTypeIdAsync(int companyTypeId, bool tracked, bool loadData, CancellationToken cancellationToken) //ch**
    {
        var baseQuery = dbContext.PackageTypes.Where(x => x.CompanyTypeId == companyTypeId);

        if (loadData)
            baseQuery = baseQuery.Include(a => a.CompanyType);

        if (!tracked)
            baseQuery = baseQuery.AsNoTracking();

        return await baseQuery.Select(x => new CompanyPackageTypeTransfer()
        {
            Id = x.Id,
            PackageTypeActive = x.PackageTypeActive,
            PackageTypeOrder = x.PackageTypeOrder,
            PackageTypeName = x.PackageTypeName,
            CompanyPackageTypeDescription = x.PackageTypeDescription,
        })
                              .ToListAsync(cancellationToken);
    }
    public async Task DeletePackageTypeAsync(PackageType packageType) //ch**
    {
        await Task.Yield();
    }
}