using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.CompanyContentEntity;
using Capitan360.Domain.Entities.ContentEntity;
using Capitan360.Domain.Repositories.ContentTypeRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyContentTypeImpl;



public class CompanyContentTypeRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyContentTypeRepository
{
    public async Task<(IReadOnlyList<CompanyContentType>, int)> GetCompanyContentTypesAsync(string? searchPhrase, int companyId, int active, int pageSize,
        int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.CompanyContentTypes.AsNoTracking()
            .Include(x => x.ContentType)
            .Where(x => x.CompanyId == companyId);


        baseQuery = baseQuery.Where(ct => string.IsNullOrEmpty(searchPhraseLower)
                         || ct.CompanyContentTypeName!.ToLower().Contains(searchPhraseLower));

        baseQuery = active switch
        {
            1 => baseQuery.Where(a => a.CompanyContentTypeActive),
            0 => baseQuery.Where(a => !a.CompanyContentTypeActive),
            _ => baseQuery
        };

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyContentType, object>>>
        {
            { nameof(CompanyContentType.CompanyContentTypeName), ct => ct.CompanyContentTypeName! },
            { nameof(CompanyContentType.CompanyContentTypeActive), ct => ct.CompanyContentTypeActive! },
            { nameof(CompanyContentType.CompanyContentTypeOrder), ct => ct.CompanyContentTypeOrder! }
        };

        sortBy ??= nameof(CompanyContentType.CompanyContentTypeOrder);

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

    public async Task<int> InsertCompanyContentTypeForCompanyAsync(CompanyContentType companyContentType, CancellationToken ct)
    {
        dbContext.CompanyContentTypes.Add(companyContentType);
        await unitOfWork.SaveChangesAsync(ct);
        return companyContentType.Id;
    }

    public async Task<int> UpdateCompanyContentTypeForCompanyAsync(CompanyContentType companyContentType, CancellationToken ct)
    {
        dbContext.Entry(companyContentType).State = EntityState.Modified;
        await unitOfWork.SaveChangesAsync(ct);
        return companyContentType.Id;
    }

    public async Task<bool> CheckExistCompanyContentTypeNameAsync(string companyContentTypeName, int? currentCompanyContentTypeId, int companyId, CancellationToken cancellationToken)//ch**
    {
        return await dbContext.CompanyContentTypes.AnyAsync(pt => pt.CompanyId == companyId && (currentCompanyContentTypeId == null || pt.Id != currentCompanyContentTypeId) && pt.CompanyContentTypeName.ToLower() == companyContentTypeName.ToLower().Trim(), cancellationToken);
    }

    public async Task MoveCompanyContentTypeUpAsync(int companyContentTypeId, CancellationToken cancellationToken)//ch**
    {
        var currentCompanyContentType = await dbContext.CompanyContentTypes.SingleOrDefaultAsync(p => p.Id == companyContentTypeId, cancellationToken: cancellationToken);
        var nextCompanyContentType = await dbContext.CompanyContentTypes.SingleOrDefaultAsync(p => p.CompanyId == currentCompanyContentType!.CompanyId && p.CompanyContentTypeOrder == currentCompanyContentType.CompanyContentTypeOrder - 1, cancellationToken: cancellationToken);

        nextCompanyContentType!.CompanyContentTypeOrder++;
        currentCompanyContentType!.CompanyContentTypeOrder--;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MoveCompanyContentTypeDownAsync(int companyContentTypeId, CancellationToken cancellationToken)
    {
        var currentCompanyContentType = await dbContext.CompanyContentTypes.SingleOrDefaultAsync(p => p.Id == companyContentTypeId, cancellationToken: cancellationToken);
        var nextCompanyContentType = await dbContext.CompanyContentTypes.SingleOrDefaultAsync(p => p.CompanyId == currentCompanyContentType!.CompanyId && p.CompanyContentTypeOrder == currentCompanyContentType.CompanyContentTypeOrder + 1, cancellationToken: cancellationToken);

        nextCompanyContentType!.CompanyContentTypeOrder--;
        currentCompanyContentType!.CompanyContentTypeOrder++;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    } //ch**

    public async Task<int> GetCountCompanyContentTypeAsync(int companyId, CancellationToken cancellationToken)//ch**
    {
        return await dbContext.CompanyContentTypes.CountAsync(pt => pt.CompanyId == companyId, cancellationToken: cancellationToken);
    }

    public async Task CreateCompanyContentTypesAsync(List<int> companiesId, ContentType contentType, CancellationToken cancellationToken)//ch**
    {
        foreach (var companyId in companiesId)
        {
            dbContext.CompanyContentTypes.Add(new CompanyContentType()
            {
                CompanyId = companyId,
                ContentTypeId = contentType.Id,
                CompanyContentTypeActive = contentType.ContentTypeActive,
                CompanyContentTypeName = contentType.ContentTypeName,
                CompanyContentTypeOrder = contentType.ContentTypeOrder,


            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AddContentTypesToCompanyContentTypeAsync(List<CompanyContentTypeTransfer> relatedContentTypes, int companyId, CancellationToken cancellationToken) //ch**
    {
        foreach (var contentType in relatedContentTypes)
        {
            dbContext.CompanyContentTypes.Add(new CompanyContentType()
            {
                CompanyContentTypeActive = contentType.ContentTypeActive,
                CompanyId = companyId,
                ContentTypeId = contentType.Id,
                CompanyContentTypeName = contentType.ContentTypeName,
                CompanyContentTypeOrder = contentType.ContentTypeOrder
            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAllContentsByCompanyIdAsync(int companyId, CancellationToken cancellationToken)
    {
        await dbContext.CompanyContentTypes
            .Where(x => x.CompanyId == companyId)
            .ExecuteUpdateAsync(s => s.SetProperty(
                x => EF.Property<bool>(x, "Deleted"),
                x => true
            ), cancellationToken);
    }

    public async Task<bool> CheckExistAnyItemAsync(int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyContentTypes.AsNoTracking()
            .AnyAsync(x => x.CompanyId == companyId, cancellationToken);
    }

    public async Task<CompanyContentType?> GetCompanyContentTypeByIdAsync(int companyContentTypeId, bool tracked, bool loadData, CancellationToken cancellationToken)//ch**
    {
        IQueryable<CompanyContentType> query = dbContext.CompanyContentTypes;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(a => a.Company);

        return await query.SingleOrDefaultAsync(a => a.Id == companyContentTypeId, cancellationToken);

    }


    public async Task DeleteCompanyContentTypeAsync(CompanyContentType companyContentType)//ch**
    {
        await Task.Yield();
    }
    public async Task<(IReadOnlyList<CompanyContentType>, int)> GetMatchingAllCompanyContentTypesAsync(string? searchPhrase, string? sortBy, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.CompanyContentTypes.AsNoTracking()
                                                     .Where(pt => searchPhraseLower == null || pt.CompanyContentTypeName.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(a => a.Company);

        if (companyId != 0)
        {
            baseQuery = baseQuery.Where(pt => pt.CompanyId == companyId);
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyContentType, object>>>
        {
            { nameof(CompanyContentType.CompanyContentTypeOrder), pt => pt.CompanyContentTypeOrder! }
        };

        sortBy ??= nameof(CompanyContentType.CompanyContentTypeOrder);

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
}