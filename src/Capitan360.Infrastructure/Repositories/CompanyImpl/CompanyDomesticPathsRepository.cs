using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.CompanyImpl;

public class CompanyDomesticPathsRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
    : ICompanyDomesticPathsRepository
{
    public async Task<int> CreateCompanyDomesticPathAsync(CompanyDomesticPaths companyDomesticPath, CancellationToken cancellationToken)
    {
        dbContext.CompanyDomesticPaths.Add(companyDomesticPath);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyDomesticPath.Id;
    }

    public void Delete(CompanyDomesticPaths companyDomesticPath)
    {
        dbContext.Entry(companyDomesticPath).Property("Deleted").CurrentValue = true;
    }

    public async Task<CompanyDomesticPaths?> GetCompanyDomesticPathById(int id, CancellationToken cancellationToken, bool track = false)
    {
        if (track)
        {
            return await dbContext.CompanyDomesticPaths

    .SingleOrDefaultAsync(cu => cu.Id == id, cancellationToken);
        }
        else
        {
            return await dbContext.CompanyDomesticPaths.AsNoTracking()

    .SingleOrDefaultAsync(cu => cu.Id == id, cancellationToken);
        }
    }

    public async Task<(IReadOnlyList<CompanyDomesticPaths>, int)> GetMatchingAllCompanyDomesticPaths(
        string? searchPhrase, int companyId, int pageSize, int pageNumber,
        int status,
        string? sortBy, int? sourceCountryId, int? sourceProvinceId, int? sourceCityId, int? destinationCountryId,
        int? destinationProvinceId, int? destinationCityId, SortDirection sortDirection,
        CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.CompanyDomesticPaths.AsNoTracking()
            .Where(c => companyId == 0 || c.CompanyId == companyId)
            .Include(a => a.SourceProvince)
            .Include(a => a.SourceCity)
            .Include(a => a.DestinationProvince)
            .Include(a => a.DestinationCity)
            .Where(cu => string.IsNullOrEmpty(searchPhraseLower) ||
                         cu.DescriptionForSearch.ToLower().Contains(searchPhraseLower) ||
                         cu.SourceCity.PersianName.ToLower().Contains(searchPhraseLower) ||
                         cu.DestinationCity.PersianName.ToLower().Contains(searchPhraseLower))

            ;

        if (sourceCountryId > 0)
            baseQuery = baseQuery.Where(x => x.SourceCountryId == sourceCountryId);
        if (sourceProvinceId > 0)
            baseQuery = baseQuery.Where(x => x.SourceProvinceId == sourceProvinceId);
        if (sourceCityId > 0)
            baseQuery = baseQuery.Where(x => x.SourceCityId == sourceCityId);

        if (destinationCountryId > 0)
            baseQuery = baseQuery.Where(x => x.DestinationCountryId == destinationCountryId);
        if (destinationProvinceId > 0)
            baseQuery = baseQuery.Where(x => x.DestinationProvinceId == destinationProvinceId);
        if (destinationCityId > 0)
            baseQuery = baseQuery.Where(x => x.DestinationCityId == destinationCityId);

        if (status is > -1 and < 2)
            baseQuery = baseQuery.Where(x => x.Active == status);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<CompanyDomesticPaths, object>>>
            {
                { nameof(CompanyDomesticPaths.Description), cu => cu.Description },
                { nameof(CompanyDomesticPaths.Active), cu => cu.Active },
                { nameof(CompanyDomesticPaths.EntranceFee), cu => cu.EntranceFee },
                { nameof(CompanyDomesticPaths.EntranceWeight), cu => cu.EntranceWeight },
                { nameof(CompanyDomesticPaths.EntranceType), cu => cu.EntranceType }
            };

            var selectedColumn = columnsSelector[sortBy];
            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var companyDomesticPaths = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companyDomesticPaths, totalCount);
    }

    public async Task<bool> CheckExistPath(int sourceCityId, int destinationCityId, int companyId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticPaths.AnyAsync(
            a => a.CompanyId == companyId && a.SourceCityId == sourceCityId && a.DestinationCityId == destinationCityId,
            cancellationToken);
    }

    public async Task<CompanyDomesticPaths?> CheckExistPath(int domesticPathId, CancellationToken cancellationToken)
    {
        var result = await dbContext.CompanyDomesticPaths.
            SingleOrDefaultAsync(x => x.Id == domesticPathId, cancellationToken: cancellationToken);

        return result;
    }
}