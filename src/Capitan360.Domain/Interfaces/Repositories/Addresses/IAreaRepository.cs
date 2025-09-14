using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Repositories.Addresses;

public interface IAreaRepository
{
    Task<int> CreateAreaAsync(Area area, string userId, CancellationToken cancellationToken);
    void Delete(Area area, string userId);
    Task<IReadOnlyList<Area>> GetAllAreas(CancellationToken cancellationToken);
    Task<Area?> GetAreaById(int id, CancellationToken cancellationToken);
    Area UpdateShadows(Area area, string userId);
    Task<(IReadOnlyList<Area>, int)> GetAllAreas(string? searchPhrase, int pageSize, int pageNumber,
        string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<(IReadOnlyList<Area>, int)> GetAllProvince(string? searchPhrase, int pageSize, int pageNumber,
    string? sortBy, SortDirection sortDirection, bool ignorePageSize, CancellationToken cancellationToken);

    Task<(IReadOnlyList<Area>, int)> GetAllCity(string? searchPhrase, int pageSize, int pageNumber,
string? sortBy, SortDirection sortDirection, int provinceId, bool ignorePageSize, CancellationToken cancellationToken);

    Task<List<Area>> GetAllCities(CancellationToken cancellationToken);

    Task<IReadOnlyList<Area>> GetAreasByParentIdAsync(int? parentId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Area>> GetDistrictAreasByCityIdAsync(int parentId, CancellationToken cancellationToken);


    Task<bool> CheckExistAreaByIdANdParentId(int areaId, int? areaLevelId, int? areaParentId, CancellationToken cancellationToken);

}