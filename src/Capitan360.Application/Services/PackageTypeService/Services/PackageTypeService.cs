using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Application.Services.PackageTypeService.Commands.CreatePackageType;
using Capitan360.Application.Services.PackageTypeService.Commands.DeletePackageType;
using Capitan360.Application.Services.PackageTypeService.Commands.MovePackageTypeDown;
using Capitan360.Application.Services.PackageTypeService.Commands.MovePackageTypeUp;
using Capitan360.Application.Services.PackageTypeService.Commands.UpdatePackageType;
using Capitan360.Application.Services.PackageTypeService.Dtos;
using Capitan360.Application.Services.PackageTypeService.Queries.GetAllPackageTypes;
using Capitan360.Application.Services.PackageTypeService.Queries.GetPackageTypeById;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.PackageEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Domain.Repositories.PackageRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.PackageTypeService.Services;

//public class PackageTypeService(ILogger<PackageTypeService> logger,
//    IMapper mapper,
//    IUnitOfWork unitOfWork,
//    IUserContext userContext,
//    IPackageTypeRepository packageTypeRepository,
//    ICompanyTypeRepository companyTypeRepository) : IPackageTypeService
//{
//    #region MyRegion
//    //public async Task<ApiResponse<int>> CreatePackageTypeAsync(CreatePackageTypeCommand packageTypeCommand, CancellationToken cancellationToken)
//    //{
//    //    logger.LogInformation("CreatePackageType is Called with {@CreateContentTypeCommand}", packageTypeCommand);

//    //    if (packageTypeCommand == null)
//    //        return ApiResponse<int>.Error(400, "ورودی ایجاد نوع محتوا نمی‌تواند null باشد");
//    //    var exist = await packageTypeRepository.CheckExistPackageTypeName(packageTypeCommand.ContentTypeName,
//    //        packageTypeCommand.CompanyTypeId, cancellationToken);

//    //    if (exist)
//    //        return ApiResponse<int>.Error(409, "نام نوع محتوا مشابه وجود دارد");

//    //    var order = await packageTypeRepository.OrderPackageType(packageTypeCommand.CompanyTypeId, cancellationToken);

//    //    var packageType = mapper.Map<PackageType>(packageTypeCommand);
//    //    packageType.OrderPackageType = order + 1;
//    //    if (packageType == null)
//    //        return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

//    //    var contentTypeId = await packageTypeRepository.CreatePackageTypeAsync(packageType, cancellationToken);
//    //    logger.LogInformation("PackageType created successfully with ID: {ContentTypeId}", contentTypeId);
//    //    return ApiResponse<int>.Created(contentTypeId, "PackageType created successfully");
//    //}

//    //public async Task<ApiResponse<PagedResult<PackageTypeDto>>> GetAllPackageTypes(GetAllPackageTypesQuery allPackageTypesQuery, CancellationToken cancellationToken)
//    //{
//    //    logger.LogInformation("GetAllPackageTypesByCompanyType is Called");
//    //    if (allPackageTypesQuery.PageSize <= 0 || allPackageTypesQuery.PageNumber <= 0)
//    //        return ApiResponse<PagedResult<PackageTypeDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

//    //    var (packageTypes, totalCount) = await packageTypeRepository.GetMatchingAllPackageTypes(
//    //        allPackageTypesQuery.SearchPhrase,
//    //        allPackageTypesQuery.CompanyTypeId,
//    //        allPackageTypesQuery.Active,
//    //        allPackageTypesQuery.PageSize,
//    //        allPackageTypesQuery.PageNumber,
//    //        allPackageTypesQuery.SortBy,
//    //        allPackageTypesQuery.SortDirection,
//    //        cancellationToken);
//    //    var packageTypeDto = mapper.Map<IReadOnlyList<PackageTypeDto>>(packageTypes) ?? Array.Empty<PackageTypeDto>();
//    //    logger.LogInformation("Retrieved {Count} package types", packageTypeDto.Count);

//    //    var data = new PagedResult<PackageTypeDto>(packageTypeDto, totalCount, allPackageTypesQuery.PageSize,
//    //        allPackageTypesQuery.PageNumber);
//    //    return ApiResponse<PagedResult<PackageTypeDto>>.Ok(data, "PackageTypes retrieved successfully");
//    //}

//    //public async Task<ApiResponse<PackageTypeDto>> GetPackageTypeByIdAsync(GetPackageTypeByIdQuery getPackageTypeByIdQuery, CancellationToken cancellationToken)
//    //{
//    //    logger.LogInformation("GetPackageTypeById is Called with ID: {Id}", getPackageTypeByIdQuery.Id);
//    //    if (getPackageTypeByIdQuery.Id <= 0)
//    //        return ApiResponse<PackageTypeDto>.Error(400, "شناسه نوع پکیج باید بزرگ‌تر از صفر باشد");

