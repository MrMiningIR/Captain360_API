using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Application.Features.PackageTypes.Commands.Create;
using Capitan360.Application.Features.PackageTypes.Commands.Delete;
using Capitan360.Application.Features.PackageTypes.Commands.MoveDown;
using Capitan360.Application.Features.PackageTypes.Commands.MoveUp;
using Capitan360.Application.Features.PackageTypes.Commands.Update;
using Capitan360.Application.Features.PackageTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.PackageTypes.Dtos;
using Capitan360.Application.Features.PackageTypes.Queries.GetAll;
using Capitan360.Application.Features.PackageTypes.Queries.GetById;
using Capitan360.Domain.Entities.PackageTypes;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Interfaces.Repositories.PackageTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.PackageTypes.Services;



public class PackageTypeService(
    ILogger<PackageTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    IPackageTypeRepository packageTypeRepository,
    ICompanyPackageTypeRepository companyPackageTypeRepository,
    ICompanyRepository companyRepository) : IPackageTypeService
{
    public async Task<ApiResponse<int>> CreatePackageTypeAsync(CreatePackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreatePackageType is Called with {@CreatePackageTypeCommand}", command);

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(command.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await packageTypeRepository.CheckExistPackageTypeNameAsync(command.Name, command.CompanyTypeId, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "نام بسته بندی تکراری است");

        int existingCount = await packageTypeRepository.GetCountPackageTypeAsync(command.CompanyTypeId, cancellationToken);

        var packageType = mapper.Map<PackageType>(command) ?? null;
        if (packageType == null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        packageType.Order = existingCount + 1;

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var packageTypeId = await packageTypeRepository.CreatePackageTypeAsync(packageType, cancellationToken);

        var companyIds = await companyRepository.GetCompaniesIdByCompanyTypeIdAsync(command.CompanyTypeId, cancellationToken);

        if (companyIds.Any())
        {
            await companyPackageTypeRepository.CreateCompanyPackageTypesAsync(companyIds, packageType, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("PackageType created successfully with {@PackageType}", packageType);
        return ApiResponse<int>.Ok(packageTypeId, "بسته بندی با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeletePackageTypeAsync(DeletePackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeletePackageType is Called with {@Id}", command.Id);

        var packageType = await packageTypeRepository.GetPackageTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (packageType is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بسته بندی نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(packageType.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        await packageTypeRepository.DeletePackageTypeAsync(packageType.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("PackageType Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "بسته بندی با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> MoveUpPackageTypeAsync(MoveUpPackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("MoveUpPackageType is Called with {@Id}", command.Id);

        var packageType = await packageTypeRepository.GetPackageTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (packageType == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بسته بندی نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(packageType.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (packageType.Order == 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        var count = await packageTypeRepository.GetCountPackageTypeAsync(packageType.CompanyTypeId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await packageTypeRepository.MovePackageTypeUpAsync(command.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("PackageType moved up successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "بسته بندی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> MoveDownPackageTypeAsync(MoveDownPackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("MoveDownPackageType is Called with {@Id}", command.Id);

        var packageType = await packageTypeRepository.GetPackageTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (packageType == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بسته بندی نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(packageType.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var count = await packageTypeRepository.GetCountPackageTypeAsync(packageType.CompanyTypeId, cancellationToken);

        if (packageType.Order == count)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await packageTypeRepository.MovePackageTypeDownAsync(command.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("PackageType moved down successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "بسته بندی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> SetPackageTypeActivityStatusAsync(UpdateActiveStatePackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetPackageTypeActivityStatus Called with {@Id}", command.Id);

        var packageType = await packageTypeRepository.GetPackageTypeByIdAsync(command.Id, false, true, cancellationToken);
        if (packageType is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بسته بندی نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(packageType.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        packageType.Active = !packageType.Active;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("PackageType activity status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت بسته بندی با موفقیت به روز رسانی شد");
    }

    public async Task<ApiResponse<PackageTypeDto>> UpdatePackageTypeAsync(UpdatePackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdatePackageType is Called with {@UpdatePackageTypeCommand}", command);

        var packageType = await packageTypeRepository.GetPackageTypeByIdAsync(command.Id, false, true, cancellationToken);
        if (packageType is null)
            return ApiResponse<PackageTypeDto>.Error(StatusCodes.Status404NotFound, "بسته بندی نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PackageTypeDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(packageType.CompanyTypeId))
            return ApiResponse<PackageTypeDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await packageTypeRepository.CheckExistPackageTypeNameAsync(command.Name, command.Id, packageType.CompanyTypeId, cancellationToken))
            return ApiResponse<PackageTypeDto>.Error(StatusCodes.Status409Conflict, "نام بسته بندی تکراری است");

        var updatedPackageType = mapper.Map(command, packageType);
        if (updatedPackageType == null)
            return ApiResponse<PackageTypeDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("PackageType updated successfully with {@UpdatePackageTypeCommand}", command);

        var updatedPackageTypeDto = mapper.Map<PackageTypeDto>(updatedPackageType);
        if (updatedPackageTypeDto == null)
            return ApiResponse<PackageTypeDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<PackageTypeDto>.Ok(updatedPackageTypeDto, "بسته بندی با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<PackageTypeDto>>> GetAllPackageTypesAsync(GetAllPackageTypesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllPackageTypes is Called");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<PackageTypeDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(query.CompanyTypeId))
            return ApiResponse<PagedResult<PackageTypeDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var (packageTypes, totalCount) = await packageTypeRepository.GetAllPackageTypesAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyTypeId,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);

        var packageTypeDtos = mapper.Map<IReadOnlyList<PackageTypeDto>>(packageTypes) ?? Array.Empty<PackageTypeDto>();
        if (packageTypeDtos == null)
            return ApiResponse<PagedResult<PackageTypeDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} packagetypes", packageTypeDtos.Count);

        var data = new PagedResult<PackageTypeDto>(packageTypeDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<PackageTypeDto>>.Ok(data, "بسته بندی‌ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<PackageTypeDto>> GetPackageTypeByIdAsync(GetPackageTypeByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetPackageTypeById is Called with {@Id}", query.Id);

        var packageType = await packageTypeRepository.GetPackageTypeByIdAsync(query.Id, false, false, cancellationToken);
        if (packageType is null)
            return ApiResponse<PackageTypeDto>.Error(StatusCodes.Status404NotFound, "بسته بندی یافت نشد");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PackageTypeDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(packageType.CompanyTypeId))
            return ApiResponse<PackageTypeDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var result = mapper.Map<PackageTypeDto>(packageType);
        if (result == null)
            return ApiResponse<PackageTypeDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("PackageType retrieved successfully with {@Id}", query.Id);
        return ApiResponse<PackageTypeDto>.Ok(result, "بسته بندی با موفقیت دریافت شد");
    }
}
