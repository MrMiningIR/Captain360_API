using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Entities.PackageEntity;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

public class CompanyPackageTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyPackageTypeRepository
{
    public async Task CreateCompanyPackageTypesAsync(List<int> companiesId, PackageType packageType, CancellationToken cancellationToken)
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
                CompanyPackageTypeDescription = "",
            });
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> CheckExistCompanyPackageTypeNameAsync(string companyPackageTypeName, int? currentCompanyPackageTypeId, int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyPackageTypes.AnyAsync(item => item.CompanyId == companyId && (currentCompanyPackageTypeId == null || item.Id != currentCompanyPackageTypeId) && item.CompanyPackageTypeName.ToLower() == companyPackageTypeName.ToLower().Trim(), cancellationToken);
    }

    public async Task<int> GetCountCompanyPackageTypeAsync(int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyPackageTypes.CountAsync(item => item.CompanyId == companyId, cancellationToken: cancellationToken);
    }

    public async Task<CompanyPackageType?> GetCompanyPackageTypeByIdAsync(int companyPackageTypeId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<CompanyPackageType> query = dbContext.CompanyPackageTypes;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(item => item.Company);

        return await query.SingleOrDefaultAsync(item => item.Id == companyPackageTypeId, cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyPackageType>?> GetCompanyPackageTypeByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<CompanyPackageType> query = dbContext.CompanyPackageTypes;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(item => item.Company);

        return await query.Where(item => item.CompanyId == companyId)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteCompanyPackageTypeAsync(CompanyPackageType cmpanyPackageType)
    {
        await Task.Yield();
    }

    public async Task MoveCompanyPackageTypeUpAsync(int companyPackageTypeId, CancellationToken cancellationToken)
    {
        var currentCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(item => item.Id == companyPackageTypeId, cancellationToken: cancellationToken);
        var nextCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(item => item.CompanyId == currentCompanyPackageType!.CompanyId && item.CompanyPackageTypeOrder == currentCompanyPackageType.CompanyPackageTypeOrder - 1, cancellationToken: cancellationToken);

        nextCompanyPackageType!.CompanyPackageTypeOrder++;
        currentCompanyPackageType!.CompanyPackageTypeOrder--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveCompanyPackageTypeDownAsync(int companyPackageTypeId, CancellationToken cancellationToken)
    {
        var currentCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(item => item.Id == companyPackageTypeId, cancellationToken: cancellationToken);
        var nextCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(item => item.CompanyId == currentCompanyPackageType!.CompanyId && item.CompanyPackageTypeOrder == currentCompanyPackageType.CompanyPackageTypeOrder + 1, cancellationToken: cancellationToken);

        nextCompanyPackageType!.CompanyPackageTypeOrder--;
        currentCompanyPackageType!.CompanyPackageTypeOrder++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyPackageType>, int)> GetAllCompanyPackageTypesAsync(string? searchPhrase, string? sortBy, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.CompanyPackageTypes.AsNoTracking()
                                                     .Where(item => searchPhraseLower == null || item.CompanyPackageTypeName.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(item => item.Company);

        if (companyId != 0)
        {
            baseQuery = baseQuery.Where(item => item.CompanyId == companyId);
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyPackageType, object>>>
        {
            { nameof(CompanyPackageType.CompanyPackageTypeOrder), item => item.CompanyPackageTypeOrder! }
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

    public async Task AddPackageTypesToCompanyPackageTypeAsync(List<CompanyPackageTypeTransfer> relatedPackageTypes, int companyId, CancellationToken cancellationToken)
    {
        foreach (var packageType in relatedPackageTypes)
        {
            dbContext.CompanyPackageTypes.Add(new CompanyPackageType()
            {
                CompanyPackageTypeActive = packageType.PackageTypeActive,
                CompanyId = companyId,
                PackageTypeId = packageType.Id,
                CompanyPackageTypeName = packageType.PackageTypeName,
                CompanyPackageTypeOrder = packageType.PackageTypeOrder,
                CompanyPackageTypeDescription = "",
            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
