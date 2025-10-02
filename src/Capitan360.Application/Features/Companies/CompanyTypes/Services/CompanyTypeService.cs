using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyTypes.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyTypes.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyTypes.Queries.GetById;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.Companies.CompanyTypes.Services;

public class CompanyTypeService(
    ILogger<CompanyTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyTypeRepository companyTypeRepository
) : ICompanyTypeService
{
    public async Task<ApiResponse<int>> CreateCompanyTypeAsync(CreateCompanyTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyType is Called with {@CreateCompanyTypeCommand}", command);

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await companyTypeRepository.CheckExistCompanyTypeNameAsync(command.TypeName, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "نام نوع شرکت تکراری است");

        if (await companyTypeRepository.CheckExistCompanyTypeDisplayNameAsync(command.DisplayName, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "نام نمایشی نوع شرکت تکراری است");

        var companyType = mapper.Map<CompanyType>(command) ?? null;
        if (companyType == null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        var companyTypeId = await companyTypeRepository.CreateCompanyTypeAsync(companyType, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyType created successfully with {@CompanyType}", companyType);
        return ApiResponse<int>.Created(companyTypeId, "نوع شرکت با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyTypeAsync(DeleteCompanyTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyType is Called with {@Id}", command.Id);

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyType = await companyTypeRepository.GetCompanyTypeByIdAsync(command.Id, true, false, cancellationToken);
        if (companyType is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "نوع شرکت نامعتبر است");

        await companyTypeRepository.DeleteCompanyTypeAsync(companyType.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyType Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "نوع شرکت با موفقیت حذف شد");
    }

    public async Task<ApiResponse<CompanyTypeDto>> UpdateCompanyTypeAsync(UpdateCompanyTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyType is Called with {@UpdateCompanyTypeCommand}", command);

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyTypeDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<CompanyTypeDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyType = await companyTypeRepository.GetCompanyTypeByIdAsync(command.Id, false, true, cancellationToken);
        if (companyType is null)
            return ApiResponse<CompanyTypeDto>.Error(StatusCodes.Status404NotFound, "نوع شرکت نامعتبر است");

        if (await companyTypeRepository.CheckExistCompanyTypeNameAsync(command.TypeName, command.Id, cancellationToken))
            return ApiResponse<CompanyTypeDto>.Error(StatusCodes.Status409Conflict, "نام نوع شرکت تکراری است");

        if (await companyTypeRepository.CheckExistCompanyTypeDisplayNameAsync(command.DisplayName, command.Id, cancellationToken))
            return ApiResponse<CompanyTypeDto>.Error(StatusCodes.Status409Conflict, "نام نمایشی نوع شرکت تکراری است");

        var updatedCompanyType = mapper.Map(command, companyType);
        if (updatedCompanyType == null)
            return ApiResponse<CompanyTypeDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyType updated successfully with {@UpdateCompanyTypeCommand}", command);

        var updatedCompanyTypeDto = mapper.Map<CompanyTypeDto>(updatedCompanyType);
        if (updatedCompanyTypeDto == null)
            return ApiResponse<CompanyTypeDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<CompanyTypeDto>.Ok(updatedCompanyTypeDto, "نوع شرکت با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyTypeDto>>> GetAllCompanyTypesAsync(GetAllCompanyTypesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyTypes is Called");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyTypeDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<PagedResult<CompanyTypeDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var (companyTypes, totalCount) = await companyTypeRepository.GetAllCompanyTypesAsync(
            query.SearchPhrase,
            query.SortBy,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);

        var companyTypeDtos = mapper.Map<IReadOnlyList<CompanyTypeDto>>(companyTypes) ?? Array.Empty<CompanyTypeDto>();
        if (companyTypeDtos == null)
            return ApiResponse<PagedResult<CompanyTypeDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {@Count} companytypes", companyTypeDtos.Count);

        var data = new PagedResult<CompanyTypeDto>(companyTypeDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyTypeDto>>.Ok(data, "نوع شرکت‌ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<CompanyTypeDto>> GetCompanyTypeByIdAsync(GetCompanyTypeByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyTypeById is Called with {@Id}", query.Id);

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyTypeDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<CompanyTypeDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyType = await companyTypeRepository.GetCompanyTypeByIdAsync(query.Id, false, false, cancellationToken);
        if (companyType is null)
            return ApiResponse<CompanyTypeDto>.Error(StatusCodes.Status404NotFound, "نوع شرکت نامعتبر است");

        var result = mapper.Map<CompanyTypeDto>(companyType);
        if (result == null)
            return ApiResponse<CompanyTypeDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("CompanyType retrieved successfully with {@Id}", query.Id);
        return ApiResponse<CompanyTypeDto>.Ok(result, "نوع شرکت با موفقیت دریافت شد");
    }
}