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

    public async Task<PackageType?> GetPackageTypeByIdAsync(int packageTypeId, bool tracked, CancellationToken cancellationToken)
    {
        return tracked ? await dbContext.PackageTypes.Include(a => a.CompanyType).SingleOrDefaultAsync(a => a.Id == packageTypeId, cancellationToken) :
                        await dbContext.PackageTypes.AsNoTracking().Include(a => a.CompanyType).SingleOrDefaultAsync(a => a.Id == packageTypeId, cancellationToken);
    }

    public async Task<(IReadOnlyList<PackageType>, int)> GetMatchingAllPackageTypesAsync(string? searchPhrase, int companyTypeId, int active,
        int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.PackageTypes.Include(a => a.CompanyType).AsNoTracking()
            .Where(pt => searchPhraseLower == null || pt.PackageTypeName.ToLower().Contains(searchPhraseLower));

        if (companyTypeId != 0)
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
            { nameof(PackageType.PackageTypeName), pt => pt.PackageTypeName },
            { nameof(PackageType.PackageTypeActive), pt => pt.PackageTypeActive },
            { nameof(PackageType.PackageTypeOrder), pt => pt.PackageTypeOrder }
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

    public async Task<bool> CheckExistPackageTypeNameAsync(string packageTypeName, int? currentPackageTypeId, int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.PackageTypes.AnyAsync(pt => pt.CompanyTypeId == companyTypeId && (currentPackageTypeId == null || pt.Id != currentPackageTypeId) && pt.PackageTypeName.ToLower() == packageTypeName.ToLower().Trim(), cancellationToken);
    }

    public async Task<int> GetCountPackageTypeAsync(int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.PackageTypes
            .CountAsync(pt => pt.CompanyTypeId == companyTypeId, cancellationToken: cancellationToken);
    }

    public async Task MovePackageTypeUpAsync(int packageTypeId, CancellationToken cancellationToken)
    {
        var currentPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(p => p.Id == packageTypeId, cancellationToken: cancellationToken);
        var nextPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(p => p.CompanyTypeId == currentPackageType.CompanyTypeId && p.PackageTypeOrder == currentPackageType.PackageTypeOrder - 1, cancellationToken: cancellationToken);

        nextPackageType.PackageTypeOrder++;
        currentPackageType.PackageTypeOrder--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MovePackageTypeDownAsync(int packageTypeId, CancellationToken cancellationToken)
    {
        var currentPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(p => p.Id == packageTypeId, cancellationToken: cancellationToken);
        var nextPackageType = await dbContext.PackageTypes.SingleOrDefaultAsync(p => p.CompanyTypeId == currentPackageType.CompanyTypeId && p.PackageTypeOrder == currentPackageType.PackageTypeOrder + 1, cancellationToken: cancellationToken);

        nextPackageType.PackageTypeOrder--;
        currentPackageType.PackageTypeOrder++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CompanyPackageTypeTransfer>> GetPackageTypesByCompanyTypeIdAsync(int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.PackageTypes.AsNoTracking().Where(x => x.CompanyTypeId == companyTypeId)
            .Select(x => new CompanyPackageTypeTransfer()
            {
                Id = x.Id,
                Active = x.PackageTypeActive,
                OrderPackageType = x.PackageTypeOrder,
                PackageTypeName = x.PackageTypeName,
                CompanyPackageTypeDescription = x.PackageTypeDescription,

            }).ToListAsync(cancellationToken);
    }
}