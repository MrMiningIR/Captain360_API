using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.ContentTypes;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.ContentTypes;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.ContentTypes;

public class ContentTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IContentTypeRepository
{
    public async Task<bool> CheckExistContentTypeNameAsync(string contentTypeName, int companyTypeId, int? currentContentTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.ContentTypes.AnyAsync(item => item.Name.ToLower() == contentTypeName.ToLower().Trim() && item.CompanyTypeId == companyTypeId && (currentContentTypeId == null || item.Id != currentContentTypeId), cancellationToken);
    }

    public async Task<int> GetCountContentTypeAsync(int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.ContentTypes.CountAsync(item => item.CompanyTypeId == companyTypeId, cancellationToken: cancellationToken);
    }

    public async Task<int> CreateContentTypeAsync(ContentType contentType, CancellationToken cancellationToken)
    {
        dbContext.ContentTypes.Add(contentType);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return contentType.Id;
    }

    public async Task<ContentType?> GetContentTypeByIdAsync(int contentTypeId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<ContentType> query = dbContext.ContentTypes;

        if (loadData)
            query = query.Include(item => item.CompanyType);

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == contentTypeId, cancellationToken);
    }

    public async Task DeletePackageTypeAsync(int contentTypeId)
    {
        await Task.Yield();
    }

    public async Task MoveContentTypeUpAsync(int contentTypeId, CancellationToken cancellationToken)
    {
        var currentContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(item => item.Id == contentTypeId, cancellationToken: cancellationToken);
        if (currentContentType == null)
            return;

        var nextContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(item => item.CompanyTypeId == currentContentType.CompanyTypeId && item.Order == currentContentType.Order - 1, cancellationToken: cancellationToken);
        if (nextContentType == null)
            return;

        nextContentType.Order++;
        currentContentType.Order--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveContentTypeDownAsync(int contentTypeId, CancellationToken cancellationToken)
    {
        var currentContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(item => item.Id == contentTypeId, cancellationToken: cancellationToken);
        if (currentContentType == null)
            return;

        var nextContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(item => item.CompanyTypeId == currentContentType.CompanyTypeId && item.Order == currentContentType.Order + 1, cancellationToken: cancellationToken);
        if (nextContentType == null)
            return;

        nextContentType.Order--;
        currentContentType.Order++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<ContentType>, int)> GetAllContentTypesAsync(string? searchPhrase, string? sortBy, int companyTypeId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();
        var baseQuery = dbContext.ContentTypes.AsNoTracking()
                                              .Where(item => searchPhraseLower == null || item.Name.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(item => item.CompanyType);

        if (companyTypeId != 0)
            baseQuery = baseQuery.Where(item => item.CompanyTypeId == companyTypeId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<ContentType, object>>>
        {
            { nameof(ContentType.Order), item => item.Order}
        };

        sortBy ??= nameof(ContentType.Order);

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

    public async Task<List<CompanyContentTypeTransfer>> GetContentTypesByCompanyTypeIdAsync(int companyTypeId, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext.ContentTypes.Where(item => item.CompanyTypeId == companyTypeId);

        return await baseQuery.Select(item => new CompanyContentTypeTransfer()
        {
            Id = item.Id,
            Active = item.Active,
            Order = item.Order,
            Name = item.Name,
            Description = item.Description,
        }).ToListAsync(cancellationToken);
    }
}