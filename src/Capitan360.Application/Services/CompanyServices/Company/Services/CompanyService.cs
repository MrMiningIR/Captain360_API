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
using Capitan360.Domain.Repositories.ContentTypeRepo;
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
   ICompanyPreferencesRepository preferencesRepository,
   ICompanySmsPatternsRepository companySmsPatternsRepository,
   ICompanyCommissionsRepository commissionsRepository

    ) : ICompanyService
{
    public async Task<ApiResponse<int>> CreateCompanyAsync(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompany is Called with {@CreateCompanyCommand}", command);

        if (await companyRepository.CheckExistCompanyNameAsync(command.Name, null, cancellationToken))
            return ApiResponse<int>.Error(400, "نام شرکت تکراری است");

        if (await companyRepository.CheckExistCompanyCodeAsync(command.Code, null, cancellationToken))
            return ApiResponse<int>.Error(400, "کد شرکت تکراری است");

        if (command.IsParentCompany && await companyRepository.CheckExistCompanyIsParentCompanyAsync(command.CompanyTypeId, null, cancellationToken))
            return ApiResponse<int>.Error(400, "نوع شرکت تکراری است");

        var companyEntity = mapper.Map<Domain.Entities.CompanyEntity.Company>(command);
        if (companyEntity is null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var companyId = await companyRepository.CreateCompanyAsync(companyEntity, cancellationToken);

        var relatedContentTypes = await contentTypeRepository.GetContentTypesByCompanyTypeIdAsync(command.CompanyTypeId, cancellationToken);
        var relatedPackageTypes = await packageTypeRepository.GetPackageTypesByCompanyTypeIdAsync(command.CompanyTypeId, cancellationToken);

        if (relatedContentTypes.Any())
        {
            await companyContentTypeRepository.AddContentTypesToCompanyContentTypeAsync(relatedContentTypes, companyId, cancellationToken);
        }

        if (relatedPackageTypes.Any())
        {
            await companyPackageTypesRepository.AddPackageTypesToCompanyPackageTypeAsync(relatedPackageTypes, companyId, cancellationToken);
        }

        await preferencesRepository.CreateCompanyPreferencesAsync(
            new Domain.Entities.CompanyEntity.CompanyPreferences()
            {
                CompanyId = companyId
            }, cancellationToken);

        await companySmsPatternsRepository.CreateCompanySmsPatternsAsync(
        new Domain.Entities.CompanyEntity.CompanySmsPatterns
        {
            CompanyId = companyId,
        }, cancellationToken);

        await commissionsRepository.CreateCompanyCommissionsAsync(
            new Domain.Entities.CompanyEntity.CompanyCommissions()
            {
                CompanyId = companyId
            }, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("Company created successfully with ID: {CompanyId}", companyId);
        return ApiResponse<int>.Created(companyId, "شرکت با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyDto>>> GetAllCompanies(GetAllCompanyQuery allCompanyQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanies is Called");

        var (companies, totalCount) = await companyRepository.GetAllCompaniesAsync(
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
        return ApiResponse<PagedResult<CompanyDto>>.Ok(data, "شرکت‌ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<CompanyDto>> GetCompanyByIdAsync(GetCompanyByIdQuery getCompanyByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyById is Called with ID: {Id}", getCompanyByIdQuery.Id);

        var company = await companyRepository.GetCompanyByIdAsync(getCompanyByIdQuery.Id, cancellationToken, getCompanyByIdQuery.Track, getCompanyByIdQuery.UserCompanyTypeId);
        if (company is null)
            return ApiResponse<CompanyDto>.Error(404, $"شرکت نامعتبر است");

        var result = mapper.Map<CompanyDto>(company);
        logger.LogInformation("Company retrieved successfully with ID: {Id}", getCompanyByIdQuery.Id);
        return ApiResponse<CompanyDto>.Ok(result, "شرکت با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyAsync(DeleteCompanyCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompany is Called with ID: {Id}", command.Id);

        var company = await companyRepository.GetCompanyByIdAsync(command.Id, cancellationToken, tracked: true);
        if (company is null)
            return ApiResponse<int>.Error(404, $"شرکت نامعتبر است");

        companyRepository.Delete(company);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Company soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "شرکت با موفقیت حذف شد");
    }

    public async Task<ApiResponse<CompanyDto>> UpdateCompanyAsync(UpdateCompanyCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompany is Called with {@UpdateCompanyCommand}", command);


        // we need UserCompanyTypeId to get access to Super admin or regular users! 

        var company = await companyRepository.GetCompanyByIdAsync(command.Id, cancellationToken, true, command.UserCompanyTypeId);
        if (company is null)
            return ApiResponse<CompanyDto>.Error(400, $"شرکت نامعتبر است");

        if (await companyRepository.CheckExistCompanyNameAsync(command.Name, command.Id, cancellationToken))
            return ApiResponse<CompanyDto>.Error(400, "نام شرکت تکراری است");



        var updatedCompany = mapper.Map(command, company);
        if (updatedCompany == null)
            return ApiResponse<CompanyDto>.Error(400, "خطا در عملیات تبدیل");

        #region Comment
        //if (command.UpdateRelatedThings)
        //{
        //    // Delete
        //    var existAnyCompanyContentRecord = await companyContentTypeRepository.CheckExistAnyItemAsync(company.Id, cancellationToken);

        //    if (existAnyCompanyContentRecord)
        //    {
        //        await companyContentTypeRepository.DeleteAllContentsByCompanyIdAsync(company.Id, cancellationToken);
        //    }
        //    var existAnyCompanyPackageRecord = await companyPackageTypesRepository.CheckExistAnyItemAsync(company.Id, cancellationToken);

        //    if (existAnyCompanyPackageRecord)
        //    {
        //        await companyPackageTypesRepository.DeleteAllPackagesByCompanyIdAsync(company.Id, cancellationToken);
        //    }

        //    // Add
        //    var relatedContentTypes = await contentTypeRepository.GetContentTypesByCompanyTypeIdAsync(command.CompanyTypeId, cancellationToken);

        //    if (relatedContentTypes.Any())
        //    {
        //        await companyContentTypeRepository.AddContentTypesToCompanyContentTypeAsync(relatedContentTypes, company.Id,
        //            cancellationToken);
        //    }

        //    var relatedPackageTypes = await packageTypeRepository.GetPackageTypesByCompanyTypeIdAsync(command.CompanyTypeId, cancellationToken);
        //    if (relatedPackageTypes.Any())
        //    {
        //        await companyPackageTypesRepository.AddPackageTypesToCompanyPackageTypeAsync(relatedPackageTypes, company.Id, cancellationToken);
        //    }
        //} 
        #endregion

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Company updated successfully with ID: {Id}", command.Id);

        var updatedCompanyDto = mapper.Map<CompanyDto>(updatedCompany);
        return ApiResponse<CompanyDto>.Updated(updatedCompanyDto, "شرکت با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<int>> SetCompanyActivityStatus(UpdateActiveStateCompanyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyActivityStatus Called with {@UpdateActiveStateCompanyCommand}", command);

        var company =
            await companyRepository.GetCompanyByIdAsync(command.Id, cancellationToken, true, 0);

        if (company is null)
            return ApiResponse<int>.Error(404, $"شرکت نامعتبر است");

        company.Active = !company.Active;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("SetCompanyActivityStatus Updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت شرکت با موفقیت به‌روزرسانی شد");
    }
}