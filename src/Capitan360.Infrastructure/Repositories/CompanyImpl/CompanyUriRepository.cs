using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

public class CompanyUriRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyUriRepository
{
    public async Task<bool> CheckExistCompanyUriUriAsync(string companyUriUri, int? currentCompanyUriId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyUris.AnyAsync(item => (currentCompanyUriId == null || item.Id != currentCompanyUriId) && item.Uri.ToLower() == companyUriUri.ToLower().Trim(), cancellationToken);
    }

    public async Task<int> CreateCompanyUriAsync(CompanyUri companyUri, CancellationToken cancellationToken)
    {
        dbContext.CompanyUris.Add(companyUri);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyUri.Id;
    }

    public async Task<CompanyUri?> GetCompanyUriByIdAsync(int companyUriId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<CompanyUri> query = dbContext.CompanyUris;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(item =>item.Company);

        return await query.SingleOrDefaultAsync(item =>item.Id == companyUriId, cancellationToken);
    }

    public async Task<IReadOnlyList<CompanyUri>?> GetCompanyUriByCompanyIdAsync(int companyUriCompanyId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<CompanyUri> query = dbContext.CompanyUris;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
            query = query.Include(item =>item.Company);

        return await query.Where(item =>item.CompanyId == companyUriCompanyId)
                          .ToListAsync(cancellationToken);
    }

    public async Task DeleteCompanyUriAsync(CompanyUri companyUri)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<CompanyUri>, int)> GetAllCompanyUrisAsync(string? searchPhrase, string? sortBy, int companyId, int active, int captain360Uri, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanyUris.AsNoTracking()
                                              .Where(item => searchPhraseLower == null || item.Description.ToLower().Contains(searchPhraseLower) || item.Uri.ToLower().Contains(searchPhraseLower));

        if (loadData)
            baseQuery = baseQuery.Include(item =>item.Company);

        baseQuery = active switch
        {
            1 => baseQuery.Where(item =>item.Active),
            0 => baseQuery.Where(item => !item.Active),
            _ => baseQuery
        };

        baseQuery = captain360Uri switch
        {
            1 => baseQuery.Where(item =>item.Captain360Uri),
            0 => baseQuery.Where(item => !item.Captain360Uri),
            _ => baseQuery
        };

        if (companyId != 0)
            baseQuery = baseQuery.Where(item => item.CompanyId == companyId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyUri, object>>>
        {
            { nameof(CompanyUri.Uri), item => item.Uri}
        };

        sortBy ??= nameof(CompanyUri.Uri);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var packageTypes = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (packageTypes, totalCount);
    }
}