using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyDomesticPaths;

public class CompanyDomesticPathRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyDomesticPathRepository
{
    public async Task<bool> CheckExistCompanyDomesticPathAsync(int companyId, int sourceCityId, int destinationCityId, int? currentCompanyDomesticPathId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticPaths.AnyAsync(cdp => cdp.CompanyId == companyId && (currentCompanyDomesticPathId == null || cdp.Id != currentCompanyDomesticPathId) &&
                                                                    cdp.SourceCityId == sourceCityId && cdp.DestinationCityId == destinationCityId, cancellationToken);
    }

    public async Task<int> CreateCompanyDomesticPathAsync(CompanyDomesticPath companyDomesticPath, CancellationToken cancellationToken)
    {
        dbContext.CompanyDomesticPaths.Add(companyDomesticPath);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyDomesticPath.Id;
    }

    public async Task<CompanyDomesticPath?> GetCompanyDomesticPathByIdAsync(int companyDomesticPathId, bool loadDataCompanyData, bool loadDataSourceData, bool loadDataDestinationData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticPath> query = dbContext.CompanyDomesticPaths;

        if (loadDataCompanyData)
        {
            query = query.Include(item => item.Company);
        }

        if (loadDataSourceData)
        {
            query = query.Include(item => item.SourceCountry);
            query = query.Include(item => item.SourceProvince);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataDestinationData)
        {
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

    public async Task<(IReadOnlyList<CompanyDomesticPath>, int)> GetAllCompanyDomesticPathsAsync(string searchPhrase, string? sortBy, int companyId, int active, int sourceCityId, int destinationCityId, bool loadDataCompanyData, bool loadDataSourceData, bool loadDataDestinationData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();
        var baseQuery = dbContext.CompanyDomesticPaths.AsNoTracking()
                                                      .Where(cdp => (cdp.SourceCity != null && (cdp.SourceCity.PersianName.ToLower().Contains(searchPhrase) || (cdp.SourceCity.EnglishName != null && cdp.SourceCity.EnglishName.ToLower().Contains(searchPhrase)))) ||
                                                                    (cdp.DestinationCountry != null && (cdp.DestinationCountry.PersianName.ToLower().Contains(searchPhrase) || (cdp.DestinationCountry.EnglishName != null && cdp.DestinationCountry.EnglishName.ToLower().Contains(searchPhrase)))));

        baseQuery = baseQuery.Where(pt => pt.CompanyId == companyId);

        baseQuery = active switch
        {
            1 => baseQuery.Where(item => item.Active),
            0 => baseQuery.Where(item => !item.Active),
            _ => baseQuery
        };

        if (sourceCityId != 0)
        {
            baseQuery = baseQuery.Where(pt => pt.SourceCityId == sourceCityId);
        }

        if (destinationCityId != 0)
        {
            baseQuery = baseQuery.Where(pt => pt.DestinationCityId == destinationCityId);
        }

        //این دوتا چون توی مرتب سازی هستند جدا شدن
        baseQuery = baseQuery.Include(item => item.DestinationCity);
        baseQuery = baseQuery.Include(item => item.SourceCity);

        if (loadDataCompanyData)
        {
            baseQuery = baseQuery.Include(item => item.Company);
        }

        if (loadDataSourceData)
        {
            baseQuery = baseQuery.Include(item => item.SourceCountry);
            baseQuery = baseQuery.Include(item => item.SourceProvince);
        }

        if (loadDataDestinationData)
        {
            baseQuery = baseQuery.Include(item => item.DestinationCountry);
            baseQuery = baseQuery.Include(item => item.DestinationProvince);
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<CompanyDomesticPath, object>>>
        {
            { "SourceCityName", cdp => cdp.SourceCity != null ? cdp.SourceCity.PersianName : string.Empty},
            { "DestinationCityName", cdp => cdp.DestinationCity != null ? cdp.DestinationCity.PersianName : string.Empty},
        };

        sortBy ??= "DestinationCityName";

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

    public async Task<IReadOnlyList<CompanyDomesticPath>?> GetCompanyDomesticPathsByCompanyIdAsync(int companyId, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext.CompanyDomesticPaths.AsNoTracking()
                                                      .Include(item => item.Company)
                                                      .Include(item => item.SourceCity)
                                                      .Include(item => item.DestinationCity)
                                                      .Where(cdp => cdp.CompanyId == companyId);

        baseQuery = baseQuery.OrderBy(cdp => cdp.DestinationCity != null ? cdp.DestinationCity.PersianName : string.Empty);

        var companyDomesticPaths = await baseQuery.ToListAsync(cancellationToken);

        return companyDomesticPaths;
    }
}