//    //    var packageType = await packageTypeRepository.GetPackageTypeById(getPackageTypeByIdQuery.Id, cancellationToken);
//    //    if (packageType is null)
//    //        return ApiResponse<PackageTypeDto>.Error(404, $"نوع محتوا با شناسه {getPackageTypeByIdQuery.Id} یافت نشد");

//    //    var result = mapper.Map<PackageTypeDto>(packageType);
//    //    logger.LogInformation("PackageType retrieved successfully with ID: {Id}", getPackageTypeByIdQuery.Id);
//    //    return ApiResponse<PackageTypeDto>.Ok(result, "PackageType retrieved successfully");
//    //}

//    //public async Task<ApiResponse<object>> DeletePackageTypeAsync(DeletePackageTypeCommand deletePackageTypeCommand, CancellationToken cancellationToken)
//    //{
//    //    logger.LogInformation("DeletePackageType is Called with ID: {Id}", deletePackageTypeCommand.Id);
//    //    if (deletePackageTypeCommand.Id <= 0)
//    //        return ApiResponse<object>.Error(400, "شناسه نوع پکیج باید بزرگ‌تر از صفر باشد");
//    //    var packageType =
//    //        await packageTypeRepository.GetPackageTypeById(deletePackageTypeCommand.Id, cancellationToken);
//    //    if (packageType is null)
//    //        return ApiResponse<object>.Error(404, $"نوع پکیج با شناسه {deletePackageTypeCommand.Id} یافت نشد");

//    //    packageTypeRepository.Delete(packageType);
//    //    await unitOfWork.SaveChangesAsync(cancellationToken);
//    //    logger.LogInformation("PackageType soft-deleted successfully with ID: {Id}", deletePackageTypeCommand.Id);
//    //    return ApiResponse<object>.Deleted("PackageType deleted successfully");
//    //}

//    //public async Task<ApiResponse<PackageTypeDto>> UpdatePackageTypeAsync(UpdatePackageTypeCommand updatePackageTypeCommand, CancellationToken cancellationToken)
//    //{
//    //    logger.LogInformation("UpdatePackageType is Called with {@UpdateContentTypeCommand}", updatePackageTypeCommand);
//    //    if (updatePackageTypeCommand == null || updatePackageTypeCommand.Id <= 0)
//    //        return ApiResponse<PackageTypeDto>.Error(400,
//    //            "شناسه نوع محتوا باید بزرگ‌تر از صفر باشد یا ورودی نامعتبر است");

//    //    var packageType = await packageTypeRepository.GetPackageTypeById(updatePackageTypeCommand.Id, cancellationToken);
//    //    if (packageType is null)
//    //        return ApiResponse<PackageTypeDto>.Error(404, $"نوع محتوا با شناسه {updatePackageTypeCommand.Id} یافت نشد");

//    //    var updatedPackageType = mapper.Map(updatePackageTypeCommand, packageType);
//    //    updatedPackageType.OrderPackageType = packageType.OrderPackageType;

//    //    await unitOfWork.SaveChangesAsync(cancellationToken);
//    //    logger.LogInformation("PackageType updated successfully with ID: {Id}", updatePackageTypeCommand.Id);

//    //    var updatedPackageTypeDto = mapper.Map<PackageTypeDto>(updatedPackageType);
//    //    return ApiResponse<PackageTypeDto>.Updated(updatedPackageTypeDto);
//    //}

//    //public async Task<ApiResponse<object>> MovePackageTypeUpAsync(MovePackageTypeUpCommand movePackageTypeUpCommand, CancellationToken cancellationToken)
//    //{
//    //    var packageType =
//    //        await companyTypeRepository.GetCompanyTypeById(movePackageTypeUpCommand.CompanyTypeId, cancellationToken);
//    //    if (packageType == null)
//    //        return ApiResponse<object>.Error(404,
//    //            $"نوع شرکت با شناسه {movePackageTypeUpCommand.CompanyTypeId} یافت نشد");

//    //    await packageTypeRepository.MovePackageTypeUpAsync(movePackageTypeUpCommand.CompanyTypeId,
//    //        movePackageTypeUpCommand.PackageTypeId, cancellationToken);
//    //    logger.LogInformation(
//    //        "PackageType moved up successfully. CompanyTypeId: {CompanyTypeId}, ContentTypeId: {PackageTypeId}",
//    //        movePackageTypeUpCommand.CompanyTypeId, movePackageTypeUpCommand.PackageTypeId);
//    //    return ApiResponse<object>.Ok("PackageType moved up successfully");
//    //}

