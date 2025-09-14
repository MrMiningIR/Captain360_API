using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Entities.ContentEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

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
                CompanyContentTypeActive = ContentType.ContentTypeActive,
                CompanyContentTypeName = ContentType.ContentTypeName,
                CompanyContentTypeOrder = ContentType.ContentTypeOrder,
                CompanyContentTypeDescription = "",
            });
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> CheckExistCompanyContentTypeNameAsync(string companyContentTypeName, int? currentCompanyContentTypeId, int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyContentTypes.AnyAsync(item => item.CompanyId == companyId && (currentCompanyContentTypeId == null || item.Id != currentCompanyContentTypeId) && item.CompanyContentTypeName.ToLower() == companyContentTypeName.ToLower().Trim(), cancellationToken);
    }

    public async Task<int> GetCountCompanyContentTypeAsync(int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyContentTypes.CountAsync(item => item.CompanyId == companyId, cancellationToken: cancellationToken);
    }

    public async Task<CompanyContentType?> GetCompanyContentTypeByIdAsync(int companyContentTypeId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<CompanyContentType> query = dbContext.CompanyContentTypes;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(item => item.Company);

        return await query.SingleOrDefaultAsync(item => item.Id == companyContentTypeId, cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyContentType>?> GetCompanyContentTypeByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<CompanyContentType> query = dbContext.CompanyContentTypes;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(item => item.Company);

        return await query.Where(item => item.CompanyId == companyId)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteCompanyContentTypeAsync(CompanyContentType cmpanyContentType)
    {
        await Task.Yield();
    }

    public async Task MoveCompanyContentTypeUpAsync(int companyContentTypeId, CancellationToken cancellationToken)
    {
        var currentCompanyContentType = await dbContext.CompanyContentTypes.SingleOrDefaultAsync(item => item.Id == companyContentTypeId, cancellationToken: cancellationToken);
        var nextCompanyContentType = await dbContext.CompanyContentTypes.SingleOrDefaultAsync(item => item.CompanyId == currentCompanyContentType!.CompanyId && item.CompanyContentTypeOrder == currentCompanyContentType.CompanyContentTypeOrder - 1, cancellationToken: cancellationToken);

        nextCompanyContentType!.CompanyContentTypeOrder++;
        currentCompanyContentType!.CompanyContentTypeOrder--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveCompanyContentTypeDownAsync(int companyContentTypeId, CancellationToken cancellationToken)
    {
        var currentCompanyContentType = await dbContext.CompanyContentTypes.SingleOrDefaultAsync(item => item.Id == companyContentTypeId, cancellationToken: cancellationToken);
        var nextCompanyContentType = await dbContext.CompanyContentTypes.SingleOrDefaultAsync(item => item.CompanyId == currentCompanyContentType!.CompanyId && item.CompanyContentTypeOrder == currentCompanyContentType.CompanyContentTypeOrder + 1, cancellationToken: cancellationToken);

        nextCompanyContentType!.CompanyContentTypeOrder--;
        currentCompanyContentType!.CompanyContentTypeOrder++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<CompanyContentType>, int)> GetAllCompanyContentTypesAsync(string? searchPhrase, string? sortBy, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.CompanyContentTypes.AsNoTracking()
                                                     .Where(item => searchPhraseLower == null || item.CompanyContentTypeName.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(item => item.Company);

        if (companyId != 0)
        {
            baseQuery = baseQuery.Where(item => item.CompanyId == companyId);
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyContentType, object>>>
        {
            { nameof(CompanyContentType.CompanyContentTypeOrder), item => item.CompanyContentTypeOrder }
        };

        sortBy ??= nameof(CompanyContentType.CompanyContentTypeOrder);

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
                CompanyContentTypeActive = ContentType.ContentTypeActive,
                CompanyId = companyId,
                ContentTypeId = ContentType.Id,
                CompanyContentTypeName = ContentType.ContentTypeName,
                CompanyContentTypeOrder = ContentType.ContentTypeOrder,
                CompanyContentTypeDescription = "",
            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}