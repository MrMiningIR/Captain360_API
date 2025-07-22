using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyType.Commands.CreateCompanyType;
using Capitan360.Application.Services.CompanyServices.CompanyType.Commands.DeleteCompanyType;
using Capitan360.Application.Services.CompanyServices.CompanyType.Commands.UpdateCompanyType;
using Capitan360.Application.Services.CompanyServices.CompanyType.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyType.Queries.GetAllCompanyTypes;
using Capitan360.Application.Services.CompanyServices.CompanyType.Queries.GetCompanyTypeById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Exceptions;
using Capitan360.Domain.Repositories.CompanyRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanyType.Services;

public class CompanyTypeService(
    ILogger<CompanyTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyTypeRepository companyTypeRepository
) : ICompanyTypeService
{
    public async Task<int> CreateCompanyTypeAsync(CreateCompanyTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyType is Called with {@CreateCompanyTypeCommand}", command);
        var companyType = mapper.Map<Domain.Entities.CompanyEntity.CompanyType>(command) ?? throw new ArgumentNullException(nameof(command));
        var companyTypeId = await companyTypeRepository.CreateCompanyTypeAsync(companyType, Guid.NewGuid().ToString(), cancellationToken);
        logger.LogInformation("CompanyType created successfully with ID: {CompanyTypeId}", companyTypeId);
        return companyTypeId;
    }

    public async Task<ApiResponse<PagedResult<CompanyTypeDto>>> GetAllCompanyTypes(GetAllCompanyTypesQuery allCompanyTypesQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyTypes is Called");
        if (allCompanyTypesQuery.PageSize <= 0 || allCompanyTypesQuery.PageNumber <= 0)
            return ApiResponse<PagedResult<CompanyTypeDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (companyTypes, totalCount) = await companyTypeRepository.GetMatchingAllCompanyTypes(
            allCompanyTypesQuery.SearchPhrase,
            allCompanyTypesQuery.PageSize,
            allCompanyTypesQuery.PageNumber,
            allCompanyTypesQuery.SortBy,
            allCompanyTypesQuery.SortDirection,
            cancellationToken);
        var companyTypeDto = mapper.Map<IReadOnlyList<CompanyTypeDto>>(companyTypes) ?? Array.Empty<CompanyTypeDto>();
        logger.LogInformation("Retrieved {Count} company types", companyTypeDto.Count);

        var data = new PagedResult<CompanyTypeDto>(companyTypeDto, totalCount, allCompanyTypesQuery.PageSize, allCompanyTypesQuery.PageNumber);

        return ApiResponse<PagedResult<CompanyTypeDto>>.Ok(data, "CompaniesType retrieved successfully");
    }

    public async Task<ApiResponse<CompanyTypeDto>> GetCompanyTypeByIdAsync(GetCompanyTypeByIdQuery getCompanyTypeByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyTypeById is Called with ID: {Id}", getCompanyTypeByIdQuery.Id);
        if (getCompanyTypeByIdQuery.Id <= 0)
            return ApiResponse<CompanyTypeDto>.Error(400, "شناسه نوع شرکت باید بزرگ‌تر از صفر باشد");

        var companyType = await companyTypeRepository.GetCompanyTypeById(getCompanyTypeByIdQuery.Id, cancellationToken);

        if (companyType is null)
            return ApiResponse<CompanyTypeDto>.Error(404, $"نوع شرکت با شناسه {getCompanyTypeByIdQuery.Id} یافت نشد");

        var result = mapper.Map<CompanyTypeDto>(companyType);
        logger.LogInformation("CompanyType retrieved successfully with ID: {Id}", getCompanyTypeByIdQuery.Id);
        return ApiResponse<CompanyTypeDto>.Ok(result, "CompanyType retrieved successfully");
    }

    public async Task DeleteCompanyTypeAsync(DeleteCompanyTypeCommand deleteCompanyTypeCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyType is Called with ID: {Id}", deleteCompanyTypeCommand.Id);
        if (deleteCompanyTypeCommand.Id <= 0)
            throw new ArgumentException("شناسه نوع شرکت باید بزرگ‌تر از صفر باشد");
        var companyType = await companyTypeRepository.GetCompanyTypeById(deleteCompanyTypeCommand.Id, cancellationToken)
                          ?? throw new KeyNotFoundException($"نوع شرکت با شناسه {deleteCompanyTypeCommand.Id} یافت نشد");
        companyTypeRepository.Delete(companyType, Guid.NewGuid().ToString());
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyType soft-deleted successfully with ID: {Id}", deleteCompanyTypeCommand.Id);
    }

    public async Task UpdateCompanyTypeAsync(UpdateCompanyTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyType is Called with {@UpdateCompanyTypeCommand}", command);
        var companyType = await companyTypeRepository.GetCompanyTypeById(command.Id, cancellationToken)
                          ?? throw new NotFoundException($"نوع شرکت با شناسه {command.Id} یافت نشد");
        mapper.Map(command, companyType);
        companyTypeRepository.UpdateShadows(companyType, Guid.NewGuid().ToString());
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyType updated successfully with ID: {Id}", command.Id);
    }
}