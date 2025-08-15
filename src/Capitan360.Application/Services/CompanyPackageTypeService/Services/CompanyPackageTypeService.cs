using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeDown;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeUp;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateActiveStateCompanyPackageType;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageTypeNameAndDescription;
using Capitan360.Application.Services.CompanyPackageTypeService.Dtos;
using Capitan360.Application.Services.CompanyPackageTypeService.Queries.GetAllCompanyPackageTypes;
using Capitan360.Application.Services.CompanyPackageTypeService.Queries.GetCompanyPackageTypeById;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Repositories.PackageTypeRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Services;



public class CompanyPackageTypeService(
    ILogger<CompanyPackageTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyPackageTypeRepository companyPackageTypeRepository,
    IPackageTypeRepository packageTypeRepository) : ICompanyPackageTypeService
{
    public async Task<ApiResponse<PagedResult<CompanyPackageTypeDto>>> GetAllCompanyPackageTypesByCompanyAsync(
        GetAllCompanyPackageTypesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyPackageTypesByCompany is Called");

        var (companyPackageTypes, totalCount) = await companyPackageTypeRepository.GetAllCompanyPackageTypesAsync(
            query.SearchPhrase,
            query.CompanyId,
            query.Active,
            query.PageSize,
            query.PageNumber,
            query.SortBy,
            query.SortDirection,
            cancellationToken);

        var companyPackageTypeDto = mapper.Map<IReadOnlyList<CompanyPackageTypeDto>>(companyPackageTypes) ?? Array.Empty<CompanyPackageTypeDto>();
        logger.LogInformation("Retrieved {Count} package types", companyPackageTypeDto.Count);

        var data = new PagedResult<CompanyPackageTypeDto>(companyPackageTypeDto, totalCount, query.PageSize,
            query.PageNumber);
        return ApiResponse<PagedResult<CompanyPackageTypeDto>>.Ok(data, "بسته‌بندی‌ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<int>> MoveCompanyPackageTypeUpAsync(MoveCompanyPackageTypeUpCommand command, CancellationToken cancellationToken)
    {
        var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.CompanyPackageTypeId, false, cancellationToken);
        if (companyPackageType == null)
            return ApiResponse<int>.Error(404, $"بسته‌بندی نامعتبر است");

        if (companyPackageType.CompanyPackageTypeOrder == 1)
            return ApiResponse<int>.Ok(command.CompanyPackageTypeId, "انجام شد");

        var count = await companyPackageTypeRepository.GetCountCompanyPackageTypeAsync(companyPackageType.CompanyId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.CompanyPackageTypeId, "انجام شد");

        await companyPackageTypeRepository.MoveCompanyPackageTypeUpAsync(command.CompanyPackageTypeId, cancellationToken);
        logger.LogInformation(
            "PackageType moved up successfully., CompanyPackageTypeId: {CompanyPackageTypeId}", command.CompanyPackageTypeId);
        return ApiResponse<int>.Ok(command.CompanyPackageTypeId, "بسته‌بندی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> MoveCompanyPackageTypeDownAsync(MoveCompanyPackageTypeDownCommand command, CancellationToken cancellationToken)
    {
        var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.CompanyPackageTypeId, false, cancellationToken);
        if (companyPackageType == null)
            return ApiResponse<int>.Error(404, $"بسته‌بندی نامعتبر است");

        if (companyPackageType.CompanyPackageTypeOrder == 1)
            return ApiResponse<int>.Ok(command.CompanyPackageTypeId, "انجام شد");

        var count = await companyPackageTypeRepository.GetCountCompanyPackageTypeAsync(companyPackageType.CompanyId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.CompanyPackageTypeId, "انجام شد");

        await companyPackageTypeRepository.MoveCompanyPackageTypeDownAsync(command.CompanyPackageTypeId, cancellationToken);
        logger.LogInformation(
            "PackageType moved down successfully., CompanyPackageTypeId: {CompanyPackageTypeId}", command.CompanyPackageTypeId);
        return ApiResponse<int>.Ok(command.CompanyPackageTypeId, "بسته‌بندی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<CompanyPackageTypeDto>> GetCompanyPackageTypeByIdAsync(GetCompanyPackageTypeByIdQuery query,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyPackageTypeByIdQuery is Called with ID: {Id}", query.Id);

        var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(query.Id, false, cancellationToken);
        if (companyPackageType is null)
            return ApiResponse<CompanyPackageTypeDto>.Error(404, $"نوع بسته‌بندی با شناسه {query.Id} یافت نشد");

        var result = mapper.Map<CompanyPackageTypeDto>(companyPackageType);
        logger.LogInformation("PackageType retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanyPackageTypeDto>.Ok(result, "بسته‌بندی با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<int>> UpdateCompanyPackageTypeNameAndDescriptionAsync(UpdateCompanyPackageTypeNameAndDescriptionCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyPackageTypeNameAsync is Called with {@UpdateCompanyPackageTypeNameAndDescriptionCommand}", command);
        var receivedCompanyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.Id, true, cancellationToken);
        if (receivedCompanyPackageType == null)
            return ApiResponse<int>.Error(404, $"بسته‌بندی نامعتبر است");

        logger.LogInformation("ExistedCompanyPackageType {@CompanyPackageType}", receivedCompanyPackageType);

        var companyPackageType = mapper.Map(command, receivedCompanyPackageType);
        if (companyPackageType is null)
            return ApiResponse<int>.Error(500, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<int>.Ok(command.Id, "اطلاعات با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<int>> SetCompanyPackageContentActivityStatusAsync(
        UpdateActiveStateCompanyPackageTypeCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyPackageContentActivityStatus Called with {@UpdateActiveStateCompanyPackageTypeCommand}", command);

        var companyPackage =
            await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.Id, true, cancellationToken);

        if (companyPackage is null)
            return ApiResponse<int>.Error(404, $"بسته‌بندی نامعتبر است");

        companyPackage.CompanyPackageTypeActive = !companyPackage.CompanyPackageTypeActive;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("SetCompanyPackageContentActivityStatus Updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت بسته‌بندی با موفقیت به‌روزرسانی شد");
    }
}