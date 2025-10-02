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


    







    public async Task<int> CreateAreaAsync(Area areaEntity, string userId, CancellationToken cancellationToken)
    {
        dbContext.Entry(areaEntity).Property("CreatedBy").CurrentValue = userId;
        dbContext.Areas.Add(areaEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return areaEntity.Id;
    }

    public void Delete(Area area, string userId)
    {
        dbContext.Entry(area).Property("Deleted").CurrentValue = true;
        UpdateShadows(area, userId);
    }

    public async Task<IReadOnlyList<Area>> GetAllAreas(CancellationToken cancellationToken)
    {
        return await dbContext.Areas
            .Include(item => item.Parent)
            .Include(item => item.Children)
            .ToListAsync(cancellationToken);
    }

    public async Task<Area?> GetAreaById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Areas
            .Include(item => item.Parent)
            .Include(item => item.Children)
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
    }

    public Area UpdateShadows(Area area, string userId)
    {
        dbContext.Entry(area).Property("UpdatedDate").CurrentValue = DateTime.UtcNow;
        dbContext.Entry(area).Property("UpdatedBy").CurrentValue = userId;
        return area;
    }

    public async Task<(IReadOnlyList<Area>, int)> GetAllAreas(string searchPhrase, int pageSize, int pageNumber,
        string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();

        var baseQuery = dbContext.Areas
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

        var areas = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (areas, totalCount);
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



    public async Task<(IReadOnlyList<Area>, int)> GetAllRegionMunicipality(string searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection,
        int cityId, bool ignorePageSize, CancellationToken cancellationToken)
    {
        searchPhrase = searchPhrase.Trim().ToLower();

        var baseQuery = dbContext.Areas.AsNoTracking()
            .Where(x => x.ParentId != null && x.ParentId == cityId)
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

    public async Task<IReadOnlyList<Area>> GetDistrictAreasByCityIdAsync(int parentId, CancellationToken cancellationToken)
    {
        return await dbContext.Areas.AsNoTracking().Where(x => x.ParentId == parentId && x.LevelId == 4)
            .ToListAsync(cancellationToken);
    }
}