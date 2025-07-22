using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.PackageEntity;
using Capitan360.Domain.Repositories.PackageRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.PackageTypeImpl;

//public class CompanyPackageTypesRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyPackageTypesRepository
//{
//    public async Task<(IReadOnlyList<CompanyPackageTypeTransfer>, int)> GetCompanyPackageTypes(string? searchPhrase, int companyTypeId, int companyId, int active, int pageSize, int pageNumber,
//        string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
//    {
//        var searchPhraseLower = searchPhrase?.ToLower();
//        var baseQuery = dbContext.PackageTypes.AsNoTracking()
//            .Where(ct => ct.CompanyTypeId == companyTypeId && ct.Active)
//            .Where(ct => searchPhraseLower == null || ct.PackageTypeName.ToLower().Contains(searchPhraseLower));
//        ;   // && ct.Active

//        var query = baseQuery
//            .OrderBy(ct => ct.OrderPackageType)
//            .GroupJoin(
//                dbContext.CompanyPackageTypes.Where(ccd => ccd.CompanyId == companyId),
//                ct => ct.Id,
//                ccd => ccd.PackageTypeId,
//                (ct, ccdGroup) => new { ContentTypeBase = ct, CustomDataGroup = ccdGroup } // نتیجه موقت join
//            )
//            .SelectMany(
//                x => x.CustomDataGroup.DefaultIfEmpty(),
//                (joined, companyPackageType) => new CompanyPackageTypeTransfer
//                {
//                    PackageTypeId = companyPackageType != null
//                        ? companyPackageType.CompanyId
//                        : joined.ContentTypeBase.Id,
//                    PackageTypeName = companyPackageType != null
//                        ? companyPackageType.PackageTypeName
//                        : joined.ContentTypeBase.PackageTypeName,
//                    PackageTypeDescription = companyPackageType != null
//                        ? companyPackageType.PackageTypeDescription
//                        : joined.ContentTypeBase.PackageTypeDescription,
//                    OrderPackageType = companyPackageType != null
//                        ? companyPackageType.OrderPackageType
//                        : joined.ContentTypeBase.OrderPackageType,
//                    Active = companyPackageType != null ? companyPackageType.Active : joined.ContentTypeBase.Active,
//                    CompanyId = companyPackageType != null ? companyPackageType.CompanyId : 0,
//                    Id = companyPackageType != null ? companyPackageType.Id : 0
//                });

//        var totalCount = await baseQuery.CountAsync(cancellationToken);
//        var columnsSelector = new Dictionary<string, Expression<Func<CompanyPackageTypeTransfer, object>>>
//        {
//            { nameof(CompanyPackageTypeTransfer.PackageTypeName), ct => ct.PackageTypeName },
//            { nameof(CompanyPackageTypeTransfer.Active), ct => ct.Active },
//            { nameof(CompanyPackageTypeTransfer.OrderPackageType), ct => ct.OrderPackageType }
//        };

//        sortBy ??= nameof(CompanyPackageTypeTransfer.OrderPackageType);

//        var selectedColumn = columnsSelector[sortBy];
//        query = sortDirection == SortDirection.Ascending
//            ? query.OrderBy(selectedColumn)
//            : query.OrderByDescending(selectedColumn);

//        var contentTypes = await query
//            .Skip(pageSize * (pageNumber - 1))
//            .Take(pageSize)
//            .ToListAsync(cancellationToken);

//        return (contentTypes, totalCount);

//    }

//    public async Task<int> InsertCompanyPackageType(CompanyPackageType companyPackageType, CancellationToken ct)
//    {
//        dbContext.CompanyPackageTypes.Add(companyPackageType);
//        await unitOfWork.SaveChangesAsync(ct);
//        return companyPackageType.Id;
//    }

//    public async Task<int> UpdateCompanyPackageType(CompanyPackageType companyPackageType, CancellationToken ct)
//    {
//        dbContext.Entry(companyPackageType).State = EntityState.Modified;
//        await unitOfWork.SaveChangesAsync(ct);
//        return companyPackageType.Id;
//    }
//}

