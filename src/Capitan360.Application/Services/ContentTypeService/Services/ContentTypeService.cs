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
using Capitan360.Domain.Entities.ContentTypes;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Companies;
using Capitan360.Domain.Repositories.ContentTypes;
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
    public async Task<ApiResponse<int>> CreateContentTypeAsync(CreateContentTypeCommand command, //Ch**
        CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateContentType is Called with {@CreateContentTypeCommand}", command);

        if (await contentTypeRepository.CheckExistContentTypeNameAsync(command.ContentTypeName, command.CompanyTypeId, null, cancellationToken))
            return ApiResponse<int>.Error(400, "نام بسته بندی تکراری است");


        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(command.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");


        if (await contentTypeRepository.CheckExistContentTypeNameAsync(command.ContentTypeName, command.CompanyTypeId, null, cancellationToken))
            return ApiResponse<int>.Error(400, "نام محتوی بار تکراری است");



        int existingCount = await contentTypeRepository.GetCountContentTypeAsync(command.CompanyTypeId, cancellationToken);

        var contentType = mapper.Map<ContentType>(command) ?? null;
        if (contentType == null)
            return ApiResponse<int>.Error(400, "مشکل در عملیات تبدیل");

        //--

        contentType.Order = existingCount + 1;
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

    public async Task<ApiResponse<PagedResult<ContentTypeDto>>> GetAllContentTypes(//Ch
        GetAllContentTypesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllContentTypesByCompanyType is Called");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<ContentTypeDto>>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(query.CompanyTypeId))
            return ApiResponse<PagedResult<ContentTypeDto>>.Error(403, "مجوز این فعالیت را ندارید");


        var (contentTypes, totalCount) = await contentTypeRepository.GetAllContentTypesAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyTypeId,
            true,
            query.PageNumber,
            query.PageSize,
          query.SortDirection,
            cancellationToken);
        var contentTypeDto = mapper.Map<IReadOnlyList<ContentTypeDto>>(contentTypes) ?? Array.Empty<ContentTypeDto>();
        logger.LogInformation("Retrieved {Count} content types", contentTypeDto.Count);

        var data = new PagedResult<ContentTypeDto>(contentTypeDto, totalCount, query.PageSize,
            query.PageNumber);

        return ApiResponse<PagedResult<ContentTypeDto>>.Ok(data, "محتو‌های بار با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<ContentTypeDto>> GetContentTypeByIdAsync(//ch**
        GetContentTypeByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetContentTypeById is Called with ID: {Id}", query.Id);

        var contentType = await contentTypeRepository.GetContentTypeByIdAsync(query.Id, false, false, cancellationToken);
        if (contentType is null)
            return ApiResponse<ContentTypeDto>.Error(400, $"بسته بندی نامعتبر است");


        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<ContentTypeDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(contentType.CompanyTypeId))
            return ApiResponse<ContentTypeDto>.Error(403, "مجوز این فعالیت را ندارید");



        var result = mapper.Map<ContentTypeDto>(contentType);
        logger.LogInformation("ContentType retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<ContentTypeDto>.Ok(result, "محتوی با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<int>> DeleteContentTypeAsync(DeleteContentTypeCommand command //ch**
       , CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteContentType is Called with ID: {Id}", command.Id);

        var contentType = await contentTypeRepository.GetContentTypeByIdAsync(command.Id, true, false, cancellationToken);
        if (contentType is null)
            return ApiResponse<int>.Error(400, $"محتوی بار نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(400, "مشکل در دریافت اطلاعات");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(contentType.CompanyTypeId))
            return ApiResponse<int>.Error(400, "مجوز این فعالیت را ندارید");

        await contentTypeRepository.DeletePackageTypeAsync(contentType.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("ContentType soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "محتوی بار با موفقیت حذف شد");
    }

    public async Task<ApiResponse<ContentTypeDto>> UpdateContentTypeAsync(UpdateContentTypeCommand command,//Ch**
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateContentType is Called with {@UpdateContentTypeCommand}", command);

        var contentType = await contentTypeRepository.GetContentTypeByIdAsync(command.Id, false, true, cancellationToken);
        if (contentType is null)
            return ApiResponse<ContentTypeDto>.Error(400, $"محتوی بار نامعتبر است");


        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<ContentTypeDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(contentType.CompanyTypeId))
            return ApiResponse<ContentTypeDto>.Error(403, "مجوز این فعالیت را ندارید");



        if (await contentTypeRepository.CheckExistContentTypeNameAsync(command.ContentTypeName, command.Id, contentType.CompanyTypeId, cancellationToken))
            return ApiResponse<ContentTypeDto>.Error(400, "نام محتوی بار تکراری است");

        var updatedContentType = mapper.Map(command, contentType);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("ContentType updated successfully with ID: {Id}", command.Id);

        var updatedContentTypeDto = mapper.Map<ContentTypeDto>(updatedContentType);
        return ApiResponse<ContentTypeDto>.Ok(updatedContentTypeDto, "محتوی با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<int>> MoveContentTypeUpAsync(MoveContentTypeUpCommand command, //ch**
        CancellationToken cancellationToken)
    {
        logger.LogInformation("MoveContentTypeUp is Called with {@MoveContentTypeUpCommand}", command);

        var contentType = await contentTypeRepository.GetContentTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (contentType == null)
            return ApiResponse<int>.Error(400, $"محتوی بار نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(contentType.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");


        if (contentType.Order == 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        var count = await contentTypeRepository.GetCountContentTypeAsync(contentType.CompanyTypeId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await contentTypeRepository.MoveContentTypeUpAsync(command.Id, cancellationToken);
        logger.LogInformation(
            "ContentType moved up successfully. ContentTypeId: {ContentTypeId}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "محتوی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> MoveContentTypeDownAsync(//Ch**
        MoveContentTypeDownCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("MoveContentTypeDown is Called with {@MoveContentTypeDownCommand}", command);

        var contentType = await contentTypeRepository.GetContentTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (contentType == null)
            return ApiResponse<int>.Error(400, $"بسته بندی نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(contentType.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        if (contentType.Order == 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");


        var count = await contentTypeRepository.GetCountContentTypeAsync(contentType.CompanyTypeId, cancellationToken);


        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await contentTypeRepository.MoveContentTypeDownAsync(command.Id, cancellationToken);
        logger.LogInformation(
            "ContentType moved down successfully, ContentTypeId: {ContentTypeId}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "محتوی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> SetContentTypeActivityStatus(UpdateActiveStateContentTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetContentActivityStatus Called with {@UpdateActiveStateContentTypeCommand}", command);

        var contentType =
            await contentTypeRepository.GetContentTypeByIdAsync(command.Id, false, true, cancellationToken);

        if (contentType is null)
            return ApiResponse<int>.Error(400, $"محتوی بار نامعتبر است");


        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(contentType.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");




        contentType.Active = !contentType.Active;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت بسته بندی با موفقیت به‌روزرسانی شد");
    }//Ch
}