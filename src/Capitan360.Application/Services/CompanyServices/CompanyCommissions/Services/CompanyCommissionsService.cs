using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.CreateCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.DeleteCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.UpdateCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetAllCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetCompanyCommissionsById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Repositories.CompanyRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.Services;

public class CompanyCommissionsService(
    ILogger<CompanyCommissionsService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyCommissionsRepository companyCommissionsRepository
) : ICompanyCommissionsService
{
    public async Task<ApiResponse<int>> CreateCompanyCommissionsAsync(CreateCompanyCommissionsCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyCommissions is Called with {@CreateCompanyCommissionsCommand}", command);
        var companyCommissions = mapper.Map<Domain.Entities.CompanyEntity.CompanyCommissions>(command);
        if (companyCommissions is null)
            return ApiResponse<int>.Error(400, "خطا در عملیات تبدیل");

        var companyCommissionsId = await companyCommissionsRepository.CreateCompanyCommissionsAsync(companyCommissions, Guid.NewGuid().ToString(), cancellationToken);
        logger.LogInformation("CompanyCommissions created successfully with ID: {CompanyCommissionsId}", companyCommissionsId);
        return ApiResponse<int>.Ok(companyCommissionsId);
    }

    public async Task<ApiResponse<PagedResult<CompanyCommissionsDto>>> GetAllCompanyCommissions(
        GetAllCompanyCommissionsQuery allCompanyCommissionsQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyCommissions is Called");
        var (companyCommissions, totalCount) = await companyCommissionsRepository.GetAllCompanyCommissionsAsync(
            allCompanyCommissionsQuery.SearchPhrase,
            allCompanyCommissionsQuery.PageSize,
            allCompanyCommissionsQuery.PageNumber,
            allCompanyCommissionsQuery.SortBy,
            allCompanyCommissionsQuery.SortDirection,
            cancellationToken);
        var companyCommissionsDto = mapper.Map<IReadOnlyList<CompanyCommissionsDto>>(companyCommissions);
        if (companyCommissionsDto is null)
            return ApiResponse<PagedResult<CompanyCommissionsDto>>.Error(400, "خطا در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company commissions", companyCommissionsDto.Count);

        var data = new PagedResult<CompanyCommissionsDto>(companyCommissionsDto, totalCount, allCompanyCommissionsQuery.PageSize, allCompanyCommissionsQuery.PageNumber);
        return ApiResponse<PagedResult<CompanyCommissionsDto>>.Ok(data);
    }

    public async Task<ApiResponse<CompanyCommissionsDto>> GetCompanyCommissionsByIdAsync(
        GetCompanyCommissionsByIdQuery getCompanyCommissionsByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyCommissionsById is Called with ID: {Id}", getCompanyCommissionsByIdQuery.Id);

        var companyCommissions =
            await companyCommissionsRepository.GetCompanyCommissionsByIdAsync(getCompanyCommissionsByIdQuery.Id, false,
                cancellationToken);
        if (companyCommissions is null)
            return ApiResponse<CompanyCommissionsDto>.Error(400, $"کمیسیون با شناسه {getCompanyCommissionsByIdQuery.Id} یافت نشد");

        var companyCommissionDto = mapper.Map<CompanyCommissionsDto>(companyCommissions);
        if (companyCommissionDto is null)
            return ApiResponse<CompanyCommissionsDto>.Error(400, "خطا در عملیات تبدیل");

        logger.LogInformation("CompanyCommissions retrieved successfully with ID: {Id}", getCompanyCommissionsByIdQuery.Id);
        return ApiResponse<CompanyCommissionsDto>.Ok(companyCommissionDto);
    }

    public async Task<ApiResponse<int>> DeleteCompanyCommissionsAsync(DeleteCompanyCommissionsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyCommissions is Called with ID: {Id}", command.Id);

        var companyCommissions =
            await companyCommissionsRepository.GetCompanyCommissionsByIdAsync(command.Id, true,
                cancellationToken);
        if (companyCommissions is null)
            return ApiResponse<int>.Error(400, $"کمیسیون با شناسه {command.Id} یافت نشد");
        companyCommissionsRepository.Delete(companyCommissions);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyCommissions soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id);
    }

    public async Task<ApiResponse<int>> UpdateCompanyCommissionsAsync(UpdateCompanyCommissionsCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyCommissions is Called with {@UpdateCompanyCommissionsCommand}", command);
        var companyCommissions =
            await companyCommissionsRepository.GetCompanyCommissionsByIdAsync(command.Id, true, cancellationToken);
        if (companyCommissions is null)
            return ApiResponse<int>.Error(400, $"کمیسیون با شناسه {command.Id} یافت نشد");


        var companyCommission = mapper.Map(command, companyCommissions);
        if (companyCommission is null)
            return ApiResponse<int>.Error(400, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyCommissions updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id);
    }
}