using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.PackageEntity;
using Capitan360.Domain.Repositories.PackageTypeRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.PackageTypeImpl;

//public class PackageTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IPackageTypeRepository
//{
//    public async Task<int> CreatePackageTypeAsync(PackageType packageType, CancellationToken cancellationToken)
//    {

//        dbContext.PackageTypes.Add(packageType);
//        await unitOfWork.SaveChangesAsync(cancellationToken);
//        return packageType.Id;
//    }

//    public void Delete(PackageType packageType)
//    {
//        dbContext.Entry(packageType).Property("Deleted").CurrentValue = true;
//    }

//    public async Task<PackageType?> GetPackageTypeById(int id, CancellationToken cancellationToken)
//    {
//        return await dbContext.PackageTypes.Include(a => a.CompanyType).SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
//    }

//    public async Task<(IReadOnlyList<PackageType>, int)> GetMatchingAllPackageTypes(string? searchPhrase, int companyTypeId, int active, int pageSize, int pageNumber,
//        string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
//    {
//        var searchPhraseLower = searchPhrase?.ToLower();
//        var baseQuery = dbContext.PackageTypes.Include(a => a.CompanyType).AsNoTracking()

//            .Where(ct => searchPhraseLower == null || ct.PackageTypeName.ToLower().Contains(searchPhraseLower));

//        if (companyTypeId != 0)
//            baseQuery = baseQuery.Where(ct => ct.CompanyTypeId == companyTypeId);

//        baseQuery = active switch
//        {
//            1 => baseQuery.Where(a => a.Active),
//            2 => baseQuery.Where(a => !a.Active),
//            _ => baseQuery
//        };

//        var totalCount = await baseQuery.CountAsync(cancellationToken);


//        var columnsSelector = new Dictionary<string, Expression<Func<PackageType, object>>>
//        {
//            { nameof(PackageType.PackageTypeName), ct => ct.PackageTypeName },
//            { nameof(PackageType.Active), ct => ct.Active },
//            { nameof(PackageType.OrderPackageType), ct => ct.OrderPackageType }
//        };

//        sortBy ??= nameof(PackageType.OrderPackageType);




//        var selectedColumn = columnsSelector[sortBy];
//        baseQuery = sortDirection == SortDirection.Ascending
//            ? baseQuery.OrderBy(selectedColumn)
//            : baseQuery.OrderByDescending(selectedColumn);


//        var contentTypes = await baseQuery
//            .Skip(pageSize * (pageNumber - 1))
//            .Take(pageSize)
//            .ToListAsync(cancellationToken);

//        return (contentTypes, totalCount);
//    }

//    public async Task<bool> CheckExistPackageTypeName(string packageTypeName, int companyTypeId, CancellationToken cancellationToken)
//    {
//        return await dbContext.PackageTypes.AnyAsync(ct => ct.CompanyTypeId == companyTypeId && ct.PackageTypeName == packageTypeName.ToLower().Trim(), cancellationToken);
//    }

//    public async Task<int> OrderPackageType(int companyTypeId, CancellationToken cancellationToken)
//    {
//        var maxOrder = await dbContext.PackageTypes
//            .Where(ca => ca.CompanyTypeId == companyTypeId)
//            .MaxAsync(ca => (int?)ca.OrderPackageType, cancellationToken) ?? 0;
//        return maxOrder;
//    }

//    public async Task MovePackageTypeUpAsync(int companyTypeId, int packageTypeId, CancellationToken cancellationToken)
//    {
//        var packageTypes = await dbContext.PackageTypes
//            .Where(c => c.CompanyTypeId == companyTypeId)
//            .OrderBy(a => a.OrderPackageType)
//            .ToListAsync(cancellationToken);

//        var currentPackageType = packageTypes.SingleOrDefault(a => a.Id == packageTypeId);
//        var currentIndex = packageTypes.IndexOf(currentPackageType!);
//        if (currentIndex <= 0)
//            return;

//        var previousPackageType = packageTypes[currentIndex - 1];
//        var currentOrder = currentPackageType!.OrderPackageType;
//        currentPackageType.OrderPackageType = previousPackageType.OrderPackageType;
//        previousPackageType.OrderPackageType = currentOrder;

//        await unitOfWork.SaveChangesAsync(cancellationToken);
//    }

//    public async Task MovePackageTypeDownAsync(int companyTypeId, int packageTypeId, CancellationToken cancellationToken)
//    {
//        var packageTypes = await dbContext.PackageTypes
//            .Where(c => c.CompanyTypeId == companyTypeId)
//            .OrderBy(a => a.OrderPackageType)
//            .ToListAsync(cancellationToken);

//        var currentPackageType = packageTypes.FirstOrDefault(a => a.Id == packageTypeId);
//        var currentIndex = packageTypes.IndexOf(currentPackageType!);
//        if (currentIndex >= packageTypes.Count - 1)
//            return;

