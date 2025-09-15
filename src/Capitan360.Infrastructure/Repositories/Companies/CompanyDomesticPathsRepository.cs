using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Companies;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.Companies;

public class CompanyDomesticPathsRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
    : ICompanyDomesticPathsRepository
{
    public async Task<bool> CheckExistCompanyDomesticPathPathAsync(int companyId, int sourceCityId, int destinationCityId, int? currentCompanyDomesticPathId,  CancellationToken cancellationToken)
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

    public async Task<CompanyDomesticPaths?> GetCompanyDomesticPathByIdAsync(int companyDomesticPathId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticPaths> query = dbContext.CompanyDomesticPaths;

        if (loadData)
        {
            query = query.Include(item => item.Company);
            query = query.Include(item => item.SourceCountry);
            query = query.Include(item => item.SourceProvince);
            query = query.Include(item => item.SourceCity);
            query = query.Include(item => item.DestinationCountry);
            query = query.Include(item => item.DestinationProvince);
            query = query.Include(item => item.DestinationCity);
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == companyDomesticPathId, cancellationToken);
    }

    public async Task DeleteCompanyDomesticPathAsync(int companyDomesticPathId)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<CompanyDomesticPaths>, int)> GetAllCompanyDomesticPathsAsync(string? searchPhrase, string? sortBy, int companyId, bool loadData, int active, int sourceCityId, int destinationCityId, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower().Trim();
        var baseQuery = dbContext.CompanyDomesticPaths.AsNoTracking()
                                                      .Where(cdp => searchPhraseLower == null || cdp.SourceCity!.PersianName.ToLower().Contains(searchPhraseLower) || cdp.SourceCity!.EnglishName!.ToLower().Contains(searchPhraseLower) ||
                                                                                                 cdp.DestinationCountry!.PersianName.ToLower().Contains(searchPhraseLower) || cdp.DestinationCountry!.EnglishName!.ToLower().Contains(searchPhraseLower));

        baseQuery = baseQuery.Where(pt => pt.CompanyId == companyId);

        baseQuery = active switch
        {
            1 => baseQuery.Where(item => item.Active),
            0 => baseQuery.Where(item => !item.Active),
            _ => baseQuery
        };

        if (loadData)
        {
            baseQuery = baseQuery.Include(item => item.Company);
            baseQuery = baseQuery.Include(item => item.SourceCountry);
            baseQuery = baseQuery.Include(item => item.SourceProvince);
            baseQuery = baseQuery.Include(item => item.SourceCity);
            baseQuery = baseQuery.Include(item => item.DestinationCountry);
            baseQuery = baseQuery.Include(item => item.DestinationProvince);
            baseQuery = baseQuery.Include(item => item.DestinationCity);
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
            { nameof(CompanyDomesticPaths.SourceCity.PersianName), cdp => cdp.SourceCity!.PersianName},
            { nameof(CompanyDomesticPaths.DestinationCity.PersianName), cdp => cdp.DestinationCity!.PersianName},
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

    public async Task<IReadOnlyList<CompanyDomesticPaths>?> GetCompanyDomesticPathsByCompanyIdAsync(int companyId, bool loadData, bool tracked,  CancellationToken cancellationToken)
    {
        var baseQuery = dbContext.CompanyDomesticPaths.AsNoTracking()
                                                      .Where(cdp => cdp.CompanyId == companyId);
        if (loadData)
        {
            baseQuery = baseQuery.Include(item => item.Company);
            baseQuery = baseQuery.Include(item => item.SourceCountry);
            baseQuery = baseQuery.Include(item => item.SourceProvince);
            baseQuery = baseQuery.Include(item => item.SourceCity);
            baseQuery = baseQuery.Include(item => item.DestinationCountry);
            baseQuery = baseQuery.Include(item => item.DestinationProvince);
            baseQuery = baseQuery.Include(item => item.DestinationCity);
        }

        baseQuery = baseQuery.OrderBy(cdp => cdp.DestinationCity!.PersianName);

        var companyDomesticPaths = await baseQuery.ToListAsync(cancellationToken);

        return companyDomesticPaths;
    }
}