using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.ContentEntity;
using Capitan360.Domain.Repositories.ContentRepo;
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



    public async Task<ContentType?> GetContentTypeById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.ContentTypes.Include(a => a.CompanyType).SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
    }


    public async Task<(IReadOnlyList<ContentType>, int)> GetMatchingAllContentTypes(string? searchPhrase, int companyTypeId, int active,
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

    public async Task<bool> CheckExistContentTypeName(string contentTypeName, int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.ContentTypes.AnyAsync(ct => ct.CompanyTypeId == companyTypeId && ct.ContentTypeName == contentTypeName.ToLower().Trim(), cancellationToken);
    }




    public async Task<int> OrderContentType(int companyTypeId, CancellationToken cancellationToken)
    {
        var maxOrder = await dbContext.ContentTypes
            .Where(ca => ca.CompanyTypeId == companyTypeId)
            .MaxAsync(ca => (int?)ca.OrderContentType, cancellationToken) ?? 0;
        return maxOrder;
    }

    public async Task MoveContentTypeUpAsync(int companyTypeId, int contentTypeId, CancellationToken cancellationToken)
    {
        var contentTypes = await dbContext.ContentTypes
            .Where(c => c.CompanyTypeId == companyTypeId)
            .OrderBy(a => a.OrderContentType)
            .ToListAsync(cancellationToken);

        var currentContentType = contentTypes.SingleOrDefault(a => a.Id == contentTypeId);
        var currentIndex = contentTypes.IndexOf(currentContentType!);
        if (currentIndex <= 0)
            return;

        var previousContentType = contentTypes[currentIndex - 1];
        var currentOrder = currentContentType!.OrderContentType;
        currentContentType.OrderContentType = previousContentType.OrderContentType;
        previousContentType.OrderContentType = currentOrder;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveContentTypeDownAsync(int companyTypeId, int contentTypeId, CancellationToken cancellationToken)
    {
        var contentTypes = await dbContext.ContentTypes
            .Where(c => c.CompanyTypeId == companyTypeId)
            .OrderBy(a => a.OrderContentType)
            .ToListAsync(cancellationToken);

        var currentContentType = contentTypes.FirstOrDefault(a => a.Id == contentTypeId);
        var currentIndex = contentTypes.IndexOf(currentContentType!);
        if (currentIndex >= contentTypes.Count - 1)
            return;

        var nextContentType = contentTypes[currentIndex + 1];
        var tempOrder = currentContentType!.OrderContentType;
        currentContentType.OrderContentType = nextContentType.OrderContentType;
        nextContentType.OrderContentType = tempOrder;

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CompanyContentTypeTransfer>> GetContentTypesByCompanyTypeId(int companyTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.ContentTypes.AsNoTracking().Where(x => x.CompanyTypeId == companyTypeId)
            .Select(x => new CompanyContentTypeTransfer
            {
                Id = x.Id,
                ContentTypeName = x.ContentTypeName,
                Active = x.Active,
                OrderContentType = x.OrderContentType

            }).ToListAsync(cancellationToken);
    }
}