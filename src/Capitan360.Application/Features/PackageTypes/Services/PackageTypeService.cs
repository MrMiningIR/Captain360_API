using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Domain.Entities.PackageTypes;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Companies;
using Capitan360.Domain.Repositories.PackageTypes;
using Microsoft.Extensions.Logging;
using Capitan360.Application.Features.PackageTypes.Dtos;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Application.Features.PackageTypes.Commands.Create;
using Capitan360.Application.Features.PackageTypes.Commands.Update;
using Capitan360.Application.Features.PackageTypes.Commands.Delete;
using Capitan360.Application.Features.PackageTypes.Commands.MoveDown;
using Capitan360.Application.Features.PackageTypes.Commands.MoveUp;
using Capitan360.Application.Features.PackageTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.PackageTypes.Queries.GetById;
using Capitan360.Application.Features.PackageTypes.Queries.GetAll;

namespace Capitan360.Application.Features.PackageTypeService.Services;



public class PackageTypeService(
    ILogger<PackageTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    IPackageTypeRepository packageTypeRepository,
    ICompanyTypeRepository companyTypeRepository,
    ICompanyPackageTypeRepository companyPackageTypeRepository,
    ICompanyRepository companyRepository) : IPackageTypeService
{
    public async Task<ApiResponse<int>> CreatePackageTypeAsync(CreatePackageTypeCommand command,//ch**
        CancellationToken cancellationToken)
    {
        logger.LogInformation("CreatePackageType is Called with {@CreatePackageTypeCommand}", command);


        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(command.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");


        if (await packageTypeRepository.CheckExistPackageTypeNameAsync(command.Name, command.CompanyTypeId, null, cancellationToken))
            return ApiResponse<int>.Error(400, "نام بسته بندی تکراری است");


        int existingCount = await packageTypeRepository.GetCountPackageTypeAsync(command.CompanyTypeId, cancellationToken);

        var packageType = mapper.Map<PackageType>(command) ?? null;
        if (packageType == null)
            return ApiResponse<int>.Error(400, "مشکل در عملیات تبدیل");

        packageType.Order = existingCount + 1;

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var packageTypeId = await packageTypeRepository.CreatePackageTypeAsync(packageType, cancellationToken);

        var companyIds =
            await companyRepository.GetCompaniesIdByCompanyTypeIdAsync(command.CompanyTypeId, cancellationToken);

        if (companyIds.Any())
        {
            await companyPackageTypeRepository.CreateCompanyPackageTypesAsync(companyIds, packageType, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("PackageType created successfully with ID: {PackageTypeId}", packageTypeId);
        return ApiResponse<int>.Ok(packageTypeId, "بسته بندی با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<PagedResult<PackageTypeDto>>> GetAllPackageTypesAsync(//ch**
        GetAllPackageTypesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllPackageTypesByCompanyType is Called");
        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<PackageTypeDto>>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(query.CompanyTypeId))
            return ApiResponse<PagedResult<PackageTypeDto>>.Error(403, "مجوز این فعالیت را ندارید");
        var (packageTypes, totalCount) = await packageTypeRepository.GetAllPackageTypesAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyTypeId,
            true,
            query.PageSize,
            query.PageNumber,
            query.SortDirection,
            cancellationToken);
        var packageTypeDto = mapper.Map<IReadOnlyList<PackageTypeDto>>(packageTypes) ?? Array.Empty<PackageTypeDto>();
        logger.LogInformation("Retrieved {Count} package types", packageTypeDto.Count);

        var data = new PagedResult<PackageTypeDto>(packageTypeDto, totalCount, query.PageSize,
            query.PageNumber);
        return ApiResponse<PagedResult<PackageTypeDto>>.Ok(data, "بسته‌بندی‌ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<PackageTypeDto>> GetPackageTypeByIdAsync(//ch**
        GetPackageTypeByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetPackageTypeById is Called with ID: {Id}", query.Id);

        var packageType = await packageTypeRepository.GetPackageTypeByIdAsync(query.Id, false, true, cancellationToken);
        if (packageType is null)
            return ApiResponse<PackageTypeDto>.Error(400, $"نوع پکیج با شناسه {query.Id} یافت نشد");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PackageTypeDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(packageType.CompanyTypeId))
            return ApiResponse<PackageTypeDto>.Error(403, "مجوز این فعالیت را ندارید");

        var result = mapper.Map<PackageTypeDto>(packageType);
        logger.LogInformation("PackageType retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<PackageTypeDto>.Ok(result, "بسته‌بندی با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<int>> DeletePackageTypeAsync(DeletePackageTypeCommand command,//ch**
        CancellationToken cancellationToken)
    {
        logger.LogInformation("DeletePackageType is Called with ID: {Id}", command.Id);

        var packageType = await packageTypeRepository.GetPackageTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (packageType is null)
            return ApiResponse<int>.Error(400, $"بسته بندی نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(packageType.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");



        await packageTypeRepository.DeletePackageTypeAsync(packageType.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("PackageType soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "بسته‌بندی با موفقیت حذف شد");
    }

    public async Task<ApiResponse<PackageTypeDto>> UpdatePackageTypeAsync(UpdatePackageTypeCommand command,//ch**
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdatePackageType is Called with {@UpdatePackageTypeCommand}", command);

        var packageType = await packageTypeRepository.GetPackageTypeByIdAsync(command.Id, true, false, cancellationToken);
        if (packageType is null)
            return ApiResponse<PackageTypeDto>.Error(400, $"بسته بندی نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PackageTypeDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(packageType.CompanyTypeId))
            return ApiResponse<PackageTypeDto>.Error(403, "مجوز این فعالیت را ندارید");



        if (await packageTypeRepository.CheckExistPackageTypeNameAsync(command.Name, command.Id, packageType.CompanyTypeId, cancellationToken))
            return ApiResponse<PackageTypeDto>.Error(400, "نام بسته بندی تکراری است");

        var updatedPackageType = mapper.Map(command, packageType);

        if (updatedPackageType == null)
            return ApiResponse<PackageTypeDto>.Error(400, "مشکل در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var updatedPackageTypeDto = mapper.Map<PackageTypeDto>(updatedPackageType);
        return ApiResponse<PackageTypeDto>.Ok(updatedPackageTypeDto, "بسته‌بندی با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<int>> MovePackageTypeUpAsync(MoveUpPackageTypeCommand command,//ch**
        CancellationToken cancellationToken)
    {
        logger.LogInformation("MovePackageTypeUp is Called with {@MovePackageTypeUpCommand}", command);

        var packageType = await packageTypeRepository.GetPackageTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (packageType == null)
            return ApiResponse<int>.Error(400, $"بسته بندی نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(400, "مشکل در دریافت اطلاعات");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(packageType.CompanyTypeId))
            return ApiResponse<int>.Error(400, "مجوز این فعالیت را ندارید");

        if (packageType.Order == 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        var count = await packageTypeRepository.GetCountPackageTypeAsync(packageType.CompanyTypeId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await packageTypeRepository.MovePackageTypeUpAsync(command.Id, cancellationToken);

        logger.LogInformation(
            "PackageType moved up successfully.  PackageTypeId: {PackageTypeId}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "بسته بندی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> MovePackageTypeDownAsync(//ch**
        MoveDownPackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("MovePackageTypeUp is Called with {@MovePackageTypeUpCommand}", command);

        var packageType = await packageTypeRepository.GetPackageTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (packageType == null)
            return ApiResponse<int>.Error(400, $"بسته بندی نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(400, "مشکل در دریافت اطلاعات");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(packageType.CompanyTypeId))
            return ApiResponse<int>.Error(400, "مجوز این فعالیت را ندارید");

        if (packageType.Order == 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        var count = await packageTypeRepository.GetCountPackageTypeAsync(packageType.CompanyTypeId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await packageTypeRepository.MovePackageTypeDownAsync(command.Id, cancellationToken);

        logger.LogInformation(
            "PackageType moved down successfully.  PackageTypeId: {PackageTypeId}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "بسته بندی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> SetPackageTypeActivityStatusAsync(UpdateActiveStatePackageTypeCommand command, CancellationToken cancellationToken)//ch**
    {
        logger.LogInformation("SetPackageTypeActivityStatus Called with {@UpdateActiveStatePackageTypeCommand}", command);

        var packageType =
            await packageTypeRepository.GetPackageTypeByIdAsync(command.Id, true, false, cancellationToken);

        if (packageType is null)
            return ApiResponse<int>.Error(400, $"بسته بندی نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(packageType.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        packageType.Active = !packageType.Active;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("وضعیت بسته بندی با موفقیت به‌روزرسانی شد: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id);
    }
}