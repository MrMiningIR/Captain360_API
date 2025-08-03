using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.Company.Commands.CreateCompany;
using Capitan360.Application.Services.CompanyServices.Company.Commands.DeleteCompany;
using Capitan360.Application.Services.CompanyServices.Company.Commands.UpdateActiveStateCompany;
using Capitan360.Application.Services.CompanyServices.Company.Commands.UpdateCompany;
using Capitan360.Application.Services.CompanyServices.Company.Dtos;
using Capitan360.Application.Services.CompanyServices.Company.Queries.GetAllCompanies;
using Capitan360.Application.Services.CompanyServices.Company.Queries.GetCompanyById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Domain.Repositories.ContentRepo;
using Capitan360.Domain.Repositories.PackageRepo;
using Capitan360.Domain.Repositories.PackageTypeRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.Company.Services;

public class CompanyService(
   ILogger<CompanyService> logger,
    IMapper mapper,
   IUnitOfWork unitOfWork,
   IUserContext userContext,
   ICompanyRepository companyRepository,
   ICompanyPackageTypeRepository companyPackageTypesRepository,
   ICompanyContentTypeRepository companyContentTypeRepository,
   IContentTypeRepository contentTypeRepository,
   IPackageTypeRepository packageTypeRepository,
   ICompanyPreferencesRepository preferencesRepository

    ) : ICompanyService
{
    public async Task<ApiResponse<int>> CreateCompanyAsync(CreateCompanyCommand companyCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompany is Called with {@CreateCompanyCommand}", companyCommand);
        if (string.IsNullOrEmpty(companyCommand.Name))
            return ApiResponse<int>.Error(400, "ورودی ایجاد شرکت نمی‌تواند null باشد");

        var companyEntity = mapper.Map<Domain.Entities.CompanyEntity.Company>(companyCommand);
        if (companyEntity == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var companyId = await companyRepository.CreateCompanyAsync(companyEntity, cancellationToken);

        var relatedContentTypes = await contentTypeRepository.GetContentTypesByCompanyTypeId(companyCommand.CompanyTypeId, cancellationToken);
        var relatedPackageTypes = await packageTypeRepository.GetPackageTypesByCompanyTypeId(companyCommand.CompanyTypeId, cancellationToken);

        if (relatedContentTypes.Any())
        {
            await companyContentTypeRepository.AddContentTypesToCompanyContentType(relatedContentTypes, companyId, cancellationToken);
        }

        if (relatedPackageTypes.Any())
        {
            await companyPackageTypesRepository.AddPackageTypesToCompanyPackageType(relatedPackageTypes, companyId, cancellationToken);
        }

        await preferencesRepository.CreateCompanyPreferencesAsync(
            new Domain.Entities.CompanyEntity.CompanyPreferences()
            {
                CompanyId = companyId
            }, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("Company created successfully with ID: {CompanyId}", companyId);
        return ApiResponse<int>.Created(companyId, "Company created successfully");
    }

    public async Task<ApiResponse<PagedResult<CompanyDto>>> GetAllCompanies(GetAllCompanyQuery allCompanyQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanies is Called");
        if (allCompanyQuery.PageSize <= 0 || allCompanyQuery.PageNumber <= 0)
            return ApiResponse<PagedResult<CompanyDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (companies, totalCount) = await companyRepository.GetMatchingAllCompanies(
            allCompanyQuery.SearchPhrase,
            allCompanyQuery.CompanyTypeId,
            allCompanyQuery.PageSize,
            allCompanyQuery.PageNumber,
            allCompanyQuery.SortBy,
            allCompanyQuery.SortDirection,
            cancellationToken);

        var companyDtos = mapper.Map<IReadOnlyList<CompanyDto>>(companies) ?? Array.Empty<CompanyDto>();
        logger.LogInformation("Retrieved {Count} companies", companyDtos.Count);

        var data = new PagedResult<CompanyDto>(companyDtos, totalCount, allCompanyQuery.PageSize, allCompanyQuery.PageNumber);
        return ApiResponse<PagedResult<CompanyDto>>.Ok(data, "Companies retrieved successfully");
    }

    public async Task<ApiResponse<CompanyDto>> GetCompanyByIdAsync(GetCompanyByIdQuery getCompanyByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyById is Called with ID: {Id}", getCompanyByIdQuery.Id);
        if (getCompanyByIdQuery.Id <= 0)
            return ApiResponse<CompanyDto>.Error(400, "شناسه شرکت باید بزرگ‌تر از صفر باشد");

        var company = await companyRepository.GetCompanyById(getCompanyByIdQuery.Id, cancellationToken, getCompanyByIdQuery.Track, getCompanyByIdQuery.UserCompanyTypeId);
        if (company is null)
            return ApiResponse<CompanyDto>.Error(404, $"شرکت با شناسه {getCompanyByIdQuery.Id} یافت نشد");

        var result = mapper.Map<CompanyDto>(company);
        logger.LogInformation("Company retrieved successfully with ID: {Id}", getCompanyByIdQuery.Id);
        return ApiResponse<CompanyDto>.Ok(result, "Company retrieved successfully");
    }

    public async Task<ApiResponse<int>> DeleteCompanyAsync(DeleteCompanyCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompany is Called with ID: {Id}", command.Id);

        var company = await companyRepository.GetCompanyById(command.Id, cancellationToken, tracked: true);
        if (company is null)
            return ApiResponse<int>.Error(404, $"شرکت با شناسه {command.Id} یافت نشد");

        companyRepository.Delete(company);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Company soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id);
    }

    public async Task<ApiResponse<int>> UpdateCompanyAsync(UpdateCompanyCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompany is Called with {@UpdateCompanyCommand}", command);

        var company = await companyRepository.GetCompanyById(command.Id, cancellationToken, true, command.UserCompanyTypeId);
        if (company is null)
            return ApiResponse<int>.Error(404, $"شرکت با شناسه {command.Id} یافت نشد");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var updatedCompany = mapper.Map(command, company);

        if (command.UpdateRelatedThings)
        {
            // Delete
            var existAnyCompanyContentRecord = await companyContentTypeRepository.CheckExistAnyItem(company.Id, cancellationToken);

            if (existAnyCompanyContentRecord)
            {
                await companyContentTypeRepository.DeleteAllContentsByCompanyId(company.Id, cancellationToken);
            }
            var existAnyCompanyPackageRecord = await companyPackageTypesRepository.CheckExistAnyItem(company.Id, cancellationToken);

            if (existAnyCompanyPackageRecord)
            {
                await companyPackageTypesRepository.DeleteAllPackagesByCompanyId(company.Id, cancellationToken);
            }

            // Add
            var relatedContentTypes = await contentTypeRepository.GetContentTypesByCompanyTypeId(command.CompanyTypeId, cancellationToken);

            if (relatedContentTypes.Any())
            {
                await companyContentTypeRepository.AddContentTypesToCompanyContentType(relatedContentTypes, company.Id,
                    cancellationToken);
            }

            var relatedPackageTypes = await packageTypeRepository.GetPackageTypesByCompanyTypeId(command.CompanyTypeId, cancellationToken);
            if (relatedPackageTypes.Any())
            {
                await companyPackageTypesRepository.AddPackageTypesToCompanyPackageType(relatedPackageTypes, company.Id, cancellationToken);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);
        logger.LogInformation("Company updated successfully with ID: {Id}", command.Id);

        return ApiResponse<int>.Ok(command.Id);
    }

    public async Task<ApiResponse<int>> SetCompanyActivityStatus(UpdateActiveStateCompanyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyActivityStatus Called with {@UpdateActiveStateCompanyCommand}", command);

        var company =
            await companyRepository.GetCompanyById(command.Id, cancellationToken, true, 0);

        if (company is null)
            return ApiResponse<int>.Error(404, $"Company Data was not Found :{command.Id}");

        company.Active = !company.Active;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("SetCompanyActivityStatus Updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id);
    }
}