using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.CreateCompanyInsurance;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.DeleteCompanyInsurance;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.UpdateCompanyInsurance;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Queries.GetAllCompanyInsurances;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.Dtos;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Repositories.CompanyRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Services;

public class CompanyInsuranceService(
    ILogger<CompanyInsuranceService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyInsuranceRepository companyInsuranceRepository,
    ICompanyTypeRepository companyTypeRepository,
    ICompanyRepository companyRepository)
    : ICompanyInsuranceService
{
    public async Task<ApiResponse<int>> CreateCompanyInsuranceAsync(CreateCompanyInsuranceCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyInsurance is Called with {@CreateCompanyInsuranceCommand}", command);

        if (command == null)
            return ApiResponse<int>.Error(400, "ورودی ایجاد بیمه شرکت نمی‌تواند null باشد");

        var exist = await companyInsuranceRepository.CheckExistCompanyInsuranceName(
            command.Name, command.CompanyTypeId, command.CompanyId, cancellationToken);

        if (exist)
            return ApiResponse<int>.Error(409, "نام بیمه مشابه وجود دارد");

        var companyType = await companyTypeRepository.GetCompanyTypeById(command.CompanyTypeId, cancellationToken);
        if (companyType == null)
            return ApiResponse<int>.Error(404, $"نوع شرکت با شناسه {command.CompanyTypeId} یافت نشد");

        var company = await companyRepository.GetCompanyById(command.CompanyId, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(404, $"شرکت با شناسه {command.CompanyId} یافت نشد");

        var companyInsurance = mapper.Map<Domain.Entities.CompanyEntity.CompanyInsurance>(command);
        if (companyInsurance == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        var companyInsuranceId = await companyInsuranceRepository.CreateCompanyInsuranceAsync(companyInsurance, cancellationToken);
        logger.LogInformation("CompanyInsurance created successfully with ID: {CompanyInsuranceId}", companyInsuranceId);
        return ApiResponse<int>.Created(companyInsuranceId, "CompanyInsurance created successfully");
    }

    public async Task<ApiResponse<PagedResult<CompanyInsuranceDto>>> GetAllCompanyInsurances(GetAllCompanyInsurancesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyInsurances is Called");

        if (query.PageSize <= 0 || query.PageNumber <= 0)
            return ApiResponse<PagedResult<CompanyInsuranceDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (companyInsurances, totalCount) = await companyInsuranceRepository.GetMatchingAllCompanyInsurances(
            query.SearchPhrase,
            query.CompanyTypeId,
            query.CompanyId,
            query.Active,
            query.PageSize,
            query.PageNumber,
            query.SortBy,
            query.SortDirection,
            cancellationToken);

        var companyInsuranceDto = mapper.Map<IReadOnlyList<CompanyInsuranceDto>>(companyInsurances) ?? Array.Empty<CompanyInsuranceDto>();
        logger.LogInformation("Retrieved {Count} company insurances", companyInsuranceDto.Count);

        var data = new PagedResult<CompanyInsuranceDto>(companyInsuranceDto, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyInsuranceDto>>.Ok(data, "CompanyInsurances retrieved successfully");
    }

    public async Task<ApiResponse<CompanyInsuranceDto>> GetCompanyInsuranceByIdAsync(GetCompanyInsuranceByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyInsuranceById is Called with ID: {Id}", query.Id);

        if (query.Id <= 0)
            return ApiResponse<CompanyInsuranceDto>.Error(400, "شناسه بیمه شرکت باید بزرگ‌تر از صفر باشد");

        var companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceById(query.Id, cancellationToken);
        if (companyInsurance == null)
            return ApiResponse<CompanyInsuranceDto>.Error(404, $"بیمه شرکت با شناسه {query.Id} یافت نشد");

        var result = mapper.Map<CompanyInsuranceDto>(companyInsurance);
        logger.LogInformation("CompanyInsurance retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanyInsuranceDto>.Ok(result, "CompanyInsurance retrieved successfully");
    }

    public async Task<ApiResponse<object>> DeleteCompanyInsuranceAsync(DeleteCompanyInsuranceCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyInsurance is Called with ID: {Id}", command.Id);

        if (command.Id <= 0)
            return ApiResponse<object>.Error(400, "شناسه بیمه شرکت باید بزرگ‌تر از صفر باشد");

        var companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceById(command.Id, cancellationToken, true);
        if (companyInsurance == null)
            return ApiResponse<object>.Error(404, $"بیمه شرکت با شناسه {command.Id} یافت نشد");

        companyInsuranceRepository.Delete(companyInsurance);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyInsurance soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<object>.Deleted("CompanyInsurance deleted successfully");
    }

    public async Task<ApiResponse<int>> UpdateCompanyInsuranceAsync(UpdateCompanyInsuranceCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyInsurance is Called with {@UpdateCompanyInsuranceCommand}", command);

        if (command == null || command.Id <= 0)
            return ApiResponse<int>.Error(400, "شناسه بیمه شرکت باید بزرگ‌تر از صفر باشد یا ورودی نامعتبر است");

        var companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceById(command.Id, cancellationToken, true);
        if (companyInsurance == null)
            return ApiResponse<int>.Error(404, $"بیمه شرکت با شناسه {command.Id} یافت نشد");

        var updatedCompanyInsurance = mapper.Map(command, companyInsurance);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyInsurance updated successfully with ID: {Id}", command.Id);


        return ApiResponse<int>.Ok(command.Id);
    }
}