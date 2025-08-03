using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.CreateCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.DeleteCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.UpdateCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Queries.GetAllCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Queries.GetCompanySmsPatternsById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Repositories.CompanyRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Services;

public class CompanySmsPatternsService(
    ILogger<CompanySmsPatternsService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanySmsPatternsRepository companySmsPatternsRepository
) : ICompanySmsPatternsService
{
    public async Task<ApiResponse<int>> CreateCompanySmsPatternsAsync(CreateCompanySmsPatternsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanySmsPatterns is Called with {@CreateCompanySmsPatternsCommand}", command);
        var companySmsPatterns = mapper.Map<Domain.Entities.CompanyEntity.CompanySmsPatterns>(command);
        if (companySmsPatterns is null)
            return ApiResponse<int>.Error(400, "خطا در عملیات تبدیل");
        var companySmsPatternsId = await companySmsPatternsRepository.CreateCompanySmsPatternsAsync(companySmsPatterns, cancellationToken);
        logger.LogInformation("CompanySmsPatterns created successfully with ID: {CompanySmsPatternsId}", companySmsPatternsId);
        return ApiResponse<int>.Ok(companySmsPatternsId);
    }

    public async Task<ApiResponse<PagedResult<CompanySmsPatternsDto>>> GetAllCompanySmsPatterns(GetAllCompanySmsPatternsQuery allCompanySmsPatternsQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanySmsPatterns is Called");
        var (companySmsPatterns, totalCount) = await companySmsPatternsRepository.GetMatchingAllCompanySmsPatterns(
            allCompanySmsPatternsQuery.SearchPhrase,
            allCompanySmsPatternsQuery.PageSize,
            allCompanySmsPatternsQuery.PageNumber,
            allCompanySmsPatternsQuery.SortBy,
            allCompanySmsPatternsQuery.SortDirection,
            cancellationToken);
        var companySmsPatternsDto = mapper.Map<IReadOnlyList<CompanySmsPatternsDto>>(companySmsPatterns);
        if (companySmsPatternsDto is null)
            return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Error(400, "خطا در عملیات تبدیل");
        var data = new PagedResult<CompanySmsPatternsDto>(companySmsPatternsDto, totalCount, allCompanySmsPatternsQuery.PageSize, allCompanySmsPatternsQuery.PageNumber);
        logger.LogInformation("Retrieved {Count} company SMS patterns", companySmsPatternsDto.Count);

        return ApiResponse<PagedResult<CompanySmsPatternsDto>>.Ok(data, "Companies retrieved successfully");
    }

    public async Task<ApiResponse<CompanySmsPatternsDto>> GetCompanySmsPatternsByIdAsync(GetCompanySmsPatternsByIdQuery getCompanySmsPatternsByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanySmsPatternsById is Called with ID: {Id}", getCompanySmsPatternsByIdQuery.Id);

        var companySmsPatterns = await companySmsPatternsRepository.GetCompanySmsPatternsById(getCompanySmsPatternsByIdQuery.Id, false, cancellationToken);
        if (companySmsPatterns is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(400, $"الگو با شناسه {getCompanySmsPatternsByIdQuery.Id} یافت نشد");
        var mappedCompanySmsPattern = mapper.Map<CompanySmsPatternsDto>(companySmsPatterns);
        if (mappedCompanySmsPattern is null)
            return ApiResponse<CompanySmsPatternsDto>.Error(400, "خطا در عملیات تبدیل");

        logger.LogInformation("CompanySmsPatterns retrieved successfully with ID: {Id}", getCompanySmsPatternsByIdQuery.Id);
        return ApiResponse<CompanySmsPatternsDto>.Ok(mappedCompanySmsPattern);
    }

    public async Task<ApiResponse<int>> DeleteCompanySmsPatternsAsync(DeleteCompanySmsPatternsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanySmsPatterns is Called with ID: {Id}", command.Id);

        var companySmsPatterns = await companySmsPatternsRepository.GetCompanySmsPatternsById(command.Id, true, cancellationToken);
        if (companySmsPatterns is null)
            return ApiResponse<int>.Error(400, $"الگو با شناسه {command.Id} یافت نشد");
        companySmsPatternsRepository.Delete(companySmsPatterns, Guid.NewGuid().ToString());
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanySmsPatterns soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id);
    }

    public async Task<ApiResponse<int>> UpdateCompanySmsPatternsAsync(UpdateCompanySmsPatternsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanySmsPatterns is Called with {@UpdateCompanySmsPatternsCommand}", command);
        var companySmsPatterns = await companySmsPatternsRepository.GetCompanySmsPatternsById(command.Id, true, cancellationToken);
        if (companySmsPatterns is null)
            return ApiResponse<int>.Error(400, $"الگو با شناسه {command.Id} یافت نشد");
        var mappedCompanySmsPattern = mapper.Map(command, companySmsPatterns);
        if (mappedCompanySmsPattern is null)
            return ApiResponse<int>.Error(400, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanySmsPatterns updated successfully with ID: {Id}", command.Id);

        return ApiResponse<int>.Ok(command.Id);
    }
}