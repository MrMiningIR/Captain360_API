using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Addresses;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Repositories.Addresses;

public class AreaRepository(
        ApplicationDbContext dbContext,
        IUnitOfWork unitOfWork) : IAreaRepository
{
    public async Task<bool> CheckExistAreaByIdAndParentId(int areaId, int? areaLevelId, int? areaParentId, CancellationToken cancellationToken)
    {
        return await dbContext.Areas.AnyAsync(item => item.Id == areaId &&
                                                      (areaLevelId == null || item.LevelId == areaLevelId) &&
                                                      (areaParentId == null || item.ParentId == areaParentId));
    }

    public async Task<bool> CheckExistCountryPersianNameAsync(string counteryPersianName, int? currentCountryId, CancellationToken cancellationToken)
    {
        return await dbContext.Areas.AnyAsync(item => item.PersianName.ToLower() == counteryPersianName.Trim().ToLower() && (currentCountryId == null || item.Id != currentCountryId), cancellationToken);
    }

    public async Task<bool> CheckExistCountryEnglishNameAsync(string counteryEnglishName, int? currentCountryId, CancellationToken cancellationToken)
    {
        return await dbContext.Areas.AnyAsync(item => item.EnglishName.ToLower() == counteryEnglishName.Trim().ToLower() && (currentCountryId == null || item.Id != currentCountryId), cancellationToken);
    }

    public async Task<bool> CheckExistProvinceOrCityOrMunicipalityPersianNameAsync(string persianName, int parentId, int? currentProvinceId, CancellationToken cancellationToken)
    {
        return await dbContext.Areas.AnyAsync(item => item.PersianName.ToLower() == persianName.Trim().ToLower() && item.ParentId.HasValue && item.ParentId == parentId && (currentProvinceId == null || item.Id != currentProvinceId), cancellationToken);
    }

    public async Task<bool> CheckExistProvinceOrCityOrMunicipalityEnglishNameAsync(string englishName, int parentId, int? currentProvinceId, CancellationToken cancellationToken)
    {
        return await dbContext.Areas.AnyAsync(item => item.EnglishName.ToLower() == englishName.Trim().ToLower() && item.ParentId.HasValue && item.ParentId == parentId && (currentProvinceId == null || item.Id != currentProvinceId), cancellationToken);
    }

    public async Task<bool> CheckExistCodeAsync(string code, int? currentAreaId, CancellationToken cancellationToken)
    {
        return await dbContext.Areas.AnyAsync(item => item.Code.ToLower() == code.Trim().ToLower() && (currentAreaId == null || item.Id != currentAreaId), cancellationToken);
    }

    public async Task<int> CreateAreaAsync(Area areaEntity, CancellationToken cancellationToken)
    {
        dbContext.Areas.Add(areaEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return areaEntity.Id;
    }

    public async Task<Area?> GetAreaByIdAsync(int areaId, bool loadData, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<Area> query = dbContext.Areas;

        if (loadData)
        {
            query.Include(item => item.Children);
            query.Include(item => item.Parent);
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == areaId, cancellationToken);
    }

    public async Task DeleteAreaAsync(int areaId, CancellationToken cancellationToken)
    {
        await Task.Yield();
    }

    public async Task<(IReadOnlyList<Area>, int)> GetAllAreasAsync(string searchPhrase, string? sortBy, int parentId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();
        var baseQuery = dbContext.Areas.AsNoTracking()
                                       .Where(item => (item.Code.Contains(searchPhrase) || item.PersianName.Contains(searchPhrase) || item.EnglishName.Contains(searchPhrase)) &&
                                                      ((parentId == 0 && !item.ParentId.HasValue) || (parentId != 0 && item.ParentId == parentId)));

        if (loadData)
        {
            baseQuery.Include(item => item.Children);
            baseQuery.Include(item => item.Parent);
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var columnsSelector = new Dictionary<string, Expression<Func<Area, object>>>
        {
            { nameof(Area.PersianName), item => item.PersianName!},
            { nameof(Area.Code), item => item.Code}
        };

        sortBy ??= nameof(Area.PersianName);

        var selectedColumn = columnsSelector[sortBy];
        baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);

        var Areas = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (Areas, totalCount);
    }

    public async Task<IReadOnlyList<Area>> GetAllCities(CancellationToken cancellationToken)
    {
        return await dbContext.Areas.Where(x => x.LevelId == 3).ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<Area>, int)> GetAllProvince(string searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection,
    bool ignorePageSize, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();

        var baseQuery = dbContext.Areas.AsNoTracking()
            .Where(x => x.ParentId != null && x.ParentId == 1)
            .Include(item => item.Parent)
            .Where(item => searchPhrase == null ||
                        (item.PersianName.ToLower().Contains(searchPhrase) ||
                         item.Code.ToLower().Contains(searchPhrase)));

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Area, object>>>
 {
     { nameof(Area.PersianName), item => item.PersianName },
     { nameof(Area.Code), item => item.Code },
     { nameof(Area.LevelId), item => item.LevelId }
 };

            var selectedColumn = columnsSelector[sortBy];
            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }


        var areas = !ignorePageSize ? await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken) : await baseQuery.ToListAsync(cancellationToken);

        return (areas, totalCount);
    }
    public async Task<IReadOnlyList<Area>> GetDistrictAreasByCityIdAsync(int parentId, CancellationToken cancellationToken)
    {
        return await dbContext.Areas.AsNoTracking().Where(x => x.ParentId == parentId && x.LevelId == 4)
            .ToListAsync(cancellationToken);
    }
    public async Task<(IReadOnlyList<Area>, int)> GetAllCity(string searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection,
    int provinceId, bool ignorePageSize, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();

        var baseQuery = dbContext.Areas.AsNoTracking()
            .Where(x => x.ParentId != null && x.ParentId == provinceId)
            .Include(item => item.Parent)
            .Where(item => searchPhrase == null ||
                        (item.PersianName.ToLower().Contains(searchPhrase) ||
                         item.Code.ToLower().Contains(searchPhrase)));

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Area, object>>>
{
    { nameof(Area.PersianName), item => item.PersianName },
    { nameof(Area.Code), item => item.Code },
    { nameof(Area.LevelId), item => item.LevelId }
};

            var selectedColumn = columnsSelector[sortBy];
            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }


        var areas = !ignorePageSize ? await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken) : await baseQuery.ToListAsync(cancellationToken);

        return (areas, totalCount);
    }
    public async Task<IReadOnlyList<Area>> GetAreasByParentIdAsync(int? parentId, CancellationToken cancellationToken)
    {
        return await dbContext.Areas
            .Where(item => item.ParentId == parentId)
            .ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<Area>, int)> GetMunicipalAreaByParentId(int parentId, string? searchPhrase, int pageSize, int pageNumber,
        string? sortBy, SortDirection sortDirection, bool ignorePageSize,
        CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();



        var baseQuery = dbContext.Areas.AsNoTracking()
            .Where(x => x.ParentId != null && x.ParentId != 1)
            .Include(item => item.Parent)
            .Where(item => searchPhrase == null ||
                        (item.PersianName.ToLower().Contains(searchPhrase) ||
                         item.Code.ToLower().Contains(searchPhrase)));

        baseQuery = baseQuery.Where(x => x.ParentId == parentId && x.LevelId == 4);
        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Area, object>>>
{
    { nameof(Area.PersianName), item => item.PersianName },
    { nameof(Area.Code), item => item.Code },
    { nameof(Area.LevelId), item => item.LevelId }
};

            var selectedColumn = columnsSelector[sortBy];
            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }


        var areas = !ignorePageSize ? await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken) : await baseQuery.ToListAsync(cancellationToken);

        return (areas, totalCount);
    }
}