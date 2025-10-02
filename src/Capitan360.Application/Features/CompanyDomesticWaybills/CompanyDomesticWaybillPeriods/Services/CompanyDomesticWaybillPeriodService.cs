using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Commands.Create;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Commands.Delete;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Commands.UpdateActiveState;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Dtos;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Queries.GetAll;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Queries.GetById;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticWaybills;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Services;

public class CompanyDomesticWaybillPeriodService(
    ILogger<CompanyDomesticWaybillPeriodService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyDomesticWaybillPeriodRepository companyDomesticWaybillPeriodRepository,
    ICompanyDomesticWaybillRepository companyDomesticWaybillRepository,
    ICompanyRepository companyRepository,
    IUserContext userContext) : ICompanyDomesticWaybillPeriodService
{
    public async Task<ApiResponse<int>> CreateCompanyDomesticWaybillPeriodAsync(CreateCompanyDomesticWaybillPeriodCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyDomesticWaybillPeriod is Called with {@CreateCompanyDomesticWaybillPeriod Command}", command);

        var company = await companyRepository.GetCompanyByIdAsync(command.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (!command.StartNumber.ToString().StartsWith(company.Code.Trim()) ||
            !command.EndNumber.ToString().StartsWith(company.Code.Trim()))
            return ApiResponse<int>.Error(StatusCodes.Status400BadRequest, "شماره بارنامه باید با کد شرکت شروع شود");

        if (await companyDomesticWaybillPeriodRepository.CheckExistCompanyDomesticWaybillPeriodCodeAsync(command.Code, command.CompanyId, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "کد مخزن تکراری است");

        var domesticWaybillPeriods = await companyDomesticWaybillPeriodRepository.GetCompanyDomesticWaybillPeriodByCompanyIdAsync(command.CompanyId, cancellationToken);
        if (domesticWaybillPeriods == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "مخزن یافت نشد");

        for (int i = 0; i < domesticWaybillPeriods.Count; i++)
        {
            if (domesticWaybillPeriods[i].StartNumber <= command.EndNumber && command.StartNumber <= domesticWaybillPeriods[i].EndNumber)
            {
                return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "مخزن تداخل دارد");
            }
        }

        var companyDomesticWaybillPeriod = mapper.Map<CompanyDomesticWaybillPeriod>(command) ?? null;
        if (companyDomesticWaybillPeriod is null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var companyDomesticWaybillPeriodId = await companyDomesticWaybillPeriodRepository.CreateCompanyDomesticWaybillPeriodAsync(companyDomesticWaybillPeriod, cancellationToken);

        for (long i = command.StartNumber; i <= command.EndNumber; i++)
            await companyDomesticWaybillRepository.InsertCompanyDomesticWaybillAsync(new CompanyDomesticWaybill
            {
                No = i,
                CompanySenderId = company.Id,
                CompanyReceiverId = null,
                CompanyReceiverUserInsertedCode = null,
                SourceCountryId = company.CountryId,
                SourceProvinceId = company.ProvinceId,
                SourceCityId = company.CityId,
                SourceMunicipalAreaId = null,
                SourceLatitude = null,
                SourceLongitude = null,
                StopCityName = null,
                DestinationCountryId = null,
                DestinationProvinceId = null,
                DestinationCityId = null,
                DestinationMunicipalAreaId = null,
                DestinationLatitude = null,
                DestinationLongitude = null,
                CompanyDomesticWaybillPeriodId = companyDomesticWaybillPeriodId,
                CompanyManifestFormId = null,
                CompanyInsuranceId = null,
                GrossWeight = null,
                DimensionalWeight = null,
                ChargeableWeight = null,
                WeightCount = null,
                Rate = null,
                CompanyDomesticWaybillTax = null,
                ExitFare = null,
                ExitStampBill = null,
                ExitPackaging = null,
                ExitAccumulation = null,
                ExitDistribution = null,
                ExitExtraSource = null,
                ExitExtraDestination = null,
                ExitPricing = null,
                ExitRevenue1 = null,
                ExitRevenue2 = null,
                ExitRevenue3 = null,
                ExitTaxCompanySender = null,
                ExitTaxCompanyReceiver = null,
                ExitInsuranceCost = null,
                ExitTaxInsuranceCost = null,
                ExitInsuranceCostGain = null,
                ExitTaxInsuranceCostGain = null,
                ExitDiscount = null,
                ExitTotalCost = null,
                HandlingInformation = null,
                FlightNo = null,
                FlightDate = null,
                CompanySenderDateFinancial = null,
                CompanySenderCashPayment = null,
                CompanySenderCashOnDelivery = null,
                CompanySenderBankPayment = null,
                CompanySenderBankId = null,
                CompanySenderBankPaymentNo = null,
                CompanySenderCreditPayment = null,
                CustomerPanelId = null,
                CompanyReceiverDateFinancial = null,
                CompanyReceiverCashPayment = null,
                CompanyReceiverBankPayment = null,
                CompanyReceiverCashOnDelivery = null,
                CompanyReceiverBankId = null,
                CompanyReceiverBankPaymentNo = null,
                CompanyReceiverCreditPayment = null,
                CompanyReceiverResponsibleCustomerId = null,
                CustomerSenderNameFamily = null,
                CustomerSenderMobile = null,
                CustomerSenderAddress = null,
                TypeOfFactorInSamanehMoadianId = null,
                CustomerSenderNationalCode = null,
                CustomerSenderEconomicCode = null,
                CustomerSenderNationalID = null,
                CustomerReceiverNameFamily = null,
                CustomerReceiverMobile = null,
                CustomerReceiverAddress = null,
                State = (short)CompanyDomesticWaybillState.Ready,
                DateIssued = null,
                TimeIssued = null,
                DateCollectiong = null,
                TimeCollectiong = null,
                BikeDeliveryInSenderCompanyId = null,
                BikeDeliveryInSenderCompanyAgent = null,
                DateReceivedAtSenderCompany = null,
                TimeReceivedAtSenderCompany = null,
                DateManifested = null,
                TimeManifested = null,
                DateAirlineDelivery = null,
                TimeAirlineDelivery = null,
                DateReceivedAtReceiverCompany = null,
                TimeReceivedAtReceiverCompany = null,
                DateDistribution = null,
                TimeDistribution = null,
                BikeDeliveryInReceiverCompanyId = null,
                BikeDeliveryInReceiverCompanyAgent = null,
                DateDelivery = null,
                TimeDelivery = null,
                EntranceDeliveryPerson = null,
                EntranceTransfereePersonName = null,
                EntranceTransfereePersonNationalCode = null,
                DescriptionSenderCompany = null,
                DescriptionReceiverCompany = null,
                DescriptionSenderCustomer = null,
                DescriptionReceiverCustomer = null,
                Lock = null,
                IsIssueFromCaptainCargoWebSite = null,
                IsIssueFromCompanyWebSite = null,
                IsIssueFromCaptainCargoWebService = null,
                IsIssueFromCompanyWebService = null,
                IsIssueFromCaptainCargoPanel = null,
                IsIssueFromCaptainCargoDesktop = null,
                CaptainCargoPrice = null,
                CounterId = null,
                Dirty = null,
            }, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticWaybillPeriod created successfully with {@CompanyDomesticWaybillPeriod}", companyDomesticWaybillPeriod);
        return ApiResponse<int>.Created(companyDomesticWaybillPeriodId, "مخزن با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyDomesticWaybillPeriodAsync(DeleteCompanyDomesticWaybillPeriodCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyDomesticWaybillPeriod is Called with {@Id}", command.Id);

        var companyDomesticWaybillPeriod = await companyDomesticWaybillPeriodRepository.GetCompanyDomesticWaybillPeriodByIdAsync(command.Id, true, false, cancellationToken);
        if (companyDomesticWaybillPeriod is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "مخزن نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticWaybillPeriod.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await companyDomesticWaybillRepository.AnyIssunedCompanyDomesticWaybillByCompanyDomesticPeriodIdAsync(command.Id, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "بارنامه صادر شده وجود دارد");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var listDomesticWabillId = await companyDomesticWaybillRepository.GetCompanyDomesticWaybillIdByCompanyDomesticWaybillPeriodIdAsync(command.Id, cancellationToken);
        for (int i = 0; i < listDomesticWabillId.Count; i++)
        {
            await companyDomesticWaybillRepository.DeleteCompanyDomesticWaybillAsync(listDomesticWabillId[i], cancellationToken);
        }

        await companyDomesticWaybillPeriodRepository.DeleteCompanyDomesticWaybillPeriodAsync(command.Id, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticWaybillPeriod Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "مخزن با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> SetCompanyDomesticWaybillPeriodActivityStatusAsync(UpdateActiveStateCompanyDomesticWaybillPeriodCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyDomesticWaybillPeriodActivityStatus Called with {@Id}", command.Id);

        var cmopanyDomesticWaybillPeriod = await companyDomesticWaybillPeriodRepository.GetCompanyDomesticWaybillPeriodByIdAsync(command.Id, false, true,  cancellationToken);
        if (cmopanyDomesticWaybillPeriod is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "مخزن نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(cmopanyDomesticWaybillPeriod.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        cmopanyDomesticWaybillPeriod.Active = !cmopanyDomesticWaybillPeriod.Active;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticWaybillPeriod activity status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت مخزن با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<CompanyDomesticWaybillPeriodDto>> UpdateCompanyDomesticWaybillPeriodAsync(UpdateCompanyDomesticWaybillPeriodCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyDomesticWaybillPeriod is Called with {@UpdateCompanyDomesticWaybillPeriodCommand}", command);

        var domesticWaybillPeriod = await companyDomesticWaybillPeriodRepository.GetCompanyDomesticWaybillPeriodByIdAsync(command.Id, false, true,  cancellationToken);
        if (domesticWaybillPeriod == null)
            return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(StatusCodes.Status404NotFound, "مخزن نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(domesticWaybillPeriod.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (!command.StartNumber.ToString().StartsWith(company.Code.Trim()) ||
            !command.StartNumber.ToString().StartsWith(company.Code.Trim()))
            return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(StatusCodes.Status400BadRequest, "شماره بارنامه باید با کد شرکت شروع شود");

        if (await companyDomesticWaybillPeriodRepository.CheckExistCompanyDomesticWaybillPeriodCodeAsync(command.Code, domesticWaybillPeriod.CompanyId, domesticWaybillPeriod.Id, cancellationToken))
            return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(StatusCodes.Status409Conflict, "کد مخزن تکراری است");

        var companyDomesticWaybillPeriods = await companyDomesticWaybillPeriodRepository.GetCompanyDomesticWaybillPeriodByCompanyIdAsync(company.Id, cancellationToken);
        if (companyDomesticWaybillPeriods == null)
            return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(StatusCodes.Status404NotFound, "مشکل در دریافت اطلاعات");

        for (int i = 0; i < companyDomesticWaybillPeriods.Count; i++)
        {
            if (companyDomesticWaybillPeriods[i].Id != domesticWaybillPeriod.Id && companyDomesticWaybillPeriods[i].StartNumber <= command.EndNumber && command.StartNumber <= companyDomesticWaybillPeriods[i].EndNumber)
            {
                return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(StatusCodes.Status409Conflict, "مخزن تداخل دارد");
            }
        }

        if (await companyDomesticWaybillRepository.AnyIssunedCompanyDomesticWaybillByCompanyDomesticPeriodIdStartNumberEndNumberAsync(command.Id, command.StartNumber, domesticWaybillPeriod.EndNumber, cancellationToken))
            return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(StatusCodes.Status409Conflict, "بارنامه صادر شده وجود دارد");

        bool StartIsExpand = domesticWaybillPeriod.StartNumber > command.StartNumber;
        bool EndIsExpand = domesticWaybillPeriod.EndNumber < command.EndNumber;
        bool StartIsLesser = domesticWaybillPeriod.StartNumber < command.StartNumber;
        bool EndIsLesser = domesticWaybillPeriod.EndNumber > command.EndNumber;

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        if (StartIsLesser)
        {
            List<int> listDomesticWabillId = await companyDomesticWaybillRepository.GetCompanyDomesticWaybillIdByCompanyDomesticWaybillPeriodIdAndLessThanStartNumberAsync(command.Id, command.StartNumber, cancellationToken);
            for (int i = 0; i < listDomesticWabillId.Count; i++)
            {
                await companyDomesticWaybillRepository.DeleteCompanyDomesticWaybillAsync(listDomesticWabillId[i], cancellationToken);
            }
        }

        if (EndIsLesser)
        {
            List<int> listDomesticWabillId = await companyDomesticWaybillRepository.GetCompanyDomesticWaybillIdByCompanyDomesticWaybillPeriodIdAndGreatherThanEndNumberAsync(command.Id, command.EndNumber, cancellationToken);
            for (int i = 0; i < listDomesticWabillId.Count; i++)
            {
                await companyDomesticWaybillRepository.DeleteCompanyDomesticWaybillAsync(listDomesticWabillId[i], cancellationToken);
            }
        }

        if (StartIsExpand)
        {
            for (long i = command.StartNumber; i < domesticWaybillPeriod.StartNumber && i <= command.EndNumber; i++)
            {
                await companyDomesticWaybillRepository.InsertCompanyDomesticWaybillAsync(new CompanyDomesticWaybill
                {
                    No = i,
                    CompanySenderId = company.Id,
                    CompanyReceiverId = null,
                    CompanyReceiverUserInsertedCode = null,
                    SourceCountryId = company.CountryId,
                    SourceProvinceId = company.ProvinceId,
                    SourceCityId = company.CityId,
                    SourceMunicipalAreaId = null,
                    SourceLatitude = null,
                    SourceLongitude = null,
                    StopCityName = null,
                    DestinationCountryId = null,
                    DestinationProvinceId = null,
                    DestinationCityId = null,
                    DestinationMunicipalAreaId = null,
                    DestinationLatitude = null,
                    DestinationLongitude = null,
                    CompanyDomesticWaybillPeriodId = command.Id,
                    CompanyManifestFormId = null,
                    CompanyInsuranceId = null,
                    GrossWeight = null,
                    DimensionalWeight = null,
                    ChargeableWeight = null,
                    WeightCount = null,
                    Rate = null,
                    CompanyDomesticWaybillTax = null,
                    ExitFare = null,
                    ExitStampBill = null,
                    ExitPackaging = null,
                    ExitAccumulation = null,
                    ExitDistribution = null,
                    ExitExtraSource = null,
                    ExitExtraDestination = null,
                    ExitPricing = null,
                    ExitRevenue1 = null,
                    ExitRevenue2 = null,
                    ExitRevenue3 = null,
                    ExitTaxCompanySender = null,
                    ExitTaxCompanyReceiver = null,
                    ExitInsuranceCost = null,
                    ExitTaxInsuranceCost = null,
                    ExitInsuranceCostGain = null,
                    ExitTaxInsuranceCostGain = null,
                    ExitDiscount = null,
                    ExitTotalCost = null,
                    HandlingInformation = null,
                    FlightNo = null,
                    FlightDate = null,
                    CompanySenderDateFinancial = null,
                    CompanySenderCashPayment = null,
                    CompanySenderCashOnDelivery = null,
                    CompanySenderBankPayment = null,
                    CompanySenderBankId = null,
                    CompanySenderBankPaymentNo = null,
                    CompanySenderCreditPayment = null,
                    CustomerPanelId = null,
                    CompanyReceiverDateFinancial = null,
                    CompanyReceiverCashPayment = null,
                    CompanyReceiverBankPayment = null,
                    CompanyReceiverCashOnDelivery = null,
                    CompanyReceiverBankId = null,
                    CompanyReceiverBankPaymentNo = null,
                    CompanyReceiverCreditPayment = null,
                    CompanyReceiverResponsibleCustomerId = null,
                    CustomerSenderNameFamily = null,
                    CustomerSenderMobile = null,
                    CustomerSenderAddress = null,
                    TypeOfFactorInSamanehMoadianId = null,
                    CustomerSenderNationalCode = null,
                    CustomerSenderEconomicCode = null,
                    CustomerSenderNationalID = null,
                    CustomerReceiverNameFamily = null,
                    CustomerReceiverMobile = null,
                    CustomerReceiverAddress = null,
                    State = (short)CompanyDomesticWaybillState.Ready,
                    DateIssued = null,
                    TimeIssued = null,
                    DateCollectiong = null,
                    TimeCollectiong = null,
                    BikeDeliveryInSenderCompanyId = null,
                    BikeDeliveryInSenderCompanyAgent = null,
                    DateReceivedAtSenderCompany = null,
                    TimeReceivedAtSenderCompany = null,
                    DateManifested = null,
                    TimeManifested = null,
                    DateAirlineDelivery = null,
                    TimeAirlineDelivery = null,
                    DateReceivedAtReceiverCompany = null,
                    TimeReceivedAtReceiverCompany = null,
                    DateDistribution = null,
                    TimeDistribution = null,
                    BikeDeliveryInReceiverCompanyId = null,
                    BikeDeliveryInReceiverCompanyAgent = null,
                    DateDelivery = null,
                    TimeDelivery = null,
                    EntranceDeliveryPerson = null,
                    EntranceTransfereePersonName = null,
                    EntranceTransfereePersonNationalCode = null,
                    DescriptionSenderCompany = null,
                    DescriptionReceiverCompany = null,
                    DescriptionSenderCustomer = null,
                    DescriptionReceiverCustomer = null,
                    Lock = null,
                    IsIssueFromCaptainCargoWebSite = null,
                    IsIssueFromCompanyWebSite = null,
                    IsIssueFromCaptainCargoWebService = null,
                    IsIssueFromCompanyWebService = null,
                    IsIssueFromCaptainCargoPanel = null,
                    IsIssueFromCaptainCargoDesktop = null,
                    CaptainCargoPrice = null,
                    CounterId = null,
                    Dirty = null,
                }, cancellationToken);
            }
        }

        if (EndIsExpand)
        {
            List<string> storeProcedure = new List<string>();

            for (long i = Math.Max(command.StartNumber, domesticWaybillPeriod.EndNumber) + 1; i <= command.EndNumber; i++)
            {
                await companyDomesticWaybillRepository.InsertCompanyDomesticWaybillAsync(new CompanyDomesticWaybill
                {
                    No = i,
                    CompanySenderId = company.Id,
                    CompanyReceiverId = null,
                    CompanyReceiverUserInsertedCode = null,
                    SourceCountryId = company.CountryId,
                    SourceProvinceId = company.ProvinceId,
                    SourceCityId = company.CityId,
                    SourceMunicipalAreaId = null,
                    SourceLatitude = null,
                    SourceLongitude = null,
                    StopCityName = null,
                    DestinationCountryId = null,
                    DestinationProvinceId = null,
                    DestinationCityId = null,
                    DestinationMunicipalAreaId = null,
                    DestinationLatitude = null,
                    DestinationLongitude = null,
                    CompanyDomesticWaybillPeriodId = command.Id,
                    CompanyManifestFormId = null,
                    CompanyInsuranceId = null,
                    GrossWeight = null,
                    DimensionalWeight = null,
                    ChargeableWeight = null,
                    WeightCount = null,
                    Rate = null,
                    CompanyDomesticWaybillTax = null,
                    ExitFare = null,
                    ExitStampBill = null,
                    ExitPackaging = null,
                    ExitAccumulation = null,
                    ExitDistribution = null,
                    ExitExtraSource = null,
                    ExitExtraDestination = null,
                    ExitPricing = null,
                    ExitRevenue1 = null,
                    ExitRevenue2 = null,
                    ExitRevenue3 = null,
                    ExitTaxCompanySender = null,
                    ExitTaxCompanyReceiver = null,
                    ExitInsuranceCost = null,
                    ExitTaxInsuranceCost = null,
                    ExitInsuranceCostGain = null,
                    ExitTaxInsuranceCostGain = null,
                    ExitDiscount = null,
                    ExitTotalCost = null,
                    HandlingInformation = null,
                    FlightNo = null,
                    FlightDate = null,
                    CompanySenderDateFinancial = null,
                    CompanySenderCashPayment = null,
                    CompanySenderCashOnDelivery = null,
                    CompanySenderBankPayment = null,
                    CompanySenderBankId = null,
                    CompanySenderBankPaymentNo = null,
                    CompanySenderCreditPayment = null,
                    CustomerPanelId = null,
                    CompanyReceiverDateFinancial = null,
                    CompanyReceiverCashPayment = null,
                    CompanyReceiverBankPayment = null,
                    CompanyReceiverCashOnDelivery = null,
                    CompanyReceiverBankId = null,
                    CompanyReceiverBankPaymentNo = null,
                    CompanyReceiverCreditPayment = null,
                    CompanyReceiverResponsibleCustomerId = null,
                    CustomerSenderNameFamily = null,
                    CustomerSenderMobile = null,
                    CustomerSenderAddress = null,
                    TypeOfFactorInSamanehMoadianId = null,
                    CustomerSenderNationalCode = null,
                    CustomerSenderEconomicCode = null,
                    CustomerSenderNationalID = null,
                    CustomerReceiverNameFamily = null,
                    CustomerReceiverMobile = null,
                    CustomerReceiverAddress = null,
                    State = (short)CompanyDomesticWaybillState.Ready,
                    DateIssued = null,
                    TimeIssued = null,
                    DateCollectiong = null,
                    TimeCollectiong = null,
                    BikeDeliveryInSenderCompanyId = null,
                    BikeDeliveryInSenderCompanyAgent = null,
                    DateReceivedAtSenderCompany = null,
                    TimeReceivedAtSenderCompany = null,
                    DateManifested = null,
                    TimeManifested = null,
                    DateAirlineDelivery = null,
                    TimeAirlineDelivery = null,
                    DateReceivedAtReceiverCompany = null,
                    TimeReceivedAtReceiverCompany = null,
                    DateDistribution = null,
                    TimeDistribution = null,
                    BikeDeliveryInReceiverCompanyId = null,
                    BikeDeliveryInReceiverCompanyAgent = null,
                    DateDelivery = null,
                    TimeDelivery = null,
                    EntranceDeliveryPerson = null,
                    EntranceTransfereePersonName = null,
                    EntranceTransfereePersonNationalCode = null,
                    DescriptionSenderCompany = null,
                    DescriptionReceiverCompany = null,
                    DescriptionSenderCustomer = null,
                    DescriptionReceiverCustomer = null,
                    Lock = null,
                    IsIssueFromCaptainCargoWebSite = null,
                    IsIssueFromCompanyWebSite = null,
                    IsIssueFromCaptainCargoWebService = null,
                    IsIssueFromCompanyWebService = null,
                    IsIssueFromCaptainCargoPanel = null,
                    IsIssueFromCaptainCargoDesktop = null,
                    CaptainCargoPrice = null,
                    CounterId = null,
                    Dirty = null,
                }, cancellationToken);
            }
        }

        var updatedDomesticWaybillPeriod = mapper.Map(command, domesticWaybillPeriod);
        if (updatedDomesticWaybillPeriod is null)
            return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticWaybillPeriod updated successfully with {@UpdateCompanyDomesticWaybillPeriodCommand}", command);

        var updatedDomesticWaybillPeriodDto = mapper.Map<CompanyDomesticWaybillPeriodDto>(updatedDomesticWaybillPeriod);
        return ApiResponse<CompanyDomesticWaybillPeriodDto>.Ok(updatedDomesticWaybillPeriodDto, "مخزن با موفقیت به‌روزرسانی شد");
    }


    public async Task<ApiResponse<PagedResult<CompanyDomesticWaybillPeriodDto>>> GetAllCompanyDomesticWaybillPeriodsAsync(GetAllCompanyDomesticWaybillPeriodsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyDomesticWaybillPeriods is Called");

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<PagedResult<CompanyDomesticWaybillPeriodDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyDomesticWaybillPeriodDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<PagedResult<CompanyDomesticWaybillPeriodDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var (companyDomesticWaybillPeriods, totalCount) = await companyDomesticWaybillPeriodRepository.GetAllCompanyDomesticWaybillPeriodsAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyId,
            query.Active,
            query.HasReadyForm,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);

        var companyDomesticWaybillPeriodDtos = mapper.Map<IReadOnlyList<CompanyDomesticWaybillPeriodDto>>(companyDomesticWaybillPeriods) ?? Array.Empty<CompanyDomesticWaybillPeriodDto>();
        if (companyDomesticWaybillPeriodDtos == null)
            return ApiResponse<PagedResult<CompanyDomesticWaybillPeriodDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company domesticWaybill periods", companyDomesticWaybillPeriodDtos.Count);

        var data = new PagedResult<CompanyDomesticWaybillPeriodDto>(companyDomesticWaybillPeriodDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyDomesticWaybillPeriodDto>>.Ok(data, "محتوهای بار با موفقیت دریافت شدند");
    }

  public async   Task<ApiResponse<CompanyDomesticWaybillPeriodDto>> GetCompanyDomesticWaybillPeriodByIdAsync(GetCompanyDomesticWaybillPeriodByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyDomesticWaybillPeriodById is Called with {@Id}", query.Id);

        var domesticWaybillPeriod = await companyDomesticWaybillPeriodRepository.GetCompanyDomesticWaybillPeriodByIdAsync(query.Id, false, false, cancellationToken);
        if (domesticWaybillPeriod is null)
            return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(404, "مخزن یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(domesticWaybillPeriod.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(404, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyDomesticWaybillPeriodDto = mapper.Map<CompanyDomesticWaybillPeriodDto>(domesticWaybillPeriod);
        if (companyDomesticWaybillPeriodDto == null)
            return ApiResponse<CompanyDomesticWaybillPeriodDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("CompanyDomesticWaybillPeriod retrieved successfully with {@Id}", query.Id);

        return ApiResponse<CompanyDomesticWaybillPeriodDto>.Ok(companyDomesticWaybillPeriodDto, "مخزن با موفقیت دریافت شد");
    }
}

