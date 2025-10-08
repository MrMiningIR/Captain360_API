using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Domain.Interfaces.Repositories.Addresses;

public interface IAreaRepository
{
    Task<bool> CheckExistAreaByIdAndParentId(int areaId, int? areaLevelId, int? areaParentId, CancellationToken cancellationToken);

    Task<bool> CheckExistCountryPersianNameAsync(string counteryPersianName, int? currentCountryId, CancellationToken cancellationToken);

    Task<bool> CheckExistCountryEnglishNameAsync(string counteryEnglishName, int? currentCountryId, CancellationToken cancellationToken);

    Task<bool> CheckExistProvinceOrCityOrMunicipalityPersianNameAsync(string persianName, int parentId, int? currentProvinceId, CancellationToken cancellationToken);

    Task<bool> CheckExistProvinceOrCityOrMunicipalityEnglishNameAsync(string englishName, int parentId, int? currentProvinceId, CancellationToken cancellationToken);

    Task<bool> CheckExistCodeAsync(string code, int? currentAreaId, CancellationToken cancellationToken);

    Task<int> CreateAreaAsync(Area areaEntity, CancellationToken cancellationToken);

    Task<Area?> GetAreaByIdAsync(int areaId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task DeleteAreaAsync(int areaId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<Area>, int)> GetAllAreasAsync(string searchPhrase, string? sortBy, int parentId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);
}