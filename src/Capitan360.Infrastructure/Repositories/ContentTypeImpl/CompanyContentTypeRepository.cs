using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.ContentEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.ContentTypeImpl;

//public class CompanyContentTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyContentTypeRepository
//{
//    //---
//    public async Task<(IReadOnlyList<CompanyContentType>, int)> GetCompanyContentTypes(string? searchPhrase, int companyId, int active, int pageSize,
//        int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
//    {
//        var searchPhraseLower = searchPhrase?.ToLower();
//        var baseQuery = dbContext.CompanyContentTypes.AsNoTracking()
//            .Where(ct => searchPhraseLower == null || ct.ContentTypeName!.ToLower().Contains(searchPhraseLower));

//        baseQuery = active switch
//        {
//            1 => baseQuery.Where(a => a.Active),
//            0 => baseQuery.Where(a => !a.Active),
//            _ => baseQuery
//        };

//        var totalCount = await baseQuery.CountAsync(cancellationToken);

//        var columnsSelector = new Dictionary<string, Expression<Func<CompanyContentType, object>>>
// {
//     { nameof(CompanyContentType.ContentTypeName), ct => ct.ContentTypeName! },
//     { nameof(CompanyContentType.Active), ct => ct.Active! },
//     { nameof(CompanyContentType.OrderContentType), ct => ct.OrderContentType! }
// };

//        sortBy ??= nameof(CompanyContentType.OrderContentType);

//        var selectedColumn = columnsSelector[sortBy];
//        baseQuery = sortDirection == SortDirection.Ascending
//            ? baseQuery.OrderBy(selectedColumn)
//            : baseQuery.OrderByDescending(selectedColumn);

//        var contentTypes = await baseQuery
//            .Skip(pageSize * (pageNumber - 1))
//            .Take(pageSize)
//            .ToListAsync(cancellationToken);

//        return (contentTypes, totalCount);
//    }

//    public async Task<int> UpdateCompanyContentTypeForCompany(CompanyContentType companyContentType, CancellationToken ct)
//    {
//        dbContext.Entry(companyContentType).State = EntityState.Modified;
//        await unitOfWork.SaveChangesAsync(ct);
//        return companyContentType.Id;
//    }

//    public async Task<int> InsertCompanyContentTypeForCompany(CompanyContentType companyContentType, CancellationToken ct)
//    {
//        dbContext.CompanyContentTypes.Add(companyContentType);
//        await unitOfWork.SaveChangesAsync(ct);
//        return companyContentType.Id;
//    }

//    public async Task<CompanyContentType?> CheckExistCompanyContentTypeName(int companyId, int contentTypeId,
//        CancellationToken cancellationToken)
//    {
//        var result = await dbContext.CompanyContentTypes.SingleOrDefaultAsync(
//            x => x.CompanyId == companyId && x.ContentTypeId == contentTypeId, cancellationToken);

//        return result;
//    }

//    public async Task MoveContentTypeUpAsync(int companyId, int contentTypeId, CancellationToken cancellationToken)
//    {
//        var contentTypes = await dbContext.CompanyContentTypes
//     .Where(c => c.CompanyId == companyId)
//     .OrderBy(a => a.OrderContentType)
//     .ToListAsync(cancellationToken);

//        var currentContentType = contentTypes.SingleOrDefault(a => a.CompanyId == companyId && a.ContentTypeId == contentTypeId);
//        var currentIndex = contentTypes.IndexOf(currentContentType!);
//        if (currentIndex <= 0)
//            return;

//        var previousContentType = contentTypes[currentIndex - 1];
//        var currentOrder = currentContentType!.OrderContentType;
//        currentContentType.OrderContentType = previousContentType.OrderContentType;
//        previousContentType.OrderContentType = currentOrder;

//        await unitOfWork.SaveChangesAsync(cancellationToken);
//    }

//    public async Task MoveContentTypeDownAsync(int companyId, int contentTypeId, CancellationToken cancellationToken)
//    {
//        var contentTypes = await dbContext.CompanyContentTypes
//    .Where(c => c.CompanyId == companyId)
//    .OrderBy(a => a.OrderContentType)
//    .ToListAsync(cancellationToken);

//        var currentContentType = contentTypes.FirstOrDefault(a => a.CompanyId == companyId && a.ContentTypeId == contentTypeId);
//        var currentIndex = contentTypes.IndexOf(currentContentType!);
//        if (currentIndex >= contentTypes.Count - 1)
//            return;

