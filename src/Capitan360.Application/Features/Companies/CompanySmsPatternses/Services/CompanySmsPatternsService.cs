using AutoMapper;
using Capitan360.Application.Common;
using Microsoft.Extensions.Logging;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Commands.Create;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Commands.Update;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Dtos;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Queries.GetByCompanyId;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Queries.GetById;
using Capitan360.Domain.Interfaces;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Entities.Companies;
using Microsoft.AspNetCore.Http;

namespace Capitan360.Application.Features.Companies.CompanySmsPatternses.Services;

public class CompanySmsPatternsService(
    ILogger<CompanySmsPatternsService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanySmsPatternsRepository companySmsPatternsRepository,
    ICompanyRepository companyRepository
) : ICompanySmsPatternsService
{
    public async Task<ApiResponse<int>> CreateCompanySmsPatternsAsync(CreateCompanySmsPatternsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanySmsPatterns is Called with {@CreateCompanySmsPatternsCommand}", command);

        var company = await companyRepository.GetCompanyByIdAsync(command.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companySmsPatterns = mapper.Map<CompanySmsPatterns>(command);
        if (companySmsPatterns is null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        var companySmsPatternsId = await companySmsPatternsRepository.CreateCompanySmsPatternsAsync(companySmsPatterns, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanySmsPatterns created successfully with {@CompanySmsPatterns}", companySmsPatterns);
        return ApiResponse<int>.Created(companySmsPatternsId, "تنظیمات پیامک با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanySmsPatternsAsync(DeleteCompanySmsPatternsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanySmsPatterns is Called with {@Id}", command.Id);

        var companySmsPatterns = await companySmsPatternsRepository.GetCompanySmsPatternsByIdAsync(command.Id, false, false, cancellationToken);
        if (companySmsPatterns is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "تنظیمات پیامک نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companySmsPatterns.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        await companySmsPatternsRepository.DeleteCompanySmsPatternsAsync(companySmsPatterns.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("CompanySmsPatterns Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "تنظیمات پیامک با موفقیت حذف شد");
    }

    public async Task<ApiResponse<CompanySmsPatternsDto>> UpdateCompanySmsPatternsAsync(UpdateCompanySmsPatternsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanySmsPatterns is Called with {@UpdateCompanySmsPatternsCommand}", command);

        var companySmsPatterns = await companySmsPatternsRepository.GetCompanySmsPatternsByIdAsync(command.Id, false, true,  cancellationToken);
        if (companySmsPatterns is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status404NotFound, "تنظیمات پیامک نا معتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companySmsPatterns.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var updatedCompanySmsPattern = mapper.Map(command, companySmsPatterns);
        if (updatedCompanySmsPattern is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanySmsPatterns updated successfully with {@UpdateCompanySmsPatternsCommand}", command);

        var updatedCompanySmsPatternsDto = mapper.Map<CompanySmsPatternsDto>(updatedCompanySmsPattern);
        if (updatedCompanySmsPatternsDto == null)
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<CompanySmsPatternsDto>.Ok(updatedCompanySmsPatternsDto, "تنظیمات پیامک با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanySmsPatternsDto>>> GetAllCompanySmsPatternsAsync(GetAllCompanySmsPatternsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanySmsPatterns is Called");
        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (query.CompanyId != 0 && query.CompanyTypeId != 0)
        {
            if (!user.IsSuperAdmin() && !user.IsSuperManager(query.CompanyTypeId) && !user.IsManager(query.CompanyId))
                return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId != 0 && query.CompanyTypeId == 0)
        {
            var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, true, false, cancellationToken);
            if (company is null)
                return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(400, "شرکت نامعتبر است");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId == 0 && query.CompanyTypeId != 0)
        {
            if (!user.IsSuperAdmin() && !user.IsSuperManager(query.CompanyTypeId))
                return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId == 0 && query.CompanyTypeId == 0)
        {
            if (!user.IsSuperAdmin())
                return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }

        var (companySmsPatterns, totalCount) = await companySmsPatternsRepository.GetAllCompanySmsPatternsAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyTypeId,
            query.CompanyId,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);
        
        var companySmsPatternsDtos = mapper.Map<IReadOnlyList<CompanySmsPatternsDto>>(companySmsPatterns) ?? Array.Empty<CompanySmsPatternsDto>();
        if (companySmsPatternsDtos == null)
            return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company smsPatterns", companySmsPatternsDtos.Count);

        var data = new PagedResult<CompanySmsPatternsDto>(companySmsPatternsDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Ok(data, "تنظیمات پیامک با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<CompanySmsPatternsDto>> GetCompanySmsPatternsByCompanyIdAsync(GetCompanySmsPatternsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanySmsPatternsByCompanyId is Called with {@Id}", query.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companySmsPatterns = await companySmsPatternsRepository.GetCompanySmsPatternsByCompanyIdAsync(query.CompanyId, cancellationToken);
        if (companySmsPatterns is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status404NotFound, "تنظیمات پیامک یافت نشد");

        var companySmsPatternDto = mapper.Map<CompanySmsPatternsDto>(companySmsPatterns);
        if (companySmsPatternDto is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        logger.LogInformation("CompanySmsPatterns retrieved successfully with CompanyId {@Id}", query.CompanyId);
        return ApiResponse<CompanySmsPatternsDto>.Ok(companySmsPatternDto, "تنظیمات پیامک با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<CompanySmsPatternsDto>> GetCompanySmsPatternsByIdAsync(GetCompanySmsPatternsByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanySmsPatternsById is Called with {@Id}", query.Id);

        var companySmsPatterns = await companySmsPatternsRepository.GetCompanySmsPatternsByIdAsync(query.Id, false, false, cancellationToken);
        if (companySmsPatterns is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status404NotFound, "تنظیمات پیامک یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companySmsPatterns.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companySmsPatternsDto = mapper.Map<CompanySmsPatternsDto>(companySmsPatterns);
        if (companySmsPatternsDto is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        logger.LogInformation("CompanySmsPatterns retrieved successfully with {@Id}", query.Id);
        return ApiResponse<CompanySmsPatternsDto>.Ok(companySmsPatternsDto, "تنظیمات پیامک با موفقیت دریافت شد");
    }
}