//        var nextPackageType = packageTypes[currentIndex + 1];
//        var tempOrder = currentPackageType!.OrderPackageType;
//        currentPackageType.OrderPackageType = nextPackageType.OrderPackageType;
//        nextPackageType.OrderPackageType = tempOrder;

//        await dbContext.SaveChangesAsync(cancellationToken);
//    }
//}

public class PackageTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IPackageTypeRepository
{
    public async Task<int> CreatePackageTypeAsync(PackageType packageType, CancellationToken cancellationToken)
    {
        dbContext.PackageTypes.Add(packageType);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return packageType.Id;
    }

    public void Delete(PackageType packageType)
    {
        dbContext.Entry(packageType).Property("Deleted").CurrentValue = true;
    }

    public async Task<PackageType?> GetPackageTypeById(int id, CancellationToken cancellationToken, bool tracked)
    {
        if (tracked)
        {

            return await dbContext.PackageTypes.Include(a => a.CompanyType).SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
        else
        {
            return await dbContext.PackageTypes.AsNoTracking().Include(a => a.CompanyType).SingleOrDefaultAsync(a => a.Id == id, cancellationToken);

        }
    }

    public async Task<(IReadOnlyList<PackageType>, int)> GetMatchingAllPackageTypes(string? searchPhrase, int companyTypeId, int active,
        int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.PackageTypes.Include(a => a.CompanyType).AsNoTracking()
            .Where(pt => searchPhraseLower == null || pt.PackageTypeName.ToLower().Contains(searchPhraseLower));

        if (companyTypeId != 0)
            baseQuery = baseQuery.Where(pt => pt.CompanyTypeId == companyTypeId);

        baseQuery = active switch
        {
            1 => baseQuery.Where(a => a.Active),
            0 => baseQuery.Where(a => !a.Active),
            _ => baseQuery
        };

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<PackageType, object>>>
        {
            { nameof(PackageType.PackageTypeName), pt => pt.PackageTypeName },
            { nameof(PackageType.Active), pt => pt.Active },
            { nameof(PackageType.OrderPackageType), pt => pt.OrderPackageType }
        };

        sortBy ??= nameof(PackageType.OrderPackageType);

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

    public async Task<PackageType?> CheckExistPackageTypeName(string packageTypeName, int companyTypeId,
        CancellationToken cancellationToken)
    {
        return await dbContext.PackageTypes.SingleOrDefaultAsync(pt => pt.CompanyTypeId == companyTypeId && pt.PackageTypeName.ToLower().Trim() == packageTypeName.ToLower().Trim(), cancellationToken);
    }

    public async Task<int> OrderPackageType(int companyTypeId, CancellationToken cancellationToken)
    {
        var maxOrder = await dbContext.PackageTypes
            .Where(pt => pt.CompanyTypeId == companyTypeId)
            .MaxAsync(pt => (int?)pt.OrderPackageType, cancellationToken) ?? 0;
        return maxOrder;
    }

    public async Task MovePackageTypeUpAsync(int companyTypeId, int packageTypeId, CancellationToken cancellationToken)
    {
        var packageTypes = await dbContext.PackageTypes
            .Where(p => p.CompanyTypeId == companyTypeId)
            .OrderBy(a => a.OrderPackageType)
            .ToListAsync(cancellationToken);

        var currentPackageType = packageTypes.SingleOrDefault(a => a.Id == packageTypeId);
        var currentIndex = packageTypes.IndexOf(currentPackageType!);
        if (currentIndex <= 0)
            return;

        var previousPackageType = packageTypes[currentIndex - 1];
        var currentOrder = currentPackageType!.OrderPackageType;
        currentPackageType.OrderPackageType = previousPackageType.OrderPackageType;
        previousPackageType.OrderPackageType = currentOrder;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MovePackageTypeDownAsync(int companyTypeId, int packageTypeId, CancellationToken cancellationToken)
    {
        var packageTypes = await dbContext.PackageTypes
            .Where(p => p.CompanyTypeId == companyTypeId)
            .OrderBy(a => a.OrderPackageType)
            .ToListAsync(cancellationToken);

        var currentPackageType = packageTypes.SingleOrDefault(a => a.Id == packageTypeId);
        var currentIndex = packageTypes.IndexOf(currentPackageType!);
        if (currentIndex >= packageTypes.Count - 1)
            return;

        var nextPackageType = packageTypes[currentIndex + 1];
        var tempOrder = currentPackageType!.OrderPackageType;
        currentPackageType.OrderPackageType = nextPackageType.OrderPackageType;
        nextPackageType.OrderPackageType = tempOrder;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CompanyPackageTypeTransfer>> GetPackageTypesByCompanyTypeId(int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.PackageTypes.AsNoTracking().Where(x => x.CompanyTypeId == companyTypeId)
            .Select(x => new CompanyPackageTypeTransfer()
            {
                Id = x.Id,
                Active = x.Active,
                OrderPackageType = x.OrderPackageType,
                PackageTypeName = x.PackageTypeName

            }).ToListAsync(cancellationToken);
    }
}