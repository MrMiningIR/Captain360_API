using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyTypes.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyTypes.Queries.GetById;
using Capitan360.Application.Features.ContentTypes.Commands.Create;
using Capitan360.Application.Features.ContentTypes.Commands.Delete;
using Capitan360.Application.Features.ContentTypes.Commands.MoveDown;
using Capitan360.Application.Features.ContentTypes.Commands.MoveUp;
using Capitan360.Application.Features.ContentTypes.Commands.Update;
using Capitan360.Application.Features.ContentTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.ContentTypes.Dtos;
using Capitan360.Application.Features.ContentTypes.Queries.GetAll;
using Capitan360.Application.Features.ContentTypes.Queries.GetById;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Application.Features.PackageTypes.Commands.Create;
using Capitan360.Application.Features.PackageTypes.Commands.MoveDown;
using Capitan360.Application.Features.PackageTypes.Commands.MoveUp;
using Capitan360.Application.Features.PackageTypes.Commands.Update;
using Capitan360.Application.Features.PackageTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.PackageTypes.Dtos;
using Capitan360.Application.Features.PackageTypes.Queries.GetAll;
using Capitan360.Domain.Entities.ContentTypes;
using Capitan360.Domain.Entities.PackageTypes;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Interfaces.Repositories.ContentTypes;
using Capitan360.Domain.Interfaces.Repositories.PackageTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.ContentTypes.Services;



public class ContentTypeService(
    ILogger<ContentTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    IContentTypeRepository contentTypeRepository,
    ICompanyContentTypeRepository companyContentTypeRepository,
    ICompanyRepository companyRepository) : IContentTypeService
{
    public async Task<ApiResponse<int>> CreateContentTypeAsync(CreateContentTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateContentType is Called with {@CreateContentTypeCommand}", command);

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(command.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        if (await contentTypeRepository.CheckExistContentTypeNameAsync(command.Name, command.CompanyTypeId, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "نام بسته بندی تکراری است");

        int existingCount = await contentTypeRepository.GetCountContentTypeAsync(command.CompanyTypeId, cancellationToken);

        var contentType = mapper.Map<ContentType>(command) ?? null;
        if (contentType == null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");
        contentType.Order = existingCount + 1;

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var contentTypeId = await contentTypeRepository.CreateContentTypeAsync(contentType, cancellationToken);

        var companyIds = await companyRepository.GetCompaniesIdByCompanyTypeIdAsync(command.CompanyTypeId, cancellationToken);

        if (companyIds.Any())
        {
            await companyContentTypeRepository.CreateCompanyContentTypesAsync(companyIds, contentType, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("ContentType created successfully with {@ContentType}", contentType);
        return ApiResponse<int>.Created(contentTypeId, "بسته بندی با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteContentTypeAsync(DeleteContentTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteContentType is Called with {@Id}", command.Id);

        var ContentType = await contentTypeRepository.GetContentTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (ContentType is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(ContentType.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        await contentTypeRepository.DeleteContentTypeAsync(ContentType.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("ContentType Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "محتوی بار با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> MoveUpContentTypeAsync(MoveUpContentTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("MoveUpContentType is Called with {@Id}", command.Id);

        var contentType = await contentTypeRepository.GetContentTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (contentType == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(contentType.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (contentType.Order == 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        var count = await contentTypeRepository.GetCountContentTypeAsync(contentType.CompanyTypeId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await contentTypeRepository.MoveContentTypeUpAsync(command.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("ContentType moved up successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "محتوی بار با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> MoveDownContentTypeAsync(MoveDownContentTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("MoveDownContentType is Called with {@Id}", command.Id);

        var contentType = await contentTypeRepository.GetContentTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (contentType == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(contentType.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (contentType.Order == 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        var count = await contentTypeRepository.GetCountContentTypeAsync(contentType.CompanyTypeId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await contentTypeRepository.MoveContentTypeDownAsync(command.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("ContentType moved down successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "محتوی بار با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> SetContentTypeActivityStatusAsync(UpdateActiveStateContentTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetContentTypeActivityStatus Called with {@Id}", command.Id);

        var ContentType = await contentTypeRepository.GetContentTypeByIdAsync(command.Id, false, true, cancellationToken);
        if (ContentType is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(ContentType.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        ContentType.Active = !ContentType.Active;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("ContentType activity status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت بسته بندی با موفقیت به روز رسانی شد");
    }

    public async Task<ApiResponse<ContentTypeDto>> UpdateContentTypeAsync(UpdateContentTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateContentType is Called with {@UpdateContentTypeCommand}", command);

        var contentType = await contentTypeRepository.GetContentTypeByIdAsync(command.Id, false, true, cancellationToken);
        if (contentType is null)
            return ApiResponse<ContentTypeDto>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<ContentTypeDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(contentType.CompanyTypeId))
            return ApiResponse<ContentTypeDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await contentTypeRepository.CheckExistContentTypeNameAsync(command.Name, command.Id, contentType.CompanyTypeId, cancellationToken))
            return ApiResponse<ContentTypeDto>.Error(StatusCodes.Status409Conflict, "نام محتوی بار تکراری است");

        var updatedContentType = mapper.Map(command, contentType);
        if (updatedContentType == null)
            return ApiResponse<ContentTypeDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("ContentType updated successfully with {@UpdateContentTypeCommand}", command);

        var updatedContentTypeDto = mapper.Map<ContentTypeDto>(updatedContentType);
        if (updatedContentTypeDto == null)
            return ApiResponse<ContentTypeDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<ContentTypeDto>.Ok(updatedContentTypeDto, "محتوی بار با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<ContentTypeDto>>> GetAllContentTypesAsync(GetAllContentTypesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllContentTypes is Called");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<ContentTypeDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(query.CompanyTypeId))
            return ApiResponse<PagedResult<ContentTypeDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var (contentTypes, totalCount) = await contentTypeRepository.GetAllContentTypesAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyTypeId,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);

        var contentTypeDtos = mapper.Map<IReadOnlyList<ContentTypeDto>>(contentTypes) ?? Array.Empty<ContentTypeDto>();
        if (contentTypeDtos == null)
            return ApiResponse<PagedResult<ContentTypeDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} contenttypes", contentTypeDtos.Count);

        var data = new PagedResult<ContentTypeDto>(contentTypeDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<ContentTypeDto>>.Ok(data, "محتویات بار با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<ContentTypeDto>> GetContentTypeByIdAsync(GetContentTypeByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetContentTypeById is Called with {@Id}", query.Id);

        var ContentType = await contentTypeRepository.GetContentTypeByIdAsync(query.Id, false, false, cancellationToken);
        if (ContentType is null)
            return ApiResponse<ContentTypeDto>.Error(StatusCodes.Status404NotFound, "محتوی بار یافت نشد");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<ContentTypeDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(ContentType.CompanyTypeId))
            return ApiResponse<ContentTypeDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var result = mapper.Map<ContentTypeDto>(ContentType);
        if (result == null)
            return ApiResponse<ContentTypeDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("ContentType retrieved successfully with {@Id}", query.Id);
        return ApiResponse<ContentTypeDto>.Ok(result, "محتوی بار با موفقیت دریافت شد");
    }
}
