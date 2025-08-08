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
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Queries.GetCompanyPreferencesById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Repositories.CompanyRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanyPreferences.Services;


public class CompanyPreferencesService(
    ILogger<CompanyPreferencesService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyPreferencesRepository companyPreferencesRepository
) : ICompanyPreferencesService
{
    public async Task<ApiResponse<int>> CreateCompanyPreferencesAsync(CreateCompanyPreferencesCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyPreferences is Called with {@CreateCompanyPreferencesCommand}", command);
        if (string.IsNullOrEmpty(command.EconomicCode))
            return ApiResponse<int>.Error(400, "ورودی ایجاد شرکت نمی‌تواند null باشد");

        var companyPreferences = mapper.Map<Domain.Entities.CompanyEntity.CompanyPreferences>(command);
        if (companyPreferences == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        var companyPreferencesId = await companyPreferencesRepository.CreateCompanyPreferencesAsync(companyPreferences, cancellationToken);

        logger.LogInformation("CompanyPreferences created successfully with ID: {CompanyPreferencesId}", companyPreferencesId);
        return ApiResponse<int>.Created(companyPreferencesId, "Company created successfully");
    }

    public async Task<ApiResponse<PagedResult<CompanyPreferencesDto>>> GetAllCompanyPreferences(
        GetAllCompanyPreferencesQuery allCompanyPreferencesQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyPreferences is Called");
        var (companyPreferences, totalCount) = await companyPreferencesRepository.GetAllCompanyPreferencesAsync(
            allCompanyPreferencesQuery.SearchPhrase,
            allCompanyPreferencesQuery.PageSize,
            allCompanyPreferencesQuery.PageNumber,
            allCompanyPreferencesQuery.SortBy,
            allCompanyPreferencesQuery.SortDirection,
            cancellationToken);
        var companyPreferencesDto = mapper.Map<IReadOnlyList<CompanyPreferencesDto>>(companyPreferences);

        logger.LogInformation("Retrieved {Count} company preferences", companyPreferencesDto.Count);
        var data = new PagedResult<CompanyPreferencesDto>(companyPreferencesDto, totalCount, allCompanyPreferencesQuery.PageSize, allCompanyPreferencesQuery.PageNumber);
        return ApiResponse<PagedResult<CompanyPreferencesDto>>.Ok(data, "Companies retrieved successfully");
    }

    public async Task<ApiResponse<CompanyPreferencesDto>> GetCompanyPreferencesByIdAsync(
        GetCompanyPreferencesByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyPreferencesById is Called with ID: {Id}", query.Id);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(query.Id, false, cancellationToken);
        if (companyPreferences == null)
            return ApiResponse<CompanyPreferencesDto>.Error(404, $"تنظیمات شرکت نامعتبر است");

        var result = mapper.Map<CompanyPreferencesDto>(companyPreferences);
        logger.LogInformation("CompanyPreferences retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanyPreferencesDto>.Ok(result, "PackageType retrieved successfully");
    }

    public async Task<ApiResponse<int>> DeleteCompanyPreferencesAsync(DeleteCompanyPreferencesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyPreferences is Called with ID: {Id}", command.Id);

        var companyPreferences =
            await companyPreferencesRepository.GetCompanyPreferencesByCompanyIdAsync(command.Id, true,
                cancellationToken);
        if (companyPreferences is null)
            return ApiResponse<int>.Error(400, $"تنظیمات با شناسه {command.Id} یافت نشد");

        companyPreferencesRepository.Delete(companyPreferences, Guid.NewGuid().ToString());
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyPreferences soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id);
    }

    public async Task<ApiResponse<CompanyPreferencesDto>> UpdateCompanyPreferencesAsync(UpdateCompanyPreferencesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyPreferences is Called with {@UpdateCompanyPreferencesCommand}", command);

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(command.Id, true, cancellationToken);
        if (companyPreferences == null)
            return ApiResponse<CompanyPreferencesDto>.Error(404, $"تنظیمات نامعتبر است");

        var mapped = mapper.Map(command, companyPreferences);
        if (mapped == null)
            return ApiResponse<CompanyPreferencesDto>.Error(400, "مشکل در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyPreferences updated successfully with ID: {Id}", command.Id);

        var updatedCompanyPreferencesCommandDto = mapper.Map<CompanyPreferencesDto>(command);
        return ApiResponse<CompanyPreferencesDto>.Ok(updatedCompanyPreferencesCommandDto, "تنظیمات شرکت با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<int>> SetCompanyInternationalAirlineCargoStatusAsync(UpdateInternationalAirlineCargoStateCompanyPreferencesCommand updateInternationalAirlineCargoStateCompanyPreferencesCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyInternationalAirlineCargoStatus Called with {@UpdateActiveStatePackageTypeCommand}", updateInternationalAirlineCargoStateCompanyPreferencesCommand);

        var packageType = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(updateInternationalAirlineCargoStateCompanyPreferencesCommand.Id, true, cancellationToken);
        if (packageType == null)
            return ApiResponse<int>.Error(404, $"تنظمیات شرکت نامعتبر است");

        packageType.ActiveInternationalAirlineCargo = !packageType.ActiveInternationalAirlineCargo;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("SetCompanyInternationalAirlineCargoStatus Updated successfully with ID: {Id}", updateInternationalAirlineCargoStateCompanyPreferencesCommand.Id);
        return ApiResponse<int>.Ok(updateInternationalAirlineCargoStateCompanyPreferencesCommand.Id, "وضعیت بارنامه خارجی با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<int>> SetCompanyIssueDomesticWaybillStatusAsync(UpdateIssueDomesticWaybillStateCompanyPreferencesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyIssueDomesticWaybillStatus Called with {@UpdateActiveStatePackageTypeCommand}", command);

        var packageType = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(command.Id, true, cancellationToken);
        if (packageType == null)
            return ApiResponse<int>.Error(404, $"تنظمیات شرکت نامعتبر است");

        packageType.ActiveIssueDomesticWaybill = !packageType.ActiveIssueDomesticWaybill;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("SetCompanyIssueDomesticWaybillStatus Updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت صدور بارنامه داخلی با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<int>> SetCompanyShowInSearchEngineStatusAsync(UpdateShowInSearchEngineStateCompanyPreferencesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyShowInSearchEngineStatus Called with {@UpdateActiveStatePackageTypeCommand}", command);

        var packageType = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(command.Id, true, cancellationToken);
        if (packageType == null)
            return ApiResponse<int>.Error(404, $"تنظمیات شرکت نامعتبر است");

        packageType.ActiveShowInSearchEngine = !packageType.ActiveShowInSearchEngine;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("SetCompanyShowInSearchEngineStatus Updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت نمایش در موتور جستجو با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<int>> SetCompanyWebServiceSearchEngineStatusAsync(UpdateWebServiceSearchEngineStateCompanyPreferencesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyWebServiceSearchStatus Called with {@UpdateActiveStatePackageTypeCommand}", command);

        var packageType = await companyPreferencesRepository.GetCompanyPreferencesByIdAsync(command.Id, true, cancellationToken);
        if (packageType == null)
            return ApiResponse<int>.Error(404, $"تنظمیات شرکت نامعتبر است");


        packageType.ActiveInWebServiceSearchEngine = !packageType.ActiveInWebServiceSearchEngine;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("SetCompanyWebServiceSearchStatus Updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت نمایش در موتور جستجوی وب سرویس با موفقیت به‌روزرسانی شد");

    }
}