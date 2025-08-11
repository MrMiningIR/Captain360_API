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
            1 => baseQuery.Where(a => a.Active),
            0 => baseQuery.Where(a => !a.Active),
            _ => baseQuery
        };

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyPackageType, object>>>
        {
            { nameof(CompanyPackageType.CompanyPackageTypeName), pt => pt.CompanyPackageTypeName! },
            { nameof(CompanyPackageType.Active), pt => pt.Active! },
            { nameof(CompanyPackageType.OrderCompanyPackageType), pt => pt.OrderCompanyPackageType! }
        };

        sortBy ??= nameof(CompanyPackageType.OrderCompanyPackageType);

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

    public async Task<bool> CheckExistCompanyPackageTypeNameAsync(string companyPackageTypeName, int? currentCompanyPackageTypeId, int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyPackageTypes.AnyAsync(pt => pt.CompanyId == companyId && (currentCompanyPackageTypeId == null || pt.Id != currentCompanyPackageTypeId) && pt.CompanyPackageTypeName.ToLower() == companyPackageTypeName.ToLower().Trim(), cancellationToken);
    }

    public async Task MoveCompanyPackageTypeUpAsync(int companyPackageTypeId, CancellationToken cancellationToken)
    {
        var currentCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(p => p.Id == companyPackageTypeId, cancellationToken: cancellationToken);
        var nextCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(p => p.CompanyId == currentCompanyPackageType!.CompanyId && p.OrderCompanyPackageType == currentCompanyPackageType.OrderCompanyPackageType - 1, cancellationToken: cancellationToken);

        nextCompanyPackageType!.OrderCompanyPackageType++;
        currentCompanyPackageType!.OrderCompanyPackageType--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveCompanyPackageTypeDownAsync(int companyPackageTypeId, CancellationToken cancellationToken)
    {
        var currentCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(p => p.Id == companyPackageTypeId, cancellationToken: cancellationToken);
        var nextCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(p => p.CompanyId == currentCompanyPackageType!.CompanyId && p.OrderCompanyPackageType == currentCompanyPackageType.OrderCompanyPackageType + 1, cancellationToken: cancellationToken);

        nextCompanyPackageType!.OrderCompanyPackageType--;
        currentCompanyPackageType!.OrderCompanyPackageType++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetCountCompanyPackageTypeAsync(int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyPackageTypes
             .CountAsync(pt => pt.CompanyId == companyId, cancellationToken: cancellationToken);
    }

    public async Task CreateCompanyPackageTypesAsync(List<int> companiesId, PackageType packageType, CancellationToken cancellationToken)
    {
        foreach (var companyId in companiesId)
        {
            dbContext.CompanyPackageTypes.Add(new CompanyPackageType()
            {
                CompanyId = companyId,
                PackageTypeId = packageType.Id,
                Active = packageType.Active,
                CompanyPackageTypeName = packageType.PackageTypeName,
                OrderCompanyPackageType = packageType.OrderPackageType,
            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AddPackageTypesToCompanyPackageTypeAsync(List<CompanyPackageTypeTransfer> relatedPackageTypes, int companyId, CancellationToken cancellationToken)
    {
        foreach (var packageType in relatedPackageTypes)
        {
            dbContext.CompanyPackageTypes.Add(new CompanyPackageType()
            {
                Active = packageType.Active,
                CompanyId = companyId,
                PackageTypeId = packageType.Id,
                CompanyPackageTypeName = packageType.PackageTypeName,
                OrderCompanyPackageType = packageType.OrderPackageType
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

    public async Task<CompanyPackageType?> GetCompanyPackageTypeByIdAsync(int companyPackageTypeId, bool tracked, CancellationToken cancellationToken)
    {
        return tracked ?
        await dbContext.CompanyPackageTypes.Include(x => x.PackageType)
        .SingleOrDefaultAsync(x => x.Id == companyPackageTypeId,
        cancellationToken)
            : await dbContext.CompanyPackageTypes.AsNoTracking().Include(x => x.PackageType)
        .SingleOrDefaultAsync(x => x.Id == companyPackageTypeId,
        cancellationToken);
    }
}
