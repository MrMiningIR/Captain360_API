using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeDown;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeUp;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentType;
using Capitan360.Application.Services.CompanyContentTypeService.Dtos;
using Capitan360.Application.Services.CompanyContentTypeService.Queries.GetAllCompanyContentTypes;
using Capitan360.Application.Services.CompanyContentTypeService.Queries.GetCompanyContentTypeById;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Domain.Repositories.ContentRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyContentTypeService.Services;

public class CompanyContentTypeService(
    ILogger<CompanyContentTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyContentTypeRepository companyContentTypeRepository,
    IContentTypeRepository contentTypeRepository) : ICompanyContentTypeService
{
    public async Task<ApiResponse<int>> UpdateCompanyContentTypeAsync(UpdateCompanyContentTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyContentTypeAsync is Called with {@UpdateCompanyContentTypeCommand}", command);

        if (string.IsNullOrEmpty(command.ContentTypeName) && command.Active is null)
            return ApiResponse<int>.Error(400, "ورودی ایجاد نوع محتوا نمی‌تواند null باشد");

        var originalContentType =
            await contentTypeRepository.GetContentTypeById(command.ContentTypeId, cancellationToken);
        if (originalContentType is null)
            return ApiResponse<int>.Error(404, "محتوای اصلی وجود ندارد");

        logger.LogInformation("OriginalContentType {@OriginalContentType}", originalContentType);

        var exist = await companyContentTypeRepository.CheckExistCompanyContentTypeName(command.CompanyId, command.ContentTypeId, cancellationToken);

        if (exist is null)
            return ApiResponse<int>.Error(404, "محتوای مورد نظر وجود ندارد");

        logger.LogInformation("ExistedCompanyContentType {@CompanyContentType}", exist);

        var mappedUpdatedCompanyContentType = mapper.Map(command, exist);
        if (mappedUpdatedCompanyContentType == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<int>.Ok(command.Id, "CompanyContentType updated successfully");
    }

    public async Task<ApiResponse<PagedResult<CompanyContentTypeDto>>> GetAllCompanyContentTypesByCompany(
        GetAllCompanyContentTypesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyContentTypesByCompany is Called");
        if (query.PageSize <= 0 || query.PageNumber <= 0)
            return ApiResponse<PagedResult<CompanyContentTypeDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (companyContentTypes, totalCount) = await companyContentTypeRepository.GetCompanyContentTypes(
            query.SearchPhrase,
            query.CompanyId,
            query.Active,
            query.PageSize,
            query.PageNumber,
            query.SortBy,
            query.SortDirection,
            cancellationToken);

        var companyContentTypeDto = mapper.Map<IReadOnlyList<CompanyContentTypeDto>>(companyContentTypes) ?? Array.Empty<CompanyContentTypeDto>();
        logger.LogInformation("Retrieved {Count} content types", companyContentTypeDto.Count);

        var data = new PagedResult<CompanyContentTypeDto>(companyContentTypeDto, totalCount, query.PageSize,
            query.PageNumber);
        return ApiResponse<PagedResult<CompanyContentTypeDto>>.Ok(data, "CompanyContentType retrieved successfully");
    }

    public async Task<ApiResponse<object>> MoveContentTypeUpAsync(MoveCompanyContentTypeUpCommand command, CancellationToken cancellationToken)
    {
        var existedRecord =
            await companyContentTypeRepository.CheckExistCompanyContentTypeName(command.CompanyId, command.ContentTypeId, cancellationToken);
        if (existedRecord == null)
            return ApiResponse<object>.Error(404,
                $"محتوای شرکت با شناسه {command.CompanyId} یافت نشد");

        await companyContentTypeRepository.MoveContentTypeUpAsync(command.CompanyId,
            command.ContentTypeId, cancellationToken);
        logger.LogInformation(
            "ContentType moved up successfully. CompanyContentType: {CompanyId}, ContentTypeId: {ContentTypeId}",
            existedRecord.Id, command.ContentTypeId);
        return ApiResponse<object>.Ok("ContentType moved up successfully");
    }

    public async Task<ApiResponse<object>> MoveContentTypeDownAsync(MoveCompanyContentTypeDownCommand command, CancellationToken cancellationToken)
    {
        var existedRecord =
            await companyContentTypeRepository.CheckExistCompanyContentTypeName(command.CompanyId, command.ContentTypeId, cancellationToken);
        if (existedRecord == null)
            return ApiResponse<object>.Error(404,
                $"محتوای شرکت با شناسه {command.CompanyId} یافت نشد");

        await companyContentTypeRepository.MoveContentTypeDownAsync(command.CompanyId,
            command.ContentTypeId, cancellationToken);
        logger.LogInformation(
            "ContentType moved down successfully. CompanyContentType: {CompanyId}, ContentTypeId: {ContentTypeId}",
            existedRecord.Id, command.ContentTypeId);
        return ApiResponse<object>.Ok("ContentType moved down successfully");
    }

    public async Task<ApiResponse<CompanyContentTypeDto>> GetCompanyContentTypeByIdAsync(GetCompanyContentTypeByIdQuery query,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyContentTypeByIdQuery is Called with ID: {Id}", query.Id);
        if (query.Id <= 0)
            return ApiResponse<CompanyContentTypeDto>.Error(400, "شناسه نوع محتوا باید بزرگ‌تر از صفر باشد");

        var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeById(query.Id, cancellationToken);
        if (companyContentType is null)
            return ApiResponse<CompanyContentTypeDto>.Error(404, $"نوع محتوا با شناسه {query.Id} یافت نشد");

        var result = mapper.Map<CompanyContentTypeDto>(companyContentType);
        logger.LogInformation("ContentType retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanyContentTypeDto>.Ok(result, "ContentType retrieved successfully");
    }

    public async Task<ApiResponse<int>> UpdateCompanyContentTypeNameAsync(UpdateCompanyContentTypeNameCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyContentTypeNameAsync is Called with {@UpdateCompanyContentTypeNameCommand}", command);

        if (command.Id <= 0)
            return ApiResponse<int>.Error(400, "شناسه نوع محتوا نمی‌تواند 0 باشد");
        if (string.IsNullOrEmpty(command.ContentTypeName))
            return ApiResponse<int>.Error(400, "ورودی ایجاد نوع محتوا نمی‌تواند null باشد");

        var originalContentType =
            await contentTypeRepository.GetContentTypeById(command.OriginalContentId, cancellationToken);
        if (originalContentType is null)
            return ApiResponse<int>.Error(404, "محتوای اصلی وجود ندارد");

        logger.LogInformation("OriginalContentType {@OriginalContentType}", originalContentType);

        var exist = await companyContentTypeRepository.GetCompanyContentTypeById(command.Id, cancellationToken, true);

        if (exist is null)
            return ApiResponse<int>.Error(404, "محتوای مورد نظر وجود ندارد");

        logger.LogInformation("ExistedCompanyContentType {@CompanyContentType}", exist);

        exist.ContentTypeName = command.ContentTypeName;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<int>.Ok(command.Id, "CompanyContentType updated successfully");
    }
}