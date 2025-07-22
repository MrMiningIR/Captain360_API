using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Repositories.AddressRepo;
using Capitan360.Infrastructure.Persistence;
using System.Linq.Expressions;
using Capitan360.Domain.Entities.AddressEntity;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.AddressImpl;

public class AreaRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IAreaRepository
{
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
            .Include(a => a.Parent)
            .Include(a => a.Children)
            .ToListAsync(cancellationToken);
    }

    public async Task<Area?> GetAreaById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Areas
            .Include(a => a.Parent)
            .Include(a => a.Children)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public Area UpdateShadows(Area area, string userId)
    {
        dbContext.Entry(area).Property("UpdatedDate").CurrentValue = DateTime.UtcNow;
        dbContext.Entry(area).Property("UpdatedBy").CurrentValue = userId;
        return area;
    }

    public async Task<(IReadOnlyList<Area>, int)> GetMatchingAllAreas(string? searchPhrase, int pageSize, int pageNumber,
        string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.Areas
            .Include(a => a.Parent)
            .Where(a => searchPhraseLower == null ||
                        (a.PersianName.ToLower().Contains(searchPhraseLower) ||
                         a.Code.ToLower().Contains(searchPhraseLower)));

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Area, object>>>
            {
                { nameof(Area.PersianName), a => a.PersianName },
                { nameof(Area.Code), a => a.Code },
                { nameof(Area.LevelId), a => a.LevelId }
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

    public async Task<(IReadOnlyList<Area>, int)> GetAllProvince(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection,
        bool ignorePageSize, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.Areas.AsNoTracking()
            .Where(x => x.ParentId != null && x.ParentId == 1)
            .Include(a => a.Parent)
            .Where(a => searchPhraseLower == null ||
                        (a.PersianName.ToLower().Contains(searchPhraseLower) ||
                         a.Code.ToLower().Contains(searchPhraseLower)));

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Area, object>>>
     {
         { nameof(Area.PersianName), a => a.PersianName },
         { nameof(Area.Code), a => a.Code },
         { nameof(Area.LevelId), a => a.LevelId }
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

    public async Task<(IReadOnlyList<Area>, int)> GetAllCity(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection,
        int provinceId, bool ignorePageSize, CancellationToken cancellationToken)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.Areas.AsNoTracking()
            .Where(x => x.ParentId != null && x.ParentId == provinceId)
            .Include(a => a.Parent)
            .Where(a => searchPhraseLower == null ||
                        (a.PersianName.ToLower().Contains(searchPhraseLower) ||
                         a.Code.ToLower().Contains(searchPhraseLower)));

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Area, object>>>
{
    { nameof(Area.PersianName), a => a.PersianName },
    { nameof(Area.Code), a => a.Code },
    { nameof(Area.LevelId), a => a.LevelId }
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
            .Where(a => a.ParentId == parentId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Area>> GetDistrictAreasByCityIdAsync(int parentId, CancellationToken cancellationToken)
    {
        return await dbContext.Areas.AsNoTracking().Where(x => x.ParentId == parentId && x.LevelId == 4)
            .ToListAsync(cancellationToken);
    }
}