//        var nextContentType = contentTypes[currentIndex + 1];
//        var tempOrder = currentContentType!.OrderContentType;
//        currentContentType.OrderContentType = nextContentType.OrderContentType;
//        nextContentType.OrderContentType = tempOrder;

//        await dbContext.SaveChangesAsync(cancellationToken);
//    }

//    public async Task<int> OrderContentType(int companyId, CancellationToken cancellationToken)
//    {
//        var maxOrder = await dbContext.CompanyContentTypes
//            .Where(ca => ca.CompanyId == companyId)
//            .MaxAsync(ca => (int?)ca.OrderContentType, cancellationToken) ?? 0;
//        return maxOrder;
//    }

//    // ---
//    public async Task CreateCompanyContentTypes(List<int> eligibleCompanies, ContentType contentType,
//        CancellationToken cancellationToken)
//    {
//        foreach (var companyId in eligibleCompanies)
//        {
//            dbContext.CompanyContentTypes.Add(new CompanyContentType()
//            {
//                CompanyId = companyId,
//                ContentTypeId = contentType.Id,
//                Active = contentType.Active,
//                ContentTypeName = contentType.ContentTypeName,
//                OrderContentType = contentType.OrderContentType
//            });
//        }

//        await unitOfWork.SaveChangesAsync(cancellationToken);
//    }

//    public async Task AddContentTypesToCompanyContentType(List<CompanyContentTypeTransfer> relatedContentTypes,
//        int companyId, CancellationToken cancellationToken)
//    {
//        foreach (var contentType in relatedContentTypes)
//        {
//            dbContext.CompanyContentTypes.Add(new CompanyContentType()
//            {
//                Active = contentType.Active,
//                CompanyId = companyId,
//                ContentTypeId = contentType.Id,
//                ContentTypeName = contentType.ContentTypeName,
//                OrderContentType = contentType.OrderContentType
//            });
//        }

//        await unitOfWork.SaveChangesAsync(cancellationToken);
//    }

//    public async Task DeleteAllContentsByCompanyId(int companyId, CancellationToken cancellationToken)
//    {


//        await dbContext.CompanyContentTypes
//     .Where(x => x.CompanyId == companyId)
//     .ExecuteUpdateAsync(s => s.SetProperty(
//         x => EF.Property<bool>(x, "Deleted"),
//         x => true
//     ), cancellationToken);
//    }

//    public async Task<bool> CheckExistAnyItem(int companyId, CancellationToken cancellationToken)
//    {
//        return await dbContext.CompanyContentTypes.AsNoTracking().AnyAsync(x => x.CompanyId == companyId, cancellationToken);
//    }
//}

