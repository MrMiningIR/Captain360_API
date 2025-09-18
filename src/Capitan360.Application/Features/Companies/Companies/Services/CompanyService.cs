using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.Companies.Commands.Create;
using Capitan360.Application.Features.Companies.Companies.Commands.Delete;
using Capitan360.Application.Features.Companies.Companies.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.Companies.Commands.Update;
using Capitan360.Application.Features.Companies.Companies.Dtos;
using Capitan360.Application.Features.Companies.Companies.Queries.GetAll;
using Capitan360.Application.Features.Companies.Companies.Queries.GetById;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Addresses;
using Capitan360.Domain.Repositories.Companies;
using Capitan360.Domain.Repositories.ContentTypes;
using Capitan360.Domain.Repositories.PackageTypes;
using Microsoft.Extensions.Logging;
using Capitan360.Application.Features.Identities.Identities.Services;

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
   ICompanyCommissionsRepository commissionsRepository, IAreaRepository areaRepository

    ) : ICompanyService
{
    public async Task<ApiResponse<int>> CreateCompanyAsync(CreateCompanyCommand createCompanyCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompany is Called with {@CreateCompanyCommand}", createCompanyCommand);

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(createCompanyCommand.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        if (await companyRepository.CheckExistCompanyNameAsync(createCompanyCommand.Name, null, cancellationToken))
            return ApiResponse<int>.Error(400, "نام شرکت تکراری است");

        if (await companyRepository.CheckExistCompanyCodeAsync(createCompanyCommand.Code, null, cancellationToken))
            return ApiResponse<int>.Error(400, "کد شرکت تکراری است");

        if (createCompanyCommand.IsParentCompany && await companyRepository.CheckExistCompanyIsParentAsync(createCompanyCommand.CompanyTypeId, null, cancellationToken))
            return ApiResponse<int>.Error(400, "نوع شرکت تکراری است");

        if (!await areaRepository.CheckExistAreaByIdAndParentId(createCompanyCommand.CityId, (int)AreaType.City, createCompanyCommand.ProvinceId, cancellationToken) ||
            !await areaRepository.CheckExistAreaByIdAndParentId(createCompanyCommand.ProvinceId, (int)AreaType.Province, createCompanyCommand.CountryId, cancellationToken) ||
            !await areaRepository.CheckExistAreaByIdAndParentId(createCompanyCommand.CountryId, (int)AreaType.Country, null, cancellationToken))
            return ApiResponse<int>.Error(400, "اطلاعات شهر نامعتبر است");

        var companyEntity = mapper.Map<Domain.Entities.Companies.Company>(createCompanyCommand);
        if (companyEntity is null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var companyId = await companyRepository.CreateCompanyAsync(companyEntity, cancellationToken);

        var relatedContentTypes = await contentTypeRepository.GetContentTypesByCompanyTypeIdAsync(createCompanyCommand.CompanyTypeId, cancellationToken);
        var relatedPackageTypes = await packageTypeRepository.GetPackageTypesByCompanyTypeIdAsync(createCompanyCommand.CompanyTypeId, cancellationToken);

        if (relatedContentTypes.Any())
        {
            await companyContentTypeRepository.AddContentTypesToCompanyContentTypeAsync(relatedContentTypes, companyId, cancellationToken);
        }

        if (relatedPackageTypes.Any())
        {
            await companyPackageTypesRepository.AddPackageTypesToCompanyPackageTypeAsync(relatedPackageTypes, companyId, cancellationToken);
        }

        await preferencesRepository.CreateCompanyPreferencesAsync(
            new Domain.Entities.Companies.CompanyPreferences()
            {
                CompanyId = companyId,
                ActiveInternationalAirlineCargo = false,
                ActiveInWebServiceSearchEngine = false,
                ActiveIssueDomesticWaybill = false,
                ActiveShowInSearchEngine = false,
                CaptainCargoCode = null,
                CaptainCargoName = null,
                EconomicCode = null,
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
                NationalId = null,
                RegistrationId = null,
                Tax = 0,
            }, cancellationToken);

        await companySmsPatternsRepository.CreateCompanySmsPatternsAsync(
        new Domain.Entities.Companies.CompanySmsPatterns
        {
            CompanyId = companyId,
            PatternSmsCancelByCustomerCompany = null,
            PatternSmsCancelByCustomerReceiver = null,
            PatternSmsCancelByCustomerSender = null,
            PatternSmsCancelReceiver = null,
            PatternSmsCancelSender = null,
            PatternSmsDeliverReceiver = null,
            PatternSmsDeliverSender = null,
            PatternSmsIssueCompany = null,
            PatternSmsIssueReceiver = null,
            PatternSmsIssueSender = null,
            PatternSmsManifestReceiver = null,
            PatternSmsManifestSender = null,
            PatternSmsPackageInCompanyReceiver = null,
            PatternSmsPackageInCompanySender = null,
            PatternSmsReceivedInReceiverCompanyReceiver = null,
            PatternSmsReceivedInReceiverCompanySender = null,
            PatternSmsSendManifestReceiverCompany = null,
            PatternSmsSendReceiverPeakReceiver = null,
            PatternSmsSendReceiverPeakSender = null,
            PatternSmsSendSenderPeakReceiver = null,
            PatternSmsSendSenderPeakSender = null,
            SmsPanelNumber = null,
            SmsPanelPassword = null,
            SmsPanelUserName = null,
        }, cancellationToken);

        await commissionsRepository.CreateCompanyCommissionsAsync(
            new Domain.Entities.Companies.CompanyCommissions()
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

        logger.LogInformation("Company created successfully with ID: {CompanyId}", companyId);
        return ApiResponse<int>.Created(companyId, "شرکت با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyAsync(DeleteCompanyCommand deleteCompanyCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompany is Called with ID: {Id}", deleteCompanyCommand.Id);

        var company = await companyRepository.GetCompanyByIdAsync(deleteCompanyCommand.Id, true, false, cancellationToken);
        if (company is null)
            return ApiResponse<int>.Error(404, $"شرکت نامعتبر است");

        await companyRepository.DeleteCompanyAsync(company);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Company soft-deleted successfully with ID: {Id}", deleteCompanyCommand.Id);
        return ApiResponse<int>.Ok(deleteCompanyCommand.Id, "شرکت با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> SetCompanyActivityStatus(UpdateActiveStateCompanyCommand updateActiveStateCompanyCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyActivityStatus Called with {@UpdateActiveStateCompanyCommand}", updateActiveStateCompanyCommand);

        var company = await companyRepository.GetCompanyByIdAsync(updateActiveStateCompanyCommand.Id, true, false, cancellationToken);
        if (company is null)
            return ApiResponse<int>.Error(404, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        company.Active = !company.Active;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("وضعیت شرکت با موفقیت به‌روزرسانی شد: {Id}", updateActiveStateCompanyCommand.Id);
        return ApiResponse<int>.Ok(updateActiveStateCompanyCommand.Id, "وضعیت شرکت با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<CompanyDto>> UpdateCompanyAsync(UpdateCompanyCommand updateCompanyCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompany is Called with {@UpdateCompanyCommand}", updateCompanyCommand);

        var company = await companyRepository.GetCompanyByIdAsync(updateCompanyCommand.Id, true, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyDto>.Error(403, "مجوز این فعالیت را ندارید");

        if (await companyRepository.CheckExistCompanyNameAsync(updateCompanyCommand.Name, updateCompanyCommand.Id, cancellationToken))
            return ApiResponse<CompanyDto>.Error(400, "نام شرکت تکراری است");

        var updatedCompany = mapper.Map(updateCompanyCommand, company);
        if (updatedCompany == null)
            return ApiResponse<CompanyDto>.Error(400, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Company updated successfully with ID: {Id}", updateCompanyCommand.Id);

        var updatedCompanyDto = mapper.Map<CompanyDto>(updatedCompany);
        return ApiResponse<CompanyDto>.Updated(updatedCompanyDto, "شرکت با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyDto>>> GetAllCompanies(GetAllCompanyQuery getAllCompanyQuery, CancellationToken cancellationToken)
    {
        //logger.LogInformation("GetAllCompanies is Called");

        //var user = userContext.GetCurrentUser();
        //if (user == null)
        //    return ApiResponse<PagedResult<CompanyDto>>.Error(400, "مشکل در دریافت اطلاعات");

        //if (getAllCompanyQuery.companyId != 0 && getAllCompanyQuery.CompanyTypeId != 0)
        //{
        //    if (!user.IsSuperAdmin() && !user.IsSuperManager(getAllCompanyQuery.CompanyTypeId) && !user.IsManager(getAllCompanyQuery.CompanyId))
        //        return ApiResponse<PagedResult<CompanyDto>>.Error(400, "مجوز این فعالیت را ندارید");
        //}
        //else if (getAllCompanyQuery.CompanyId != 0 && getAllCompanyQuery.CompanyTypeId == 0)
        //{
        //    var company = await companyRepository.GetCompanyByIdAsync(getAllCompanyQuery.CompanyId, true, false, cancellationToken);
        //    if (company is null)
        //        return ApiResponse<PagedResult<CompanyDto>>.Error(400, $"شرکت نامعتبر است");

        //    if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
        //        return ApiResponse<PagedResult<CompanyDto>>.Error(400, "مجوز این فعالیت را ندارید");
        //}
        //else if (getAllCompanyQuery.CompanyId == 0 && getAllCompanyQuery.CompanyTypeId != 0)
        //{
        //    if (!user.IsSuperAdmin() && !user.IsSuperManager(getAllCompanyQuery.CompanyTypeId))
        //        return ApiResponse<PagedResult<CompanyDto>>.Error(400, "مجوز این فعالیت را ندارید");
        //}
        //else if (getAllCompanyQuery.CompanyId == 0 && getAllCompanyQuery.CompanyTypeId == 0)
        //{
        //    if (!user.IsSuperAdmin())
        //        return ApiResponse<PagedResult<CompanyDto>>.Error(400, "مجوز این فعالیت را ندارید");
        //}

        //var (companies, totalCount) = await companyRepository.GetMatchingAllCompaniesAsync(
        //    getAllCompanyQuery.SearchPhrase,
        //    getAllCompanyQuery.SortBy,
        //    getAllCompanyQuery.CompanyId,
        //    getAllCompanyQuery.CompanyTypeId,
        //    getAllCompanyQuery.CityId,
        //    getAllCompanyQuery.IsParentCompany,
        //    getAllCompanyQuery.Active,
        //    true,
        //    getAllCompanyQuery.PageNumber,
        //    getAllCompanyQuery.PageSize,
        //    getAllCompanyQuery.SortDirection,
        //    cancellationToken);

        //var companyDtos = mapper.Map<IReadOnlyList<CompanyDto>>(companies) ?? Array.Empty<CompanyDto>();
        //logger.LogInformation("Retrieved {Count} companies", companyDtos.Count);

        //var data = new PagedResult<CompanyDto>(companyDtos, totalCount, getAllCompanyQuery.PageSize, getAllCompanyQuery.PageNumber);
        //return ApiResponse<PagedResult<CompanyDto>>.Ok(data, "شرکت‌ها با موفقیت دریافت شدند");

        throw new NotImplementedException();
    }

    public async Task<ApiResponse<CompanyDto>> GetCompanyByIdAsync(GetCompanyByIdQuery getCompanyByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyById is Called with ID: {Id}", getCompanyByIdQuery.Id);

        var company = await companyRepository.GetCompanyByIdAsync(getCompanyByIdQuery.Id, false, true, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyDto>.Error(404, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyDto>.Error(403, "مجوز این فعالیت را ندارید");

        var result = mapper.Map<CompanyDto>(company);
        logger.LogInformation("Company retrieved successfully with ID: {Id}", getCompanyByIdQuery.Id);
        return ApiResponse<CompanyDto>.Ok(result, "شرکت با موفقیت دریافت شد");
    }
}