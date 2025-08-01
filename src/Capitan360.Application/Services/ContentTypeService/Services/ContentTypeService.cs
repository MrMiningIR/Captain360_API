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
using Capitan360.Domain.Repositories.ContentRepo;
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
    public async Task<ApiResponse<int>> CreateContentTypeAsync(CreateContentTypeCommand contentTypeCommand,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateContentType is Called with {@CreateContentTypeCommand}", contentTypeCommand);


        if (string.IsNullOrEmpty(contentTypeCommand.ContentTypeName))
            return ApiResponse<int>.Error(400, "ورودی ایجاد نوع محتوی نمی‌تواند null باشد");
        var exist = await contentTypeRepository.CheckExistContentTypeName(contentTypeCommand.ContentTypeName,
            contentTypeCommand.CompanyTypeId, cancellationToken);

        if (exist is not null)
            return ApiResponse<int>.Error(400, "نام نوع محتوی مشابه وجود دارد");

        int order = await contentTypeRepository.OrderContentType(contentTypeCommand.CompanyTypeId, cancellationToken);

        var contentType = mapper.Map<ContentType>(contentTypeCommand) ?? null;
        if (contentType == null)
            return ApiResponse<int>.Error(400, "مشکل در عملیات تبدیل");

        //--
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        contentType.OrderContentType = order + 1;

        var contentTypeId = await contentTypeRepository.CreateContentTypeAsync(contentType, cancellationToken);

        var getEligibleCommandlines =
            await companyRepository.GetCompaniesIdByCompanyTypeId(contentTypeCommand.CompanyTypeId, cancellationToken);

        if (getEligibleCommandlines.Any())
        {
            await companyContentTypeRepository.CreateCompanyContentTypes(getEligibleCommandlines, contentType, cancellationToken);

        }


        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        //--

        logger.LogInformation("ContentType created successfully with ID: {ContentTypeId}", contentTypeId);
        return ApiResponse<int>.Ok(contentTypeId, "ContentType created successfully");
    }

    public async Task<ApiResponse<PagedResult<ContentTypeDto>>> GetAllContentTypes(
        GetAllContentTypesQuery allContentTypeQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllContentTypesByCompanyType is Called");
        if (allContentTypeQuery.PageSize <= 0 || allContentTypeQuery.PageNumber <= 0)
            return ApiResponse<PagedResult<ContentTypeDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (contentTypes, totalCount) = await contentTypeRepository.GetMatchingAllContentTypes(
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
        return ApiResponse<PagedResult<ContentTypeDto>>.Ok(data, "ContentTypes retrieved successfully");
    }

    public async Task<ApiResponse<ContentTypeDto>> GetContentTypeByIdAsync(
        GetContentTypeByIdQuery getContentTypeByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetContentTypeById is Called with ID: {Id}", getContentTypeByIdQuery.Id);
        if (getContentTypeByIdQuery.Id <= 0)
            return ApiResponse<ContentTypeDto>.Error(400, "شناسه نوع محتوی باید بزرگ‌تر از صفر باشد");

        var contentType = await contentTypeRepository.GetContentTypeById(getContentTypeByIdQuery.Id, cancellationToken, false);
        if (contentType is null)
            return ApiResponse<ContentTypeDto>.Error(404, $"نوع محتوی با شناسه {getContentTypeByIdQuery.Id} یافت نشد");

        var result = mapper.Map<ContentTypeDto>(contentType);
        logger.LogInformation("ContentType retrieved successfully with ID: {Id}", getContentTypeByIdQuery.Id);
        return ApiResponse<ContentTypeDto>.Ok(result, "ContentType retrieved successfully");
    }

    public async Task<ApiResponse<object>> DeleteContentTypeAsync(DeleteContentTypeCommand deleteContentTypeCommand,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteContentType is Called with ID: {Id}", deleteContentTypeCommand.Id);
        if (deleteContentTypeCommand.Id <= 0)
            return ApiResponse<object>.Error(400, "شناسه نوع محتوی باید بزرگ‌تر از صفر باشد");

        var contentType =
            await contentTypeRepository.GetContentTypeById(deleteContentTypeCommand.Id, cancellationToken, true);
        if (contentType is null)
            return ApiResponse<object>.Error(404, $"نوع محتوی با شناسه {deleteContentTypeCommand.Id} یافت نشد");

        contentTypeRepository.Delete(contentType);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("ContentType soft-deleted successfully with ID: {Id}", deleteContentTypeCommand.Id);
        return ApiResponse<object>.Deleted("ContentType deleted successfully");
    }

    //--
    public async Task<ApiResponse<ContentTypeDto>> UpdateContentTypeAsync(UpdateContentTypeCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateContentType is Called with {@UpdateContentTypeCommand}", command);
        if (command is not { Id: > 0 })
            return ApiResponse<ContentTypeDto>.Error(400,
                "شناسه نوع محتوی باید بزرگ‌تر از صفر باشد یا ورودی نامعتبر است");

        var contentType = await contentTypeRepository.GetContentTypeById(command.Id, cancellationToken, true);
        if (contentType is null)
            return ApiResponse<ContentTypeDto>.Error(404, $"نوع محتوی با شناسه {command.Id} یافت نشد");


        var exist = await contentTypeRepository.CheckExistContentTypeName(command.ContentTypeName,
    command.CompanyTypeId, cancellationToken);


        if (exist is not null && exist.Id != contentType.Id)
            return ApiResponse<ContentTypeDto>.Error(400, "نام نوع محتوی مشابه وجود دارد");

        var updatedContentType = mapper.Map(command, contentType);
        updatedContentType.OrderContentType = contentType.OrderContentType;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("ContentType updated successfully with ID: {Id}", command.Id);

        var updatedContentTypeDto = mapper.Map<ContentTypeDto>(updatedContentType);
        return ApiResponse<ContentTypeDto>.Ok(updatedContentTypeDto);
    }

    public async Task<ApiResponse<object>> MoveContentTypeUpAsync(MoveContentTypeUpCommand moveContentTypeUpCommand,
        CancellationToken cancellationToken)
    {
        var companyType =
            await companyTypeRepository.GetCompanyTypeById(moveContentTypeUpCommand.CompanyTypeId, cancellationToken);
        if (companyType == null)
            return ApiResponse<object>.Error(404,
                $"نوع شرکت با شناسه {moveContentTypeUpCommand.CompanyTypeId} یافت نشد");

        await contentTypeRepository.MoveContentTypeUpAsync(moveContentTypeUpCommand.CompanyTypeId,
            moveContentTypeUpCommand.ContentTypeId, cancellationToken);
        logger.LogInformation(
            "ContentType moved up successfully. CompanyTypeId: {CompanyTypeId}, ContentTypeId: {ContentTypeId}",
            moveContentTypeUpCommand.CompanyTypeId, moveContentTypeUpCommand.ContentTypeId);
        return ApiResponse<object>.Ok("ContentType moved up successfully");
    }

    public async Task<ApiResponse<object>> MoveContentTypeDownAsync(
        MoveContentTypeDownCommand moveContentTypeDownCommand, CancellationToken cancellationToken)
    {
        var companyType =
            await companyTypeRepository.GetCompanyTypeById(moveContentTypeDownCommand.CompanyTypeId, cancellationToken);
        if (companyType == null)
            return ApiResponse<object>.Error(404,
                $"نوع شرکت با شناسه {moveContentTypeDownCommand.CompanyTypeId} یافت نشد");

        await contentTypeRepository.MoveContentTypeDownAsync(moveContentTypeDownCommand.CompanyTypeId,
            moveContentTypeDownCommand.ContentTypeId, cancellationToken);
        logger.LogInformation(
            "ContentType moved down successfully. CompanyTypeId: {CompanyTypeId}, ContentTypeId: {ContentTypeId}",
            moveContentTypeDownCommand.CompanyTypeId, moveContentTypeDownCommand.ContentTypeId);
        return ApiResponse<object>.Ok("ContentType moved down successfully");
    }

    public async Task<ApiResponse<int>> SetContentActivityStatus(UpdateActiveStateContentTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetContentActivityStatus Called with {@UpdateActiveStateContentTypeCommand}", command);

        var contentType =
            await contentTypeRepository.GetContentTypeById(command.Id, cancellationToken, true);

        if (contentType is null)
            return ApiResponse<int>.Error(404, $"contentType Data was not Found :{command.Id}");

        contentType.Active = !contentType.Active;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("SetContentActivityStatus Updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id);
    }

    // CompanyContentType
}