using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.CreateCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.DeleteCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.UpdateCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetAllCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetCompanyCommissionsByCompanyId;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetCompanyCommissionsById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.CompanyRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.Services;

public class CompanyCommissionsService(
    ILogger<CompanyCommissionsService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyCommissionsRepository companyCommissionsRepository, ICompanyRepository companyRepository
) : ICompanyCommissionsService
{
    public async Task<ApiResponse<int>> CreateCompanyCommissionsAsync(CreateCompanyCommissionsCommand command,//ch**
        CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyCommissions is Called with {@CreateCompanyCommissionsCommand}", command);

        var company = await companyRepository.GetCompanyByIdAsync(command.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");



        var companyCommissions = mapper.Map<Domain.Entities.Companies.CompanyCommissions>(command);
        if (companyCommissions is null)
            return ApiResponse<int>.Error(400, "خطا در عملیات تبدیل");

        var companyCommissionsId = await companyCommissionsRepository.CreateCompanyCommissionsAsync(companyCommissions, cancellationToken);
        logger.LogInformation("CompanyCommissions created successfully with ID: {CompanyCommissionsId}", companyCommissionsId);
        return ApiResponse<int>.Ok(companyCommissionsId, "کمیسیون با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyCommissionsDto>>> GetAllCompanyCommissionsAsync(
        GetAllCompanyCommissionsQuery query, CancellationToken cancellationToken)
    {
        //logger.LogInformation("GetAllCompanyCommissions is Called");
        //var (companyCommissions, totalCount) = await companyCommissionsRepository.GetMatchingAllCompanyCommissionsAsync(
        //    query.SearchPhrase,
        //    query.SortBy, TODO, TODO, TODO,
        //    query.PageNumber,
        //    query.PageSize, query.SortDirection, cancellationToken);
        //var companyCommissionsDto = mapper.Map<IReadOnlyList<CompanyCommissionsDto>>(companyCommissions);
        //if (companyCommissionsDto is null)
        //    return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(400, "خطا در عملیات تبدیل");

        //logger.LogInformation("Retrieved {Count} company commissions", companyCommissionsDto.Count);

        //var data = new PagedResult<CompanyCommissionsDto>(companyCommissionsDto, totalCount, query.PageSize, query.PageNumber);
        //return ApiResponse<PagedResult<CompanyCommissionsDto>>.Ok(data);

        throw new NotImplementedException();
    }

    //public async Task<ApiResponse<PagedResult<CompanyCommissionsDto>>> GetAllCompanyCommissionsAsync(GetAllCompanyCommissionsQuery getAllCompanyCommissionsQuery, CancellationToken cancellationToken)
    //{
    //    logger.LogInformation("GetAllCompanyCommissions is Called");

    //    var user = userContext.GetCurrentUser();
    //    if (user == null)
    //        return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(400, "مشکل در دریافت اطلاعات");

    //    if (getAllCompanyCommissionsQuery.CompanyId != 0 && getAllCompanyCommissionsQuery.CompanyTypeId != 0)
    //    {
    //        if (!user.IsSuperAdmin() && !user.IsSuperManager(getAllCompanyCommissionsQuery.CompanyTypeId) && !user.IsManager(getAllCompanyCommissionsQuery.CompanyId))
    //            return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(400, "مجوز این فعالیت را ندارید");
    //    }
    //    else if (getAllCompanyCommissionsQuery.CompanyId != 0 && getAllCompanyCommissionsQuery.CompanyTypeId == 0)
    //    {
    //        var company = await companyRepository.GetCompanyByIdAsync(getAllCompanyCommissionsQuery.CompanyId, true, false, cancellationToken);
    //        if (company is null)
    //            return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(400, $"شرکت نامعتبر است");

    //        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
    //            return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(400, "مجوز این فعالیت را ندارید");
    //    }
    //    else if (getAllCompanyCommissionsQuery.CompanyId == 0 && getAllCompanyCommissionsQuery.CompanyTypeId != 0)
    //    {
    //        if (!user.IsSuperAdmin() && !user.IsSuperManager(getAllCompanyCommissionsQuery.CompanyTypeId))
    //            return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(400, "مجوز این فعالیت را ندارید");
    //    }
    //    else if (getAllCompanyCommissionsQuery.CompanyId == 0 && getAllCompanyCommissionsQuery.CompanyTypeId == 0)
    //    {
    //        if (!user.IsSuperAdmin())
    //            return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(400, "مجوز این فعالیت را ندارید");
    //    }

    //    var (companyCommissions, totalCount) = await companyCommissionsRepository.GetMatchingAllCompanyCommissionsAsync(
    //        getAllCompanyCommissionsQuery.SearchPhrase,
    //        getAllCompanyCommissionsQuery.SortBy,
    //        getAllCompanyCommissionsQuery.CompanyId,
    //        getAllCompanyCommissionsQuery.CompanyId,
    //        true,
    //        getAllCompanyCommissionsQuery.PageNumber,
    //        getAllCompanyCommissionsQuery.PageSize,
    //        getAllCompanyCommissionsQuery.SortDirection,
    //        cancellationToken);

    //    var companyCommissionsDto = mapper.Map<IReadOnlyList<CompanyCommissionsDto>>(companyCommissions) ?? Array.Empty<CompanyCommissionsDto>();
    //    logger.LogInformation("Retrieved {Count} company commissions", companyCommissionsDto.Count);

    //    var data = new PagedResult<CompanyCommissionsDto>(companyCommissionsDto, totalCount, getAllCompanyCommissionsQuery.PageSize, getAllCompanyCommissionsQuery.PageNumber);
    //    return ApiResponse<PagedResult<CompanyCommissionsDto>>.Ok(data);
    //}


    public async Task<ApiResponse<CompanyCommissionsDto>> GetCompanyCommissionsByIdAsync(
        GetCompanyCommissionsByIdQuery query, CancellationToken cancellationToken)//ch**
    {
        logger.LogInformation("GetCompanyCommissionsById is Called with ID: {Id}", query.Id);

        var companyCommissions = await companyCommissionsRepository.GetCompanyCommissionsByIdAsync(query.Id, false, true, cancellationToken);
        if (companyCommissions is null)
            return ApiResponse<CompanyCommissionsDto>.Error(400, $"کمیسیون یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companyCommissions.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyCommissionsDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyCommissionsDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyCommissionsDto>.Error(403, "مجوز این فعالیت را ندارید");

        var companyCommissionDto = mapper.Map<CompanyCommissionsDto>(companyCommissions);
        if (companyCommissionDto is null)
            return ApiResponse<CompanyCommissionsDto>.Error(400, "خطا در عملیات تبدیل");

        logger.LogInformation("CompanyCommissions retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanyCommissionsDto>.Ok(companyCommissionDto);
    }

    public async Task<ApiResponse<int>> DeleteCompanyCommissionsAsync(DeleteCompanyCommissionsCommand command, CancellationToken cancellationToken)//ch**
    {
        logger.LogInformation("DeleteCompanyCommissions is Called with ID: {Id}", command.Id);

        var companyCommissions = await companyCommissionsRepository.GetCompanyCommissionsByIdAsync(command.Id, false, false, cancellationToken);
        if (companyCommissions is null)
            return ApiResponse<int>.Error(400, $"کمیسیون نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyCommissions.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        await companyCommissionsRepository.DeleteCompanyCommissionsAsync(companyCommissions);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyCommissions soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "کمیسیون با موفقیت حذف شد");
    }

    public async Task<ApiResponse<CompanyCommissionsDto>> UpdateCompanyCommissionsAsync(UpdateCompanyCommissionsCommand command,//ch
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyCommissions is Called with {@UpdateCompanyCommissionsCommand}", command);

        var companyCommissions = await companyCommissionsRepository.GetCompanyCommissionsByIdAsync(command.Id, true, false, cancellationToken);
        if (companyCommissions is null)
            return ApiResponse<CompanyCommissionsDto>.Error(400, $"کمیسیون نا معتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyCommissions.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyCommissionsDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyCommissionsDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyCommissionsDto>.Error(403, "مجوز این فعالیت را ندارید");

        var updatedCompanyCommission = mapper.Map(command, companyCommissions);
        if (updatedCompanyCommission is null)
            return ApiResponse<CompanyCommissionsDto>.Error(400, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("کمیسیون با موفقیت به‌روزرسانی شد: {Id}", command.Id);

        var updatedCompanyCommissionsDto = mapper.Map<CompanyCommissionsDto>(updatedCompanyCommission);
        return ApiResponse<CompanyCommissionsDto>.Ok(updatedCompanyCommissionsDto, "کمیسیون با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<CompanyCommissionsDto>> GetCompanyCommissionsByCompanyIdAsync(GetCompanyCommissionsByCompanyId.GetCompanyCommissionsByCompanyIdQuery query, CancellationToken cancellationToken)//ch**
    {
        logger.LogInformation("GetCompanyCommissionsByCompanyId is Called with ID: {Id}", query.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyCommissionsDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyCommissionsDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyCommissionsDto>.Error(403, "مجوز این فعالیت را ندارید");

        var companyCommissions = await companyCommissionsRepository.GetCompanyCommissionsByCompanyIdAsync(query.CompanyId, false, true, cancellationToken);
        if (companyCommissions is null)
            return ApiResponse<CompanyCommissionsDto>.Error(400, $"کمیسیون یافت نشد");

        var companyCommissionDto = mapper.Map<CompanyCommissionsDto>(companyCommissions);
        if (companyCommissionDto is null)
            return ApiResponse<CompanyCommissionsDto>.Error(400, "خطا در عملیات تبدیل");

        logger.LogInformation("CompanyCommissions retrieved successfully with ID: {Id}", query.CompanyId);
        return ApiResponse<CompanyCommissionsDto>.Ok(companyCommissionDto);
    }


}