public class CompanyPackageTypesRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyPackageTypeRepository
{
    public async Task<(IReadOnlyList<CompanyPackageType>, int)> GetCompanyPackageTypes(string? searchPhrase, int companyId, int active, int pageSize,
        int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.CompanyPackageTypes.AsNoTracking()
            .Include(x => x.PackageType)
            .Where(x => x.CompanyId == companyId);



        baseQuery = baseQuery.Where(pt => string.IsNullOrEmpty(searchPhraseLower)
                         || pt.PackageTypeName.ToLower().Contains(searchPhraseLower));

        baseQuery = active switch
        {
            1 => baseQuery.Where(a => a.Active),
            0 => baseQuery.Where(a => !a.Active),
            _ => baseQuery
        };

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyPackageType, object>>>
        {
            { nameof(CompanyPackageType.PackageTypeName), pt => pt.PackageTypeName! },
            { nameof(CompanyPackageType.Active), pt => pt.Active! },
            { nameof(CompanyPackageType.OrderPackageType), pt => pt.OrderPackageType! }
        };

        sortBy ??= nameof(CompanyPackageType.OrderPackageType);

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

    public async Task<int> InsertCompanyPackageTypeForCompany(CompanyPackageType companyPackageType, CancellationToken ct)
    {
        dbContext.CompanyPackageTypes.Add(companyPackageType);
        await unitOfWork.SaveChangesAsync(ct);
        return companyPackageType.Id;
    }

    public async Task<int> UpdateCompanyPackageTypeForCompany(CompanyPackageType companyPackageType, CancellationToken ct)
    {
        dbContext.Entry(companyPackageType).State = EntityState.Modified;
        await unitOfWork.SaveChangesAsync(ct);
        return companyPackageType.Id;
    }

    public async Task<CompanyPackageType?> CheckExistCompanyPackageTypeName(int companyId, int packageTypeId, CancellationToken cancellationToken)
    {
        var result = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(
            x => x.CompanyId == companyId && x.PackageTypeId == packageTypeId, cancellationToken);

        return result;
    }

    public async Task MoveCompanyPackageTypeUpAsync(int companyId, int packageTypeId, CancellationToken cancellationToken)
    {
        var packageTypes = await dbContext.CompanyPackageTypes
            .Where(p => p.CompanyId == companyId)
            .OrderBy(a => a.OrderPackageType)
            .ToListAsync(cancellationToken);

        var currentPackageType = packageTypes.SingleOrDefault(a => a.CompanyId == companyId && a.PackageTypeId == packageTypeId);
        var currentIndex = packageTypes.IndexOf(currentPackageType!);
        if (currentIndex <= 0)
            return;

        var previousPackageType = packageTypes[currentIndex - 1];
        var currentOrder = currentPackageType!.OrderPackageType;
        currentPackageType.OrderPackageType = previousPackageType.OrderPackageType;
        previousPackageType.OrderPackageType = currentOrder;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveCompanyPackageTypeDownAsync(int companyId, int packageTypeId, CancellationToken cancellationToken)
    {
        var packageTypes = await dbContext.CompanyPackageTypes
            .Where(p => p.CompanyId == companyId)
            .OrderBy(a => a.OrderPackageType)
            .ToListAsync(cancellationToken);

        var currentPackageType = packageTypes.SingleOrDefault(a => a.CompanyId == companyId && a.PackageTypeId == packageTypeId);
        var currentIndex = packageTypes.IndexOf(currentPackageType!);
        if (currentIndex >= packageTypes.Count - 1)
            return;

        var nextPackageType = packageTypes[currentIndex + 1];
        var tempOrder = currentPackageType!.OrderPackageType;
        currentPackageType.OrderPackageType = nextPackageType.OrderPackageType;
        nextPackageType.OrderPackageType = tempOrder;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> OrderPackageType(int companyId, CancellationToken cancellationToken)
    {
        var maxOrder = await dbContext.CompanyPackageTypes
            .Where(pt => pt.CompanyId == companyId)
            .MaxAsync(pt => (int?)pt.OrderPackageType, cancellationToken) ?? 0;
        return maxOrder;
    }

    public async Task CreateCompanyPackageTypes(List<int> eligibleCommandlines, PackageType packageType, CancellationToken cancellationToken)
    {
        foreach (var companyId in eligibleCommandlines)
        {
            dbContext.CompanyPackageTypes.Add(new CompanyPackageType()
            {
                CompanyId = companyId,
                PackageTypeId = packageType.Id,
                Active = packageType.Active,
                PackageTypeName = packageType.PackageTypeName,
                OrderPackageType = packageType.OrderPackageType
            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AddPackageTypesToCompanyPackageType(List<CompanyPackageTypeTransfer> relatedPackageTypes, int companyId, CancellationToken cancellationToken)
    {
        foreach (var packageType in relatedPackageTypes)
        {
            dbContext.CompanyPackageTypes.Add(new CompanyPackageType()
            {
                Active = packageType.Active,
                CompanyId = companyId,
                PackageTypeId = packageType.Id,
                PackageTypeName = packageType.PackageTypeName,
                OrderPackageType = packageType.OrderPackageType
            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAllPackagesByCompanyId(int companyId, CancellationToken cancellationToken)
    {
        await dbContext.CompanyPackageTypes
.Where(x => x.CompanyId == companyId)
.ExecuteUpdateAsync(s => s.SetProperty(
 x => EF.Property<bool>(x, "Deleted"),
 x => true
), cancellationToken);
    }

    public async Task<bool> CheckExistAnyItem(int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyPackageTypes.AsNoTracking()
            .AnyAsync(x => x.CompanyId == companyId, cancellationToken);
    }

    public async Task<CompanyPackageType?> GetCompanyPackageTypeById(int companyPackageTypeId, CancellationToken cancellationToken, bool tracking = false)
    {
        if (tracking)
        {
            return await dbContext.CompanyPackageTypes.Include(x => x.PackageType)
    .SingleOrDefaultAsync(x => x.Id == companyPackageTypeId,
    cancellationToken);
        }
        else
        {
            return await dbContext.CompanyPackageTypes.AsNoTracking().Include(x => x.PackageType)
    .SingleOrDefaultAsync(x => x.Id == companyPackageTypeId,
    cancellationToken);
        }

    }
}