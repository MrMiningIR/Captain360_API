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
        return await dbContext.ContentTypes.AnyAsync(item => item.CompanyTypeId == companyTypeId && (currentContentTypeId == null || item.Id != currentContentTypeId) && item.ContentTypeName.ToLower() == contentTypeName.ToLower().Trim(), cancellationToken);
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

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(item => item.CompanyType);

        return await query.SingleOrDefaultAsync(item => item.Id == contentTypeId, cancellationToken);
    }

    public async Task DeletePackageTypeAsync(ContentType contentType)
    {
        await Task.Yield();
    }

    public async Task MoveContentTypeUpAsync(int contentTypeId, CancellationToken cancellationToken)
    {
        var currentContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(item => item.Id == contentTypeId, cancellationToken: cancellationToken);
        if (currentContentType == null)
            return;

        var nextContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(item => item.CompanyTypeId == currentContentType.CompanyTypeId && item.ContentTypeOrder == currentContentType.ContentTypeOrder - 1, cancellationToken: cancellationToken);
        if (nextContentType == null)
            return;

        nextContentType.ContentTypeOrder++;
        currentContentType.ContentTypeOrder--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveContentTypeDownAsync(int contentTypeId, CancellationToken cancellationToken)
    {
        var currentContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(item => item.Id == contentTypeId, cancellationToken: cancellationToken);
        if (currentContentType == null)
            return;

        var nextContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(item => item.CompanyTypeId == currentContentType.CompanyTypeId && item.ContentTypeOrder == currentContentType.ContentTypeOrder + 1, cancellationToken: cancellationToken);
        if (nextContentType == null)
            return;

        nextContentType.ContentTypeOrder--;
        currentContentType.ContentTypeOrder++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<ContentType>, int)> GetAllContentTypesAsync(string? searchPhrase, string? sortBy, int companyTypeId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.ContentTypes.AsNoTracking()
                                              .Where(item => searchPhraseLower == null || item.ContentTypeName.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(item => item.CompanyType);

        if (companyTypeId != 0)
            baseQuery = baseQuery.Where(item => item.CompanyTypeId == companyTypeId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<ContentType, object>>>
        {
            { nameof(ContentType.ContentTypeOrder), item => item.ContentTypeOrder}
        };

        sortBy ??= nameof(ContentType.ContentTypeOrder);

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

    public async Task<List<CompanyContentTypeTransfer>> GetContentTypesByCompanyTypeIdAsync(int companyTypeId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext.ContentTypes.Where(item => item.CompanyTypeId == companyTypeId);

        if (loadData)
            baseQuery = baseQuery.Include(item => item.CompanyType);

        if (!tracked)
            baseQuery = baseQuery.AsNoTracking();

        return await baseQuery.Select(item => new CompanyContentTypeTransfer()
        {
            Id = item.Id,
            ContentTypeActive = item.ContentTypeActive,
            ContentTypeOrder = item.ContentTypeOrder,
            ContentTypeName = item.ContentTypeName,
            CompanyContentTypeDescription = item.ContentTypeDescription,
        }).ToListAsync(cancellationToken);
    }
}