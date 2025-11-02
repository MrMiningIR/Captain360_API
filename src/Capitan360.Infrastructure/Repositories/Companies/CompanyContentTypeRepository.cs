using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.ContentTypes;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.Companies;

public class CompanyContentTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyContentTypeRepository
{
    public async Task CreateCompanyContentTypesAsync(List<int> companiesId, ContentType ContentType, CancellationToken cancellationToken)
    {
        foreach (var companyId in companiesId)
        {
            dbContext.CompanyContentTypes.Add(new CompanyContentType()
            {
                CompanyId = companyId,
                ContentTypeId = ContentType.Id,
                Active = ContentType.Active,
                Name = ContentType.Name,
                Order = ContentType.Order,
                Description = "",
            });
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> CheckExistCompanyContentTypeNameAsync(string companyContentTypeName, int companyId, int? currentCompanyContentTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyContentTypes.AnyAsync(item => item.Name.ToLower() == companyContentTypeName.Trim().ToLower() && item.CompanyId == companyId && (currentCompanyContentTypeId == null || item.Id != currentCompanyContentTypeId), cancellationToken);
    }

    public async Task<int> GetCountCompanyContentTypeAsync(int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyContentTypes.CountAsync(item => item.CompanyId == companyId, cancellationToken);
    }

    public async Task<CompanyContentType?> GetCompanyContentTypeByIdAsync(int companyContentTypeId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyContentType> query = dbContext.CompanyContentTypes;

        if (loadData)
            query = query.Include(item => item.Company);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == companyContentTypeId, cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyContentType>?> GetCompanyContentTypeByCompanyIdAsync(int companyId, CancellationToken cancellationToken)
    {
        IQueryable<CompanyContentType> query = dbContext.CompanyContentTypes.Include(item => item.Company)
                                                                            .AsNoTracking();

        return await query.Where(item => item.CompanyId == companyId)
                          .OrderBy(item => item.Order)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteCompanyContentTypeAsync(int cmpanyContentTypeId, CancellationToken cancellationToken)
    {
        await Task.Yield();
    }

    public async Task MoveCompanyContentTypeUpAsync(int companyContentTypeId, CancellationToken cancellationToken)
    {
        var currentCompanyContentType = await dbContext.CompanyContentTypes.SingleOrDefaultAsync(item => item.Id == companyContentTypeId, cancellationToken);
        if (currentCompanyContentType == null)
            return;

        var nextCompanyContentType = await dbContext.CompanyContentTypes.SingleOrDefaultAsync(item => item.CompanyId == currentCompanyContentType!.CompanyId && item.Order == currentCompanyContentType.Order - 1, cancellationToken);
        if (nextCompanyContentType == null)
            return;

        nextCompanyContentType.Order++;
        currentCompanyContentType.Order--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveCompanyContentTypeDownAsync(int companyContentTypeId, CancellationToken cancellationToken)
    {
        var currentCompanyContentType = await dbContext.CompanyContentTypes.SingleOrDefaultAsync(item => item.Id == companyContentTypeId, cancellationToken);
        if (currentCompanyContentType == null)
            return;

        var nextCompanyContentType = await dbContext.CompanyContentTypes.SingleOrDefaultAsync(item => item.CompanyId == currentCompanyContentType!.CompanyId && item.Order == currentCompanyContentType.Order + 1, cancellationToken);
        if (nextCompanyContentType == null)
            return;

        nextCompanyContentType.Order--;
        currentCompanyContentType.Order++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyContentType>, int)> GetAllCompanyContentTypesAsync(string searchPhrase, string? sortBy, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();
        var baseQuery = dbContext.CompanyContentTypes.AsNoTracking()
                                                     .Where(item => item.Name.ToLower().Contains(searchPhrase));

        if (loadData)
            baseQuery = baseQuery.Include(item => item.Company).Include(x => x.ContentType);

        if (companyId != 0)
        {
            baseQuery = baseQuery.Where(item => item.CompanyId == companyId);
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyContentType, object>>>
        {
            { nameof(CompanyContentType.Order), item => item.Order }
        };

        sortBy ??= nameof(CompanyContentType.Order);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var ContentTypes = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (ContentTypes, totalCount);
    }

    public async Task AddContentTypesToCompanyContentTypeAsync(List<CompanyContentTypeTransfer> relatedContentTypes, int companyId, CancellationToken cancellationToken)
    {
        foreach (var ContentType in relatedContentTypes)
        {
            dbContext.CompanyContentTypes.Add(new CompanyContentType()
            {
                Active = ContentType.Active,
                CompanyId = companyId,
                ContentTypeId = ContentType.Id,
                Name = ContentType.Name,
                Order = ContentType.Order,
                Description = "",
            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}