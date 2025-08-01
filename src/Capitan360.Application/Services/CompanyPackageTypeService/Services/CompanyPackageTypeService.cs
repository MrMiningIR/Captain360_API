using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeDown;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeUp;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateActiveStateCompanyPackageType;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageType;
using Capitan360.Application.Services.CompanyPackageTypeService.Dtos;
using Capitan360.Application.Services.CompanyPackageTypeService.Queries.GetAllCompanyPackageTypes;
using Capitan360.Application.Services.CompanyPackageTypeService.Queries.GetCompanyPackageTypeById;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Repositories.PackageRepo;
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
    public async Task<ApiResponse<int>> UpdateCompanyPackageTypeAsync(UpdateCompanyPackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyPackageTypeAsync is Called with {@UpdateCompanyPackageTypeCommand}", command);

        if (string.IsNullOrEmpty(command.CompanyPackageTypeName) && command.Active is null)
            return ApiResponse<int>.Error(400, "ورودی ایجاد نوع پکیج نمی‌تواند null باشد");

        var originalPackageType =
            await packageTypeRepository.GetPackageTypeById(command.PackageTypeId, cancellationToken, false);
        if (originalPackageType is null)
            return ApiResponse<int>.Error(404, "پکیج اصلی وجود ندارد");

        logger.LogInformation("OriginalPackageType {@OriginalPackageType}", originalPackageType);

        var exist = await companyPackageTypeRepository.CheckExistCompanyPackageTypeName(command.CompanyId, command.PackageTypeId, cancellationToken);

        if (exist is null)
            return ApiResponse<int>.Error(404, "پکیج مورد نظر وجود ندارد");

        logger.LogInformation("ExistedCompanyPackageType {@CompanyPackageType}", exist);

        var mappedUpdatedCompanyPackageType = mapper.Map(command, exist);
        if (mappedUpdatedCompanyPackageType == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<int>.Ok(command.Id, "CompanyPackageType updated successfully");
    }

    public async Task<ApiResponse<PagedResult<CompanyPackageTypeDto>>> GetAllCompanyPackageTypesByCompany(
        GetAllCompanyPackageTypesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyPackageTypesByCompany is Called");
        if (query.PageSize <= 0 || query.PageNumber <= 0)
            return ApiResponse<PagedResult<CompanyPackageTypeDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (companyPackageTypes, totalCount) = await companyPackageTypeRepository.GetCompanyPackageTypes(
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
        return ApiResponse<PagedResult<CompanyPackageTypeDto>>.Ok(data, "CompanyPackageType retrieved successfully");
    }

    public async Task<ApiResponse<object>> MoveCompanyPackageTypeUpAsync(MoveCompanyPackageTypeUpCommand command, CancellationToken cancellationToken)
    {
        var existedRecord =
            await companyPackageTypeRepository.CheckExistCompanyPackageTypeName(command.CompanyId, command.PackageTypeId, cancellationToken);
        if (existedRecord == null)
            return ApiResponse<object>.Error(404,
                $"پکیج شرکت با شناسه {command.CompanyId} یافت نشد");

        await companyPackageTypeRepository.MoveCompanyPackageTypeUpAsync(command.CompanyId,
            command.PackageTypeId, cancellationToken);
        logger.LogInformation(
            "PackageType moved up successfully. CompanyPackageType: {CompanyId}, PackageTypeId: {PackageTypeId}",
            existedRecord.Id, command.PackageTypeId);
        return ApiResponse<object>.Ok("PackageType moved up successfully");
    }

    public async Task<ApiResponse<object>> MoveCompanyPackageTypeDownAsync(MoveCompanyPackageTypeDownCommand command, CancellationToken cancellationToken)
    {
        var existedRecord =
            await companyPackageTypeRepository.CheckExistCompanyPackageTypeName(command.CompanyId, command.PackageTypeId, cancellationToken);
        if (existedRecord == null)
            return ApiResponse<object>.Error(404,
                $"پکیج شرکت با شناسه {command.CompanyId} یافت نشد");

        await companyPackageTypeRepository.MoveCompanyPackageTypeDownAsync(command.CompanyId,
            command.PackageTypeId, cancellationToken);
        logger.LogInformation(
            "PackageType moved down successfully. CompanyPackageType: {CompanyId}, PackageTypeId: {PackageTypeId}",
            existedRecord.Id, command.PackageTypeId);
        return ApiResponse<object>.Ok("PackageType moved down successfully");
    }

    public async Task<ApiResponse<CompanyPackageTypeDto>> GetCompanyPackageTypeByIdAsync(GetCompanyPackageTypeByIdQuery query,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyPackageTypeByIdQuery is Called with ID: {Id}", query.Id);
        if (query.Id <= 0)
            return ApiResponse<CompanyPackageTypeDto>.Error(400, "شناسه نوع پکیج باید بزرگ‌تر از صفر باشد");

        var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeById(query.Id, cancellationToken);
        if (companyPackageType is null)
            return ApiResponse<CompanyPackageTypeDto>.Error(404, $"نوع پکیج با شناسه {query.Id} یافت نشد");

        var result = mapper.Map<CompanyPackageTypeDto>(companyPackageType);
        logger.LogInformation("PackageType retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanyPackageTypeDto>.Ok(result, "PackageType retrieved successfully");
    }

    public async Task<ApiResponse<int>> UpdateCompanyPackageTypeNameAsync(UpdateCompanyPackageTypeNameCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyPackageTypeNameAsync is Called with {@UpdateCompanyPackageTypeCommand}", command);

        if (command.Id <= 0)
            return ApiResponse<int>.Error(400, "شناسه بسته بندی نمیتواند 0 باشد");
        if (string.IsNullOrEmpty(command.PackageTypeName))
            return ApiResponse<int>.Error(400, "ورودی ایجاد نوع پکیج نمی‌تواند null باشد");

        var originalPackageType =
            await packageTypeRepository.GetPackageTypeById(command.OriginalPackageId, cancellationToken, false);
        if (originalPackageType is null)
            return ApiResponse<int>.Error(404, "پکیج اصلی وجود ندارد");

        logger.LogInformation("OriginalPackageType {@OriginalPackageType}", originalPackageType);

        var exist = await companyPackageTypeRepository.GetCompanyPackageTypeById(command.Id, cancellationToken, true);

        if (exist is null)
            return ApiResponse<int>.Error(404, "پکیج مورد نظر وجود ندارد");

        logger.LogInformation("ExistedCompanyPackageType {@CompanyPackageType}", exist);



        exist.PackageTypeName = command.PackageTypeName;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<int>.Ok(command.Id, "CompanyPackageType updated successfully");
    }

    public async Task<ApiResponse<int>> SetCompanyPackageContentActivityStatus(UpdateActiveStateCompanyPackageTypeCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyPackageContentActivityStatus Called with {@UpdateActiveStateCompanyContentTypeCommand}", command);

        var companyPackage =
            await companyPackageTypeRepository.GetCompanyPackageTypeById(command.Id, cancellationToken, true);

        if (companyPackage is null)
            return ApiResponse<int>.Error(404, $"CompanyPackage Data was not Found :{command.Id}");

        companyPackage.Active = !companyPackage.Active;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("SetCompanyPackageContentActivityStatus Updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id);
    }
}