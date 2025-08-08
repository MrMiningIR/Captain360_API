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
    public async Task<int> CreateContentTypeAsync(ContentType contentType, CancellationToken cancellationToken)
    {
        dbContext.ContentTypes.Add(contentType);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return contentType.Id;
    }

    public void Delete(ContentType contentType)
    {
        dbContext.Entry(contentType).Property("Deleted").CurrentValue = true;
    }

    public async Task<ContentType?> GetContentTypeByIdAsync(int contentTypeId, bool tracked, CancellationToken cancellationToken)
    {
        return tracked ? await dbContext.ContentTypes.Include(a => a.CompanyType).SingleOrDefaultAsync(a => a.Id == contentTypeId, cancellationToken)
            : await dbContext.ContentTypes.AsNoTracking().Include(a => a.CompanyType).SingleOrDefaultAsync(a => a.Id == contentTypeId, cancellationToken);
    }

    public async Task<(IReadOnlyList<ContentType>, int)> GetMatchingAllContentTypesAsync(string? searchPhrase, int companyTypeId, int active,
        int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.ContentTypes.Include(a => a.CompanyType).AsNoTracking()

            .Where(ct => searchPhraseLower == null || ct.ContentTypeName.ToLower().Contains(searchPhraseLower));

        if (companyTypeId != 0)
            baseQuery = baseQuery.Where(ct => ct.CompanyTypeId == companyTypeId);

        baseQuery = active switch
        {
            1 => baseQuery.Where(a => a.Active),
            0 => baseQuery.Where(a => !a.Active),
            _ => baseQuery
        };

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<ContentType, object>>>
        {
            { nameof(ContentType.ContentTypeName), ct => ct.ContentTypeName },
            { nameof(ContentType.Active), ct => ct.Active },
            { nameof(ContentType.OrderContentType), ct => ct.OrderContentType }
        };

        sortBy ??= nameof(ContentType.OrderContentType);

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

    public async Task<bool> CheckExistContentTypeNameAsync(string contentTypeName, int? currentContentTypeId, int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.ContentTypes.AnyAsync(pt => pt.CompanyTypeId == companyTypeId && (currentContentTypeId == null || pt.Id != currentContentTypeId) && pt.ContentTypeName.ToLower() == contentTypeName.ToLower().Trim(), cancellationToken);
    }

    public async Task<int> GetCountContentTypeAsync(int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.ContentTypes
             .CountAsync(ca => ca.CompanyTypeId == companyTypeId, cancellationToken: cancellationToken);
    }

    public async Task MoveContentTypeUpAsync(int contentTypeId, CancellationToken cancellationToken)
    {
        var currentContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(p => p.Id == contentTypeId, cancellationToken: cancellationToken);
        var nextContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(p => p.CompanyTypeId == currentContentType!.CompanyTypeId && p.OrderContentType == currentContentType.OrderContentType - 1, cancellationToken: cancellationToken);

        nextContentType!.OrderContentType++;
        currentContentType!.OrderContentType--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveContentTypeDownAsync(int contentTypeId, CancellationToken cancellationToken)
    {
        var currentContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(p => p.Id == contentTypeId, cancellationToken: cancellationToken);
        var nextContentType = await dbContext.ContentTypes.SingleOrDefaultAsync(p => p.CompanyTypeId == currentContentType!.CompanyTypeId && p.OrderContentType == currentContentType.OrderContentType + 1, cancellationToken: cancellationToken);

        nextContentType!.OrderContentType--;
        currentContentType!.OrderContentType++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CompanyContentTypeTransfer>> GetContentTypesByCompanyTypeIdAsync(int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.ContentTypes.AsNoTracking().Where(x => x.CompanyTypeId == companyTypeId)
            .Select(x => new CompanyContentTypeTransfer()
            {
                Id = x.Id,
                Active = x.Active,
                OrderContentType = x.OrderContentType,
                ContentTypeName = x.ContentTypeName,
                CompanyContentTypeDescription = x.ContentTypeDescription,
            }).ToListAsync(cancellationToken);
    }
}