public class CompanyContentTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyContentTypeRepository
{
    public async Task<(IReadOnlyList<CompanyContentType>, int)> GetCompanyContentTypes(string? searchPhrase, int companyId, int active, int pageSize,
        int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.CompanyContentTypes.AsNoTracking()
            .Include(x => x.ContentType)
            .Where(x => x.CompanyId == companyId);

        var z = await baseQuery.ToListAsync(cancellationToken);

        baseQuery = baseQuery.Where(ct => string.IsNullOrEmpty(searchPhraseLower)
                         || ct.ContentTypeName!.ToLower().Contains(searchPhraseLower));

        baseQuery = active switch
        {
            1 => baseQuery.Where(a => a.Active),
            0 => baseQuery.Where(a => !a.Active),
            _ => baseQuery
        };

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyContentType, object>>>
        {
            { nameof(CompanyContentType.ContentTypeName), ct => ct.ContentTypeName! },
            { nameof(CompanyContentType.Active), ct => ct.Active! },
            { nameof(CompanyContentType.OrderContentType), ct => ct.OrderContentType! }
        };

        sortBy ??= nameof(CompanyContentType.OrderContentType);

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

    public async Task<int> InsertCompanyContentTypeForCompany(CompanyContentType companyContentType, CancellationToken ct)
    {
        dbContext.CompanyContentTypes.Add(companyContentType);
        await unitOfWork.SaveChangesAsync(ct);
        return companyContentType.Id;
    }

    public async Task<int> UpdateCompanyContentTypeForCompany(CompanyContentType companyContentType, CancellationToken ct)
    {
        dbContext.Entry(companyContentType).State = EntityState.Modified;
        await unitOfWork.SaveChangesAsync(ct);
        return companyContentType.Id;
    }

    public async Task<CompanyContentType?> CheckExistCompanyContentTypeName(int companyId, int contentTypeId, CancellationToken cancellationToken)
    {
        var result = await dbContext.CompanyContentTypes.SingleOrDefaultAsync(
            x => x.CompanyId == companyId && x.ContentTypeId == contentTypeId, cancellationToken);

        return result;
    }

    public async Task MoveContentTypeUpAsync(int companyId, int contentTypeId, CancellationToken cancellationToken)
    {
        var contentTypes = await dbContext.CompanyContentTypes
            .Where(c => c.CompanyId == companyId)
            .OrderBy(a => a.OrderContentType)
            .ToListAsync(cancellationToken);

        var currentContentType = contentTypes.SingleOrDefault(a => a.CompanyId == companyId && a.ContentTypeId == contentTypeId);
        var currentIndex = contentTypes.IndexOf(currentContentType!);
        if (currentIndex <= 0)
            return;

        var previousContentType = contentTypes[currentIndex - 1];
        var currentOrder = currentContentType!.OrderContentType;
        currentContentType.OrderContentType = previousContentType.OrderContentType;
        previousContentType.OrderContentType = currentOrder;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveContentTypeDownAsync(int companyId, int contentTypeId, CancellationToken cancellationToken)
    {
        var contentTypes = await dbContext.CompanyContentTypes
            .Where(c => c.CompanyId == companyId)
            .OrderBy(a => a.OrderContentType)
            .ToListAsync(cancellationToken);

        var currentContentType = contentTypes.SingleOrDefault(a => a.CompanyId == companyId && a.ContentTypeId == contentTypeId);
        var currentIndex = contentTypes.IndexOf(currentContentType!);
        if (currentIndex >= contentTypes.Count - 1)
            return;

        var nextContentType = contentTypes[currentIndex + 1];
        var tempOrder = currentContentType!.OrderContentType;
        currentContentType.OrderContentType = nextContentType.OrderContentType;
        nextContentType.OrderContentType = tempOrder;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> OrderContentType(int companyId, CancellationToken cancellationToken)
    {
        var maxOrder = await dbContext.CompanyContentTypes
            .Where(ct => ct.CompanyId == companyId)
            .MaxAsync(ct => (int?)ct.OrderContentType, cancellationToken) ?? 0;
        return maxOrder;
    }

    public async Task CreateCompanyContentTypes(List<int> eligibleCompanies, ContentType contentType, CancellationToken cancellationToken)
    {
        foreach (var companyId in eligibleCompanies)
        {
            dbContext.CompanyContentTypes.Add(new CompanyContentType()
            {
                CompanyId = companyId,
                ContentTypeId = contentType.Id,
                Active = contentType.Active,
                ContentTypeName = contentType.ContentTypeName,
                OrderContentType = contentType.OrderContentType
            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    //public Task AddContentTypesToCompanyContentType(List<CompanyContentTypeTransfer> relatedContentTypes, int companyId, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}

    public async Task AddContentTypesToCompanyContentType(List<CompanyContentTypeTransfer> relatedContentTypes, int companyId, CancellationToken cancellationToken)
    {
        foreach (var contentType in relatedContentTypes)
        {
            dbContext.CompanyContentTypes.Add(new CompanyContentType()
            {
                Active = contentType.Active,
                CompanyId = companyId,
                ContentTypeId = contentType.Id,
                ContentTypeName = contentType.ContentTypeName,
                OrderContentType = contentType.OrderContentType
            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAllContentsByCompanyId(int companyId, CancellationToken cancellationToken)
    {
        await dbContext.CompanyContentTypes
            .Where(x => x.CompanyId == companyId)
            .ExecuteUpdateAsync(s => s.SetProperty(
                x => EF.Property<bool>(x, "Deleted"),
                x => true
            ), cancellationToken);
    }

    public async Task<bool> CheckExistAnyItem(int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyContentTypes.AsNoTracking()
            .AnyAsync(x => x.CompanyId == companyId, cancellationToken);
    }

    public async Task<CompanyContentType?> GetCompanyContentTypeById(int companyContentTypeId, CancellationToken cancellationToken, bool tracking = false)
    {
        if (tracking)
        {
            return await dbContext.CompanyContentTypes
                .Include(x => x.ContentType)
                .SingleOrDefaultAsync(x => x.Id == companyContentTypeId, cancellationToken);
        }
        else
        {
            return await dbContext.CompanyContentTypes
                .AsNoTracking()
                .Include(x => x.ContentType)
                .SingleOrDefaultAsync(x => x.Id == companyContentTypeId, cancellationToken);
        }
    }
}