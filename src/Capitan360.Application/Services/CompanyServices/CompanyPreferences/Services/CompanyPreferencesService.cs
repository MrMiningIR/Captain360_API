using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.CreateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.DeleteCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateInternationalAirlineCargoStateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateIssueDomesticWaybillStateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateShowInSearchEngineStateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateWebServiceSearchEngineStateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Queries.GetAllCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Queries.GetCompanyPreferencesByCompanyId;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Queries.GetCompanyPreferencesById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.CompanyRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanyPreferences.Services;


public class CompanyPreferencesService(
    ILogger<CompanyPreferencesService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyPreferencesRepository companyPreferencesRepository, ICompanyRepository companyRepository
) : ICompanyPreferencesService
{
    public async Task<ApiResponse<int>> CreateCompanyPreferencesAsync(CreateCompanyPreferencesCommand createCompanyPreferencesCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyPreferences is Called with {@CreateCompanyPreferencesCommand}", createCompanyPreferencesCommand);

        var company = await companyRepository.GetCompanyByIdAsync(createCompanyPreferencesCommand.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(400, "مشکل در دریافت اطلاعات");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(400, "مجوز این فعالیت را ندارید");

        var companyPreferences = mapper.Map<Domain.Entities.Companies.CompanyPreferences>(createCompanyPreferencesCommand);
        if (companyPreferences is null)
            return ApiResponse<int>.Error(400, "خطا در عملیات تبدیل");

        var companyPreferencesId = await companyPreferencesRepository.CreateCompanyPreferencesAsync(companyPreferences, cancellationToken);

        logger.LogInformation("CompanyPreferences created successfully with ID: {CompanyPreferencesId}", companyPreferencesId);
        return ApiResponse<int>.Ok(companyPreferencesId, "تنظیمات با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyPreferencesAsync(DeleteCompanyPreferencesCommand deleteCompanyPreferencesCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyPreferences is Called with ID: {Id}", deleteCompanyPreferencesCommand.Id);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(deleteCompanyPreferencesCommand.Id, true, false, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<int>.Error(400, $"تنظیمات نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPreferences.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(400, "مشکل در دریافت اطلاعات");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(400, "مجوز این فعالیت را ندارید");

        await companyPreferencesRepository.DeleteCompanyPreferencesAsync(companyPreferences);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyPreferences soft-deleted successfully with ID: {Id}", deleteCompanyPreferencesCommand.Id);
        return ApiResponse<int>.Ok(deleteCompanyPreferencesCommand.Id, "تنظیمات با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> SetCompanyInternationalAirlineCargoStatusAsync(UpdateInternationalAirlineCargoStateCompanyPreferencesCommand updateInternationalAirlineCargoStateCompanyPreferencesCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyInternationalAirlineCargoStatus Called with {@UpdateInternationalAirlineCargoStateCompanyPreferencesCommand}", updateInternationalAirlineCargoStateCompanyPreferencesCommand);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(updateInternationalAirlineCargoStateCompanyPreferencesCommand.Id, true, false, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<int>.Error(400, $"تنظیمات نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPreferences.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(400, "مشکل در دریافت اطلاعات");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(400, "مجوز این فعالیت را ندارید");

        companyPreferences.ActiveInternationalAirlineCargo = !companyPreferences.ActiveInternationalAirlineCargo;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("وضعیت بار خارجی شرکت با موفقیت به‌روزرسانی شد: {Id}", updateInternationalAirlineCargoStateCompanyPreferencesCommand.Id);
        return ApiResponse<int>.Ok(updateInternationalAirlineCargoStateCompanyPreferencesCommand.Id);
    }

    public async Task<ApiResponse<int>> SetCompanyIssueDomesticWaybillStatusAsync(UpdateIssueDomesticWaybillStateCompanyPreferencesCommand updateIssueDomesticWaybillStateCompanyPreferencesCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyIssueDomesticWaybillStatus Called with {@UpdateIssueDomesticWaybillStateCompanyPreferencesCommand}", updateIssueDomesticWaybillStateCompanyPreferencesCommand);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(updateIssueDomesticWaybillStateCompanyPreferencesCommand.Id, true, false, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<int>.Error(400, $"تنظیمات نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPreferences.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(400, "مشکل در دریافت اطلاعات");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(400, "مجوز این فعالیت را ندارید");

        companyPreferences.ActiveInternationalAirlineCargo = !companyPreferences.ActiveInternationalAirlineCargo;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("وضعیت صدور بارنامه شرکت با موفقیت به‌روزرسانی شد: {Id}", updateIssueDomesticWaybillStateCompanyPreferencesCommand.Id);
        return ApiResponse<int>.Ok(updateIssueDomesticWaybillStateCompanyPreferencesCommand.Id);
    }

    public async Task<ApiResponse<int>> SetCompanyShowInSearchEngineStatusAsync(UpdateShowInSearchEngineStateCompanyPreferencesCommand updateShowInSearchEngineStateCompanyPreferencesCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyShowInSearchEngineStatus Called with {@UpdateShowInSearchEngineStateCompanyPreferencesCommand}", updateShowInSearchEngineStateCompanyPreferencesCommand);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(updateShowInSearchEngineStateCompanyPreferencesCommand.Id, true, false, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<int>.Error(400, $"تنظیمات نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPreferences.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(400, "مشکل در دریافت اطلاعات");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(400, "مجوز این فعالیت را ندارید");

        companyPreferences.ActiveInternationalAirlineCargo = !companyPreferences.ActiveInternationalAirlineCargo;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("وضعیت نمایش شرکت در موتور جستجوی با موفقیت به‌روزرسانی شد: {Id}", updateShowInSearchEngineStateCompanyPreferencesCommand.Id);
        return ApiResponse<int>.Ok(updateShowInSearchEngineStateCompanyPreferencesCommand.Id);
    }

    public async Task<ApiResponse<int>> SetCompanyWebServiceSearchEngineStatusAsync(UpdateWebServiceSearchEngineStateCompanyPreferencesCommand updateWebServiceSearchEngineStateCompanyPreferencesCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyWebServiceSearchEngineStatus Called with {@UpdateWebServiceSearchEngineStateCompanyPreferencesCommand}", updateWebServiceSearchEngineStateCompanyPreferencesCommand);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(updateWebServiceSearchEngineStateCompanyPreferencesCommand.Id, true, false, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<int>.Error(400, $"کمیسیون نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPreferences.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(400, "مشکل در دریافت اطلاعات");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(400, "مجوز این فعالیت را ندارید");

        companyPreferences.ActiveInternationalAirlineCargo = !companyPreferences.ActiveInternationalAirlineCargo;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("وضعیت نمایش شرکت در وب سرویس با موفقیت به‌روزرسانی شد: {Id}", updateWebServiceSearchEngineStateCompanyPreferencesCommand.Id);
        return ApiResponse<int>.Ok(updateWebServiceSearchEngineStateCompanyPreferencesCommand.Id);
    }

    public async Task<ApiResponse<CompanyPreferencesDto>> UpdateCompanyPreferencesAsync(UpdateCompanyPreferencesCommand updateCompanyPreferencesCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyPreferences is Called with {@UpdateCompanyPreferencesCommand}", updateCompanyPreferencesCommand);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(updateCompanyPreferencesCommand.Id, true, false, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<CompanyPreferencesDto>.Error(400, $"تنظیمات نا معتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPreferences.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyPreferencesDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyPreferencesDto>.Error(400, "مشکل در دریافت اطلاعات");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyPreferencesDto>.Error(400, "مجوز این فعالیت را ندارید");

        var updatedCompanyCommission = mapper.Map(updateCompanyPreferencesCommand, companyPreferences);
        if (updatedCompanyCommission is null)
            return ApiResponse<CompanyPreferencesDto>.Error(400, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("تنظیمات با موفقیت به‌روزرسانی شد: {Id}", updateCompanyPreferencesCommand.Id);

        var updatedCompanyPreferencesDto = mapper.Map<CompanyPreferencesDto>(updatedCompanyCommission);
        return ApiResponse<CompanyPreferencesDto>.Ok(updatedCompanyPreferencesDto, "تنظیمات با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyPreferencesDto>>> GetAllCompanyPreferencesAsync(GetAllCompanyPreferencesQuery getAllCompanyPreferencesQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyPreferences is Called");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyPreferencesDto>>.Error(400, "مشکل در دریافت اطلاعات");

        if (getAllCompanyPreferencesQuery.CompanyId != 0 && getAllCompanyPreferencesQuery.CompanyTypeId != 0)
        {
            if (!user.IsSuperAdmin() && !user.IsSuperManager(getAllCompanyPreferencesQuery.CompanyTypeId) && !user.IsManager(getAllCompanyPreferencesQuery.CompanyId))
                return ApiResponse<PagedResult<CompanyPreferencesDto>>.Error(400, "مجوز این فعالیت را ندارید");
        }
        else if (getAllCompanyPreferencesQuery.CompanyId != 0 && getAllCompanyPreferencesQuery.CompanyTypeId == 0)
        {
            var company = await companyRepository.GetCompanyByIdAsync(getAllCompanyPreferencesQuery.CompanyId, true, false, cancellationToken);
            if (company is null)
                return ApiResponse<PagedResult<CompanyPreferencesDto>>.Error(400, $"شرکت نامعتبر است");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<PagedResult<CompanyPreferencesDto>>.Error(400, "مجوز این فعالیت را ندارید");
        }
        else if (getAllCompanyPreferencesQuery.CompanyId == 0 && getAllCompanyPreferencesQuery.CompanyTypeId != 0)
        {
            if (!user.IsSuperAdmin() && !user.IsSuperManager(getAllCompanyPreferencesQuery.CompanyTypeId))
                return ApiResponse<PagedResult<CompanyPreferencesDto>>.Error(400, "مجوز این فعالیت را ندارید");
        }
        else if (getAllCompanyPreferencesQuery.CompanyId == 0 && getAllCompanyPreferencesQuery.CompanyTypeId == 0)
        {
            if (!user.IsSuperAdmin())
                return ApiResponse<PagedResult<CompanyPreferencesDto>>.Error(400, "مجوز این فعالیت را ندارید");
        }

        var (companyPreferences, totalCount) = await companyPreferencesRepository.GetAllCompanyPreferencesAsync(
            getAllCompanyPreferencesQuery.SearchPhrase,
            getAllCompanyPreferencesQuery.SortBy,
            getAllCompanyPreferencesQuery.CompanyTypeId,
            getAllCompanyPreferencesQuery.CompanyId,
            true,
            getAllCompanyPreferencesQuery.PageNumber,
            getAllCompanyPreferencesQuery.PageSize,
            getAllCompanyPreferencesQuery.SortDirection,
            cancellationToken);

        var companyPreferencesDto = mapper.Map<IReadOnlyList<CompanyPreferencesDto>>(companyPreferences) ?? Array.Empty<CompanyPreferencesDto>();
        logger.LogInformation("Retrieved {Count} company commissions", companyPreferencesDto.Count);

        var data = new PagedResult<CompanyPreferencesDto>(companyPreferencesDto, totalCount, getAllCompanyPreferencesQuery.PageSize, getAllCompanyPreferencesQuery.PageNumber);
        return ApiResponse<PagedResult<CompanyPreferencesDto>>.Ok(data);
    }

    public async Task<ApiResponse<CompanyPreferencesDto>> GetCompanyPreferencesByCompanyIdAsync(GetCompanyPreferencesByCompanyIdQuery getCompanyPreferencesByCompanyIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyPreferencesByCompanyId is Called with ID: {Id}", getCompanyPreferencesByCompanyIdQuery.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(getCompanyPreferencesByCompanyIdQuery.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyPreferencesDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyPreferencesDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyPreferencesDto>.Error(403, "مجوز این فعالیت را ندارید");

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByCompanyIdAsync(getCompanyPreferencesByCompanyIdQuery.CompanyId, false, true, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<CompanyPreferencesDto>.Error(400, $"تنظیمات یافت نشد");

        var companyCommissionDto = mapper.Map<CompanyPreferencesDto>(companyPreferences);
        if (companyCommissionDto is null)
            return ApiResponse<CompanyPreferencesDto>.Error(400, "خطا در عملیات تبدیل");

        logger.LogInformation("CompanyPreferences retrieved successfully with ID: {Id}", getCompanyPreferencesByCompanyIdQuery.CompanyId);
        return ApiResponse<CompanyPreferencesDto>.Ok(companyCommissionDto);
    }

    public async Task<ApiResponse<CompanyPreferencesDto>> GetCompanyPreferencesByIdAsync(GetCompanyPreferencesByIdQuery getCompanyPreferencesByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyPreferencesById is Called with ID: {Id}", getCompanyPreferencesByIdQuery.Id);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(getCompanyPreferencesByIdQuery.Id, false, true, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<CompanyPreferencesDto>.Error(400, $"تنظیمات یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companyPreferences.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyPreferencesDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyPreferencesDto>.Error(400, "مشکل در دریافت اطلاعات");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyPreferencesDto>.Error(400, "مجوز این فعالیت را ندارید");

        var companyCommissionDto = mapper.Map<CompanyPreferencesDto>(companyPreferences);
        if (companyCommissionDto is null)
            return ApiResponse<CompanyPreferencesDto>.Error(400, "خطا در عملیات تبدیل");

        logger.LogInformation("CompanyPreferences retrieved successfully with ID: {Id}", getCompanyPreferencesByIdQuery.Id);
        return ApiResponse<CompanyPreferencesDto>.Ok(companyCommissionDto);
    }
}