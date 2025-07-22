using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.CreateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.DeleteCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.UpdateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Queries.GetAllCompanyUris;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Queries.GetCompanyUriById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Repositories.CompanyUriRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanyUri.Services;

public class CompanyUriService(
    ILogger<CompanyUriService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyUriRepository companyUriRepository
) : ICompanyUriService
{
    public async Task<ApiResponse<int>> CreateCompanyUriAsync(CreateCompanyUriCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyUri is Called with {@CreateCompanyUriCommand}", command);

        if (string.IsNullOrEmpty(command.Uri))
            return ApiResponse<int>.Error(400, "ورودی ایجاد URI شرکت نمی‌تواند null باشد");

        var exist = await companyUriRepository.CheckExistUri(command.Uri, command.CompanyId, cancellationToken);

        if (exist)
            return ApiResponse<int>.Error(400, "Uri مشابه وجود دارد");

        var companyUri = mapper.Map<Domain.Entities.CompanyEntity.CompanyUri>(command);
        if (companyUri == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        var companyUriId = await companyUriRepository.CreateCompanyUriAsync(companyUri, cancellationToken);
        logger.LogInformation("CompanyUri created successfully with ID: {CompanyUriId}", companyUriId);
        return ApiResponse<int>.Ok(companyUriId, "CompanyUri created successfully");
    }

    public async Task<ApiResponse<PagedResult<CompanyUriDto>>> GetAllCompanyUris(GetAllCompanyUrisQuery allCompanyUrisQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyUris is Called");
        if (allCompanyUrisQuery.PageSize <= 0 || allCompanyUrisQuery.PageNumber <= 0)
            return ApiResponse<PagedResult<CompanyUriDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (companyUris, totalCount) = await companyUriRepository.GetMatchingAllCompanyUris(
            allCompanyUrisQuery.SearchPhrase,
            allCompanyUrisQuery.CompanyId,
            allCompanyUrisQuery.Active,
            allCompanyUrisQuery.PageSize,
            allCompanyUrisQuery.PageNumber,
            allCompanyUrisQuery.SortBy,
            allCompanyUrisQuery.SortDirection,
            cancellationToken);
        var companyUriDtos = mapper.Map<IReadOnlyList<CompanyUriDto>>(companyUris) ?? Array.Empty<CompanyUriDto>();

        logger.LogInformation("Retrieved {Count} company URIs", companyUriDtos.Count);

        var data = new PagedResult<CompanyUriDto>(companyUriDtos, totalCount, allCompanyUrisQuery.PageSize, allCompanyUrisQuery.PageNumber);
        return ApiResponse<PagedResult<CompanyUriDto>>.Ok(data, "Company URIs retrieved successfully");
    }

    public async Task<ApiResponse<CompanyUriDto>> GetCompanyUriByIdAsync(GetCompanyUriByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyUriById is Called with ID: {Id}", query.Id);
        if (query.Id <= 0)
            return ApiResponse<CompanyUriDto>.Error(400, "شناسه URI شرکت باید بزرگ‌تر از صفر باشد");

        var companyUri = await companyUriRepository.GetCompanyUriById(query.Id, cancellationToken);
        if (companyUri is null)
            return ApiResponse<CompanyUriDto>.Error(404, $"URI شرکت با شناسه {query.Id} یافت نشد");

        var result = mapper.Map<CompanyUriDto>(companyUri);
        logger.LogInformation("CompanyUri retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanyUriDto>.Ok(result, "CompanyUri retrieved successfully");
    }

    public async Task<ApiResponse<object>> DeleteCompanyUriAsync(DeleteCompanyUriCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyUri is Called with ID: {Id}", command.Id);
        if (command.Id <= 0)
            return ApiResponse<object>.Error(400, "شناسه URI شرکت باید بزرگ‌تر از صفر باشد");

        var companyUri = await companyUriRepository.GetCompanyUriById(command.Id, cancellationToken);
        if (companyUri is null)
            return ApiResponse<object>.Error(404, $"URI شرکت با شناسه {command.Id} یافت نشد");

        companyUriRepository.Delete(companyUri);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyUri soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<object>.Deleted("CompanyUri deleted successfully");
    }

    public async Task<ApiResponse<int>> UpdateCompanyUriAsync(UpdateCompanyUriCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyUri is Called with {@UpdateCompanyUriCommand}", command);
        if (command.Id <= 0)
            return ApiResponse<int>.Error(400, "شناسه URI شرکت باید بزرگ‌تر از صفر باشد یا ورودی نامعتبر است");

        var companyUri = await companyUriRepository.GetCompanyUriById(command.Id, cancellationToken);
        if (companyUri is null)
            return ApiResponse<int>.Error(404, $"URI شرکت با شناسه {command.Id} یافت نشد");

        var updatedCompanyUri = mapper.Map(command, companyUri);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyUri updated successfully with ID: {Id}", command.Id);

        var updatedCompanyUriDto = mapper.Map<CompanyUriDto>(updatedCompanyUri);
        return ApiResponse<int>.Ok(command.Id);
    }
}