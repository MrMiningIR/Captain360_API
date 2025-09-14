using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.CreateCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.DeleteCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.UpdateCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Queries.GetAllCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Queries.GetCompanySmsPatternsByCompanyId;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Queries.GetCompanySmsPatternsById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.CompanyRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Services;

public class CompanySmsPatternsService(
    ILogger<CompanySmsPatternsService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanySmsPatternsRepository companySmsPatternsRepository, ICompanyRepository companyRepository
) : ICompanySmsPatternsService
{
    public async Task<ApiResponse<int>> CreateCompanySmsPatternsAsync(CreateCompanySmsPatternsCommand command, CancellationToken cancellationToken)//ch**
    {
        logger.LogInformation("CreateCompanySmsPatterns is Called with {@CreateCompanySmsPatternsCommand}", command);

        var company = await companyRepository.GetCompanyByIdAsync(command.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        var companySmsPatterns = mapper.Map<Domain.Entities.Companies.CompanySmsPatterns>(command);
        if (companySmsPatterns is null)
            return ApiResponse<int>.Error(400, "خطا در عملیات تبدیل");

        var companySmsPatternsId = await companySmsPatternsRepository.CreateCompanySmsPatternsAsync(companySmsPatterns, cancellationToken);

        logger.LogInformation("CompanySmsPatterns created successfully with ID: {CompanySmsPatternsId}", companySmsPatternsId);
        return ApiResponse<int>.Ok(companySmsPatternsId, "تنظیمات پیامک با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<PagedResult<CompanySmsPatternsDto>>> GetAllCompanySmsPatternsAsync(GetAllCompanySmsPatternsQuery query, CancellationToken cancellationToken) //ch**
    {
        //logger.LogInformation("GetAllCompanySmsPatterns is Called");
        //var (companySmsPatterns, totalCount) = await companySmsPatternsRepository.GetMatchingAllCompanySmsPatternsAsync(
        //    allCompanySmsPatternsQuery.SearchPhrase,
        //    allCompanySmsPatternsQuery.SortBy, TODO, TODO, TODO,
        //    allCompanySmsPatternsQuery.PageNumber,
        //    allCompanySmsPatternsQuery.PageSize, allCompanySmsPatternsQuery.SortDirection, cancellationToken);
        //var companySmsPatternsDto = mapper.Map<IReadOnlyList<CompanySmsPatternsDto>>(companySmsPatterns);
        //if (companySmsPatternsDto is null)
        //    return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(400, "خطا در عملیات تبدیل");
        //var data = new PagedResult<CompanySmsPatternsDto>(companySmsPatternsDto, totalCount, allCompanySmsPatternsQuery.PageSize, allCompanySmsPatternsQuery.PageNumber);
        //logger.LogInformation("Retrieved {Count} company SMS patterns", companySmsPatternsDto.Count);

        //return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Ok(data, "Companies retrieved successfully");

        //--
        //logger.LogInformation("GetAllCompanySmsPatterns is Called");
        //var user = userContext.GetCurrentUser();
        //if (user == null)
        //    return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(400, "مشکل در دریافت اطلاعات");

        //if (getAllCompanySmsPatternsQuery.CompanyId != 0 && getAllCompanySmsPatternsQuery.CompanyTypeId != 0)
        //{
        //    if (!user.IsSuperAdmin() && !user.IsSuperManager(getAllCompanySmsPatternsQuery.CompanyTypeId) && !user.IsManager(getAllCompanySmsPatternsQuery.CompanyId))
        //        return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(400, "مجوز این فعالیت را ندارید");
        //}
        //else if (getAllCompanySmsPatternsQuery.CompanyId != 0 && getAllCompanySmsPatternsQuery.CompanyTypeId == 0)
        //{
        //    var company = await companyRepository.GetCompanyByIdAsync(getAllCompanySmsPatternsQuery.CompanyId, true, false, cancellationToken);
        //    if (company is null)
        //        return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(400, $"شرکت نامعتبر است");

        //    if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
        //        return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(400, "مجوز این فعالیت را ندارید");
        //}
        //else if (getAllCompanySmsPatternsQuery.CompanyId == 0 && getAllCompanySmsPatternsQuery.CompanyTypeId != 0)
        //{
        //    if (!user.IsSuperAdmin() && !user.IsSuperManager(getAllCompanySmsPatternsQuery.CompanyTypeId))
        //        return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(400, "مجوز این فعالیت را ندارید");
        //}
        //else if (getAllCompanySmsPatternsQuery.CompanyId == 0 && getAllCompanySmsPatternsQuery.CompanyTypeId == 0)
        //{
        //    if (!user.IsSuperAdmin())
        //        return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(400, "مجوز این فعالیت را ندارید");
        //}

        //var (companySmsPatterns, totalCount) = await companySmsPatternsRepository.GetMatchingAllCompanySmsPatternsAsync(
        //    getAllCompanySmsPatternsQuery.SearchPhrase,
        //    getAllCompanySmsPatternsQuery.SortBy,
        //    getAllCompanySmsPatternsQuery.CompanyId,
        //    getAllCompanySmsPatternsQuery.CompanyId,
        //    true,
        //    getAllCompanySmsPatternsQuery.PageNumber,
        //    getAllCompanySmsPatternsQuery.PageSize,
        //    getAllCompanySmsPatternsQuery.SortDirection,
        //    cancellationToken);

        //var companySmsPatternsDto = mapper.Map<IReadOnlyList<CompanySmsPatternsDto>>(companySmsPatterns) ?? Array.Empty<CompanySmsPatternsDto>();
        //logger.LogInformation("Retrieved {Count} company SmsPatterns", companySmsPatternsDto.Count);

        //var data = new PagedResult<CompanySmsPatternsDto>(companySmsPatternsDto, totalCount, getAllCompanySmsPatternsQuery.PageSize, getAllCompanySmsPatternsQuery.PageNumber);
        //return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Ok(data);

        throw new NotImplementedException();
    }

    public async Task<ApiResponse<CompanySmsPatternsDto>> GetCompanySmsPatternsByIdAsync(GetCompanySmsPatternsByIdQuery query, CancellationToken cancellationToken)//ch**
    {
        logger.LogInformation("GetCompanySmsPatternsById is Called with ID: {Id}", query.Id);

        var companySmsPatterns = await companySmsPatternsRepository.GetCompanySmsPatternsByIdAsync(query.Id, false, true, cancellationToken);
        if (companySmsPatterns is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(400, $"تنظیمات پیامک یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companySmsPatterns.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanySmsPatternsDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanySmsPatternsDto>.Error(403, "مجوز این فعالیت را ندارید");

        var companySmsPatternDto = mapper.Map<CompanySmsPatternsDto>(companySmsPatterns);
        if (companySmsPatternDto is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(400, "خطا در عملیات تبدیل");

        logger.LogInformation("CompanySmsPatterns retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanySmsPatternsDto>.Ok(companySmsPatternDto);
    }

    public async Task<ApiResponse<int>> DeleteCompanySmsPatternsAsync(DeleteCompanySmsPatternsCommand command, CancellationToken cancellationToken)//ch**
    {
        //    logger.LogInformation("DeleteCompanySmsPatterns is Called with ID: {Id}", command.Id);

        //    var companySmsPatterns = await companySmsPatternsRepository.GetCompanySmsPatternsByIdAsync(command.Id, true, TODO, cancellationToken);
        //    if (companySmsPatterns is null)
        //        return ApiResponse<int>.Error(400, $"الگو با شناسه {command.Id} یافت نشد");
        //    companySmsPatternsRepository.DeleteCompanySmsPatternsAsync(companySmsPatterns, Guid.NewGuid().ToString());
        //    await unitOfWork.SaveChangesAsync(cancellationToken);

        //    logger.LogInformation("CompanySmsPatterns soft-deleted successfully with ID: {Id}", command.Id);
        //    return ApiResponse<int>.Ok(command.Id);

        throw new NotImplementedException();
    }

    public async Task<ApiResponse<CompanySmsPatternsDto>> UpdateCompanySmsPatternsAsync(UpdateCompanySmsPatternsCommand command, CancellationToken cancellationToken)//ch**
    {
        logger.LogInformation("UpdateCompanySmsPatterns is Called with {@UpdateCompanySmsPatternsCommand}", command);

        var companySmsPatterns = await companySmsPatternsRepository.GetCompanySmsPatternsByIdAsync(command.Id, true, false, cancellationToken);
        if (companySmsPatterns is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(400, $"تنظیمات پیامک نا معتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companySmsPatterns.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanySmsPatternsDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanySmsPatternsDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanySmsPatternsDto>.Error(403, "مجوز این فعالیت را ندارید");

        var updatedCompanySmsPattern = mapper.Map(command, companySmsPatterns);
        if (updatedCompanySmsPattern is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(400, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("تنظیمات پیامک با موفقیت به‌روزرسانی شد: {Id}", command.Id);

        var updatedCompanySmsPatternsDto = mapper.Map<CompanySmsPatternsDto>(updatedCompanySmsPattern);
        return ApiResponse<CompanySmsPatternsDto>.Ok(updatedCompanySmsPatternsDto, "تنظیمات پیامک با موفقیت به‌روزرسانی شد");
    }


    public async Task<ApiResponse<CompanySmsPatternsDto>> GetCompanySmsPatternsByCompanyIdAsync(GetCompanySmsPatternsByCompanyId.GetCompanySmsPatternsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanySmsPatternsByCompanyId is Called with ID: {Id}", query.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanySmsPatternsDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanySmsPatternsDto>.Error(403, "مجوز این فعالیت را ندارید");

        var companySmsPatterns = await companySmsPatternsRepository.GetCompanySmsPatternsByCompanyIdAsync(query.CompanyId, false, true, cancellationToken);
        if (companySmsPatterns is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(400, $"تنظیمات پیامک یافت نشد");

        var companySmsPatternDto = mapper.Map<CompanySmsPatternsDto>(companySmsPatterns);
        if (companySmsPatternDto is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(400, "خطا در عملیات تبدیل");

        logger.LogInformation("CompanySmsPatterns retrieved successfully with ID: {Id}", query.CompanyId);
        return ApiResponse<CompanySmsPatternsDto>.Ok(companySmsPatternDto);
    }
}