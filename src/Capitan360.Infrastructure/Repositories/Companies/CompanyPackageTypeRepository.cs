using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.PackageTypes;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Capitan360.Domain.Entities.PackageTypes;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Enums;
using NetTopologySuite.Index.HPRtree;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Interfaces.Repositories.Companies;

namespace Capitan360.Infrastructure.Repositories.Companies;

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
                Active = packageType.Active,
                Name = packageType.Name,
                Order = packageType.Order,
                Description = "",
            });
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> CheckExistCompanyPackageTypeNameAsync(string companyPackageTypeName, int companyId, int? currentCompanyPackageTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyPackageTypes.AnyAsync(item => item.Name.ToLower() == companyPackageTypeName.Trim().ToLower() && item.CompanyId == companyId && (currentCompanyPackageTypeId == null || item.Id != currentCompanyPackageTypeId), cancellationToken);
    }

    public async Task<int> GetCountCompanyPackageTypeAsync(int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyPackageTypes.CountAsync(item => item.CompanyId == companyId, cancellationToken);
    }

    public async Task<CompanyPackageType?> GetCompanyPackageTypeByIdAsync(int companyPackageTypeId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyPackageType> query = dbContext.CompanyPackageTypes;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == companyPackageTypeId, cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyPackageType>?> GetCompanyPackageTypeByCompanyIdAsync(int companyId, CancellationToken cancellationToken)
    {
        IQueryable<CompanyPackageType> query = dbContext.CompanyPackageTypes.Include(item => item.Company)
                                                                            .AsNoTracking();

        return await query.Where(item => item.CompanyId == companyId)
                          .OrderBy(item => item.Order)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteCompanyPackageTypeAsync(int cmpanyPackageTypeId, CancellationToken cancellationToken)
    {
        await Task.Yield();
    }

    public async Task MoveCompanyPackageTypeUpAsync(int companyPackageTypeId, CancellationToken cancellationToken)
    {
        var currentCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(item => item.Id == companyPackageTypeId, cancellationToken);
        if (currentCompanyPackageType == null)
            return;

        var nextCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(item => item.CompanyId == currentCompanyPackageType!.CompanyId && item.Order == currentCompanyPackageType.Order - 1, cancellationToken);
        if (nextCompanyPackageType == null)
            return;

        nextCompanyPackageType.Order++;
        currentCompanyPackageType.Order--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveCompanyPackageTypeDownAsync(int companyPackageTypeId, CancellationToken cancellationToken)
    {
        var currentCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(item => item.Id == companyPackageTypeId, cancellationToken);
        if (currentCompanyPackageType == null)
            return;

        var nextCompanyPackageType = await dbContext.CompanyPackageTypes.SingleOrDefaultAsync(item => item.CompanyId == currentCompanyPackageType!.CompanyId && item.Order == currentCompanyPackageType.Order + 1, cancellationToken);
        if (nextCompanyPackageType == null)
            return;

        nextCompanyPackageType.Order--;
        currentCompanyPackageType.Order++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyPackageType>, int)> GetAllCompanyPackageTypesAsync(string searchPhrase, string? sortBy, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();
        var baseQuery = dbContext.CompanyPackageTypes.AsNoTracking()
                                                     .Where(item => item.Name.ToLower().Contains(searchPhrase));

        if (loadData)
            baseQuery = baseQuery.Include(item => item.Company).Include(x => x.PackageType);

        if (companyId != 0)
        {
            baseQuery = baseQuery.Where(item => item.CompanyId == companyId);
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyPackageType, object>>>
        {
            { nameof(CompanyPackageType.Order), item => item.Order }
        };

        sortBy ??= nameof(CompanyPackageType.Order);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var companyPackageTypes = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyPackageTypes, totalCount);
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
                Name = packageType.Name,
                Order = packageType.Order,
                Description = "",
            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