//    //public async Task<ApiResponse<object>> MovePackageTypeDownAsync(MovePackageTypeDownCommand movePackageTypeDownCommand,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var packageType =
//    //        await companyTypeRepository.GetCompanyTypeById(movePackageTypeDownCommand.CompanyTypeId, cancellationToken);
//    //    if (packageType == null)
//    //        return ApiResponse<object>.Error(404,
//    //            $"نوع شرکت با شناسه {movePackageTypeDownCommand.CompanyTypeId} یافت نشد");

//    //    await packageTypeRepository.MovePackageTypeDownAsync(movePackageTypeDownCommand.CompanyTypeId,
//    //        movePackageTypeDownCommand.PackageTypeId, cancellationToken);
//    //    logger.LogInformation(
//    //        "ContentType moved down successfully. CompanyTypeId: {CompanyTypeId}, ContentTypeId: {PackageTypeId}",
//    //        movePackageTypeDownCommand.CompanyTypeId, movePackageTypeDownCommand.PackageTypeId);
//    //    return ApiResponse<object>.Ok("PackageType moved down successfully");
//    //} 

//    #endregion























//}

public class PackageTypeService(
    ILogger<PackageTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    IPackageTypeRepository packageTypeRepository,
    ICompanyTypeRepository companyTypeRepository,
    ICompanyPackageTypeRepository companyPackageTypeRepository,
    ICompanyRepository companyRepository)
    : IPackageTypeService
{
    public async Task<ApiResponse<int>> CreatePackageTypeAsync(CreatePackageTypeCommand packageTypeCommand,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("CreatePackageType is Called with {@CreatePackageTypeCommand}", packageTypeCommand);

        if (string.IsNullOrEmpty(packageTypeCommand.PackageTypeName))
            return ApiResponse<int>.Error(400, "ورودی ایجاد نوع پکیج نمی‌تواند null باشد");

        var exist = await packageTypeRepository.CheckExistPackageTypeName(packageTypeCommand.PackageTypeName,
            packageTypeCommand.CompanyTypeId, cancellationToken);

        if (exist)
            return ApiResponse<int>.Error(400, "نام نوع پکیج مشابه وجود دارد");

        int order = await packageTypeRepository.OrderPackageType(packageTypeCommand.CompanyTypeId, cancellationToken);

        var packageType = mapper.Map<PackageType>(packageTypeCommand) ?? null;
        if (packageType == null)
            return ApiResponse<int>.Error(400, "مشکل در عملیات تبدیل");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        packageType.OrderPackageType = order + 1;

        var packageTypeId = await packageTypeRepository.CreatePackageTypeAsync(packageType, cancellationToken);

        var getEligibleCommandlines =
            await companyRepository.GetCompaniesIdByCompanyTypeId(packageTypeCommand.CompanyTypeId, cancellationToken);

        if (getEligibleCommandlines.Any())
        {
            await companyPackageTypeRepository.CreateCompanyPackageTypes(getEligibleCommandlines, packageType, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("PackageType created successfully with ID: {PackageTypeId}", packageTypeId);
        return ApiResponse<int>.Ok(packageTypeId, "PackageType created successfully");
    }

    public async Task<ApiResponse<PagedResult<PackageTypeDto>>> GetAllPackageTypes(
        GetAllPackageTypesQuery allPackageTypesQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllPackageTypesByCompanyType is Called");
        if (allPackageTypesQuery.PageSize <= 0 || allPackageTypesQuery.PageNumber <= 0)
            return ApiResponse<PagedResult<PackageTypeDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (packageTypes, totalCount) = await packageTypeRepository.GetMatchingAllPackageTypes(
            allPackageTypesQuery.SearchPhrase,
            allPackageTypesQuery.CompanyTypeId,
            allPackageTypesQuery.Active,
            allPackageTypesQuery.PageSize,
            allPackageTypesQuery.PageNumber,
            allPackageTypesQuery.SortBy,
            allPackageTypesQuery.SortDirection,
            cancellationToken);
        var packageTypeDto = mapper.Map<IReadOnlyList<PackageTypeDto>>(packageTypes) ?? Array.Empty<PackageTypeDto>();
        logger.LogInformation("Retrieved {Count} package types", packageTypeDto.Count);

        var data = new PagedResult<PackageTypeDto>(packageTypeDto, totalCount, allPackageTypesQuery.PageSize,
            allPackageTypesQuery.PageNumber);
        return ApiResponse<PagedResult<PackageTypeDto>>.Ok(data, "PackageTypes retrieved successfully");
    }

    public async Task<ApiResponse<PackageTypeDto>> GetPackageTypeByIdAsync(
        GetPackageTypeByIdQuery getPackageTypeByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetPackageTypeById is Called with ID: {Id}", getPackageTypeByIdQuery.Id);
        if (getPackageTypeByIdQuery.Id <= 0)
            return ApiResponse<PackageTypeDto>.Error(400, "شناسه نوع پکیج باید بزرگ‌تر از صفر باشد");

        var packageType = await packageTypeRepository.GetPackageTypeById(getPackageTypeByIdQuery.Id, cancellationToken);
        if (packageType is null)
            return ApiResponse<PackageTypeDto>.Error(404, $"نوع پکیج با شناسه {getPackageTypeByIdQuery.Id} یافت نشد");

        var result = mapper.Map<PackageTypeDto>(packageType);
        logger.LogInformation("PackageType retrieved successfully with ID: {Id}", getPackageTypeByIdQuery.Id);
        return ApiResponse<PackageTypeDto>.Ok(result, "PackageType retrieved successfully");
    }

    public async Task<ApiResponse<object>> DeletePackageTypeAsync(DeletePackageTypeCommand deletePackageTypeCommand,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("DeletePackageType is Called with ID: {Id}", deletePackageTypeCommand.Id);
        if (deletePackageTypeCommand.Id <= 0)
            return ApiResponse<object>.Error(400, "شناسه نوع پکیج باید بزرگ‌تر از صفر باشد");

        var packageType = await packageTypeRepository.GetPackageTypeById(deletePackageTypeCommand.Id, cancellationToken);
        if (packageType is null)
            return ApiResponse<object>.Error(404, $"نوع پکیج با شناسه {deletePackageTypeCommand.Id} یافت نشد");

        packageTypeRepository.Delete(packageType);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("PackageType soft-deleted successfully with ID: {Id}", deletePackageTypeCommand.Id);
        return ApiResponse<object>.Deleted("PackageType deleted successfully");
    }

    public async Task<ApiResponse<PackageTypeDto>> UpdatePackageTypeAsync(UpdatePackageTypeCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdatePackageType is Called with {@UpdatePackageTypeCommand}", command);
        if (command == null || command.Id <= 0)
            return ApiResponse<PackageTypeDto>.Error(400,
                "شناسه نوع پکیج باید بزرگ‌تر از صفر باشد یا ورودی نامعتبر است");

        var packageType = await packageTypeRepository.GetPackageTypeById(command.Id, cancellationToken);
        if (packageType is null)
            return ApiResponse<PackageTypeDto>.Error(404, $"نوع پکیج با شناسه {command.Id} یافت نشد");


        // TODO : need to Check Existed Name

        var updatedPackageType = mapper.Map(command, packageType);
        updatedPackageType.OrderPackageType = packageType.OrderPackageType;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("PackageType updated successfully with ID: {Id}", command.Id);

        var updatedPackageTypeDto = mapper.Map<PackageTypeDto>(updatedPackageType);
        return ApiResponse<PackageTypeDto>.Ok(updatedPackageTypeDto);
    }

    public async Task<ApiResponse<object>> MovePackageTypeUpAsync(MovePackageTypeUpCommand movePackageTypeUpCommand,
        CancellationToken cancellationToken)
    {
        var companyType = await companyTypeRepository.GetCompanyTypeById(movePackageTypeUpCommand.CompanyTypeId, cancellationToken);
        if (companyType == null)
            return ApiResponse<object>.Error(404,
                $"نوع شرکت با شناسه {movePackageTypeUpCommand.CompanyTypeId} یافت نشد");

        await packageTypeRepository.MovePackageTypeUpAsync(movePackageTypeUpCommand.CompanyTypeId,
            movePackageTypeUpCommand.PackageTypeId, cancellationToken);
        logger.LogInformation(
            "PackageType moved up successfully. CompanyTypeId: {CompanyTypeId}, PackageTypeId: {PackageTypeId}",
            movePackageTypeUpCommand.CompanyTypeId, movePackageTypeUpCommand.PackageTypeId);
        return ApiResponse<object>.Ok("PackageType moved up successfully");
    }

    public async Task<ApiResponse<object>> MovePackageTypeDownAsync(
        MovePackageTypeDownCommand movePackageTypeDownCommand, CancellationToken cancellationToken)
    {
        var companyType = await companyTypeRepository.GetCompanyTypeById(movePackageTypeDownCommand.CompanyTypeId, cancellationToken);
        if (companyType == null)
            return ApiResponse<object>.Error(404,
                $"نوع شرکت با شناسه {movePackageTypeDownCommand.CompanyTypeId} یافت نشد");

        await packageTypeRepository.MovePackageTypeDownAsync(movePackageTypeDownCommand.CompanyTypeId,
            movePackageTypeDownCommand.PackageTypeId, cancellationToken);
        logger.LogInformation(
            "PackageType moved down successfully. CompanyTypeId: {CompanyTypeId}, PackageTypeId: {PackageTypeId}",
            movePackageTypeDownCommand.CompanyTypeId, movePackageTypeDownCommand.PackageTypeId);
        return ApiResponse<object>.Ok("PackageType moved down successfully");
    }
}