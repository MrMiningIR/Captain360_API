using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeDown;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeUp;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateActiveStateCompanyContentType;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentTypeName;
using Capitan360.Application.Services.CompanyContentTypeService.Dtos;
using Capitan360.Application.Services.CompanyContentTypeService.Queries.GetAllCompanyContentTypes;
using Capitan360.Application.Services.CompanyContentTypeService.Queries.GetCompanyContentTypeById;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Repositories.ContentTypeRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyContentTypeService.Services;

public class CompanyContentTypeService(
    ILogger<CompanyContentTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyContentTypeRepository companyContentTypeRepository,
    IContentTypeRepository contentTypeRepository) : ICompanyContentTypeService
{
    public async Task<ApiResponse<PagedResult<CompanyContentTypeDto>>> GetAllCompanyContentTypesByCompany(
        GetAllCompanyContentTypesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyContentTypesByCompany is Called");

        var (companyContentTypes, totalCount) = await companyContentTypeRepository.GetCompanyContentTypesAsync(
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
        return ApiResponse<PagedResult<CompanyContentTypeDto>>.Ok(data, "محتوی‌ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<int>> MoveContentTypeUpAsync(MoveCompanyContentTypeUpCommand command, CancellationToken cancellationToken)
    {
        var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.CompanyContentTypeId, false, cancellationToken);
        if (companyContentType == null)
            return ApiResponse<int>.Error(404, $"بسته بندی نامعتبر است");

        if (companyContentType.OrderContentType == 1)
            return ApiResponse<int>.Ok(command.CompanyContentTypeId, "انجام شد");

        var count = await companyContentTypeRepository.GetCountCompanyContentTypeAsync(companyContentType.CompanyId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.CompanyContentTypeId, "انجام شد");

        await companyContentTypeRepository.MoveCompanyContentTypeUpAsync(command.CompanyContentTypeId, cancellationToken);
        logger.LogInformation(
            "ContentType moved up successfully., ContentTypeId: {CompanyContentTypeId}", command.CompanyContentTypeId);
        return ApiResponse<int>.Ok(command.CompanyContentTypeId, "محتوی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> MoveContentTypeDownAsync(MoveCompanyContentTypeDownCommand command, CancellationToken cancellationToken)
    {
        var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.CompanyContentTypeId, false, cancellationToken);
        if (companyContentType == null)
            return ApiResponse<int>.Error(404, $"بسته بندی نامعتبر است");

        if (companyContentType.OrderContentType == 1)
            return ApiResponse<int>.Ok(command.CompanyContentTypeId, "انجام شد");

        var count = await companyContentTypeRepository.GetCountCompanyContentTypeAsync(companyContentType.CompanyId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.CompanyContentTypeId, "انجام شد");

        await companyContentTypeRepository.MoveCompanyContentTypeDownAsync(command.CompanyContentTypeId, cancellationToken);
        logger.LogInformation(
            "ContentType moved up successfully., ContentTypeId: {CompanyContentTypeId}", command.CompanyContentTypeId);
        return ApiResponse<int>.Ok(command.CompanyContentTypeId, "محتوی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<CompanyContentTypeDto>> GetCompanyContentTypeByIdAsync(GetCompanyContentTypeByIdQuery query,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyContentTypeByIdQuery is Called with ID: {Id}", query.Id);

        var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(query.Id, false, cancellationToken);
        if (companyContentType is null)
            return ApiResponse<CompanyContentTypeDto>.Error(404, $"نوع محتوی با شناسه {query.Id} یافت نشد");

        var result = mapper.Map<CompanyContentTypeDto>(companyContentType);
        logger.LogInformation("ContentType retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanyContentTypeDto>.Ok(result, "محتوی با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<int>> UpdateCompanyContentTypeNameAndDescriptionAsync(UpdateCompanyContentTypeNameAndDescriptionCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyContentTypeNameAsync is Called with {@UpdateCompanyContentTypeNameCommand}", command);
        var receivedCompanyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.Id, true, cancellationToken);
        if (receivedCompanyContentType == null)
            return ApiResponse<int>.Error(404, $"بسته بندی نامعتبر است");

        logger.LogInformation("ExistedCompanyContentType {@CompanyContentType}", receivedCompanyContentType);

        var companyContentType = mapper.Map(command, receivedCompanyContentType);
        if (companyContentType is null)

            return ApiResponse<int>.Error(500, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<int>.Ok(command.Id, "اطلاعات با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<int>> SetCompanyContentActivityStatus(
        UpdateActiveStateCompanyContentTypeCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyContentActivityStatus Called with {@UpdateActiveStateCompanyContentTypeCommand}", command);

        var companyContent =
            await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.Id, true, cancellationToken);

        if (companyContent is null)
            return ApiResponse<int>.Error(404, $"بسته بندی نامعتبر است");

        companyContent.Active = !companyContent.Active;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("SetCompanyContentActivityStatus Updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت بسته بندی با موفقیت به‌روزرسانی شد");
    }
}