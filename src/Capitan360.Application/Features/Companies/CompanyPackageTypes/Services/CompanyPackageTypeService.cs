using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveDown;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveUp;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateName;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetByCompanyId;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetById;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Services;

public class CompanyPackageTypeService(
    ILogger<CompanyPackageTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyPackageTypeRepository companyPackageTypeRepository,
    ICompanyRepository companyRepository,
    IUserContext userContext) : ICompanyPackageTypeService
{
    public async Task<ApiResponse<int>> MoveUpCompanyPackageTypeAsync(MoveUpCompanyPackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("MoveCompanyPackageTypeUp is Called with {@MoveCompanyPackageTypeUpCommand}", command);

        var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (companyPackageType == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بسته بندی نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPackageType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(404, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (companyPackageType.Order == 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        var count = await companyPackageTypeRepository.GetCountCompanyPackageTypeAsync(companyPackageType.CompanyId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await companyPackageTypeRepository.MoveCompanyPackageTypeUpAsync(command.Id, cancellationToken);
        logger.LogInformation(
            "PackageType moved up successfully., CompanyPackageTypeId: {CompanyPackageTypeId}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "بسته بندی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> MoveDownCompanyPackageTypeAsync(MoveDownCompanyPackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("MoveDownCompanyPackageType is Called with {@MoveCompanyPackageTypeDownCommand}", command);

        var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (companyPackageType == null)
            return ApiResponse<int>.Error(404, "بسته بندی نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPackageType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(404, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var count = await companyPackageTypeRepository.GetCountCompanyPackageTypeAsync(companyPackageType.CompanyId, cancellationToken);

        if (companyPackageType.Order == count)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await companyPackageTypeRepository.MoveCompanyPackageTypeDownAsync(command.Id, cancellationToken);
        logger.LogInformation("PackageType moved down successfully., CompanyPackageTypeId: {CompanyPackageTypeId}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "بسته بندی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> SetCompanyPackageTypeActivityStatusAsync(UpdateActiveStateCompanyPackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyPackageTypeActivityStatus Called with {@Id}", command.Id);

        var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.Id, false, true, cancellationToken);
        if (companyPackageType is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بسته بندی نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPackageType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        companyPackageType.Active = !companyPackageType.Active;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyPackageType activity status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت بسته بندی با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<int>> UpdateCompanyPackageTypeNameAsync(UpdateCompanyPackageTypeNameCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyPackageTypeName is Called with {@UpdateCompanyPackageTypeNameCommand}", command);

        var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.Id,  false, true, cancellationToken);
        if (companyPackageType is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بسته بندی نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPackageType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await companyPackageTypeRepository.CheckExistCompanyPackageTypeNameAsync(command.Name, command.Id, companyPackageType.CompanyId, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "نام بسته بندی تکراری است");

        companyPackageType.Name = companyPackageType.Name;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyPackageTypeName updated successfully with {@UpdateCompanyPackageTypeNameCommand}", command);

        return ApiResponse<int>.Ok(command.Id, "نام بسته بندی با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<CompanyPackageTypeDto>> UpdateCompanyPackageTypeAsync(UpdateCompanyPackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyPackageType is Called with {@UpdateCompanyPackageTypeCommand}", command);
      
        var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.Id,  false, true, cancellationToken);
        if (companyPackageType == null)
            return ApiResponse<CompanyPackageTypeDto>.Error(StatusCodes.Status404NotFound, "بسته بندی نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPackageType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyPackageTypeDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyPackageTypeDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyPackageTypeDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await companyPackageTypeRepository.CheckExistCompanyPackageTypeNameAsync(command.Name, command.Id, companyPackageType.CompanyId, cancellationToken))
            return ApiResponse<CompanyPackageTypeDto>.Error(StatusCodes.Status409Conflict, "نام بسته بندی تکراری است");

        var updatedComapnyPackageType = mapper.Map(command, companyPackageType);
        if (updatedComapnyPackageType is null)
            return ApiResponse<CompanyPackageTypeDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyPackageType updated successfully with {@UpdateCompanyPackageTypeCommand}", command);

        var updatedComapnyPackageTypeDto = mapper.Map<CompanyPackageTypeDto>(updatedComapnyPackageType);
        if (updatedComapnyPackageTypeDto == null)
            return ApiResponse<CompanyPackageTypeDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<CompanyPackageTypeDto>.Ok(updatedComapnyPackageTypeDto, "بسته بندی با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyPackageTypeDto>>> GetAllCompanyPackageTypesAsync(GetAllCompanyPackageTypesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyPackageTypes is Called");

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<PagedResult<CompanyPackageTypeDto>>.Error(404, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyPackageTypeDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<PagedResult<CompanyPackageTypeDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var (companyPackageTypes, totalCount) = await companyPackageTypeRepository.GetAllCompanyPackageTypesAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyId,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);
        
        var companyPackageTypeDtos = mapper.Map<IReadOnlyList<CompanyPackageTypeDto>>(companyPackageTypes) ?? Array.Empty<CompanyPackageTypeDto>();
        if (companyPackageTypeDtos == null)
            return ApiResponse<PagedResult<CompanyPackageTypeDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} companypackage types", companyPackageTypeDtos.Count);

        var data = new PagedResult<CompanyPackageTypeDto>(companyPackageTypeDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyPackageTypeDto>>.Ok(data, "بسته بندی‌ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<IReadOnlyList<CompanyPackageTypeDto>>> GetCompanyPackageTypeByCompanyIdAsync(GetCompanyPackageTypeByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyPackageTypeByCompanyId is Called with {@Id}", query.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<IReadOnlyList<CompanyPackageTypeDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<IReadOnlyList<CompanyPackageTypeDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<IReadOnlyList<CompanyPackageTypeDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyPackageTypes = await companyPackageTypeRepository.GetCompanyPackageTypeByCompanyIdAsync(query.CompanyId, cancellationToken);
        if (companyPackageTypes is null)
            return ApiResponse<IReadOnlyList<CompanyPackageTypeDto>>.Error(StatusCodes.Status404NotFound, "بسته بندی یافت نشد");

        var companyPackageTypeDtos = mapper.Map<IReadOnlyList<CompanyPackageTypeDto>>(companyPackageTypes) ?? Array.Empty<CompanyPackageTypeDto>();
        if (companyPackageTypeDtos == null)
            return ApiResponse<IReadOnlyList<CompanyPackageTypeDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company package types GetByCompanyId", companyPackageTypeDtos.Count);

        return ApiResponse<IReadOnlyList<CompanyPackageTypeDto>>.Ok(companyPackageTypeDtos, "بسته بندی‌ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<CompanyPackageTypeDto>> GetCompanyPackageTypeByIdAsync(GetCompanyPackageTypeByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyPackageTypeById is Called with {@Id}", query.Id);

        var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(query.Id, false, false, cancellationToken);
        if (companyPackageType is null)
            return ApiResponse<CompanyPackageTypeDto>.Error(StatusCodes.Status404NotFound, "بسته بندی یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companyPackageType.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyPackageTypeDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyPackageTypeDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyPackageTypeDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyPackageTypeDto = mapper.Map<CompanyPackageTypeDto>(companyPackageType);
        if (companyPackageTypeDto == null)
            return ApiResponse<CompanyPackageTypeDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("CompanyPackageType retrieved successfully with {@Id}", query.Id);
        return ApiResponse<CompanyPackageTypeDto>.Ok(companyPackageTypeDto, "بسته بندی با موفقیت دریافت شد");
    }
}

