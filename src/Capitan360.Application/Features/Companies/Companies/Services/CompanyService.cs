using AutoMapper;
using Capitan360.Application.Common;
using Microsoft.Extensions.Logging;
using Capitan360.Application.Features.Companies.Companies.Commands.Create;
using Capitan360.Application.Features.Companies.Companies.Commands.Delete;
using Capitan360.Application.Features.Companies.Companies.Commands.Update;
using Capitan360.Application.Features.Companies.Companies.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.Companies.Dtos;
using Capitan360.Application.Features.Companies.Companies.Queries.GetAll;
using Capitan360.Application.Features.Companies.Companies.Queries.GetById;
using Capitan360.Domain.Interfaces;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Interfaces.Repositories.ContentTypes;
using Capitan360.Domain.Interfaces.Repositories.PackageTypes;
using Capitan360.Domain.Interfaces.Repositories.Addresses;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Capitan360.Application.Features.Companies.Companies.Services;

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
   ICompanyCommissionsRepository commissionsRepository,
    IAreaRepository areaRepository
    ) : ICompanyService
{
    public async Task<ApiResponse<int>> CreateCompanyAsync(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompany is Called with {@CreateCompanyCommand}", command);

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(command.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await companyRepository.CheckExistCompanyNameAsync(command.Name, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "نام شرکت تکراری است");

        if (await companyRepository.CheckExistCompanyCodeAsync(command.Code, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "کد شرکت تکراری است");
        
        if (command.IsParentCompany && await companyRepository.CheckExistCompanyIsParentAsync(command.CompanyTypeId, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "برای هر نوع بار تنها یک شرکت مادر داریم");

        if (!await areaRepository.CheckExistAreaByIdAndParentId(command.CityId, (int)AreaType.City, command.ProvinceId, cancellationToken) ||
            !await areaRepository.CheckExistAreaByIdAndParentId(command.ProvinceId, (int)AreaType.Province, command.CountryId, cancellationToken) ||
            !await areaRepository.CheckExistAreaByIdAndParentId(command.CountryId, (int)AreaType.Country, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "اطلاعات شهر نامعتبر است");

        var company = mapper.Map<Company>(command);
        if (company is null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var companyId = await companyRepository.CreateCompanyAsync(company, cancellationToken);

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
            new CompanyPreferences()
            {
                CompanyId = companyId,
                ActiveInternationalAirlineCargo = false,
                ActiveInWebServiceSearchEngine = false,
                ActiveIssueDomesticWaybill = false,
                ActiveShowInSearchEngine = false,
                CaptainCargoCode = "",
                CaptainCargoName = "",
                EconomicCode = "",
                ExitAccumulationInTax = false,
                ExitAccumulationMinWeightIsFixed = false,
                ExitDistributionInTax = false,
                ExitExtraDestinationInTax = false,
                ExitExtraSourceInTax = false,
                ExitExtraSourceMinWeightIsFixed = false,
                ExitFareInTax = false,
                ExitPackagingInTax = false,
                ExitPackagingMinWeightIsFixed = false,
                ExitPricingInTax = false,
                ExitPricingMinWeightIsFixed = false,
                ExitRevenue1InTax = false,
                ExitRevenue1MinWeightIsFixed = false,
                ExitRevenue2InTax = false,
                ExitRevenue2MinWeightIsFixed = false,
                ExitRevenue3MinWeightIsFixed = false,
                ExitRevenue3InTax = false,
                ExitStampBillInTax = false,
                ExitStampBillMinWeightIsFixed = false,
                NationalId = "",
                RegistrationId = "",
                Tax = 0,
            }, cancellationToken);

        await companySmsPatternsRepository.CreateCompanySmsPatternsAsync(
        new CompanySmsPatterns
        {
            CompanyId = companyId,
            PatternSmsCancelByCustomerCompany = "",
            ActivePatternSmsCancelByCustomerCompany = false,
            PatternSmsCancelByCustomerReceiver = "",
            ActivePatternSmsCancelByCustomerReceiver = false,
            PatternSmsCancelByCustomerSender = "",
            ActivePatternSmsCancelByCustomerSender = false,
            PatternSmsCancelReceiver = "",
            ActivePatternSmsCancelReceiver = false,
            PatternSmsCancelSender = "",
            ActivePatternSmsCancelSender = false,
            PatternSmsDeliverReceiver = "",
            ActivePatternSmsDeliverReceiver = false,
            PatternSmsDeliverSender = "",
            ActivePatternSmsDeliverSender = false,
            PatternSmsIssueCompany = "",
            ActivePatternSmsIssueCompany = false,
            PatternSmsIssueReceiver = "",
            ActivePatternSmsIssueReceiver = false,
            PatternSmsIssueSender = "",
            ActivePatternSmsIssueSender = false,
            PatternSmsManifestReceiver = "",
            ActivePatternSmsManifestReceiver = false,
            PatternSmsManifestSender = "",
            ActivePatternSmsManifestSender = false,
            PatternSmsPackageInCompanyReceiver = "",
            ActivePatternSmsPackageInCompanyReceiver = false,
            PatternSmsPackageInCompanySender = "",
            ActivePatternSmsPackageInCompanySender = false,
            PatternSmsReceivedInReceiverCompanyReceiver = "",
            ActivePatternSmsReceivedInReceiverCompanyReceiver = false,
            PatternSmsReceivedInReceiverCompanySender = "",
            ActivePatternSmsReceivedInReceiverCompanySender = false,
            PatternSmsSendManifestReceiverCompany = "",
            ActivePatternSmsSendManifestReceiverCompany = false,
            PatternSmsSendReceiverPeakReceiver = "",
            ActivePatternSmsSendReceiverPeakReceiver = false,
            PatternSmsSendReceiverPeakSender = "",
            ActivePatternSmsSendReceiverPeakSender = false,
            PatternSmsSendSenderPeakReceiver = "",
            ActivePatternSmsSendSenderPeakReceiver = false,
            PatternSmsSendSenderPeakSender = "",
            ActivePatternSmsSendSenderPeakSender = false,
            SmsPanelNumber = "",
            SmsPanelPassword = "",
            SmsPanelUserName = "",
        }, cancellationToken);

        await commissionsRepository.CreateCompanyCommissionsAsync(
            new CompanyCommissions()
            {
                CompanyId = companyId,
                CommissionFromCaptainCargoDesktop = 0,
                CommissionFromCaptainCargoPanel = 0,
                CommissionFromCaptainCargoWebService = 0,
                CommissionFromCaptainCargoWebSite = 0,
                CommissionFromCompanyWebService = 0,
                CommissionFromCompanyWebSite = 0,
            }, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("Company created successfully with {@Company}", company);
        return ApiResponse<int>.Created(companyId, "شرکت با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyAsync(DeleteCompanyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompany is Called with {@Id}", command.Id);

        var company = await companyRepository.GetCompanyByIdAsync(command.Id, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        await companyRepository.DeleteCompanyAsync(company.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Company Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "شرکت با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> SetCompanyActivityStatus(UpdateActiveStateCompanyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyActivityStatus Called with {@Id}", command.Id);

        var company = await companyRepository.GetCompanyByIdAsync(command.Id, false, true, cancellationToken);
        if (company is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        company.Active = !company.Active;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Company activity status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت شرکت با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<CompanyDto>> UpdateCompanyAsync(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompany is Called with {@UpdateCompanyCommand}", command);

        var company = await companyRepository.GetCompanyByIdAsync(command.Id, false, true,  cancellationToken);
        if (company is null)
            return ApiResponse<CompanyDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await companyRepository.CheckExistCompanyNameAsync(command.Name, command.Id, cancellationToken))
            return ApiResponse<CompanyDto>.Error(StatusCodes.Status409Conflict, "نام شرکت تکراری است");

        var updatedCompany = mapper.Map(command, company);
        if (updatedCompany == null)
            return ApiResponse<CompanyDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Company updated successfully with {@UpdateCompanyCommand}", command);

        var updatedCompanyDto = mapper.Map<CompanyDto>(updatedCompany);
        if (updatedCompanyDto == null)
            return ApiResponse<CompanyDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<CompanyDto>.Updated(updatedCompanyDto, "شرکت با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyDto>>> GetAllCompanies(GetAllCompanyQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanies is Called");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (query.CompanyId != 0 && query.CompanyTypeId != 0)
        {
            if (!user.IsSuperAdmin() && !user.IsSuperManager(query.CompanyTypeId) && !user.IsManager(query.CompanyId))
                return ApiResponse<PagedResult<CompanyDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId != 0 && query.CompanyTypeId == 0)
        {
            var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
            if (company is null)
                return ApiResponse<PagedResult<CompanyDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<PagedResult<CompanyDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId == 0 && query.CompanyTypeId != 0)
        {
            if (!user.IsSuperAdmin() && !user.IsSuperManager(query.CompanyTypeId))
                return ApiResponse<PagedResult<CompanyDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId == 0 && query.CompanyTypeId == 0)
        {
            if (!user.IsSuperAdmin())
                return ApiResponse<PagedResult<CompanyDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }

        var (companies, totalCount) = await companyRepository.GetAllCompaniesAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyId,
            query.CompanyTypeId,
            query.CityId,
            query.IsParentCompany,
            query.Active,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);

        var companyDtos = mapper.Map<IReadOnlyList<CompanyDto>>(companies) ?? Array.Empty<CompanyDto>();
        if (companyDtos == null)
            return ApiResponse<PagedResult<CompanyDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} companies", companyDtos.Count);

        var data = new PagedResult<CompanyDto>(companyDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyDto>>.Ok(data, "شرکت‌ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<CompanyDto>> GetCompanyByIdAsync(GetCompanyByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyById is Called with {@Id}", query.Id);

        var company = await companyRepository.GetCompanyByIdAsync(query.Id, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var result = mapper.Map<CompanyDto>(company);
        if (result == null)
            return ApiResponse<CompanyDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Company retrieved successfully with {@Id}", query.Id);
        return ApiResponse<CompanyDto>.Ok(result, "شرکت با موفقیت دریافت شد");
    }
}