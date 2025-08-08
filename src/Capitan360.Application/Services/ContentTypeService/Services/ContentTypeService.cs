using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.ContentTypeService.Commands.CreateContentType;
using Capitan360.Application.Services.ContentTypeService.Commands.DeleteContentType;
using Capitan360.Application.Services.ContentTypeService.Commands.MoveDownContentType;
using Capitan360.Application.Services.ContentTypeService.Commands.MoveUpContentType;
using Capitan360.Application.Services.ContentTypeService.Commands.UpdateActiveStateContentType;
using Capitan360.Application.Services.ContentTypeService.Commands.UpdateContentType;
using Capitan360.Application.Services.ContentTypeService.Dtos;
using Capitan360.Application.Services.ContentTypeService.Queries.GetAllContentTypes;
using Capitan360.Application.Services.ContentTypeService.Queries.GetContentTypeById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.ContentEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Domain.Repositories.ContentTypeRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.ContentTypeService.Services;

public class ContentTypeService(
    ILogger<ContentTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    IContentTypeRepository contentTypeRepository,
    ICompanyTypeRepository companyTypeRepository,
    ICompanyContentTypeRepository companyContentTypeRepository,
    ICompanyRepository companyRepository)
    : IContentTypeService
{
    public async Task<ApiResponse<int>> CreateContentTypeAsync(CreateContentTypeCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateContentType is Called with {@CreateContentTypeCommand}", command);

        if (await contentTypeRepository.CheckExistContentTypeNameAsync(command.ContentTypeName, null, command.CompanyTypeId, cancellationToken))
            return ApiResponse<int>.Error(400, "نام بسته بندی تکراری است");

        int existingCount = await contentTypeRepository.GetCountContentTypeAsync(command.CompanyTypeId, cancellationToken);

        var contentType = mapper.Map<ContentType>(command) ?? null;
        if (contentType == null)
            return ApiResponse<int>.Error(400, "مشکل در عملیات تبدیل");

        //--

        contentType.OrderContentType = existingCount + 1;
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var contentTypeId = await contentTypeRepository.CreateContentTypeAsync(contentType, cancellationToken);

        var companyIds =
            await companyRepository.GetCompaniesIdByCompanyTypeIdAsync(command.CompanyTypeId, cancellationToken);

        if (companyIds.Any())
        {
            await companyContentTypeRepository.CreateCompanyContentTypesAsync(companyIds, contentType, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("ContentType created successfully with ID: {ContentTypeId}", contentTypeId);
        return ApiResponse<int>.Ok(contentTypeId, "بسته بندی با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<PagedResult<ContentTypeDto>>> GetAllContentTypes(
        GetAllContentTypesQuery allContentTypeQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllContentTypesByCompanyType is Called");

        var (contentTypes, totalCount) = await contentTypeRepository.GetMatchingAllContentTypesAsync(
            allContentTypeQuery.SearchPhrase,
            allContentTypeQuery.CompanyTypeId,
            allContentTypeQuery.Active,
            allContentTypeQuery.PageSize,
            allContentTypeQuery.PageNumber,
            allContentTypeQuery.SortBy,
            allContentTypeQuery.SortDirection,
            cancellationToken);
        var contentTypeDto = mapper.Map<IReadOnlyList<ContentTypeDto>>(contentTypes) ?? Array.Empty<ContentTypeDto>();
        logger.LogInformation("Retrieved {Count} content types", contentTypeDto.Count);

        var data = new PagedResult<ContentTypeDto>(contentTypeDto, totalCount, allContentTypeQuery.PageSize,
            allContentTypeQuery.PageNumber);

        return ApiResponse<PagedResult<ContentTypeDto>>.Ok(data, "محتوی‌ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<ContentTypeDto>> GetContentTypeByIdAsync(
        GetContentTypeByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetContentTypeById is Called with ID: {Id}", query.Id);

        var contentType = await contentTypeRepository.GetContentTypeByIdAsync(query.Id, false, cancellationToken);
        if (contentType is null)
            return ApiResponse<ContentTypeDto>.Error(404, $"بسته بندی نامعتبر است");

        var result = mapper.Map<ContentTypeDto>(contentType);
        logger.LogInformation("ContentType retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<ContentTypeDto>.Ok(result, "محتوی با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<int>> DeleteContentTypeAsync(DeleteContentTypeCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteContentType is Called with ID: {Id}", command.Id);

        var contentType =
            await contentTypeRepository.GetContentTypeByIdAsync(command.Id, true, cancellationToken);
        if (contentType is null)
            return ApiResponse<int>.Error(404, $"نوع محتوی با شناسه {command.Id} یافت نشد");

        contentTypeRepository.Delete(contentType);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("ContentType soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Deleted("ContentType deleted successfully");
    }

    public async Task<ApiResponse<ContentTypeDto>> UpdateContentTypeAsync(UpdateContentTypeCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateContentType is Called with {@UpdateContentTypeCommand}", command);

        var contentType = await contentTypeRepository.GetContentTypeByIdAsync(command.Id, true, cancellationToken);
        if (contentType is null)
            return ApiResponse<ContentTypeDto>.Error(404, $"نوع محتوی با شناسه {command.Id} یافت نشد");

        if (await contentTypeRepository.CheckExistContentTypeNameAsync(command.ContentTypeName, command.Id, command.CompanyTypeId, cancellationToken))
            return ApiResponse<ContentTypeDto>.Error(400, "نام بسته بندی تکراری است");

        var updatedContentType = mapper.Map(command, contentType);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("ContentType updated successfully with ID: {Id}", command.Id);

        var updatedContentTypeDto = mapper.Map<ContentTypeDto>(updatedContentType);
        return ApiResponse<ContentTypeDto>.Ok(updatedContentTypeDto, "محتوی با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<int>> MoveContentTypeUpAsync(MoveContentTypeUpCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("MoveContentTypeUp is Called with {@MoveContentTypeUpCommand}", command);
        var contentType = await contentTypeRepository.GetContentTypeByIdAsync(command.ContentTypeId, false, cancellationToken);
        if (contentType == null)
            return ApiResponse<int>.Error(404, $"بسته بندی نامعتبر است");
        if (contentType.OrderContentType == 1)
            return ApiResponse<int>.Ok(command.ContentTypeId, "انجام شد");

        var count = await contentTypeRepository.GetCountContentTypeAsync(contentType.CompanyTypeId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.ContentTypeId, "انجام شد");

        await contentTypeRepository.MoveContentTypeUpAsync(command.ContentTypeId, cancellationToken);
        logger.LogInformation(
            "ContentType moved up successfully. ContentTypeId: {ContentTypeId}", command.ContentTypeId);
        return ApiResponse<int>.Ok(command.ContentTypeId, "محتوی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> MoveContentTypeDownAsync(
        MoveContentTypeDownCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("MoveContentTypeDown is Called with {@MoveContentTypeDownCommand}", command);

        var contentType = await contentTypeRepository.GetContentTypeByIdAsync(command.ContentTypeId, false, cancellationToken);
        if (contentType == null)
            return ApiResponse<int>.Error(404, $"بسته بندی نامعتبر است");

        var count = await contentTypeRepository.GetCountContentTypeAsync(contentType.CompanyTypeId, cancellationToken);

        if (contentType.OrderContentType == count)
            return ApiResponse<int>.Ok(command.ContentTypeId, "انجام شد");

        if (count <= 1)
            return ApiResponse<int>.Ok(command.ContentTypeId, "انجام شد");

        await contentTypeRepository.MoveContentTypeDownAsync(command.ContentTypeId, cancellationToken);
        logger.LogInformation(
            "ContentType moved down successfully, ContentTypeId: {ContentTypeId}", command.ContentTypeId);
        return ApiResponse<int>.Ok(command.ContentTypeId, "محتوی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> SetContentTypeActivityStatus(UpdateActiveStateContentTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetContentActivityStatus Called with {@UpdateActiveStateContentTypeCommand}", command);

        var contentType =
            await contentTypeRepository.GetContentTypeByIdAsync(command.Id, true, cancellationToken);

        if (contentType is null)
            return ApiResponse<int>.Error(404, $"بسته بندی نامعتبر است");

        contentType.Active = !contentType.Active;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("SetContentActivityStatus Updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت بسته بندی با موفقیت به‌روزرسانی شد");
    }
}