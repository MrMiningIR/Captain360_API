using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.CompanyPackageEntity;
using Capitan360.Domain.Entities.PackageEntity;
using Capitan360.Domain.Repositories.PackageTypeRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyPackageTypeImpl;

public class CompanyPackageTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyPackageTypeRepository
{
    public async Task<(IReadOnlyList<CompanyPackageType>, int)> GetAllCompanyPackageTypesAsync(string? searchPhrase, int companyId, int active, int pageSize,
        int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.CompanyPackageTypes.AsNoTracking()
            .Include(x => x.PackageType)
            .Where(x => x.CompanyId == companyId);

        if (companyId != 0)
        {
            baseQuery = baseQuery.Where(pt => pt.CompanyId == companyId);
        }

        baseQuery = baseQuery.Where(pt => string.IsNullOrEmpty(searchPhraseLower)
                         || pt.CompanyPackageTypeName.ToLower().Contains(searchPhraseLower));

        baseQuery = active switch
        {
            1 => baseQuery.Where(a => a.CompanyPackageTypeActive),
            0 => baseQuery.Where(a => !a.CompanyPackageTypeActive),
            _ => baseQuery
        };

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyPackageType, object>>>
        {
            { nameof(CompanyPackageType.CompanyPackageTypeName), pt => pt.CompanyPackageTypeName! },
            { nameof(CompanyPackageType.CompanyPackageTypeActive), pt => pt.CompanyPackageTypeActive! },
            { nameof(CompanyPackageType.CompanyPackageTypeOrder), pt => pt.CompanyPackageTypeOrder! }
        };

        sortBy ??= nameof(CompanyPackageType.CompanyPackageTypeOrder);

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

    public async Task<int> InsertCompanyPackageTypeForCompanyAsync(CompanyPackageType companyPackageType, CancellationToken ct)
    {
        dbContext.CompanyPackageTypes.Add(companyPackageType);
        await unitOfWork.SaveChangesAsync(ct);
        return companyPackageType.Id;
    }

    public async Task<int> UpdateCompanyPackageTypeForCompanyAsync(CompanyPackageType companyPackageType, CancellationToken ct)
    {
        dbContext.Entry(companyPackageType).State = EntityState.Modified;
        await unitOfWork.SaveChangesAsync(ct);
        return companyPackageType.Id;
    }

    public async Task<bool> CheckExistCompanyPackageTypeNameAsync(string companyPackageTypeName, int? currentCompanyPackageTypeId, int companyId, CancellationToken cancellationToken)//ch**
    {
        return await dbContext.CompanyPackageTypes.AnyAsync(pt => pt.CompanyId == companyId && (currentCompanyPackageTypeId == null || pt.Id != currentCompanyPackageTypeId) && pt.CompanyPackageTypeName.ToLower() == companyPackageTypeName.ToLower().Trim(), cancellationToken);
    }

    public async Task MoveCompanyPackageTypeUpAsync(int companyPackageTypeId, CancellationToken cancellationToken)//ch**
    {
        var currentCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(p => p.Id == companyPackageTypeId, cancellationToken: cancellationToken);
        var nextCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(p => p.CompanyId == currentCompanyPackageType!.CompanyId && p.CompanyPackageTypeOrder == currentCompanyPackageType.CompanyPackageTypeOrder - 1, cancellationToken: cancellationToken);

        nextCompanyPackageType!.CompanyPackageTypeOrder++;
        currentCompanyPackageType!.CompanyPackageTypeOrder--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveCompanyPackageTypeDownAsync(int companyPackageTypeId, CancellationToken cancellationToken)//ch**
    {
        var currentCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(p => p.Id == companyPackageTypeId, cancellationToken: cancellationToken);
        var nextCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(p => p.CompanyId == currentCompanyPackageType!.CompanyId && p.CompanyPackageTypeOrder == currentCompanyPackageType.CompanyPackageTypeOrder + 1, cancellationToken: cancellationToken);

        nextCompanyPackageType!.CompanyPackageTypeOrder--;
        currentCompanyPackageType!.CompanyPackageTypeOrder++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetCountCompanyPackageTypeAsync(int companyId, CancellationToken cancellationToken)//ch**
    {
        return await dbContext.CompanyPackageTypes.CountAsync(pt => pt.CompanyId == companyId, cancellationToken: cancellationToken);
    }

    public async Task CreateCompanyPackageTypesAsync(List<int> companiesId, PackageType packageType, CancellationToken cancellationToken)//ch**
    {
        foreach (var companyId in companiesId)
        {
            dbContext.CompanyPackageTypes.Add(new CompanyPackageType()
            {
                CompanyId = companyId,
                PackageTypeId = packageType.Id,
                CompanyPackageTypeActive = packageType.PackageTypeActive,
                CompanyPackageTypeName = packageType.PackageTypeName,
                CompanyPackageTypeOrder = packageType.PackageTypeOrder,
            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AddPackageTypesToCompanyPackageTypeAsync(List<CompanyPackageTypeTransfer> relatedPackageTypes, int companyId, CancellationToken cancellationToken)//ch**
    {
        foreach (var packageType in relatedPackageTypes)
        {
            dbContext.CompanyPackageTypes.Add(new CompanyPackageType()
            {
                CompanyPackageTypeActive = packageType.PackageTypeActive,
                CompanyId = companyId,
                PackageTypeId = packageType.Id,
                CompanyPackageTypeName = packageType.PackageTypeName,
                CompanyPackageTypeOrder = packageType.PackageTypeOrder
            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAllPackagesByCompanyIdAsync(int companyId, CancellationToken cancellationToken)
    {
        await dbContext.CompanyPackageTypes
.Where(x => x.CompanyId == companyId)
.ExecuteUpdateAsync(s => s.SetProperty(
 x => EF.Property<bool>(x, "Deleted"),
 x => true
), cancellationToken);
    }

    public async Task<bool> CheckExistAnyItemAsync(int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyPackageTypes.AsNoTracking()
            .AnyAsync(x => x.CompanyId == companyId, cancellationToken);
    }

    public async Task<CompanyPackageType?> GetCompanyPackageTypeByIdAsync(int companyPackageTypeId, bool tracked, bool loadData, CancellationToken cancellationToken)//ch**
    {
        IQueryable<CompanyPackageType> query = dbContext.CompanyPackageTypes;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.Company);

        return await query.SingleOrDefaultAsync(a => a.Id == companyPackageTypeId, cancellationToken);
    }
    public async Task DeleteCompanyPackageTypeAsync(CompanyPackageType companyPackageType)//ch**
    {
        await Task.Yield();
    }
    public async Task<(IReadOnlyList<CompanyPackageType>, int)> GetMatchingAllCompanyPackageTypesAsync(string? searchPhrase, string? sortBy, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.CompanyPackageTypes.AsNoTracking()
                                                     .Where(pt => searchPhraseLower == null || pt.CompanyPackageTypeName.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(a => a.Company);

        if (companyId != 0)
        {
            baseQuery = baseQuery.Where(pt => pt.CompanyId == companyId);
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyPackageType, object>>>
        {
            { nameof(CompanyPackageType.CompanyPackageTypeOrder), pt => pt.CompanyPackageTypeOrder! }
        };

        sortBy ??= nameof(CompanyPackageType.CompanyPackageTypeOrder);

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
}
