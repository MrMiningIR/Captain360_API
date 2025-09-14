using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

public class CompanyDomesticPathsRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
    : ICompanyDomesticPathsRepository
{
    public async Task<bool> CheckExistCompanyDomesticPathPathAsync(int sourceCityId, int destinationCityId, int? currentCompanyDomesticPathId, int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticPaths.AnyAsync(cdp => cdp.CompanyId == companyId && (currentCompanyDomesticPathId == null || cdp.Id != currentCompanyDomesticPathId) &&
                                                                    cdp.SourceCityId == sourceCityId && cdp.DestinationCityId == destinationCityId, cancellationToken);
    }

    public async Task<int> CreateCompanyDomesticPathAsync(CompanyDomesticPaths companyDomesticPath, CancellationToken cancellationToken)
    {
        dbContext.CompanyDomesticPaths.Add(companyDomesticPath);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyDomesticPath.Id;
    }

    public async Task<CompanyDomesticPaths?> GetCompanyDomesticPathByIdAsync(int companyDomesticPathId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticPaths> query = dbContext.CompanyDomesticPaths;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadData)
        {
            query = query.Include(a => a.Company);
            query = query.Include(a => a.SourceCountry);
            query = query.Include(a => a.SourceProvince);
            query = query.Include(a => a.SourceCity);
            query = query.Include(a => a.DestinationCountry);
            query = query.Include(a => a.DestinationProvince);
            query = query.Include(a => a.DestinationCity);
        }

        return await query.SingleOrDefaultAsync(a => a.Id == companyDomesticPathId, cancellationToken);
    }

    public async Task DeleteCompanyDomesticPathAsync(CompanyDomesticPaths companyDomesticPath)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<CompanyDomesticPaths>, int)> GetMatchingAllCompanyDomesticPathsAsync(string? searchPhrase, string? sortBy, int companyId, bool loadData, int active, int sourceCityId, int destinationCityId, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanyDomesticPaths.AsNoTracking()
                                                      .Where(cdp => searchPhraseLower == null || cdp.SourceCity.PersianName.ToLower().Contains(searchPhraseLower) || cdp.SourceCity.EnglishName.ToLower().Contains(searchPhraseLower) ||
                                                                                                 cdp.DestinationCountry.PersianName.ToLower().Contains(searchPhraseLower) || cdp.DestinationCountry.EnglishName.ToLower().Contains(searchPhraseLower));

        baseQuery = baseQuery.Where(pt => pt.CompanyId == companyId);

        baseQuery = active switch
        {
            1 => baseQuery.Where(a => a.Active),
            0 => baseQuery.Where(a => !a.Active),
            _ => baseQuery
        };

        if (loadData)
        {
            baseQuery = baseQuery.Include(a => a.Company);
            baseQuery = baseQuery.Include(a => a.SourceCountry);
            baseQuery = baseQuery.Include(a => a.SourceProvince);
            baseQuery = baseQuery.Include(a => a.SourceCity);
            baseQuery = baseQuery.Include(a => a.DestinationCountry);
            baseQuery = baseQuery.Include(a => a.DestinationProvince);
            baseQuery = baseQuery.Include(a => a.DestinationCity);
        }

        if (sourceCityId != 0)
        {
            baseQuery = baseQuery.Where(pt => pt.SourceCityId == sourceCityId);
        }

        if (destinationCityId != 0)
        {
            baseQuery = baseQuery.Where(pt => pt.DestinationCityId == destinationCityId);
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyDomesticPaths, object>>>
        {
            { nameof(CompanyDomesticPaths.SourceCity.PersianName), cdp => cdp.SourceCity.PersianName},
            { nameof(CompanyDomesticPaths.DestinationCity.PersianName), cdp => cdp.DestinationCity.PersianName},
        };

        sortBy ??= nameof(CompanyDomesticPaths.DestinationCity.PersianName);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var companyDomesticPaths = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyDomesticPaths, totalCount);
    }

    public async Task<IReadOnlyList<CompanyDomesticPaths>?> GetCompanyDomesticPathsByCompanyIdAsync(int companyId, bool tracked, bool loadData, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext.CompanyDomesticPaths.AsNoTracking()
                                                      .Where(cdp => cdp.CompanyId == companyId);
        if (loadData)
        {
            baseQuery = baseQuery.Include(a => a.Company);
            baseQuery = baseQuery.Include(a => a.SourceCountry);
            baseQuery = baseQuery.Include(a => a.SourceProvince);
            baseQuery = baseQuery.Include(a => a.SourceCity);
            baseQuery = baseQuery.Include(a => a.DestinationCountry);
            baseQuery = baseQuery.Include(a => a.DestinationProvince);
            baseQuery = baseQuery.Include(a => a.DestinationCity);
        }

        baseQuery = baseQuery.OrderBy(cdp => cdp.DestinationCity.PersianName);

        var companyDomesticPaths = await baseQuery.ToListAsync(cancellationToken);

        return companyDomesticPaths;
    }
}