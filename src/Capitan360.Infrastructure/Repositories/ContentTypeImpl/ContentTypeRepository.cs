using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.ContentEntity;
using Capitan360.Domain.Repositories.ContentTypeRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.ContentTypeImpl;

public class ContentTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IContentTypeRepository
{
    public async Task<bool> CheckExistContentTypeNameAsync(string contentTypeName, int? currentContentTypeId, int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.ContentTypes.AnyAsync(pt => pt.CompanyTypeId == companyTypeId
                                                           && (currentContentTypeId == null || pt.Id != currentContentTypeId) && pt.ContentTypeName.ToLower() == contentTypeName.ToLower().Trim(), cancellationToken);
    }

    public async Task<int> GetCountContentTypeAsync(int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.ContentTypes.CountAsync(pt => pt.CompanyTypeId == companyTypeId, cancellationToken: cancellationToken);
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

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.CompanyType);

        return await query.SingleOrDefaultAsync(a => a.Id == contentTypeId, cancellationToken);
    }

    public async Task Delete(ContentType contentType)
    {
        await Task.Yield();
    }

    public async Task MoveContentTypeUpAsync(int contentTypeId, CancellationToken cancellationToken)
    {
        var currentContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(p => p.Id == contentTypeId, cancellationToken: cancellationToken);
        var nextContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(p => p.CompanyTypeId == currentContentType.CompanyTypeId && p.ContentTypeOrder == currentContentType.ContentTypeOrder - 1, cancellationToken: cancellationToken);

        if (nextContentType != null)
            nextContentType.ContentTypeOrder++;

        if (currentContentType != null)
            currentContentType.ContentTypeOrder--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<ContentType>, int)> GetMatchingAllContentTypesAsync(string? searchPhrase,
        string? sortBy, int companyTypeId, int active, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.ContentTypes.AsNoTracking()
                                              .Where(pt => searchPhraseLower == null || pt.ContentTypeName.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(a => a.CompanyType);


        if (companyTypeId != 1)
            baseQuery = baseQuery.Where(ct => ct.CompanyTypeId == companyTypeId);

        baseQuery = active switch
        {
            1 => baseQuery.Where(a => a.ContentTypeActive),
            0 => baseQuery.Where(a => !a.ContentTypeActive),
            _ => baseQuery
        };

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<ContentType, object>>>
        {
            { nameof(ContentType.ContentTypeOrder), pt => pt.ContentTypeOrder}
        };

        sortBy ??= nameof(ContentType.ContentTypeOrder);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var contentTypes = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (contentTypes, totalCount);
    }

    public async Task MoveContentTypeDownAsync(int contentTypeId, CancellationToken cancellationToken)
    {
        var currentContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(p => p.Id == contentTypeId, cancellationToken: cancellationToken);

        var nextContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(p => p.CompanyTypeId == currentContentType.CompanyTypeId && p.ContentTypeOrder == currentContentType.ContentTypeOrder + 1, cancellationToken: cancellationToken);

        if (nextContentType != null)
            nextContentType.ContentTypeOrder--;
        if (currentContentType != null)
            currentContentType.ContentTypeOrder++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CompanyContentTypeTransfer>> GetContentTypesByCompanyTypeIdAsync(int companyTypeId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext.ContentTypes.Where(x => x.CompanyTypeId == companyTypeId);

        if (loadData)
            baseQuery = baseQuery.Include(a => a.CompanyType);

        if (!tracked)
            baseQuery = baseQuery.AsNoTracking();

        return await baseQuery.Select(x => new CompanyContentTypeTransfer()
        {
            Id = x.Id,
            ContentTypeActive = x.ContentTypeActive,
            ContentTypeOrder = x.ContentTypeOrder,
            ContentTypeName = x.ContentTypeName,
            CompanyContentTypeDescription = x.ContentTypeDescription,
        })
                              .ToListAsync(cancellationToken);
    }
    //public async Task<(IReadOnlyList<ContentType>, int)> GetMatchingAllContentTypesAsync(string? searchPhrase, string? sortBy, int companyTypeId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    //{
    //    var searchPhraseLower = searchPhrase?.ToLower();
    //    var baseQuery = dbContext.ContentTypes.AsNoTracking()
    //                                          .Where(pt => searchPhraseLower == null || pt.ContentTypeName.ToLower().Contains(searchPhraseLower));

    //    if (loadData)
    //        baseQuery = baseQuery.Include(a => a.CompanyType);

    //    if (companyTypeId != 0)
    //        baseQuery = baseQuery.Where(pt => pt.CompanyTypeId == companyTypeId);

    //    var totalCount = await baseQuery.CountAsync(cancellationToken);

    //    var columnsSelector = new Dictionary<string, Expression<Func<ContentType, object>>>
    //    {
    //        { nameof(ContentType.ContentTypeOrder), pt => pt.ContentTypeOrder}
    //    };

    //    sortBy ??= nameof(ContentType.ContentTypeOrder);

    //    var selectedColumn = columnsSelector[sortBy];
    //    baseQuery = sortDirection == SortDirection.Ascending
    //        ? baseQuery.OrderBy(selectedColumn)
    //        : baseQuery.OrderByDescending(selectedColumn);

    //    var contentTypes = await baseQuery
    //        .Skip(pageSize * (pageNumber - 1))
    //        .Take(pageSize)
    //        .ToListAsync(cancellationToken);

    //    return (contentTypes, totalCount);
    //}
}