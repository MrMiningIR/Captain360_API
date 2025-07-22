using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.CreateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.DeleteCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateCompanyPreferences;
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

    public async Task<PagedResult<CompanyPreferencesDto>> GetAllCompanyPreferences(GetAllCompanyPreferencesQuery allCompanyPreferencesQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyPreferences is Called");
        var (companyPreferences, totalCount) = await companyPreferencesRepository.GetMatchingAllCompanyPreferences(
            allCompanyPreferencesQuery.SearchPhrase,
            allCompanyPreferencesQuery.PageSize,
            allCompanyPreferencesQuery.PageNumber,
            allCompanyPreferencesQuery.SortBy,
            allCompanyPreferencesQuery.SortDirection,
            cancellationToken);
        var companyPreferencesDto = mapper.Map<IReadOnlyList<CompanyPreferencesDto>>(companyPreferences);
        logger.LogInformation("Retrieved {Count} company preferences", companyPreferencesDto.Count);
        return new PagedResult<CompanyPreferencesDto>(companyPreferencesDto, totalCount, allCompanyPreferencesQuery.PageSize, allCompanyPreferencesQuery.PageNumber);
    }

    public async Task<ApiResponse<CompanyPreferencesDto>> GetCompanyPreferencesByIdAsync(
        GetCompanyPreferencesByIdQuery getCompanyPreferencesByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyPreferencesById is Called with ID: {Id}", getCompanyPreferencesByIdQuery.Id);
        if (getCompanyPreferencesByIdQuery.Id <= 0)
            return ApiResponse<CompanyPreferencesDto>.Error(400, "شناسه معتبر نیست");

        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByCompanyId(getCompanyPreferencesByIdQuery.Id, cancellationToken);

        if (companyPreferences is null)
            return ApiResponse<CompanyPreferencesDto>.Ok(new CompanyPreferencesDto(), "اطلاعات یافت نشد");

        var result = mapper.Map<CompanyPreferencesDto>(companyPreferences);
        logger.LogInformation("CompanyPreferences retrieved successfully with ID: {Id}", getCompanyPreferencesByIdQuery.Id);
        return ApiResponse<CompanyPreferencesDto>.Ok(result);
    }

    public async Task DeleteCompanyPreferencesAsync(DeleteCompanyPreferencesCommand deleteCompanyPreferencesCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyPreferences is Called with ID: {Id}", deleteCompanyPreferencesCommand.Id);
        if (deleteCompanyPreferencesCommand.Id <= 0)
            throw new ArgumentException("شناسه تنظیمات باید بزرگ‌تر از صفر باشد");
        var companyPreferences = await companyPreferencesRepository.GetCompanyPreferencesByCompanyId(deleteCompanyPreferencesCommand.Id, cancellationToken)
                                 ?? throw new KeyNotFoundException($"تنظیمات با شناسه {deleteCompanyPreferencesCommand.Id} یافت نشد");
        companyPreferencesRepository.Delete(companyPreferences, Guid.NewGuid().ToString());
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyPreferences soft-deleted successfully with ID: {Id}", deleteCompanyPreferencesCommand.Id);
    }

    public async Task<ApiResponse<int>> UpdateCompanyPreferencesAsync(UpdateCompanyPreferencesCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyPreferences is Called with {@UpdateCompanyPreferencesCommand}", command);
        var companyPreferences =
            await companyPreferencesRepository.GetCompanyPreferencesById(command.Id, cancellationToken, true);
        if (companyPreferences is null)
            return ApiResponse<int>.Error(400, "ترجیحات شرکت یافت نشد.");

        var mappedCompanyPreferences = mapper.Map(command, companyPreferences);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyPreferences updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id);
    }
}