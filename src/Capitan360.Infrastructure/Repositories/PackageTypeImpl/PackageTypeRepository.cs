using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.PackageEntity;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Repositories.PackageTypeRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.PackageTypeImpl;

public class PackageTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IPackageTypeRepository
{
    public async Task<bool> CheckExistPackageTypeNameAsync(string packageTypeName, int? currentPackageTypeId, int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.PackageTypes.AnyAsync(item => item.CompanyTypeId == companyTypeId && (currentPackageTypeId == null || item.Id != currentPackageTypeId) && item.PackageTypeName.ToLower() == packageTypeName.ToLower().Trim(), cancellationToken);
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

    public async Task<PackageType?> GetPackageTypeByIdAsync(int packageTypeId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<PackageType> query = dbContext.PackageTypes;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(item => item.CompanyType);

        return await query.SingleOrDefaultAsync(item => item.Id == packageTypeId, cancellationToken);
    }

    public async Task DeletePackageTypeAsync(PackageType packageType)
    {
        await Task.Yield();
    }

    public async Task MovePackageTypeUpAsync(int packageTypeId, CancellationToken cancellationToken)
    {
        var currentPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(item => item.Id == packageTypeId, cancellationToken: cancellationToken);
        if (currentPackageType == null)
            return;

        var nextPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(item => item.CompanyTypeId == currentPackageType.CompanyTypeId && item.PackageTypeOrder == currentPackageType.PackageTypeOrder - 1, cancellationToken: cancellationToken);
        if (nextPackageType == null)
            return;

        nextPackageType.PackageTypeOrder++;
        currentPackageType.PackageTypeOrder--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MovePackageTypeDownAsync(int packageTypeId, CancellationToken cancellationToken)
    {
        var currentPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(item => item.Id == packageTypeId, cancellationToken: cancellationToken);
        if (currentPackageType == null)
            return;

        var nextPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(item => item.CompanyTypeId == currentPackageType.CompanyTypeId && item.PackageTypeOrder == currentPackageType.PackageTypeOrder + 1, cancellationToken: cancellationToken);
        if (nextPackageType == null)
            return;

        nextPackageType.PackageTypeOrder--;
        currentPackageType.PackageTypeOrder++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<PackageType>, int)> GetAllPackageTypesAsync(string? searchPhrase, string? sortBy, int companyTypeId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.PackageTypes.AsNoTracking()
                                              .Where(item => searchPhraseLower == null || item.PackageTypeName.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(item => item.CompanyType);

        if (companyTypeId != 0)
            baseQuery = baseQuery.Where(item => item.CompanyTypeId == companyTypeId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<PackageType, object>>>
        {
            { nameof(PackageType.PackageTypeOrder), item => item.PackageTypeOrder}
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

    public async Task<List<CompanyPackageTypeTransfer>> GetPackageTypesByCompanyTypeIdAsync(int companyTypeId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext.PackageTypes.Where(item => item.CompanyTypeId == companyTypeId);

        if (loadData)
            baseQuery = baseQuery.Include(item => item.CompanyType);

        if (!tracked)
            baseQuery = baseQuery.AsNoTracking();

        return await baseQuery.Select(item => new CompanyPackageTypeTransfer()
        {
            Id = item.Id,
            PackageTypeActive = item.PackageTypeActive,
            PackageTypeOrder = item.PackageTypeOrder,
            PackageTypeName = item.PackageTypeName,
            CompanyPackageTypeDescription = item.PackageTypeDescription,
        }).ToListAsync(cancellationToken);
    }
}