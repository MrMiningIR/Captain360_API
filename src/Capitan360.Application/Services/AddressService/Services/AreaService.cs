using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.AddressService.Commands.CreateArea;
using Capitan360.Application.Services.AddressService.Commands.DeleteArea;
using Capitan360.Application.Services.AddressService.Commands.UpdateArea;
using Capitan360.Application.Services.AddressService.Dtos;
using Capitan360.Application.Services.AddressService.Queries.GetAllAreas;
using Capitan360.Application.Services.AddressService.Queries.GetAreaById;
using Capitan360.Application.Services.AddressService.Queries.GetCityArea;
using Capitan360.Application.Services.AddressService.Queries.GetProvinceArea;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Addresses;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.AddressService.Services;

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
        if (command == null)
            return ApiResponse<int>.Error(400, "ورودی ایجاد منطقه نمی‌تواند null باشد");

        var areaEntity = mapper.Map<Area>(command);
        if (areaEntity == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        var areaId = await areaRepository.CreateAreaAsync(areaEntity, Guid.NewGuid().ToString(), cancellationToken);
        logger.LogInformation("Area created successfully with ID: {AreaId}", areaId);
        return ApiResponse<int>.Created(areaId, "Area created successfully");
    }

    public async Task<ApiResponse<PagedResult<AreaDto>>> GetAllAreas(GetAllAreaQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllAreas is Called");
        if (query.PageSize <= 0 || query.PageNumber <= 0)
            return ApiResponse<PagedResult<AreaDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (areas, totalCount) = await areaRepository.GetAllAreas(
            query.SearchPhrase, query.PageSize, query.PageNumber, query.SortBy,
            query.SortDirection, cancellationToken);

        var areaDtos = mapper.Map<IReadOnlyList<AreaDto>>(areas) ?? Array.Empty<AreaDto>();
        logger.LogInformation("Retrieved {Count} areas", areaDtos.Count);

        var data = new PagedResult<AreaDto>(areaDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<AreaDto>>.Ok(data, "Areas retrieved successfully");
    }

    public async Task<ApiResponse<AreaDto>> GetAreaByIdAsync(GetAreaByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAreaById is Called with ID: {Id}", query.Id);
        if (query.Id <= 0)
            return ApiResponse<AreaDto>.Error(400, "شناسه منطقه باید بزرگ‌تر از صفر باشد");

        var area = await areaRepository.GetAreaById(query.Id, cancellationToken);
        if (area is null)
            return ApiResponse<AreaDto>.Error(400, $"منطقه با شناسه {query.Id} یافت نشد");

        var result = mapper.Map<AreaDto>(area);
        logger.LogInformation("Area retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<AreaDto>.Ok(result, "Area retrieved successfully");
    }

    public async Task<ApiResponse<object>> DeleteAreaAsync(DeleteAreaCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteArea is Called with ID: {Id}", command.Id);
        if (command.Id <= 0)
            return ApiResponse<object>.Error(400, "شناسه منطقه باید بزرگ‌تر از صفر باشد");

        var area = await areaRepository.GetAreaById(command.Id, cancellationToken);
        if (area is null)
            return ApiResponse<object>.Error(400, $"منطقه با شناسه {command.Id} یافت نشد");

        areaRepository.Delete(area, Guid.NewGuid().ToString());
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Area soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<object>.Deleted("Area deleted successfully");
    }

    public async Task<ApiResponse<AreaDto>> UpdateAreaAsync(UpdateAreaCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateArea is Called with {@UpdateAreaCommand}", command);
        if (command == null || command.Id <= 0)
            return ApiResponse<AreaDto>.Error(400, "شناسه منطقه باید بزرگ‌تر از صفر باشد یا ورودی نامعتبر است");

        var area = await areaRepository.GetAreaById(command.Id, cancellationToken);
        if (area is null)
            return ApiResponse<AreaDto>.Error(400, $"منطقه با شناسه {command.Id} یافت نشد");

        var updatedArea = mapper.Map(command, area);
        areaRepository.UpdateShadows(updatedArea, Guid.NewGuid().ToString());

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Area updated successfully with ID: {Id}", command.Id);

        var updatedAreaDto = mapper.Map<AreaDto>(updatedArea);
        return ApiResponse<AreaDto>.Updated(updatedAreaDto);
    }

    public async Task<ApiResponse<IReadOnlyList<AreaDto>>> GetAreasByParentIdAsync(int? parentId, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAreasByParentId is Called with ParentId: {ParentId}", parentId);
        var areas = await areaRepository.GetAreasByParentIdAsync(parentId, cancellationToken);
        var areaDtos = mapper.Map<IReadOnlyList<AreaDto>>(areas) ?? Array.Empty<AreaDto>();
        logger.LogInformation("Retrieved {Count} areas for ParentId: {ParentId}", areaDtos.Count, parentId);
        return ApiResponse<IReadOnlyList<AreaDto>>.Ok(areaDtos, "Areas retrieved successfully");
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
}