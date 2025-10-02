using AutoMapper;
using Capitan360.Application.Common;
using Microsoft.Extensions.Logging;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Dtos;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Queries.GetByCompanyId;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Queries.GetById;
using Capitan360.Domain.Interfaces;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Entities.Companies;
using Microsoft.AspNetCore.Http;

namespace Capitan360.Application.Features.Companies.CompanyCommissionses.Services;

public class CompanyCommissionsService(
    ILogger<CompanyCommissionsService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyCommissionsRepository companyCommissionsRepository,
    ICompanyRepository companyRepository
) : ICompanyCommissionsService
{
    public async Task<ApiResponse<int>> CreateCompanyCommissionsAsync(CreateCompanyCommissionsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyCommissions is Called with {@CreateCompanyCommissionsCommand}", command);

        var company = await companyRepository.GetCompanyByIdAsync(command.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyCommissions = mapper.Map<CompanyCommissions>(command);
        if (companyCommissions is null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        var companyCommissionsId = await companyCommissionsRepository.CreateCompanyCommissionsAsync(companyCommissions, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyCommissions created successfully with {@CompanyCommissions}", companyCommissions);
        return ApiResponse<int>.Created(companyCommissionsId, "کمیسیون با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyCommissionsAsync(DeleteCompanyCommissionsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyCommissions is Called with {@Id}", command.Id);

        var companyCommissions = await companyCommissionsRepository.GetCompanyCommissionsByIdAsync(command.Id, false, false, cancellationToken);
        if (companyCommissions is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "کمیسیون نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyCommissions.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        await companyCommissionsRepository.DeleteCompanyCommissionsAsync(companyCommissions.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("CompanyCommissions Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "کمیسیون با موفقیت حذف شد");
    }

    public async Task<ApiResponse<CompanyCommissionsDto>> UpdateCompanyCommissionsAsync(UpdateCompanyCommissionsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyCommissions is Called with {@UpdateCompanyCommissionsCommand}", command);

        var companyCommissions = await companyCommissionsRepository.GetCompanyCommissionsByIdAsync(command.Id, false, true, cancellationToken);
        if (companyCommissions is null)
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status404NotFound, "کمیسیون نا معتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyCommissions.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var updatedCompanyCommission = mapper.Map(command, companyCommissions);
        if (updatedCompanyCommission is null)
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyCommissions updated successfully with {@UpdateCompanyCommissionsCommand}", command);

        var updatedCompanyCommissionsDto = mapper.Map<CompanyCommissionsDto>(updatedCompanyCommission);
        if (updatedCompanyCommissionsDto == null)
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<CompanyCommissionsDto>.Ok(updatedCompanyCommissionsDto, "کمیسیون با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyCommissionsDto>>> GetAllCompanyCommissionsAsync(GetAllCompanyCommissionsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyCommissions is Called");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (query.CompanyId != 0 && query.CompanyTypeId != 0)
        {
            if (!user.IsSuperAdmin() && !user.IsSuperManager(query.CompanyTypeId) && !user.IsManager(query.CompanyId))
                return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId != 0 && query.CompanyTypeId == 0)
        {
            var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, true, false, cancellationToken);
            if (company is null)
                return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(400, "شرکت نامعتبر است");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId == 0 && query.CompanyTypeId != 0)
        {
            if (!user.IsSuperAdmin() && !user.IsSuperManager(query.CompanyTypeId))
                return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId == 0 && query.CompanyTypeId == 0)
        {
            if (!user.IsSuperAdmin())
                return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }

        var (companyCommissions, totalCount) = await companyCommissionsRepository.GetAllCompanyCommissionsAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyTypeId,
            query.CompanyId,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);
        
        var companyCommissionsDtos = mapper.Map<IReadOnlyList<CompanyCommissionsDto>>(companyCommissions) ?? Array.Empty<CompanyCommissionsDto>();
        if (companyCommissionsDtos == null)
            return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company commissions", companyCommissionsDtos.Count);

        var data = new PagedResult<CompanyCommissionsDto>(companyCommissionsDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyCommissionsDto>>.Ok(data, "کمیسیون ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<CompanyCommissionsDto>> GetCompanyCommissionsByCompanyIdAsync(GetCompanyCommissionsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyCommissionsByCompanyId is Called with {@Id}", query.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyCommissions = await companyCommissionsRepository.GetCompanyCommissionsByCompanyIdAsync(query.CompanyId, cancellationToken);
        if (companyCommissions is null)
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status404NotFound, "کمیسیون یافت نشد");

        var companyCommissionDto = mapper.Map<CompanyCommissionsDto>(companyCommissions);
        if (companyCommissionDto is null)
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        logger.LogInformation("CompanyCommissions retrieved successfully with CompanyId {@Id}", query.CompanyId);
        return ApiResponse<CompanyCommissionsDto>.Ok(companyCommissionDto, "کمیسیون با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<CompanyCommissionsDto>> GetCompanyCommissionsByIdAsync(GetCompanyCommissionsByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyCommissionsById is Called with {@Id}", query.Id);

        var companyCommissions = await companyCommissionsRepository.GetCompanyCommissionsByIdAsync(query.Id, false, false, cancellationToken);
        if (companyCommissions is null)
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status404NotFound, "کمیسیون یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companyCommissions.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyCommissionsDto = mapper.Map<CompanyCommissionsDto>(companyCommissions);
        if (companyCommissionsDto is null)
            return ApiResponse<CompanyCommissionsDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        logger.LogInformation("CompanyCommissions retrieved successfully with {@Id}", query.Id);
        return ApiResponse<CompanyCommissionsDto>.Ok(companyCommissionsDto, "کمیسیون با موفقیت دریافت شد");
    }
}
