using AutoMapper;
using Capitan360.Application.Common;
using Microsoft.Extensions.Logging;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateInternationalAirlineCargoState;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateIssueDomesticWaybillState;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateShowInSearchEngineState;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateWebServiceSearchEngineState;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Dtos;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Queries.GetByCompanyId;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Queries.GetById;
using Capitan360.Domain.Interfaces;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Entities.Companies;
using Microsoft.AspNetCore.Http;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Services;

public class CompanyPreferencesService(
    ILogger<CompanyPreferencesService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyPreferencesRepository companyPreferencesRepository,
    ICompanyRepository companyRepository
) : ICompanyPreferencesService
{
    public async Task<ApiResponse<int>> CreateCompanyPreferencesAsync(CreateCompanyPreferencesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyPreferences is Called with {@CreateCompanyPreferencesCommand}", command);

        var company = await companyRepository.GetCompanyByIdAsync(command.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyPreferences = mapper.Map<CompanyPreferences>(command);
        if (companyPreferences is null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        var companyPreferencesId = await companyPreferencesRepository.CreateCompanyPreferencesAsync(companyPreferences, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyPreferences created successfully with {@CompanyPreferences}", companyPreferences);
        return ApiResponse<int>.Created(companyPreferencesId, "تنظیمات با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyPreferencesAsync(DeleteCompanyPreferencesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyPreferences is Called with {@Id}", command.Id);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(command.Id, false, false, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "تنظیمات نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPreferences.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        await companyPreferencesRepository.DeleteCompanyPreferencesAsync(companyPreferences.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyPreferences Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "تنظیمات با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> SetCompanyInternationalAirlineCargoStatusAsync(UpdateInternationalAirlineCargoStateCompanyPreferencesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyInternationalAirlineCargoStatus Called with {@Id}", command.Id);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(command.Id, false, true, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "تنظیمات نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPreferences.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        companyPreferences.ActiveInternationalAirlineCargo = !companyPreferences.ActiveInternationalAirlineCargo;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyPreferences international airline cargo status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت نمایش بار خارجی با موفقیت به روز رسانی شد");
    }

    public async Task<ApiResponse<int>> SetCompanyIssueDomesticWaybillStatusAsync(UpdateIssueDomesticWaybillStateCompanyPreferencesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyIssueDomesticWaybillStatus Called with {@Id}", command.Id);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(command.Id, false, true, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "تنظیمات نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPreferences.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        companyPreferences.ActiveInternationalAirlineCargo = !companyPreferences.ActiveInternationalAirlineCargo;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyPreferences issue domesticWaybill status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت صدور بارنامه داخلی با موفقیت به روز رسانی شد");
    }

    public async Task<ApiResponse<int>> SetCompanyShowInSearchEngineStatusAsync(UpdateShowInSearchEngineStateCompanyPreferencesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyShowInSearchEngineStatus Called with {@Id}", command.Id);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(command.Id, false, true, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "تنظیمات نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPreferences.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        companyPreferences.ActiveInternationalAirlineCargo = !companyPreferences.ActiveInternationalAirlineCargo;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyPreferences show in search engine status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت نمایش در موتور جستجو با موفقیت به روز رسانی شد");
    }

    public async Task<ApiResponse<int>> SetCompanyWebServiceSearchEngineStatusAsync(UpdateWebServiceSearchEngineStateCompanyPreferencesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyWebServiceSearchEngineStatus Called with {@Id}", command.Id);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(command.Id, false, true, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "کمیسیون نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPreferences.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        companyPreferences.ActiveInternationalAirlineCargo = !companyPreferences.ActiveInternationalAirlineCargo;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyPreferences web service search engine status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت نمایش در موتور جستجوی وب سرویس با موفقیت به روز رسانی شد");
    }

    public async Task<ApiResponse<CompanyPreferencesDto>> UpdateCompanyPreferencesAsync(UpdateCompanyPreferencesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyPreferences is Called with {@UpdateCompanyPreferencesCommand}", command);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(command.Id, false, true, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status404NotFound, "تنظیمات نا معتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPreferences.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var updatedCompanyPreferences = mapper.Map(command, companyPreferences);
        if (updatedCompanyPreferences is null)
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyPreferences updated successfully with {@UpdateCompanyPreferencesCommand}", command);

        var updatedCompanyPreferencesDto = mapper.Map<CompanyPreferencesDto>(updatedCompanyPreferences);
        if (updatedCompanyPreferencesDto == null)
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<CompanyPreferencesDto>.Ok(updatedCompanyPreferencesDto, "تنظیمات با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyPreferencesDto>>> GetAllCompanyPreferencesAsync(GetAllCompanyPreferencesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyPreferences is Called");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyPreferencesDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (query.CompanyId != 0 && query.CompanyTypeId != 0)
        {
            if (!user.IsSuperAdmin() && !user.IsSuperManager(query.CompanyTypeId) && !user.IsManager(query.CompanyId))
                return ApiResponse<PagedResult<CompanyPreferencesDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId != 0 && query.CompanyTypeId == 0)
        {
            var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, true, false, cancellationToken);
            if (company is null)
                return ApiResponse<PagedResult<CompanyPreferencesDto>>.Error(400, "شرکت نامعتبر است");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<PagedResult<CompanyPreferencesDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId == 0 && query.CompanyTypeId != 0)
        {
            if (!user.IsSuperAdmin() && !user.IsSuperManager(query.CompanyTypeId))
                return ApiResponse<PagedResult<CompanyPreferencesDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId == 0 && query.CompanyTypeId == 0)
        {
            if (!user.IsSuperAdmin())
                return ApiResponse<PagedResult<CompanyPreferencesDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }

        var (companyPreferences, totalCount) = await companyPreferencesRepository.GetAllCompanyPreferencesAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyTypeId,
            query.CompanyId,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);
        
        var companyPreferencesDtos = mapper.Map<IReadOnlyList<CompanyPreferencesDto>>(companyPreferences) ?? Array.Empty<CompanyPreferencesDto>();
        if (companyPreferencesDtos == null)
            return ApiResponse<PagedResult<CompanyPreferencesDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company preferences", companyPreferencesDtos.Count);

        var data = new PagedResult<CompanyPreferencesDto>(companyPreferencesDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyPreferencesDto>>.Ok(data, "تنظیمات با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<CompanyPreferencesDto>> GetCompanyPreferencesByCompanyIdAsync(GetCompanyPreferencesByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyPreferencesByCompanyId is Called with {@CompanyId}", query.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByCompanyIdAsync(query.CompanyId, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status404NotFound, "تنظیمات یافت نشد");

        var companyPreferenceDto = mapper.Map<CompanyPreferencesDto>(companyPreferences);
        if (companyPreferenceDto is null)
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        logger.LogInformation("CompanyPreferences retrieved successfully with CompanyId {@Id}", query.CompanyId);
        return ApiResponse<CompanyPreferencesDto>.Ok(companyPreferenceDto, "تنظیمات با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<CompanyPreferencesDto>> GetCompanyPreferencesByIdAsync(GetCompanyPreferencesByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyPreferencesById is Called with {@Id}", query.Id);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(query.Id, false, false, cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status404NotFound, "تنظیمات یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companyPreferences.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyPreferencesDto = mapper.Map<CompanyPreferencesDto>(companyPreferences);
        if (companyPreferencesDto is null)
            return ApiResponse<CompanyPreferencesDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        logger.LogInformation("CompanyPreferences retrieved successfully with {@Id}", query.Id);
        return ApiResponse<CompanyPreferencesDto>.Ok(companyPreferencesDto, "تنظیمات با موفقیت دریافت شد");
    }
}