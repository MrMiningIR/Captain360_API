using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Addresses.Areas.Commands.Create;
using Capitan360.Application.Features.Addresses.Areas.Commands.Delete;
using Capitan360.Application.Features.Addresses.Areas.Commands.Update;
using Capitan360.Application.Features.Addresses.Areas.Dtos;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetAllChildren;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetById;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetCity;
using Capitan360.Application.Features.Addresses.Areas.Queries.GetProvince;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Addresses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.Addresses.Areas.Services;

public class AreaService(
    ILogger<AreaService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    IAreaRepository areaRepository) : IAreaService
{
    public async Task<ApiResponse<int>> CreateAreaAsync(CreateAreaCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateArea is Called with {@CreateAreaCommand}", command);

        string item, itemParent;
        switch (command.LevelId)
        {
            case (short)AreaType.Country:
                item = "کشور";
                itemParent = "";
                break;
            case (short)AreaType.Province:
                item = "استان";
                itemParent = "کشور";
                break;
            case (short)AreaType.City:
                item = "شهر";
                itemParent = "استان";
                break;
            case (short)AreaType.RegionMunicipality:
                item = "منطقه شهرداری";
                itemParent = "شهر";
                break;
            default:
                item = "";
                itemParent = "";
                break;
        }

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await areaRepository.CheckExistCodeAsync(command.Code, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "کد تکراری است");

        if (command.LevelId == (short)AreaType.Country)
        {
            if (await areaRepository.CheckExistCountryPersianNameAsync(command.PersianName, null, cancellationToken))
                return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "نام فارسی تکراری است");

            if (command.EnglishName.Trim() != "" && await areaRepository.CheckExistCountryEnglishNameAsync(command.EnglishName, null, cancellationToken))
                return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "نام انگلیسی تکراری است");
        }
        else if (command.LevelId == (short)AreaType.Province || command.LevelId == (short)AreaType.City || command.LevelId == (short)AreaType.RegionMunicipality)
        {
            if (!command.ParentId.HasValue)
                return ApiResponse<int>.Error(StatusCodes.Status400BadRequest, itemParent + " معتبر نیست");

            if (await areaRepository.CheckExistProvinceOrCityOrMunicipalityPersianNameAsync(command.PersianName, (int)command.ParentId, null, cancellationToken))
                return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "نام فارسی تکراری است");

            if (command.EnglishName.Trim() != "" && await areaRepository.CheckExistProvinceOrCityOrMunicipalityEnglishNameAsync(command.EnglishName, (int)command.ParentId, null, cancellationToken))
                return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "نام انگلیسی تکراری است");
        }
        else
        {
            return ApiResponse<int>.Error(StatusCodes.Status400BadRequest, item + " معتبر نیست");
        }

        var area = mapper.Map<Area>(command) ?? null;
        if (area == null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        var areaId = await areaRepository.CreateAreaAsync(area, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Area created successfully with {@Area}", area);
        return ApiResponse<int>.Created(areaId, item + " با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<PagedResult<AreaDto>>> GetAllAreas(GetAllChildrenAreaQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllAreas is Called");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<AreaDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<PagedResult<AreaDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var (areas, totalCount) = await areaRepository.GetAllAreasAsync(
              query.SearchPhrase,
              query.SortBy,
              query.ParentId,
              true,
              query.PageNumber,
              query.PageSize,
              query.SortDirection,
              cancellationToken);

        var areaDtos = mapper.Map<IReadOnlyList<AreaDto>>(areas) ?? Array.Empty<AreaDto>();
        if (areaDtos == null)
            return ApiResponse<PagedResult<AreaDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} areas", areaDtos.Count);

        string item = "";
        if (areaDtos.Count > 0)
        {
            switch (areaDtos.First().LevelId)
            {
                case (short)AreaType.Country:
                    item = "کشور";
                    break;
                case (short)AreaType.Province:
                    item = "استان";
                    break;
                case (short)AreaType.City:
                    item = "شهر";
                    break;
                case (short)AreaType.RegionMunicipality:
                    item = "منطقه شهرداری";
                    break;
            }
        }
        else
        {
            item = "اطلاعات";
        }

        var data = new PagedResult<AreaDto>(areaDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<AreaDto>>.Ok(data, item + " با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<AreaDto>> GetAreaByIdAsync(GetAreaByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAreaById is Called with {@Id}", query.Id);

        var area = await areaRepository.GetAreaByIdAsync(query.Id, false, false, cancellationToken);
        if (area is null)
            return ApiResponse<AreaDto>.Error(StatusCodes.Status404NotFound, "اطلاعات یافت نشد");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<AreaDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<AreaDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var result = mapper.Map<AreaDto>(area);
        if (result == null)
            return ApiResponse<AreaDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        string item = "";
        switch (area.LevelId)
        {
            case (short)AreaType.Country:
                item = "کشور";
                break;
            case (short)AreaType.Province:
                item = "استان";
                break;
            case (short)AreaType.City:
                item = "شهر";
                break;
            case (short)AreaType.RegionMunicipality:
                item = "منطقه شهرداری";
                break;
        }

        logger.LogInformation("Area retrieved successfully with {@Id}", query.Id);
        return ApiResponse<AreaDto>.Ok(result, item + " با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<int>> DeleteAreaAsync(DeleteAreaCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteArea is Called with {@Id}", command.Id);

        var area = await areaRepository.GetAreaByIdAsync(command.Id, false, false, cancellationToken);
        if (area is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "اطلاعات نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        await areaRepository.DeleteAreaAsync(area.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        string item = "";
        switch (area.LevelId)
        {
            case (short)AreaType.Country:
                item = "کشور";
                break;
            case (short)AreaType.Province:
                item = "استان";
                break;
            case (short)AreaType.City:
                item = "شهر";
                break;
            case (short)AreaType.RegionMunicipality:
                item = "منطقه شهرداری";
                break;
        }

        logger.LogInformation("Area Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, item + " با موفقیت حذف شد");
    }

    public async Task<ApiResponse<AreaDto>> UpdateAreaAsync(UpdateAreaCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateArea is Called with {@UpdateAreaCommand}", command);

        var area = await areaRepository.GetAreaByIdAsync(command.Id, false, true, cancellationToken);
        if (area is null)
            return ApiResponse<AreaDto>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<AreaDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<AreaDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        string item, itemParent;
        switch (area.LevelId)
        {
            case (short)AreaType.Country:
                item = "کشور";
                itemParent = "";
                break;
            case (short)AreaType.Province:
                item = "استان";
                itemParent = "کشور";
                break;
            case (short)AreaType.City:
                item = "شهر";
                itemParent = "استان";
                break;
            case (short)AreaType.RegionMunicipality:
                item = "منطقه شهرداری";
                itemParent = "شهر";
                break;
            default:
                item = "";
                itemParent = "";
                break;
        }

        if (await areaRepository.CheckExistCodeAsync(command.Code, area.Id, cancellationToken))
            return ApiResponse<AreaDto>.Error(StatusCodes.Status409Conflict, "کد تکراری است");

        if (area.LevelId == (short)AreaType.Country)
        {
            if (await areaRepository.CheckExistCountryPersianNameAsync(command.PersianName, area.Id, cancellationToken))
                return ApiResponse<AreaDto>.Error(StatusCodes.Status409Conflict, "نام فارسی تکراری است");

            if (command.EnglishName.Trim() != "" && await areaRepository.CheckExistCountryEnglishNameAsync(command.EnglishName, area.Id, cancellationToken))
                return ApiResponse<AreaDto>.Error(StatusCodes.Status409Conflict, "نام انگلیسی تکراری است");
        }
        else if (area.LevelId == (short)AreaType.Province || area.LevelId == (short)AreaType.City || area.LevelId == (short)AreaType.RegionMunicipality)
        {
            if (!area.ParentId.HasValue)
                return ApiResponse<AreaDto>.Error(StatusCodes.Status400BadRequest, itemParent + " معتبر نیست");

            if (await areaRepository.CheckExistProvinceOrCityOrMunicipalityPersianNameAsync(command.PersianName, (int)area.ParentId, area.Id, cancellationToken))
                return ApiResponse<AreaDto>.Error(StatusCodes.Status409Conflict, "نام فارسی تکراری است");

            if (command.EnglishName.Trim() != "" && await areaRepository.CheckExistProvinceOrCityOrMunicipalityEnglishNameAsync(command.EnglishName, (int)area.ParentId, area.Id, cancellationToken))
                return ApiResponse<AreaDto>.Error(StatusCodes.Status409Conflict, "نام انگلیسی تکراری است");
        }
        else
        {
            return ApiResponse<AreaDto>.Error(StatusCodes.Status400BadRequest, item + " معتبر نیست");
        }

        var updatedArea = mapper.Map(command, area);
        if (updatedArea == null)
            return ApiResponse<AreaDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Area updated successfully with {@UpdateAreaCommand}", command);

        var updatedAreaDto = mapper.Map<AreaDto>(updatedArea);
        if (updatedAreaDto == null)
            return ApiResponse<AreaDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<AreaDto>.Ok(updatedAreaDto, item + " با موفقیت به‌روزرسانی شد");
    }
    public async Task<ApiResponse<PagedResult<ProvinceAreaDto>>> GetAllProvince(GetProvinceAreaQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllProvince is Called");
        if (query.PageSize <= 0 || query.PageNumber <= 0)
            return ApiResponse<PagedResult<ProvinceAreaDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (areas, totalCount) = await areaRepository.GetAllProvince(
            query.SearchPhrase, query.PageSize, query.PageNumber, query.SortBy,
            query.SortDirection, query.IgnorePageSize, cancellationToken);

        var provinceDtos = mapper.Map<IReadOnlyList<ProvinceAreaDto>>(areas) ?? Array.Empty<ProvinceAreaDto>();
        logger.LogInformation("Retrieved {Count} Provinces", provinceDtos.Count);

        var data = new PagedResult<ProvinceAreaDto>(provinceDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<ProvinceAreaDto>>.Ok(data, "GetAllProvince retrieved successfully");
    }
    public async Task<ApiResponse<List<AreaItemDto>>> GetDistricts(int cityId, CancellationToken cancellationToken)
    {
        if (cityId <= 0)
            return ApiResponse<List<AreaItemDto>>.Error(400, "شناسه شهر صحیح نیست");

        var districts = await areaRepository.GetDistrictAreasByCityIdAsync(cityId, cancellationToken);
        var mappedDistricts = mapper.Map<List<AreaItemDto>>(districts);
        if (mappedDistricts is null)
            return ApiResponse<List<AreaItemDto>>.Error(400, "خطای تبدیل");

        return ApiResponse<List<AreaItemDto>>.Ok(mappedDistricts);
    }

    public async Task<ApiResponse<PagedResult<CityAreaDto>>> GetAllCityByProvinceId(GetCityAreaQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCityByProvinceId is Called");
        if (query.PageSize <= 0 || query.PageNumber <= 0)
            return ApiResponse<PagedResult<CityAreaDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (areas, totalCount) = await areaRepository.GetAllCity(
            query.SearchPhrase, query.PageSize, query.PageNumber, query.SortBy,
            query.SortDirection, query.ProvinceId, query.IgnorePageSize, cancellationToken);

        var provinceDtos = mapper.Map<IReadOnlyList<CityAreaDto>>(areas) ?? Array.Empty<CityAreaDto>();
        logger.LogInformation("Retrieved {Count} Cities", provinceDtos.Count);

        var data = new PagedResult<CityAreaDto>(provinceDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CityAreaDto>>.Ok(data, "GetAllCityByProvinceId retrieved successfully");
    }
    public async Task<ApiResponse<IReadOnlyList<AreaDto>>> GetAreasByParentIdAsync(int? parentId, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAreasByParentId is Called with ParentId: {ParentId}", parentId);
        var areas = await areaRepository.GetAreasByParentIdAsync(parentId, cancellationToken);
        var areaDtos = mapper.Map<IReadOnlyList<AreaDto>>(areas) ?? Array.Empty<AreaDto>();
        logger.LogInformation("Retrieved {Count} areas for ParentId: {ParentId}", areaDtos.Count, parentId);
        return ApiResponse<IReadOnlyList<AreaDto>>.Ok(areaDtos, "Areas retrieved successfully");
    }
}
