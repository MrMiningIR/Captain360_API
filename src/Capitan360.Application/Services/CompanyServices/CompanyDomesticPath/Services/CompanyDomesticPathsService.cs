using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.CreateCompanyDomesticPath;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.DeleteCompanyDomesticPath;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.UpdateCompanyDomesticPath;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Queries.GetAllCompanyDomesticPaths;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Queries.GetCompanyDomesticPathById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Services;

public class CompanyDomesticPathsService(
    ILogger<CompanyDomesticPathsService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyDomesticPathsRepository companyDomesticPathsRepository)
    : ICompanyDomesticPathsService
{


    public async Task<ApiResponse<int>> CreateCompanyDomesticPathAsync(CreateCompanyDomesticPathCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyDomesticPath is Called with {@CreateCompanyDomesticPathCommand}", command);

        if (command == null)
            return ApiResponse<int>.Error(400, "ورودی ایجاد مسیر داخلی شرکت نمی‌تواند null باشد");

        var exist = await companyDomesticPathsRepository.CheckExistPath(
            command.SourceCityId, command.DestinationCityId, command.CompanyId, cancellationToken);

        if (exist)
            return ApiResponse<int>.Error(409, "مسیر مشابه وجود دارد");

        var companyDomesticPath = mapper.Map<CompanyDomesticPaths>(command);
        if (companyDomesticPath == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        var companyDomesticPathId = await companyDomesticPathsRepository.CreateCompanyDomesticPathAsync(companyDomesticPath, cancellationToken);
        logger.LogInformation("CompanyDomesticPath created successfully with ID: {CompanyDomesticPathId}", companyDomesticPathId);
        return ApiResponse<int>.Created(companyDomesticPathId, "CompanyDomesticPath created successfully");
    }

    public async Task<ApiResponse<PagedResult<CompanyDomesticPathDto>>> GetAllCompanyDomesticPaths(
        GetAllCompanyDomesticPathsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyDomesticPaths is Called");
        if (query.PageSize <= 0 || query.PageNumber <= 0)
            return ApiResponse<PagedResult<CompanyDomesticPathDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (companyDomesticPaths, totalCount) = await companyDomesticPathsRepository.GetMatchingAllCompanyDomesticPaths(
            query.SearchPhrase, query.CompanyId, query.PageSize, query.PageNumber, query.Status, query.SortBy, query.SourceCountryId, query.SourceProvinceId, query.SourceCityId
            , query.DestinationCountryId, query.DestinationProvinceId, query.DestinationCityId,
            query.SortDirection, cancellationToken);

        var companyDomesticPathDtos = mapper.Map<IReadOnlyList<CompanyDomesticPathDto>>(companyDomesticPaths) ?? Array.Empty<CompanyDomesticPathDto>();

        logger.LogInformation("Retrieved {Count} company domestic paths", companyDomesticPathDtos.Count);

        var data = new PagedResult<CompanyDomesticPathDto>(companyDomesticPathDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyDomesticPathDto>>.Ok(data, "Company domestic paths retrieved successfully");
    }

    public async Task<ApiResponse<CompanyDomesticPathDto>> GetCompanyDomesticPathByIdAsync(
        GetCompanyDomesticPathByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyDomesticPathById is Called with ID: {Id}", query.Id);
        if (query.Id <= 0)
            return ApiResponse<CompanyDomesticPathDto>.Error(400, "شناسه مسیر داخلی شرکت باید بزرگ‌تر از صفر باشد");

        var companyDomesticPath = await companyDomesticPathsRepository.GetCompanyDomesticPathById(query.Id, cancellationToken);
        if (companyDomesticPath is null)
            return ApiResponse<CompanyDomesticPathDto>.Error(404, $"مسیر داخلی شرکت با شناسه {query.Id} یافت نشد");

        var result = mapper.Map<CompanyDomesticPathDto>(companyDomesticPath);
        logger.LogInformation("CompanyDomesticPath retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanyDomesticPathDto>.Ok(result, "CompanyDomesticPath retrieved successfully");
    }

    public async Task<ApiResponse<object>> DeleteCompanyDomesticPathAsync(
        DeleteCompanyDomesticPathCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyDomesticPath is Called with ID: {Id}", command.Id);
        if (command.Id <= 0)
            return ApiResponse<object>.Error(400, "شناسه مسیر داخلی شرکت باید بزرگ‌تر از صفر باشد");

        var companyDomesticPath = await companyDomesticPathsRepository.GetCompanyDomesticPathById(command.Id, cancellationToken);
        if (companyDomesticPath is null)
            return ApiResponse<object>.Error(404, $"مسیر داخلی شرکت با شناسه {command.Id} یافت نشد");

        companyDomesticPathsRepository.Delete(companyDomesticPath);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyDomesticPath soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<object>.Deleted("CompanyDomesticPath deleted successfully");
    }

    public async Task<ApiResponse<int>> UpdateCompanyDomesticPathAsync(
        UpdateCompanyDomesticPathCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyDomesticPath is Called with {@UpdateCompanyDomesticPathCommand}", command);
        if (command.Id <= 0)
            return ApiResponse<int>.Error(400, "شناسه مسیر داخلی شرکت باید بزرگ‌تر از صفر باشد یا ورودی نامعتبر است");

        var companyDomesticPath = await companyDomesticPathsRepository.GetCompanyDomesticPathById(command.Id, cancellationToken, true);
        if (companyDomesticPath is null)
            return ApiResponse<int>.Error(404, $"مسیر داخلی شرکت با شناسه {command.Id} یافت نشد");

        var updatedCompanyDomesticPath = mapper.Map(command, companyDomesticPath);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyDomesticPath updated successfully with ID: {Id}", command.Id);

        var updatedCompanyDomesticPathDto = mapper.Map<CompanyDomesticPathDto>(updatedCompanyDomesticPath);
        return ApiResponse<int>.Ok(command.Id);
    }
}


