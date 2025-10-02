using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.AttachToCompanyManifestForm;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.AttachToCompanyManifestFormFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormCollectiongState;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormDeliveryState;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormDeliveryStateFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormReceivedAtSenderCompanyState;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFromCompanyManifestForm;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFromCompanyManifestFormFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFromDistributionState;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackToReadyStateFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackToReayState;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToCollection;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDelivery;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDeliveryFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDistribution;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToReceivedAtSenderCompany;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.Issue;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.IssueFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.UpdateFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Dtos;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Dtos;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Entities.CompanyInsurances;
using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Addresses;
using Capitan360.Domain.Interfaces.Repositories.ComapnyManifestForms;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticWaybills;
using Capitan360.Domain.Interfaces.Repositories.CompanyInsurances;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Services;

public class CompanyDomesticWaybillService(
    ILogger<CompanyDomesticWaybillService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyRepository companyRepository,
    ICompanyCommissionsRepository companyCommissionsRepository,
    IAreaRepository areaRepository,
    ICompanyDomesticPathReceiverCompanyRepository companyDomesticPathReceiverCompanyRepository,
    ICompanyDomesticPathRepository companyDomesticPathRepository,
    ICompanyInsuranceRepository companyInsuranceRepository,
    ICompanyBankRepository companyBankRepository,
    ICompanyContentTypeRepository companyContentTypeRepository,
    ICompanyPackageTypeRepository companyPackageTypeRepository,
    ICompanyDomesticWaybillRepository companyDomesticWaybillRepository,
    ICompanyManifestFormRepository companyManifestFormRepository,
    IUserContext userContext) : ICompanyDomesticWaybillService
{
////    public async Task<ApiResponse<int>> InsertCompanyDomesticWaybillFromDesktopAsync(IssueCompanyDomesticWaybillFromDesktopCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("InsertCompanyDomesticWaybillFromDesktop is Called with {@InsertCompanyDomesticWaybillFromDesktopCommand}", command);
  //
  //      var companySender = await companyRepository.GetCompanyByCodeAsync(command.CompanySenderCaptain360Code, true, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت فرستنده وجود ندارد");
  //
  //      var sourceRegionMunicipalities = await areaRepository.GetAreasByParentIdAsync(companySender.CityId, cancellationToken);
  //      if (sourceRegionMunicipalities == null)
  //          return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "منطقه شهری مبدا تعریف نشده است");
  //
  //      var companySenderCommissions = await companyCommissionsRepository.GetCompanyCommissionsByCompanyIdAsync(companySender.Id, cancellationToken);
  //      if (companySenderCommissions == null)
  //          return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "مشکل در دریافت اطلاعات شرکت");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsManager(companySender.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      Company? companyReceiver = null;
  //      IReadOnlyList<CompanyDomesticPathReceiverCompany>? companyDomesticPathReceiverCompany = null;
  //      CompanyDomesticPath? companyDomesticPath = null;
  //      Area? destinationCity = null;
  //
  //      companyReceiver = await companyRepository.GetCompanyByCodeAsync(command.CompanyReceiverUserInsertedCode, true, false, cancellationToken);
  //      if (companyReceiver != null)
  //      {
  //          companyDomesticPathReceiverCompany = await companyDomesticPathReceiverCompanyRepository.GetCompanyDomesticPathReceiverCompanyByCompanySenderIdAndReceiverCompanyIdAsync(companySender.Id, companyReceiver.Id, cancellationToken);
  //          if (companyDomesticPathReceiverCompany == null || companyDomesticPathReceiverCompany.Count < 1)
  //              return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "مسیری برای نماینده تعریف نشده است");
  //      }
  //      else
  //      {
  //          companyDomesticPathReceiverCompany = await companyDomesticPathReceiverCompanyRepository.GetCompanyDomesticPathReceiverCompanyByCompanySenderIdAndCompanyReceiverCodeAsync(companySender.Id, command.CompanyReceiverUserInsertedCode, cancellationToken);
  //          if (companyDomesticPathReceiverCompany == null || companyDomesticPathReceiverCompany.Count < 1)
  //              return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "مسیری برای نماینده تعریف نشده است");
  //
  //          companyDomesticPath = await companyDomesticPathRepository.GetCompanyDomesticPathByIdAsync(companyDomesticPathReceiverCompany.First().CompanyDomesticPathId, false, false, true, false, cancellationToken);
  //          if (companyDomesticPath == null)
  //              return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "مسیری تعریف نشده است");
  //
  //          destinationCity = await areaRepository.GetAreaById(companyDomesticPathReceiverCompany[0].CompanyDomesticPath!.DestinationCityId, cancellationToken);
  //      }
  //
  //      CompanyInsurance? companyInsurance = null;
  //      if (command.InsuranceCompanyCode != null)
  //      {
  //          companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceByCodeAsync(command.InsuranceCompanyCode, companySender.Id, false, false, cancellationToken);
  //          if (companyInsurance == null)
  //              return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت بیمه وجود ندارد");
  //      }
  //
  //      CompanyBank? companyBank = null;
  //      if (command.CompanySenderBankPayment && command.CompanySenderBankCode != null)
  //      {
  //          companyBank = await companyBankRepository.GetCompanyBankByCodeAsync(command.CompanySenderBankCode, companySender.Id, false, false, cancellationToken);
  //          if (companyBank == null)
  //              return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بانک وجود ندارد");
  //      }
  //
  //      var companyContentTypes = await companyContentTypeRepository.GetCompanyContentTypeByCompanyIdAsync(companySender.Id, cancellationToken);
  //      if (companyContentTypes == null)
  //          return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "محتوی بار وجود ندارد");
  //
  //      var companyPackageTypes = await companyPackageTypeRepository.GetCompanyPackageTypeByCompanyIdAsync(companySender.Id, cancellationToken);
  //      if (companyPackageTypes == null)
  //          return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بسته بندی وجود ندارد");
  //
  //      var domesticWaybill = await companyDomesticWaybillRepository.GetCompanyDomesticWaybillByNoAsync(command.No, false, false, false, false, cancellationToken);
  //      if (domesticWaybill != null)
  //          return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بارنامه قبلا ثبت شده است");
  //
  //      User customerPanel = new User { };
  //      if (/*!customerPanel.IsCridit   and */command.CompanySenderCreditPayment)
  //          return ApiResponse<int>.Error(400, "مشتری اعتبار ندارد");
  //
  //      var companyDomesticWaybill = mapper.Map<CompanyDomesticWaybill>(command) ?? null;
  //      if (companyDomesticWaybill == null)
  //          return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");
  //
  //      companyDomesticWaybill.CompanySenderId = companySender.Id;
  //      companyDomesticWaybill.CompanyReceiverId = companyReceiver == null ? null : companyReceiver.Id;
  //      companyDomesticWaybill.CompanyReceiverUserInsertedCode = companyReceiver != null ? null : command.CompanyReceiverUserInsertedCode;
  //      companyDomesticWaybill.SourceCountryId = companySender.CountryId;
  //      companyDomesticWaybill.SourceProvinceId = companySender.ProvinceId;
  //      companyDomesticWaybill.SourceCityId = companySender.CityId;
  //      companyDomesticWaybill.SourceMunicipalAreaId = sourceRegionMunicipalities.First().Id;
  //      companyDomesticWaybill.SourceLatitude = companySender.City!.Latitude;
  //      companyDomesticWaybill.SourceLongitude = companySender.City!.Longitude;
  //      companyDomesticWaybill.DestinationCountryId = companyReceiver != null ? companyReceiver.CountryId : companyDomesticPath!.DestinationCountryId;
  //      companyDomesticWaybill.DestinationProvinceId = companyReceiver != null ? companyReceiver.ProvinceId : companyDomesticPath!.DestinationProvinceId;
  //      companyDomesticWaybill.DestinationCityId = companyReceiver != null ? companyReceiver.CityId : companyDomesticPath!.DestinationCityId;
  //      companyDomesticWaybill.DestinationMunicipalAreaId = companyDomesticPathReceiverCompany[0].MunicipalAreaId;
  //      companyDomesticWaybill.DestinationLatitude = companyReceiver != null ? companyReceiver.City!.Latitude : destinationCity!.Latitude;
  //      companyDomesticWaybill.DestinationLongitude = companyReceiver != null ? companyReceiver.City!.Longitude : destinationCity!.Longitude;
  //      companyDomesticWaybill.CompanyInsuranceId = companyInsurance == null ? null : companyInsurance.Id;
  //      companyDomesticWaybill.CompanySenderBankId = companyBank == null ? null : companyBank.Id; ;
  //      companyDomesticWaybill.CustomerPanelId = customerPanel.Id;
  //      companyDomesticWaybill.IsIssueFromCaptainCargoWebSite = false;
  //      companyDomesticWaybill.IsIssueFromCompanyWebSite = false;
  //      companyDomesticWaybill.IsIssueFromCaptainCargoWebService = false;
  //      companyDomesticWaybill.IsIssueFromCompanyWebService = false;
  //      companyDomesticWaybill.IsIssueFromCaptainCargoPanel = false;
  //      companyDomesticWaybill.IsIssueFromCaptainCargoDesktop = true;
  //      companyDomesticWaybill.CaptainCargoPrice = companySenderCommissions.CommissionFromCaptainCargoDesktop;
  //      companyDomesticWaybill.CounterId = user.Id;
  //      companyDomesticWaybill.Dirty = false;
  //
  //      companyDomesticWaybill.CompanyDomesticWaybillPackageTypes = command.CompanyDomesticWaybillPackageTypes.Select(
  //               item => new CompanyDomesticWaybillPackageType
  //               {
  //                   ChargeableWeight = item.ChargeableWeight,
  //                   CompanyContentTypeId = companyContentTypes.FirstOrDefault(cct => cct.CompanyContentTypeName.ToLower() == item.CompanyContentTypeName)?.Id ?? companyContentTypes.First().Id,
  //                   CompanyPackageTypeId = companyPackageTypes.FirstOrDefault(cct => cct.CompanyPackageTypeName.ToLower() == item.CompanyPackageTypeName)?.Id ?? companyPackageTypes.First().Id,
  //                   CountDimension = item.CountDimension,
  //                   DeclaredValue = item.DeclaredValue,
  //                   Dimensions = item.Dimensions,
  //                   GrossWeight = item.GrossWeight,
  //                   UserInsertedContentName = item.UserInsertedContentName,
  //               }).ToList();
  //
  //      var CompanyDomesticWaybillId = await domesticWaybillRepository.InsertCompanyDomesticWaybillAsync(CompanyDomesticWaybill, cancellationToken);
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("CompanyDomesticWaybillFromDesktop created successfully ID: {issueCompanyDomesticWaybillFromDesktopCommand}", domesticWaybill.Id);
  //      return ApiResponse<int>.Ok(domesticWaybill.Id, "بارنامه با موفقیت ثبت شد");
  //  }
////
////    public async Task<ApiResponse<int>> InsertCompanyDomesticWaybillAsync(IssueCompanyDomesticWaybillCommand command, CancellationToken cancellationToken)
  //  {
  //      //logger.LogInformation("InsertCompanyDomesticWaybill is Called with {@InsertCompanyDomesticWaybillCommand}", issueCompanyDomesticWaybillCommand);
  //      //
  //      //var companySender = await companyRepository.GetCompanyByIdAsync(issueCompanyDomesticWaybillCommand.CompanySenderId, false, false, cancellationToken);
  //      //if (companySender == null)
  //      //    return ApiResponse<int>.Error(400, "شرکت فرستنده وجود ندارد");
  //      //
  //      //var companySenderCommissions = await companyCommissionsRepository.GetCompanyCommissionsByCompanyIdAsync(issueCompanyDomesticWaybillCommand.CompanySenderId, false, false, cancellationToken);
  //      //if (companySenderCommissions == null)
  //      //    return ApiResponse<int>.Error(400, "مشکل در دریافت اطلاعات شرکت");
  //      //
  //      //var user = userContext.GetCurrentUser();
  //      //if (user == null)
  //      //   return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //      //
  //      //if (!user.IsSuperAdmin() && !user.IsManager(companySender.CompanyTypeId) && !user.IsManager(companySender.Id) && !user.IsUser(companySender.Id) /*&&
  //      //    ! check allow isssue waybill based on companyType*/)
  //      //        return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //      //
  //      //Company? companyReceiver = null;
  //      //IReadOnlyList<CompanyDomesticPathReceiver>? companyDomesticPathReceiver = null;
  //      //Area? destinationCity = null;
  //      //
  //      //companyReceiver = await companyRepository.GetCompanyByCodeAsync(insertCompanyDomesticWaybillFromDesktopCommand.CompanyReceiverCaptain360Code, false, false, cancellationToken);
  //      //if (companyReceiver != null)
  //      //{
  //      //    companyDomesticPathReceiver = await companyDomesticPathReceiverRepository.GetCompanyDomesticPathReceiverByCompanySenderIdAndCompanyReceiverIdAsync(companySender.Id, companyReceiver.Id, false, true, cancellationToken);
  //      //    if (companyDomesticPathReceiver == null || companyDomesticPathReceiver.Count < 1)
  //      //        return ApiResponse<int>.Error(400, "نماینده نامعتبر است");
  //      //}
  //      //else
  //      //{
  //      //    companyDomesticPathReceiver = await companyDomesticPathReceiverRepository.GetCompanyDomesticPathReceiverByCompanySenderIdAndCompanyReceiverCodeAsync(companySender.Id, insertCompanyDomesticWaybillFromDesktopCommand.CompanyReceiverCaptain360Code, false, true, cancellationToken);
  //      //    if (companyDomesticPathReceiver == null || companyDomesticPathReceiver.Count < 1)
  //      //        return ApiResponse<int>.Error(400, "نماینده نامعتبر است");
  //      //
  //      //    destinationCity = await areaRepository.GetAreaById(companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationCityId, cancellationToken);
  //      //}
  //      //
  //      //CompanyInsurance? companyInsurance = null;
  //      //if (insertCompanyDomesticWaybillFromDesktopCommand.InsuranceCompanyCode != null)
  //      //{
  //      //    companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceByCodeAsync(insertCompanyDomesticWaybillFromDesktopCommand.InsuranceCompanyCode, companySender.Id, false, false, cancellationToken);
  //      //    if (companyInsurance == null)
  //      //        return ApiResponse<int>.Error(400, "شرکت بیمه وجود ندارد");
  //      //}
  //      //
  //      //CompanyBank? companyBank = null;
  //      //if (insertCompanyDomesticWaybillFromDesktopCommand.CompanySenderBankCode != null)
  //      //{
  //      //    companyBank = await companyBankRepository.GetCompanyBankByCodeAsync(insertCompanyDomesticWaybillFromDesktopCommand.CompanySenderBankCode, companySender.Id, false, false, cancellationToken);
  //      //    if (companyBank == null)
  //      //        return ApiResponse<int>.Error(400, "شرکت بیمه وجود ندارد");
  //      //}
  //      //
  //      //var companyContentTypes = await companyContentTypeRepository.GetCompanyContentTypeByCompanyIdAsync(companySender.Id, false, false, cancellationToken);
  //      //if (companyContentTypes == null)
  //      //    return ApiResponse<int>.Error(400, "شرکت بیمه وجود ندارد");
  //      //
  //      //var companyPackageTypes = await companyPackageTypeRepository.GetCompanyPackageTypeByCompanyIdAsync(companySender.Id, false, false, cancellationToken);
  //      //if (companyPackageTypes == null)
  //      //    return ApiResponse<int>.Error(400, "شرکت بیمه وجود ندارد");
  //      //
  //      //var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByNoAsync(insertCompanyDomesticWaybillFromDesktopCommand.No, true, true, false, false, cancellationToken);
  //      //if (domesticWaybill != null)
  //      //    return ApiResponse<int>.Error(400, "بارنامه قبلا ثبت شده است");
  //      //
  //      //if (/*!user.IsCridit   and */insertCompanyDomesticWaybillFromDesktopCommand.CompanySenderCreditPayment)
  //      //    return ApiResponse<int>.Error(400, "کاربر اعتباری نیست");
  //      //
  //      //
  //      //if (domesticWaybill.State == (short)WaybillState.Issued ||
  //      //    domesticWaybill.State == (short)WaybillState.Collectiong ||
  //      //    domesticWaybill.State == (short)WaybillState.ReceivedAtSenderCompany ||
  //      //    domesticWaybill.State == (short)WaybillState.Manifested ||
  //      //    domesticWaybill.State == (short)WaybillState.AirlineDelivery)
  //      //{
  //      //}
  //      //else if (domesticWaybill.State == (short)WaybillState.ReceivedAtReceivercompany ||
  //      //         domesticWaybill.State == (short)WaybillState.Distribution ||
  //      //         domesticWaybill.State == (short)WaybillState.Delivery)
  //      //{
  //      //    domesticWaybill.Dirty = true;
  //      //}
  //      //
  //      //var CompanyDomesticWaybill = mapper.Map<Domain.Entities.CompanyDomesticWaybillEntity.CompanyDomesticWaybill>(insertCompanyDomesticWaybillFromDesktopCommand) ?? null;
  //      //if (CompanyDomesticWaybill == null)
  //      //    return ApiResponse<int>.Error(400, "مشکل در عملیات تبدیل");
  //      //
  //      //CompanyDomesticWaybill.CompanySenderId = companySender.Id;
  //      //CompanyDomesticWaybill.CompanyReceiverId = companyReceiver == null ? null : companyReceiver.Id;
  //      //CompanyDomesticWaybill.CompanyReceiverUserInsertedCode = companyReceiver != null ? null : insertCompanyDomesticWaybillFromDesktopCommand.CompanyReceiverCaptain360Code;
  //      //CompanyDomesticWaybill.SourceCountryId = companySender.CountryId;
  //      //CompanyDomesticWaybill.SourceProvinceId = companySender.ProvinceId;
  //      //CompanyDomesticWaybill.SourceCityId = companySender.CityId;
  //      //CompanyDomesticWaybill.SourceMunicipalAreaId = sourceRegionMunicipalities.First().Id;
  //      //CompanyDomesticWaybill.SourceLatitude = companySender.City.Latitude;
  //      //CompanyDomesticWaybill.SourceLongitude = companySender.City.Longitude;
  //      //CompanyDomesticWaybill.DestinationCountryId = companyReceiver != null ? companyReceiver.CountryId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationCountryId;
  //      //CompanyDomesticWaybill.DestinationProvinceId = companyReceiver != null ? companyReceiver.ProvinceId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationProvinceId;
  //      //CompanyDomesticWaybill.DestinationCityId = companyReceiver != null ? companyReceiver.CityId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationCityId;
  //      //CompanyDomesticWaybill.DestinationMunicipalAreaId = companyDomesticPathReceiver[0].MunicipalAreaId;
  //      //CompanyDomesticWaybill.DestinationLatitude = companyReceiver != null ? companyReceiver.City.Latitude : destinationCity.Latitude;
  //      //CompanyDomesticWaybill.DestinationLongitude = companyReceiver != null ? companyReceiver.City.Longitude : destinationCity.Longitude;
  //      //CompanyDomesticWaybill.InsuranceCompanyId = companyInsurance == null ? null : companyInsurance.Id;
  //      //CompanyDomesticWaybill.CompanySenderBankId = companyBank == null ? null : companyBank.Id; ;
  //      //CompanyDomesticWaybill.CustomerPanelId = user.Id;
  //      //CompanyDomesticWaybill.IsIssueFromCaptainCargoWebSite = false;
  //      //CompanyDomesticWaybill.IsIssueFromCompanyWebSite = false;
  //      //CompanyDomesticWaybill.IsIssueFromCaptainCargoWebService = false;
  //      //CompanyDomesticWaybill.IsIssueFromCompanyWebService = false;
  //      //CompanyDomesticWaybill.IsIssueFromCaptainCargoPanel = false;
  //      //CompanyDomesticWaybill.IsIssueFromCaptainCargoDesktop = true;
  //      //CompanyDomesticWaybill.CaptainCargoPrice = companySenderCommissions.CommissionFromCaptainCargoDesktop;
  //      //CompanyDomesticWaybill.CounterId = user.Id;
  //      //CompanyDomesticWaybill.Dirty = false;
  //      //CompanyDomesticWaybill.CompanyDomesticWaybillPackageTypes = insertCompanyDomesticWaybillFromDesktopCommand.CompanyDomesticWaybillPackageTypes.Select(
  //      //         item => new Domain.Entities.CompanyDomesticWaybillEntity.CompanyDomesticWaybillPackageType
  //      //         {
  //      //             ChargeableWeight = item.ChargeableWeight,
  //      //             CompanyContentTypeId = companyContentTypes.FirstOrDefault(cct => cct.CompanyContentTypeName.ToLower() == item.CompanyContentTypeName)?.Id ?? companyContentTypes.First().Id,
  //      //             CompanyPackageTypeId = companyPackageTypes.FirstOrDefault(cct => cct.CompanyPackageTypeName.ToLower() == item.CompanyPackageTypeName)?.Id ?? companyPackageTypes.First().Id,
  //      //             CountDimension = item.CountDimension,
  //      //             DeclaredValue = item.DeclaredValue,
  //      //             Dimensions = item.Dimensions,
  //      //             GrossWeight = item.GrossWeight,
  //      //             UserInsertedContentName = item.UserInsertedContentName,
  //      //         }).ToList();
  //      //
  //      //var CompanyDomesticWaybillId = await domesticWaybillRepository.InsertCompanyDomesticWaybillAsync(CompanyDomesticWaybill, cancellationToken);
  //      //
  //      //await unitOfWork.SaveChangesAsync(cancellationToken);
  //      //
  //      //logger.LogInformation("CompanyDomesticWaybillFromDesktop created successfully ID: {insertCompanyDomesticWaybillFromDesktopCommand}", domesticWaybill.Id);
  //      //return ApiResponse<int>.Ok(domesticWaybill.Id, "بارنامه با موفقیت ثبت شد");
  //
  //      return ApiResponse<int>.Ok(1, "بارنامه با موفقیت ثبت شد");
  //  }
////
////    public async Task<ApiResponse<CompanyDomesticWaybillDto>> UpdateCompanyDomesticWaybillFromDesktopAsync(UpdateCompanyDomesticWaybillFromDesktopCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("UpdateCompanyDomesticWaybillFromDesktop is Called with {@UpdateCompanyDomesticWaybillFromDesktopCommand}", command);
  //
  //      var companySender = await companyRepository.GetCompanyByCodeAsync(command.CompanySenderCaptain360Code, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "شرکت فرستنده وجود ندارد");
  //
  //      var sourceRegionMunicipalities = await areaRepository.GetAreasByParentIdAsync(companySender.CityId, cancellationToken);
  //      if (sourceRegionMunicipalities == null)
  //          return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "منطقه شهری مبدا تعریف نشده است");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (user.IsManager(companySender.Id))
  //          return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "مجوز این فعالیت را ندارید");
  //
  //      if (/*user.IsCridit   and */command.CompanySenderCreditPayment)
  //          return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "کاربر اعتباری نیست");
  //
  //      Company? companyReceiver = null;
  //      IReadOnlyList<CompanyDomesticPathReceiver>? companyDomesticPathReceiver = null;
  //      Area? destinationCity = null;
  //
  //      companyReceiver = await companyRepository.GetCompanyByCodeAsync(command.CompanyReceiverCaptain360Code, false, false, cancellationToken);
  //      if (companyReceiver != null)
  //      {
  //          companyDomesticPathReceiver = await companyDomesticPathReceiverCompanyRepository.GetCompanyDomesticPathReceiverByCompanySenderIdAndCompanyReceiverIdAsync(companySender.Id, companyReceiver.Id, false, true, cancellationToken);
  //          if (companyDomesticPathReceiver == null || companyDomesticPathReceiver.Count < 1)
  //              return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "نماینده نامعتبر است");
  //      }
  //      else
  //      {
  //          companyDomesticPathReceiver = await companyDomesticPathReceiverCompanyRepository.GetCompanyDomesticPathReceiverByCompanySenderIdAndCompanyReceiverCodeAsync(companySender.Id, command.CompanyReceiverCaptain360Code, false, true, cancellationToken);
  //          if (companyDomesticPathReceiver == null || companyDomesticPathReceiver.Count < 1)
  //              return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "نماینده نامعتبر است");
  //
  //          destinationCity = await areaRepository.GetAreaById(companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationCityId, cancellationToken);
  //      }
  //
  //      CompanyInsurance? companyInsurance = null;
  //      if (command.InsuranceCompanyCode != null)
  //      {
  //          companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceByCodeAsync(command.InsuranceCompanyCode, companySender.Id, false, false, cancellationToken);
  //          if (companyInsurance == null)
  //              return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "شرکت بیمه وجود ندارد");
  //      }
  //
  //      CompanyBank? companyBank = null;
  //      if (command.CompanySenderBankCode != null)
  //      {
  //          companyBank = await companyBankRepository.GetCompanyBankByCodeAsync(command.CompanySenderBankCode, companySender.Id, false, false, cancellationToken);
  //          if (companyBank == null)
  //              return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "شرکت بیمه وجود ندارد");
  //      }
  //
  //      var companyContentTypes = await companyContentTypeRepository.GetCompanyContentTypeByCompanyIdAsync(companySender.Id, false, false, cancellationToken);
  //      if (companyContentTypes == null)
  //          return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "شرکت بیمه وجود ندارد");
  //
  //      var companyPackageTypes = await companyPackageTypeRepository.GetCompanyPackageTypeByCompanyIdAsync(companySender.Id, false, false, cancellationToken);
  //      if (companyPackageTypes == null)
  //          return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "شرکت بیمه وجود ندارد");
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByNoAsync(command.No, false, true, false, false, cancellationToken);
  //      if (domesticWaybill == null)
  //          return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "بارنامه وجود ندارد");
  //
  //      if (domesticWaybill.CompanySenderId != companySender.Id)
  //          return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      if (/*!user.IsCridit   and */command.CompanySenderCreditPayment)
  //          return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "کاربر اعتباری نیست");
  //
  //      domesticWaybill.CompanyReceiverId = companyReceiver == null ? null : companyReceiver.Id;
  //      domesticWaybill.CompanyReceiverUserInsertedCode = companyReceiver != null ? null : command.CompanyReceiverCaptain360Code;
  //      domesticWaybill.StopCityName = command.StopCityName;
  //      domesticWaybill.DestinationCountryId = companyReceiver != null ? companyReceiver.CountryId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationCountryId;
  //      domesticWaybill.DestinationProvinceId = companyReceiver != null ? companyReceiver.ProvinceId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationProvinceId;
  //      domesticWaybill.DestinationCityId = companyReceiver != null ? companyReceiver.CityId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationCityId;
  //      domesticWaybill.DestinationMunicipalAreaId = companyDomesticPathReceiver[0].MunicipalAreaId;
  //      domesticWaybill.DestinationLatitude = companyReceiver != null ? companyReceiver.City.Latitude : destinationCity.Latitude;
  //      domesticWaybill.DestinationLongitude = companyReceiver != null ? companyReceiver.City.Longitude : destinationCity.Longitude;
  //      domesticWaybill.InsuranceCompanyId = companyInsurance == null ? null : companyInsurance.Id;
  //      domesticWaybill.GrossWeight = command.GrossWeight;
  //      domesticWaybill.DimensionalWeight = command.DimensionalWeight;
  //      domesticWaybill.ChargeableWeight = command.ChargeableWeight;
  //      domesticWaybill.WeightCount = command.WeightCount;
  //      domesticWaybill.Rate = command.Rate;
  //      domesticWaybill.CompanyDomesticWaybillTax = command.CompanyDomesticWaybillTax;
  //      domesticWaybill.ExitFare = command.ExitFare;
  //      domesticWaybill.ExitStampBill = command.ExitStampBill;
  //      domesticWaybill.ExitPackaging = command.ExitPackaging;
  //      domesticWaybill.ExitAccumulation = command.ExitAccumulation;
  //      domesticWaybill.ExitDistribution = command.ExitDistribution;
  //      domesticWaybill.ExitExtraSource = command.ExitExtraSource;
  //      domesticWaybill.ExitExtraDestination = command.ExitExtraDestination;
  //      domesticWaybill.ExitPricing = command.ExitPricing;
  //      domesticWaybill.ExitRevenue1 = command.ExitRevenue1;
  //      domesticWaybill.ExitRevenue2 = command.ExitRevenue2;
  //      domesticWaybill.ExitRevenue3 = command.ExitRevenue3;
  //      domesticWaybill.ExitTaxCompanySender = command.ExitTaxCompanySender;
  //      domesticWaybill.ExitTaxCompanyReceiver = command.ExitTaxCompanyReceiver;
  //      domesticWaybill.ExitInsuranceCost = command.ExitInsuranceCost;
  //      domesticWaybill.ExitTaxInsuranceCost = command.ExitTaxInsuranceCost;
  //      domesticWaybill.ExitInsuranceCostGain = command.ExitInsuranceCostGain;
  //      domesticWaybill.ExitTaxInsuranceCostGain = command.ExitTaxInsuranceCostGain;
  //      domesticWaybill.ExitDiscount = command.ExitDiscount;
  //      domesticWaybill.ExitTotalCost = command.ExitTotalCost;
  //      domesticWaybill.HandlingInformation = command.HandlingInformation;
  //      domesticWaybill.FlightNo = command.FlightNo;
  //      domesticWaybill.FlightDate = command.FlightDate;
  //      domesticWaybill.CompanySenderDateFinancial = command.CompanySenderDateFinancial;
  //      domesticWaybill.CompanySenderCashPayment = command.CompanySenderCashPayment;
  //      domesticWaybill.CompanySenderCashOnDelivery = command.CompanySenderCashOnDelivery;
  //      domesticWaybill.CompanySenderBankPayment = command.CompanySenderBankPayment;
  //      domesticWaybill.CompanySenderBankId = companyBank == null ? null : companyBank.Id; ;
  //      domesticWaybill.CompanySenderBankPaymentNo = command.CompanySenderBankPaymentNo;
  //      domesticWaybill.CompanySenderCreditPayment = command.CompanySenderCreditPayment;
  //      domesticWaybill.CustomerPanelId = user.Id;
  //      domesticWaybill.CustomerSenderNameFamily = command.CustomerSenderNameFamily;
  //      domesticWaybill.CustomerSenderMobile = command.CustomerSenderMobile;
  //      domesticWaybill.CustomerSenderAddress = command.CustomerSenderAddress;
  //      domesticWaybill.TypeOfFactorInSamanehMoadianId = command.TypeOfFactorInSamanehMoadianId;
  //      domesticWaybill.CustomerSenderNationalCode = command.CustomerSenderNationalCode;
  //      domesticWaybill.CustomerSenderEconomicCode = command.CustomerSenderEconomicCode;
  //      domesticWaybill.CustomerSenderNationalID = command.CustomerSenderNationalID;
  //      domesticWaybill.CustomerReceiverNameFamily = command.CustomerReceiverNameFamily;
  //      domesticWaybill.CustomerReceiverMobile = command.CustomerReceiverMobile;
  //      domesticWaybill.CustomerReceiverAddress = command.CustomerReceiverAddress;
  //      domesticWaybill.DescriptionSenderComapny = command.DescriptionSenderComapny;
  //      domesticWaybill.Lock = command.Lock;
  //      domesticWaybill.CounterId = user.Id;
  //      domesticWaybill.CompanyDomesticWaybillPackageTypes = command.CompanyDomesticWaybillPackageTypes.Select(
  //               item => new Domain.Entities.CompanyDomesticWaybillEntity.CompanyDomesticWaybillPackageType
  //               {
  //                   ChargeableWeight = item.ChargeableWeight,
  //                   CompanyContentTypeId = companyContentTypes.FirstOrDefault(cct => cct.CompanyContentTypeName.ToLower() == item.CompanyContentTypeName)?.Id ?? companyContentTypes.First().Id,
  //                   CompanyPackageTypeId = companyPackageTypes.FirstOrDefault(cct => cct.CompanyPackageTypeName.ToLower() == item.CompanyPackageTypeName)?.Id ?? companyPackageTypes.First().Id,
  //                   CountDimension = item.CountDimension,
  //                   DeclaredValue = item.DeclaredValue,
  //                   Dimensions = item.Dimensions,
  //                   GrossWeight = item.GrossWeight,
  //                   UserInsertedContentName = item.UserInsertedContentName,
  //               }).ToList();
  //
  //      if (domesticWaybill.State == (short)WaybillState.Issued ||
  //          domesticWaybill.State == (short)WaybillState.Collectiong ||
  //          domesticWaybill.State == (short)WaybillState.ReceivedAtSenderCompany ||
  //          domesticWaybill.State == (short)WaybillState.Manifested ||
  //          domesticWaybill.State == (short)WaybillState.AirlineDelivery)
  //      {
  //          domesticWaybill.Dirty = false;
  //      }
  //      else if (domesticWaybill.State == (short)WaybillState.ReceivedAtReceiverCompany ||
  //               domesticWaybill.State == (short)WaybillState.Distribution ||
  //               domesticWaybill.State == (short)WaybillState.Delivery)
  //      {
  //          domesticWaybill.Dirty = true;
  //      }
  //
  //      var updatedCompanyDomesticWaybill = mapper.Map(command, domesticWaybill);
  //      if (updatedCompanyDomesticWaybill == null)
  //          return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "مشکل در عملیات تبدیل");
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("CompanyDomesticWaybillFromDesktop created successfully ID: {insertCompanyDomesticWaybillFromDesktopCommand}", domesticWaybill.Id);
  //      var updatedCompanyDomesticWaybillDto = mapper.Map<CompanyDomesticWaybillDto>(updatedCompanyDomesticWaybill);
  //      return ApiResponse<CompanyDomesticWaybillDto>.Ok(updatedCompanyDomesticWaybillDto, "بارنامه با موفقیت ثبت شد");
  //  }
////
////    public async Task<ApiResponse<CompanyDomesticWaybillDto>> UpdateCompanyDomesticWaybillAsync(UpdateCompanyDomesticWaybillCommand command, CancellationToken cancellationToken)
  //  {
  //      //logger.LogInformation("UpdateCompanyDomesticWaybill is Called with {@UpdateCompanyDomesticWaybillCommand}", updateCompanyDomesticWaybillCommand);
  //      //
  //      //var companySender = await companyRepository.GetCompanyByCodeAsync(updateCompanyDomesticWaybillCommand.CompanySenderCaptain360Code, false, false, cancellationToken);
  //      //if (companySender == null)
  //      //    return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "شرکت فرستنده وجود ندارد");
  //      //
  //      //var sourceRegionMunicipalities = await areaRepository.GetAreasByParentIdAsync(companySender.CityId, cancellationToken);
  //      //if (sourceRegionMunicipalities == null)
  //      //    return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "منطقه شهری مبدا تعریف نشده است");
  //      //
  //      //var user = userContext.GetCurrentUser();
  //      //if (user == null)
  //      //        return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //      //
  //      //if (user.IsManager(companySender.Id))
  //      //    return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "مجوز این فعالیت را ندارید");
  //      //
  //      //if (/*user.IsCridit   and */updateCompanyDomesticWaybillCommand.CompanySenderCreditPayment)
  //      //    return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "کاربر اعتباری نیست");
  //      //
  //      //Company? companyReceiver = null;
  //      //IReadOnlyList<CompanyDomesticPathReceiver>? companyDomesticPathReceiver = null;
  //      //Area? destinationCity = null;
  //      //
  //      //companyReceiver = await companyRepository.GetCompanyByCodeAsync(updateCompanyDomesticWaybillCommand.CompanyReceiverCaptain360Code, false, false, cancellationToken);
  //      //if (companyReceiver != null)
  //      //{
  //      //    companyDomesticPathReceiver = await companyDomesticPathReceiverRepository.GetCompanyDomesticPathReceiverByCompanySenderIdAndCompanyReceiverIdAsync(companySender.Id, companyReceiver.Id, false, true, cancellationToken);
  //      //    if (companyDomesticPathReceiver == null || companyDomesticPathReceiver.Count < 1)
  //      //        return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "نماینده نامعتبر است");
  //      //}
  //      //else
  //      //{
  //      //    companyDomesticPathReceiver = await companyDomesticPathReceiverRepository.GetCompanyDomesticPathReceiverByCompanySenderIdAndCompanyReceiverCodeAsync(companySender.Id, updateCompanyDomesticWaybillCommand.CompanyReceiverCaptain360Code, false, true, cancellationToken);
  //      //    if (companyDomesticPathReceiver == null || companyDomesticPathReceiver.Count < 1)
  //      //        return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "نماینده نامعتبر است");
  //      //
  //      //    destinationCity = await areaRepository.GetAreaById(companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationCityId, cancellationToken);
  //      //}
  //      //
  //      //CompanyInsurance? companyInsurance = null;
  //      //if (updateCompanyDomesticWaybillCommand.InsuranceCompanyCode != null)
  //      //{
  //      //    companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceByCodeAsync(updateCompanyDomesticWaybillCommand.InsuranceCompanyCode, companySender.Id, false, false, cancellationToken);
  //      //    if (companyInsurance == null)
  //      //        return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "شرکت بیمه وجود ندارد");
  //      //}
  //      //
  //      //CompanyBank? companyBank = null;
  //      //if (updateCompanyDomesticWaybillCommand.CompanySenderBankCode != null)
  //      //{
  //      //    companyBank = await companyBankRepository.GetCompanyBankByCodeAsync(updateCompanyDomesticWaybillCommand.CompanySenderBankCode, companySender.Id, false, false, cancellationToken);
  //      //    if (companyBank == null)
  //      //        return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "شرکت بیمه وجود ندارد");
  //      //}
  //      //
  //      //var companyContentTypes = await companyContentTypeRepository.GetCompanyContentTypeByCompanyIdAsync(companySender.Id, false, false, cancellationToken);
  //      //if (companyContentTypes == null)
  //      //    return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "شرکت بیمه وجود ندارد");
  //      //
  //      //var companyPackageTypes = await companyPackageTypeRepository.GetCompanyPackageTypeByCompanyIdAsync(companySender.Id, false, false, cancellationToken);
  //      //if (companyPackageTypes == null)
  //      //    return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "شرکت بیمه وجود ندارد");
  //      //
  //      //var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByNoAsync(updateCompanyDomesticWaybillCommand.No, false, true, false, false, cancellationToken);
  //      //if (domesticWaybill == null)
  //      //    return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "بارنامه وجود ندارد");
  //      //
  //      //if (domesticWaybill.CompanySenderId != companySender.Id)
  //      //    return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //      //
  //      //if (/*!user.IsCridit   and */updateCompanyDomesticWaybillCommand.CompanySenderCreditPayment)
  //      //    return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "کاربر اعتباری نیست");
  //      //
  //      // domesticWaybill.CompanyReceiverId = companyReceiver == null ? null : companyReceiver.Id;
  //      //domesticWaybill.CompanyReceiverUserInsertedCode = companyReceiver != null ? null : updateCompanyDomesticWaybillCommand.CompanyReceiverCaptain360Code;
  //      //domesticWaybill.StopCityName = updateCompanyDomesticWaybillCommand.StopCityName;
  //      //domesticWaybill.DestinationCountryId = companyReceiver != null ? companyReceiver.CountryId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationCountryId;
  //      //domesticWaybill.DestinationProvinceId = companyReceiver != null ? companyReceiver.ProvinceId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationProvinceId;
  //      //domesticWaybill.DestinationCityId = companyReceiver != null ? companyReceiver.CityId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationCityId;
  //      //domesticWaybill.DestinationMunicipalAreaId = companyDomesticPathReceiver[0].MunicipalAreaId;
  //      //domesticWaybill.DestinationLatitude = companyReceiver != null ? companyReceiver.City.Latitude : destinationCity.Latitude;
  //      //domesticWaybill.DestinationLongitude = companyReceiver != null ? companyReceiver.City.Longitude : destinationCity.Longitude;
  //      //domesticWaybill.InsuranceCompanyId = companyInsurance == null ? null : companyInsurance.Id;
  //      //domesticWaybill.GrossWeight = updateCompanyDomesticWaybillCommand.GrossWeight;
  //      //domesticWaybill.DimensionalWeight = updateCompanyDomesticWaybillCommand.DimensionalWeight;
  //      //domesticWaybill.ChargeableWeight = updateCompanyDomesticWaybillCommand.ChargeableWeight;
  //      //domesticWaybill.WeightCount = updateCompanyDomesticWaybillCommand.WeightCount;
  //      //domesticWaybill.Rate = updateCompanyDomesticWaybillCommand.Rate;
  //      //domesticWaybill.CompanyDomesticWaybillTax = updateCompanyDomesticWaybillCommand.CompanyDomesticWaybillTax;
  //      //domesticWaybill.ExitFare = updateCompanyDomesticWaybillCommand.ExitFare;
  //      //domesticWaybill.ExitStampBill = updateCompanyDomesticWaybillCommand.ExitStampBill;
  //      //domesticWaybill.ExitPackaging = updateCompanyDomesticWaybillCommand.ExitPackaging;
  //      //domesticWaybill.ExitAccumulation = updateCompanyDomesticWaybillCommand.ExitAccumulation;
  //      //domesticWaybill.ExitDistribution = updateCompanyDomesticWaybillCommand.ExitDistribution;
  //      //domesticWaybill.ExitExtraSource = updateCompanyDomesticWaybillCommand.ExitExtraSource;
  //      //domesticWaybill.ExitExtraDestination = updateCompanyDomesticWaybillCommand.ExitExtraDestination;
  //      //domesticWaybill.ExitPricing = updateCompanyDomesticWaybillCommand.ExitPricing;
  //      //domesticWaybill.ExitRevenue1 = updateCompanyDomesticWaybillCommand.ExitRevenue1;
  //      //domesticWaybill.ExitRevenue2 = updateCompanyDomesticWaybillCommand.ExitRevenue2;
  //      //domesticWaybill.ExitRevenue3 = updateCompanyDomesticWaybillCommand.ExitRevenue3;
  //      //domesticWaybill.ExitTaxCompanySender = updateCompanyDomesticWaybillCommand.ExitTaxCompanySender;
  //      //domesticWaybill.ExitTaxCompanyReceiver = updateCompanyDomesticWaybillCommand.ExitTaxCompanyReceiver;
  //      //domesticWaybill.ExitInsuranceCost = updateCompanyDomesticWaybillCommand.ExitInsuranceCost;
  //      //domesticWaybill.ExitTaxInsuranceCost = updateCompanyDomesticWaybillCommand.ExitTaxInsuranceCost;
  //      //domesticWaybill.ExitInsuranceCostGain = updateCompanyDomesticWaybillCommand.ExitInsuranceCostGain;
  //      //domesticWaybill.ExitTaxInsuranceCostGain = updateCompanyDomesticWaybillCommand.ExitTaxInsuranceCostGain;
  //      //domesticWaybill.ExitDiscount = updateCompanyDomesticWaybillCommand.ExitDiscount;
  //      //domesticWaybill.ExitTotalCost = updateCompanyDomesticWaybillCommand.ExitTotalCost;
  //      //domesticWaybill.HandlingInformation = updateCompanyDomesticWaybillCommand.HandlingInformation;
  //      //domesticWaybill.FlightNo = updateCompanyDomesticWaybillCommand.FlightNo;
  //      //domesticWaybill.FlightDate = updateCompanyDomesticWaybillCommand.FlightDate;
  //      //domesticWaybill.CompanySenderDateFinancial = updateCompanyDomesticWaybillCommand.CompanySenderDateFinancial;
  //      //domesticWaybill.CompanySenderCashPayment = updateCompanyDomesticWaybillCommand.CompanySenderCashPayment;
  //      //domesticWaybill.CompanySenderCashOnDelivery = updateCompanyDomesticWaybillCommand.CompanySenderCashOnDelivery;
  //      //domesticWaybill.CompanySenderBankPayment = updateCompanyDomesticWaybillCommand.CompanySenderBankPayment;
  //      //domesticWaybill.CompanySenderBankId = companyBank == null ? null : companyBank.Id; ;
  //      //domesticWaybill.CompanySenderBankPaymentNo = updateCompanyDomesticWaybillCommand.CompanySenderBankPaymentNo;
  //      //domesticWaybill.CompanySenderCreditPayment = updateCompanyDomesticWaybillCommand.CompanySenderCreditPayment;
  //      //domesticWaybill.CustomerPanelId = user.Id;
  //      //domesticWaybill.CustomerSenderNameFamily = updateCompanyDomesticWaybillCommand.CustomerSenderNameFamily;
  //      //domesticWaybill.CustomerSenderMobile = updateCompanyDomesticWaybillCommand.CustomerSenderMobile;
  //      //domesticWaybill.CustomerSenderAddress = updateCompanyDomesticWaybillCommand.CustomerSenderAddress;
  //      //domesticWaybill.TypeOfFactorInSamanehMoadianId = updateCompanyDomesticWaybillCommand.TypeOfFactorInSamanehMoadianId;
  //      //domesticWaybill.CustomerSenderNationalCode = updateCompanyDomesticWaybillCommand.CustomerSenderNationalCode;
  //      //domesticWaybill.CustomerSenderEconomicCode = updateCompanyDomesticWaybillCommand.CustomerSenderEconomicCode;
  //      //domesticWaybill.CustomerSenderNationalID = updateCompanyDomesticWaybillCommand.CustomerSenderNationalID;
  //      //domesticWaybill.CustomerReceiverNameFamily = updateCompanyDomesticWaybillCommand.CustomerReceiverNameFamily;
  //      //domesticWaybill.CustomerReceiverMobile = updateCompanyDomesticWaybillCommand.CustomerReceiverMobile;
  //      //domesticWaybill.CustomerReceiverAddress = updateCompanyDomesticWaybillCommand.CustomerReceiverAddress;
  //      //domesticWaybill.DescriptionSenderComapny = updateCompanyDomesticWaybillCommand.DescriptionSenderComapny;
  //      //domesticWaybill.Lock = updateCompanyDomesticWaybillCommand.Lock;
  //      //domesticWaybill.CounterId = user.Id;
  //      //domesticWaybill.CompanyDomesticWaybillPackageTypes = updateCompanyDomesticWaybillCommand.CompanyDomesticWaybillPackageTypes.Select(
  //      //         item => new Domain.Entities.CompanyDomesticWaybillEntity.CompanyDomesticWaybillPackageType
  //      //         {
  //      //             ChargeableWeight = item.ChargeableWeight,
  //      //             CompanyContentTypeId = companyContentTypes.FirstOrDefault(cct => cct.CompanyContentTypeName.ToLower() == item.CompanyContentTypeName)?.Id ?? companyContentTypes.First().Id,
  //      //             CompanyPackageTypeId = companyPackageTypes.FirstOrDefault(cct => cct.CompanyPackageTypeName.ToLower() == item.CompanyPackageTypeName)?.Id ?? companyPackageTypes.First().Id,
  //      //             CountDimension = item.CountDimension,
  //      //             DeclaredValue = item.DeclaredValue,
  //      //             Dimensions = item.Dimensions,
  //      //             GrossWeight = item.GrossWeight,
  //      //             UserInsertedContentName = item.UserInsertedContentName,
  //      //         }).ToList();
  //      //
  //      //if (domesticWaybill.State == (short)WaybillState.Issued ||
  //      //    domesticWaybill.State == (short)WaybillState.Collectiong ||
  //      //    domesticWaybill.State == (short)WaybillState.ReceivedAtSenderCompany ||
  //      //    domesticWaybill.State == (short)WaybillState.Manifested ||
  //      //    domesticWaybill.State == (short)WaybillState.AirlineDelivery)
  //      //{
  //      //    domesticWaybill.Dirty = false;
  //      //}
  //      //else if (domesticWaybill.State == (short)WaybillState.ReceivedAtReceivercompany ||
  //      //         domesticWaybill.State == (short)WaybillState.Distribution ||
  //      //         domesticWaybill.State == (short)WaybillState.Delivery)
  //      //{
  //      //    domesticWaybill.Dirty = true;
  //      //}
  //      //
  //      //var updatedCompanyDomesticWaybill = mapper.Map(updateCompanyDomesticWaybillCommand, domesticWaybill);
  //      //if (updatedCompanyDomesticWaybill == null)
  //      //    return ApiResponse<CompanyDomesticWaybillDto>.Error(400, "مشکل در عملیات تبدیل");
  //      //
  //      //await unitOfWork.SaveChangesAsync(cancellationToken);
  //      //
  //      //logger.LogInformation("CompanyDomesticWaybill created successfully ID: {insertCompanyDomesticWaybillCommand}", domesticWaybill.Id);
  //      //var updatedCompanyDomesticWaybillDto = mapper.Map<CompanyDomesticWaybillDto>(updatedCompanyDomesticWaybill);
  //      //return ApiResponse<CompanyDomesticWaybillDto>.Ok(updatedCompanyDomesticWaybillDto, "بارنامه با موفقیت ثبت شد");
  //
  //      return ApiResponse<CompanyDomesticWaybillDto>.Ok(new CompanyDomesticWaybillDto(), "بارنامه با موفقیت ثبت شد");
  //  }
////
////    public async Task<ApiResponse<bool>> ChangeStateCompanyDomesticWaybillToCollectionAsync(ChangeStateCompanyDomesticWaybillToCollectionCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("ChangeStateCompanyDomesticWaybillToCollection Called with {@ChangeStateCompanyDomesticWaybillToCollectionCommand}", command);
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(command.Id, true, false, false, false, cancellationToken);
  //      if (domesticWaybill is null)
  //          return ApiResponse<bool>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (domesticWaybill.State != (short)WaybillState.Issued)
  //          return ApiResponse<bool>.Error(400, "بارنامه باید در وضعیت صادر شده باشد");
  //
  //      var companySender = await companyRepository.GetCompanyByIdAsync(domesticWaybill.CompanySenderId, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<bool>.Error(400, "مشکل در دریافت اطلاعات شرکت");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(companySender.CompanyTypeId) && !user.IsManager(companySender.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      domesticWaybill.State = (short)WaybillState.Collectiong;
  //      domesticWaybill.DateCollectiong = Tools.GetTodayInPersianDate();
  //      domesticWaybill.TimeCollectiong = Tools.GetTime();
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", command.Id);
  //      return ApiResponse<bool>.Ok(true);
  //  }
////
////    public async Task<ApiResponse<bool>> BackCompanyDomesticWaybillFromCollectionStateAsync(BackCompanyDomesticWaybillFromCollectiongStateCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("BackCompanyDomesticWaybillFormCollectiongState Called with {@BackCompanyDomesticWaybillFormCollectiongStateCommand}", command);
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(command.Id, true, false, false, false, cancellationToken);
  //      if (domesticWaybill is null)
  //          return ApiResponse<bool>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (domesticWaybill.State != (short)WaybillState.Collectiong)
  //          return ApiResponse<bool>.Error(400, "بارنامه باید در وضعیت در حال جمع آوری باشد");
  //
  //      var companySender = await companyRepository.GetCompanyByIdAsync(domesticWaybill.CompanySenderId, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<bool>.Error(400, "مشکل در دریافت اطلاعات شرکت");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(companySender.CompanyTypeId) && !user.IsManager(companySender.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      domesticWaybill.State = (short)WaybillState.Issued;
  //      domesticWaybill.DateCollectiong = null;
  //      domesticWaybill.TimeCollectiong = null;
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", command.Id);
  //      return ApiResponse<bool>.Ok(true);
  //  }
////
////    public async Task<ApiResponse<bool>> ChangeStateCompanyDomesticWaybillToReceivedAtSenderCompanyAsync(ChangeStateCompanyDomesticWaybillToReceivedAtSenderCompanyCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("BackCompanyDomesticWaybillFormCollectiongState Called with {@BackCompanyDomesticWaybillFormCollectiongStateCommand}", command);
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(command.Id, true, false, false, false, cancellationToken);
  //      if (domesticWaybill is null)
  //          return ApiResponse<bool>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (domesticWaybill.State != (short)WaybillState.Collectiong && domesticWaybill.State != (short)WaybillState.Issued)
  //          return ApiResponse<bool>.Error(400, "بارنامه باید در وضعیت در حال جمع آوری باشد");
  //
  //      var companySender = await companyRepository.GetCompanyByIdAsync(domesticWaybill.CompanySenderId, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<bool>.Error(400, "مشکل در دریافت اطلاعات شرکت");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(companySender.CompanyTypeId) && !user.IsManager(companySender.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      domesticWaybill.State = (short)WaybillState.ReceivedAtSenderCompany;
  //      domesticWaybill.DateReceivedAtSenderCompany = Tools.GetTodayInPersianDate();
  //      domesticWaybill.TimeReceivedAtSenderCompany = Tools.GetTime();
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", command.Id);
  //      return ApiResponse<bool>.Ok(true);
  //  }
////
////    public async Task<ApiResponse<bool>> BackCompanyDomesticWaybillFromReceivedAtSenderCompanyStateAsync(BackCompanyDomesticWaybillFromReceivedAtSenderCompanyStateCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("BackCompanyDomesticWaybillFormReceivedAtSenderCompanyState Called with {@BackCompanyDomesticWaybillFormReceivedAtSenderCompanyStateCommand}", command);
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(command.Id, true, false, false, false, cancellationToken);
  //      if (domesticWaybill is null)
  //          return ApiResponse<bool>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (domesticWaybill.State != (short)WaybillState.ReceivedAtSenderCompany)
  //          return ApiResponse<bool>.Error(400, "بارنامه باید در وضعیت در حال جمع آوری باشد");
  //
  //      var companySender = await companyRepository.GetCompanyByIdAsync(domesticWaybill.CompanySenderId, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<bool>.Error(400, "مشکل در دریافت اطلاعات شرکت");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(companySender.CompanyTypeId) && !user.IsManager(companySender.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      domesticWaybill.State = domesticWaybill.DateCollectiong != null ? (short)WaybillState.Collectiong : (short)WaybillState.Issued;
  //      domesticWaybill.DateReceivedAtSenderCompany = null;
  //      domesticWaybill.TimeReceivedAtSenderCompany = null;
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", command.Id);
  //      return ApiResponse<bool>.Ok(true);
  //  }
////
////    public async Task<ApiResponse<int>> AttachCompanyDomesticWaybillToCompanyManifestFormFromDesktopAsync(AttachCompanyDomesticWaybillToCompanyManifestFormFromDesktopCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("AddToCompanyManifestFormFromDesktop is Called with {@AddToCompanyManifestFormFromDesktop}", addToCompanyManifestFormFromDesktopCommand);
  //
  //      var companySender = await companyRepository.GetCompanyByCodeAsync(addToCompanyManifestFormFromDesktopCommand.CompanySenderCaptain360Code, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<int>.Error(400, "شرکت فرستنده وجود ندارد");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsManager(companySender.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByNoAsync(addToCompanyManifestFormFromDesktopCommand.No, false, true, false, false, cancellationToken);
  //      if (domesticWaybill == null)
  //          return ApiResponse<int>.Error(400, "بارنامه وجود ندارد");
  //
  //      if (domesticWaybill.CompanySenderId != companySender.Id)
  //          return ApiResponse<int>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByNoAsync(addToCompanyManifestFormFromDesktopCommand.CompanyManifestFormNo, false, true, false, false, cancellationToken);
  //      if (manifestForm == null)
  //          return ApiResponse<int>.Error(400, "بارنامه وجود ندارد");
  //
  //      if (manifestForm.CompanySenderId != companySender.Id)
  //          return ApiResponse<int>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      if (manifestForm.DestinationCityId != domesticWaybill.DestinationCityId)
  //          return ApiResponse<int>.Error(400, "مقصد بارنامه و مانیفست باید یکی باشد");
  //
  //      if (manifestForm.CompanyReceiverId == null && domesticWaybill.CompanyReceiverId != null ||
  //          manifestForm.CompanyReceiverId != null && domesticWaybill.CompanyReceiverId == null ||
  //          manifestForm.CompanyReceiverId != null && domesticWaybill.CompanyReceiverId != null && manifestForm.CompanyReceiverId != domesticWaybill.CompanyReceiverId ||
  //          manifestForm.CompanyReceiverId == null && domesticWaybill.CompanyReceiverId == null && manifestForm.CompanyReceiverUserInsertedCode != domesticWaybill.CompanyReceiverUserInsertedCode)
  //          return ApiResponse<int>.Error(400, "نماینده مقصد بارنامه و مانیفست باید یکی باشد");
  //
  //      domesticWaybill.CompanyManifestFormId = manifestForm.Id;
  //      domesticWaybill.DateManifested = addToCompanyManifestFormFromDesktopCommand.DateManifested;
  //      domesticWaybill.TimeManifested = addToCompanyManifestFormFromDesktopCommand.TimeManifested;
  //      domesticWaybill.State = (short)WaybillState.Manifested;
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("CompanyDomesticWaybillFromDesktop created successfully ID: {insertCompanyDomesticWaybillFromDesktopCommand}", domesticWaybill.Id);
  //      return ApiResponse<int>.Ok(domesticWaybill.Id, "بارنامه با موفقیت ثبت شد");
  //  }
////
////    public async Task<ApiResponse<int>> AttachCompanyDomesticWaybillToCompanyManifestFormAsync(AttachCompanyDomesticWaybillToCompanyManifestFormCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("AddToCompanyManifestForm is Called with {@AddToCompanyManifestForm}", addToCompanyManifestFormCommand);
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(addToCompanyManifestFormCommand.Id, false, true, false, false, cancellationToken);
  //      if (domesticWaybill == null)
  //          return ApiResponse<int>.Error(400, "بارنامه وجود ندارد");
  //
  //      var companySender = await companyRepository.GetCompanyByIdAsync(domesticWaybill.CompanySenderId, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<int>.Error(400, "شرکت فرستنده وجود ندارد");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(companySender.CompanyTypeId) && !user.IsManager(companySender.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByIdAsync(addToCompanyManifestFormCommand.CompanyManifestFormId, false, true, false, false, cancellationToken);
  //      if (manifestForm == null)
  //          return ApiResponse<int>.Error(400, "بارنامه وجود ندارد");
  //
  //      if (manifestForm.CompanySenderId != companySender.Id)
  //          return ApiResponse<int>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      if (manifestForm.DestinationCityId != domesticWaybill.DestinationCityId)
  //          return ApiResponse<int>.Error(400, "مقصد بارنامه و مانیفست باید یکی باشد");
  //
  //      if (manifestForm.CompanyReceiverId == null && domesticWaybill.CompanyReceiverId != null ||
  //          manifestForm.CompanyReceiverId != null && domesticWaybill.CompanyReceiverId == null ||
  //          manifestForm.CompanyReceiverId != null && domesticWaybill.CompanyReceiverId != null && manifestForm.CompanyReceiverId != domesticWaybill.CompanyReceiverId ||
  //          manifestForm.CompanyReceiverId == null && domesticWaybill.CompanyReceiverId == null && manifestForm.CompanyReceiverUserInsertedCode != domesticWaybill.CompanyReceiverUserInsertedCode)
  //          return ApiResponse<int>.Error(400, "نماینده مقصد بارنامه و مانیفست باید یکی باشد");
  //
  //      domesticWaybill.CompanyManifestFormId = manifestForm.Id;
  //      domesticWaybill.DateManifested = Tools.GetTodayInPersianDate();
  //      domesticWaybill.TimeManifested = Tools.GetTime();
  //      domesticWaybill.State = (short)WaybillState.Manifested;
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("CompanyDomesticWaybill created successfully ID: {insertCompanyDomesticWaybillCommand}", domesticWaybill.Id);
  //      return ApiResponse<int>.Ok(domesticWaybill.Id, "بارنامه با موفقیت ثبت شد");
  //  }
////
////    public async Task<ApiResponse<int>> BackCompanyDomesticWaybillFromCompanyManifestFormFromDesktopAsync(BackCompanyDomesticWaybillFromCompanyManifestFormFromDesktopCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("UpdateCompanyDomesticWaybillFromDesktop is Called with {@UpdateCompanyDomesticWaybillFromDesktopCommand}", backCompanyDomesticWaybillFromCompanyManifestFormFromDeskpCommand);
  //
  //      var companySender = await companyRepository.GetCompanyByCodeAsync(backCompanyDomesticWaybillFromCompanyManifestFormFromDeskpCommand.CompanySenderCaptain360Code, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<int>.Error(400, "شرکت فرستنده وجود ندارد");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsManager(companySender.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByNoAsync(backCompanyDomesticWaybillFromCompanyManifestFormFromDeskpCommand.No, false, true, false, false, cancellationToken);
  //      if (domesticWaybill == null)
  //          return ApiResponse<int>.Error(400, "بارنامه وجود ندارد");
  //
  //      if (domesticWaybill.CompanySenderId != companySender.Id)
  //          return ApiResponse<int>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      if (domesticWaybill.State == (short)WaybillState.Distribution ||
  //          domesticWaybill.State == (short)WaybillState.Delivery ||
  //          domesticWaybill.State == (short)WaybillState.ReceivedAtReceiverCompany)
  //          return ApiResponse<int>.Ok(domesticWaybill.Id, domesticWaybill.State == (short)WaybillState.Distribution ? "بارنامه در حال توزیع شدن است" :
  //                                                         domesticWaybill.State == (short)WaybillState.Delivery ? "بارنامه تحویل شده است است" :
  //                                                         domesticWaybill.State == (short)WaybillState.ReceivedAtReceiverCompany ? "بارنامه در مقصد است، ابتدا مانیفست باید برگشت داده شود" : "");
  //
  //      domesticWaybill.CompanyManifestFormId = null;
  //      domesticWaybill.DateManifested = null;
  //      domesticWaybill.TimeManifested = null;
  //      domesticWaybill.State = domesticWaybill.DateReceivedAtSenderCompany != null ? (short)WaybillState.ReceivedAtSenderCompany : (short)WaybillState.Issued;
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("CompanyDomesticWaybillFromDesktop created successfully ID: {insertCompanyDomesticWaybillFromDesktopCommand}", domesticWaybill.Id);
  //      return ApiResponse<int>.Ok(domesticWaybill.Id, "بارنامه با موفقیت ثبت شد");
  //  }
////
////    public async Task<ApiResponse<int>> BackCompanyDomesticWaybillFromCompanyManifestFormAsync(BackCompanyDomesticWaybillFromCompanyManifestFormCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("UpdateCompanyDomesticWaybill is Called with {@UpdateCompanyDomesticWaybillCommand}", command);
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(command.Id, false, true, false, false, cancellationToken);
  //      if (domesticWaybill == null)
  //          return ApiResponse<int>.Error(400, "بارنامه وجود ندارد");
  //
  //      if (domesticWaybill.State == (short)WaybillState.Distribution ||
  //          domesticWaybill.State == (short)WaybillState.Delivery ||
  //          domesticWaybill.State == (short)WaybillState.ReceivedAtReceiverCompany)
  //          return ApiResponse<int>.Ok(domesticWaybill.Id, domesticWaybill.State == (short)WaybillState.Distribution ? "بارنامه در حال توزیع شدن است" :
  //                                                         domesticWaybill.State == (short)WaybillState.Delivery ? "بارنامه تحویل شده است است" :
  //                                                         domesticWaybill.State == (short)WaybillState.ReceivedAtReceiverCompany ? "بارنامه در مقصد است، ابتدا مانیفست باید برگشت داده شود" : "");
  //
  //      var companySender = await companyRepository.GetCompanyByIdAsync(domesticWaybill.CompanySenderId, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<int>.Error(400, "شرکت فرستنده وجود ندارد");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(companySender.CompanyTypeId) && !user.IsManager(companySender.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      domesticWaybill.CompanyManifestFormId = null;
  //      domesticWaybill.DateManifested = null;
  //      domesticWaybill.TimeManifested = null;
  //      domesticWaybill.State = domesticWaybill.DateReceivedAtSenderCompany != null ? (short)WaybillState.ReceivedAtSenderCompany : (short)WaybillState.Issued;
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("CompanyDomesticWaybill created successfully ID: {insertCompanyDomesticWaybillCommand}", domesticWaybill.Id);
  //      return ApiResponse<int>.Ok(domesticWaybill.Id, "بارنامه با موفقیت ثبت شد");
  //  }
////
////    public async Task<ApiResponse<bool>> ChangeStateCompanyDomesticWaybillToDistributionAsync(ChangeStateCompanyDomesticWaybillToDistributionCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("ChangeStateCompanyDomesticWaybillToDistribution Called with {@ChangeStateCompanyDomesticWaybillToDistributionCommand}", command);
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(command.Id, true, false, false, false, cancellationToken);
  //      if (domesticWaybill is null)
  //          return ApiResponse<bool>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (domesticWaybill.State != (short)WaybillState.ReceivedAtReceiverCompany)
  //          return ApiResponse<bool>.Error(400, "بارنامه باید در وضعیت صادر شده باشد");
  //
  //      var companySender = await companyRepository.GetCompanyByIdAsync(domesticWaybill.CompanySenderId, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<bool>.Error(400, "مشکل در دریافت اطلاعات شرکت");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(companySender.CompanyTypeId) && !user.IsManager(companySender.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      domesticWaybill.State = (short)WaybillState.Distribution;
  //      domesticWaybill.DateDistribution = Tools.GetTodayInPersianDate();
  //      domesticWaybill.TimeDistribution = Tools.GetTime();
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", command.Id);
  //      return ApiResponse<bool>.Ok(true);
  //  }
////
////    public async Task<ApiResponse<bool>> BackCompanyDomesticWaybillFromDistributionStateAsync(BackCompanyDomesticWaybillFromDistributionStateCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("BackCompanyDomesticWaybillFormDistributionState Called with {@BackCompanyDomesticWaybillFormDistributionStateCommand}", command);
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(command.Id, true, false, false, false, cancellationToken);
  //      if (domesticWaybill is null)
  //          return ApiResponse<bool>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (domesticWaybill.State != (short)
  //          WaybillState.Distribution)
  //          return ApiResponse<bool>.Error(400, "بارنامه باید در وضعیت در حال جمع آوری باشد");
  //
  //      var companySender = await companyRepository.GetCompanyByIdAsync(domesticWaybill.CompanySenderId, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<bool>.Error(400, "مشکل در دریافت اطلاعات شرکت");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(companySender.CompanyTypeId) && !user.IsManager(companySender.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      domesticWaybill.State = (short)WaybillState.ReceivedAtReceiverCompany;
  //      domesticWaybill.DateDistribution = null;
  //      domesticWaybill.TimeDistribution = null;
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", command.Id);
  //      return ApiResponse<bool>.Ok(true);
  //  }
////
////    public async Task<ApiResponse<bool>> ChangeStateCompanyDomesticWaybillToDeliveryAsync(ChangeStateCompanyDomesticWaybillToDeliveryCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("ChangeStateCompanyDomesticWaybillToDelivery Called with {@ChangeStateCompanyDomesticWaybillToDeliveryCommand}", command);
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(command.Id, true, false, false, false, cancellationToken);
  //      if (domesticWaybill is null)
  //          return ApiResponse<bool>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (domesticWaybill.CompanyReceiverId == null)
  //          return ApiResponse<bool>.Error(400, "شرکت معتبر نیست");
  //
  //      if (domesticWaybill.State != (short)WaybillState.ReceivedAtReceiverCompany && domesticWaybill.State != (short)WaybillState.Distribution)
  //          return ApiResponse<bool>.Error(400, "بارنامه باید در وضعیت صادر شده باشد");
  //
  //      var companyReceiver = await companyRepository.GetCompanyByIdAsync((int)domesticWaybill.CompanyReceiverId, false, false, cancellationToken);
  //      if (companyReceiver == null)
  //          return ApiResponse<bool>.Error(400, "مشکل در دریافت اطلاعات شرکت");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(companyReceiver.CompanyTypeId) && !user.IsManager(companyReceiver.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      domesticWaybill.State = (short)WaybillState.Delivery;
  //      domesticWaybill.DateDelivery = Tools.GetTodayInPersianDate();
  //      domesticWaybill.TimeDelivery = Tools.GetTime();
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", command.Id);
  //      return ApiResponse<bool>.Ok(true);
  //  }
////
////    public async Task<ApiResponse<bool>> ChangeStateCompanyDomesticWaybillToDeliveryFromDesktopAsync(ChangeStateCompanyDomesticWaybillToDeliveryFromDesktopCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("ChangeStateCompanyDomesticWaybillToDeliveryFromDesktop Called with {@ChangeStateCompanyDomesticWaybillToDeliveryFromDesktopCommand}", command);
  //
  //      var companyReceiver = await companyRepository.GetCompanyByCodeAsync(command.CompanyReceiverCaptain360Code, false, false, cancellationToken);
  //      if (companyReceiver == null)
  //          return ApiResponse<bool>.Error(400, "شرکت فرستنده وجود ندارد");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsManager(companyReceiver.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByNoAsync(command.No, false, true, false, false, cancellationToken);
  //      if (domesticWaybill == null)
  //          return ApiResponse<bool>.Error(400, "بارنامه وجود ندارد");
  //
  //      if (domesticWaybill.CompanyReceiverId == null || domesticWaybill.CompanyReceiverId != companyReceiver.Id)
  //          return ApiResponse<bool>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      if (domesticWaybill.State != (short)WaybillState.ReceivedAtReceiverCompany && domesticWaybill.State != (short)WaybillState.Distribution)
  //          return ApiResponse<bool>.Error(400, "بارنامه باید در وضعیت صادر شده باشد");
  //
  //      domesticWaybill.State = (short)WaybillState.Delivery;
  //      domesticWaybill.DateDelivery = Tools.GetTodayInPersianDate();
  //      domesticWaybill.TimeDelivery = Tools.GetTime();
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", domesticWaybill.Id);
  //      return ApiResponse<bool>.Ok(true);
  //  }
////
////    public async Task<ApiResponse<bool>> BackCompanyDomesticWaybillFormDeliveryStateAsync(BackCompanyDomesticWaybillFormDeliveryStateCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("BackCompanyDomesticWaybillFormDeliveryState Called with {@BackCompanyDomesticWaybillFormDeliveryStateCommand}", command);
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(command.Id, true, false, false, false, cancellationToken);
  //      if (domesticWaybill is null)
  //          return ApiResponse<bool>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (domesticWaybill.CompanyReceiverId == null)
  //          return ApiResponse<bool>.Error(400, "شرکت معتبر نیست");
  //
  //      if (domesticWaybill.State != (short)WaybillState.Delivery)
  //          return ApiResponse<bool>.Error(400, "بارنامه باید در وضعیت صادر شده باشد");
  //
  //      var companyReceiver = await companyRepository.GetCompanyByIdAsync((int)domesticWaybill.CompanyReceiverId, false, false, cancellationToken);
  //      if (companyReceiver == null)
  //          return ApiResponse<bool>.Error(400, "مشکل در دریافت اطلاعات شرکت");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(companyReceiver.CompanyTypeId) && !user.IsManager(companyReceiver.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      if (domesticWaybill.State != (short)WaybillState.Delivery)
  //          return ApiResponse<bool>.Error(400, "بارنامه باید در وضعیت در حال جمع آوری باشد");
  //
  //      domesticWaybill.State = domesticWaybill.DateDistribution != null ? (short)WaybillState.Distribution : (short)WaybillState.ReceivedAtReceiverCompany;
  //      domesticWaybill.DateDelivery = null;
  //      domesticWaybill.TimeDelivery = null;
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", command.Id);
  //      return ApiResponse<bool>.Ok(true);
  //  }
////
////    public async Task<ApiResponse<bool>> BackCompanyDomesticWaybillFormDeliveryStateFromDesktopAsync(BackCompanyDomesticWaybillFormDeliveryStateFromDesktopCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("BackCompanyDomesticWaybillFormDeliveryFromDesktopState Called with {@BackCompanyDomesticWaybillFormDeliveryFromDesktopStateCommand}", command);
  //
  //      var companyReceiver = await companyRepository.GetCompanyByCodeAsync(command.CompanyReceiverCaptain360Code, false, false, cancellationToken);
  //      if (companyReceiver == null)
  //          return ApiResponse<bool>.Error(400, "شرکت فرستنده وجود ندارد");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsManager(companyReceiver.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByNoAsync(command.No, false, true, false, false, cancellationToken);
  //      if (domesticWaybill == null)
  //          return ApiResponse<bool>.Error(400, "بارنامه وجود ندارد");
  //
  //      if (domesticWaybill.CompanyReceiverId == null || domesticWaybill.CompanyReceiverId != companyReceiver.Id)
  //          return ApiResponse<bool>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      if (domesticWaybill.State != (short)WaybillState.Delivery)
  //          return ApiResponse<bool>.Error(400, "بارنامه باید در وضعیت صادر شده باشد");
  //
  //      domesticWaybill.State = domesticWaybill.DateDistribution != null ? (short)WaybillState.Distribution : (short)WaybillState.ReceivedAtReceiverCompany;
  //      domesticWaybill.DateDelivery = null;
  //      domesticWaybill.TimeDelivery = null;
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", domesticWaybill.Id);
  //      return ApiResponse<bool>.Ok(true);
  //
  //  }
////
////    public async Task<ApiResponse<int>> BackCompanyDomesticWaybillToReadyStateFromDesktopAsync(BackCompanyDomesticWaybillToReadyStateFromDesktopCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("UpdateCompanyDomesticWaybillFromDesktop is Called with {@UpdateCompanyDomesticWaybillFromDesktopCommand}", backToReadyCompanyDomesticWaybillFromDesktopCommand);
  //
  //      var companySender = await companyRepository.GetCompanyByCodeAsync(backToReadyCompanyDomesticWaybillFromDesktopCommand.CompanySenderCaptain360Code, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<int>.Error(400, "شرکت فرستنده وجود ندارد");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (user.IsManager(companySender.Id))
  //          return ApiResponse<int>.Error(400, "مجوز این فعالیت را ندارید");
  //
  //      var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByNoAsync(backToReadyCompanyDomesticWaybillFromDesktopCommand.No, false, true, false, false, cancellationToken);
  //      if (domesticWaybill == null)
  //          return ApiResponse<int>.Error(400, "بارنامه وجود ندارد");
  //
  //      if (domesticWaybill.CompanySenderId != companySender.Id)
  //          return ApiResponse<int>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      domesticWaybill.CompanyManifestFormId = null;
  //      domesticWaybill.DateManifested = null;
  //      domesticWaybill.TimeManifested = null;
  //
  //
  //      if (domesticWaybill.State == (short)WaybillState.Issued ||
  //          domesticWaybill.State == (short)WaybillState.Collectiong ||
  //          domesticWaybill.State == (short)WaybillState.ReceivedAtSenderCompany ||
  //          domesticWaybill.State == (short)WaybillState.Manifested ||
  //          domesticWaybill.State == (short)WaybillState.AirlineDelivery)
  //      {
  //          if ((bool)domesticWaybill.IsIssueFromCaptainCargoDesktop)
  //          {
  //              await domesticWaybillRepository.DeleteCompanyDomesticWaybillAsync(domesticWaybill.Id, cancellationToken);
  //              return ApiResponse<int>.Ok(domesticWaybill.Id, "بارنامه با موفقیت ثبت شد");
  //          }
  //          else
  //          {
  //              domesticWaybill.CompanyReceiverId = null;
  //              domesticWaybill.CompanyReceiverUserInsertedCode = null;
  //              domesticWaybill.SourceCountryId = null;
  //              domesticWaybill.SourceProvinceId = null;
  //              domesticWaybill.SourceCityId = null;
  //              domesticWaybill.SourceMunicipalAreaId = null;
  //              domesticWaybill.SourceLatitude = null;
  //              domesticWaybill.SourceLongitude = null;
  //              domesticWaybill.StopCityName = null;
  //              domesticWaybill.DestinationCountryId = null;
  //              domesticWaybill.DestinationProvinceId = null;
  //              domesticWaybill.DestinationCityId = null;
  //              domesticWaybill.DestinationMunicipalAreaId = null;
  //              domesticWaybill.DestinationLatitude = null;
  //              domesticWaybill.DestinationLongitude = null;
  //              domesticWaybill.CompanyDomesticWaybillPeriodId = null;
  //              domesticWaybill.CompanyManifestFormId = null;
  //              domesticWaybill.InsuranceCompanyId = null;
  //              domesticWaybill.GrossWeight = null;
  //              domesticWaybill.DimensionalWeight = null;
  //              domesticWaybill.ChargeableWeight = null;
  //              domesticWaybill.WeightCount = null;
  //              domesticWaybill.Rate = null;
  //              domesticWaybill.CompanyDomesticWaybillTax = null;
  //              domesticWaybill.ExitFare = null;
  //              domesticWaybill.ExitStampBill = null;
  //              domesticWaybill.ExitPackaging = null;
  //              domesticWaybill.ExitAccumulation = null;
  //              domesticWaybill.ExitDistribution = null;
  //              domesticWaybill.ExitExtraSource = null;
  //              domesticWaybill.ExitExtraDestination = null;
  //              domesticWaybill.ExitPricing = null;
  //              domesticWaybill.ExitRevenue1 = null;
  //              domesticWaybill.ExitRevenue2 = null;
  //              domesticWaybill.ExitRevenue3 = null;
  //              domesticWaybill.ExitTaxCompanySender = null;
  //              domesticWaybill.ExitTaxCompanyReceiver = null;
  //              domesticWaybill.ExitInsuranceCost = null;
  //              domesticWaybill.ExitTaxInsuranceCost = null;
  //              domesticWaybill.ExitInsuranceCostGain = null;
  //              domesticWaybill.ExitTaxInsuranceCostGain = null;
  //              domesticWaybill.ExitDiscount = null;
  //              domesticWaybill.ExitTotalCost = null;
  //              domesticWaybill.HandlingInformation = null;
  //              domesticWaybill.FlightNo = null;
  //              domesticWaybill.FlightDate = null;
  //              domesticWaybill.CompanySenderDateFinancial = null;
  //              domesticWaybill.CompanySenderCashPayment = null;
  //              domesticWaybill.CompanySenderCashOnDelivery = null;
  //              domesticWaybill.CompanySenderBankPayment = null;
  //              domesticWaybill.CompanySenderBankId = null;
  //              domesticWaybill.CompanySenderBankPaymentNo = null;
  //              domesticWaybill.CompanySenderCreditPayment = null;
  //              domesticWaybill.CustomerPanelId = null;
  //              domesticWaybill.CompanyReceiverDateFinancial = null;
  //              domesticWaybill.CompanyReceiverCashPayment = null;
  //              domesticWaybill.CompanyReceiverBankPayment = null;
  //              domesticWaybill.CompanyReceiverCashOnDelivery = null;
  //              domesticWaybill.CompanyReceiverBankId = null;
  //              domesticWaybill.CompanyReceiverBankPaymentNo = null;
  //              domesticWaybill.CompanyReceiverCreditPayment = null;
  //              domesticWaybill.CompanyReceiverResponsibleCustomerId = null;
  //              domesticWaybill.CustomerSenderNameFamily = null;
  //              domesticWaybill.CustomerSenderMobile = null;
  //              domesticWaybill.CustomerSenderAddress = null;
  //              domesticWaybill.TypeOfFactorInSamanehMoadianId = null;
  //              domesticWaybill.CustomerSenderNationalCode = null;
  //              domesticWaybill.CustomerSenderEconomicCode = null;
  //              domesticWaybill.CustomerSenderNationalID = null;
  //              domesticWaybill.CustomerReceiverNameFamily = null;
  //              domesticWaybill.CustomerReceiverMobile = null;
  //              domesticWaybill.CustomerReceiverAddress = null;
  //              domesticWaybill.State = null;
  //              domesticWaybill.DateIssued = null;
  //              domesticWaybill.TimeIssued = null;
  //              domesticWaybill.DateCollectiong = null;
  //              domesticWaybill.TimeCollectiong = null;
  //              domesticWaybill.BikeDeliveryInSenderCompanyId = null;
  //              domesticWaybill.BikeDeliveryInSenderCompanyAgent = null;
  //              domesticWaybill.DateReceivedAtSenderCompany = null;
  //              domesticWaybill.TimeReceivedAtSenderCompany = null;
  //              domesticWaybill.DateManifested = null;
  //              domesticWaybill.TimeManifested = null;
  //              domesticWaybill.DateAirlineDelivery = null;
  //              domesticWaybill.TimeAirlineDelivery = null;
  //              domesticWaybill.DateReceivedAtReceiverCompany = null;
  //              domesticWaybill.TimeReceivedAtReceiverCompany = null;
  //              domesticWaybill.DateDistribution = null;
  //              domesticWaybill.TimeDistribution = null;
  //              domesticWaybill.BikeDeliveryInReceiverCompanyId = null;
  //              domesticWaybill.BikeDeliveryInReceiverCompanyAgent = null;
  //              domesticWaybill.DateDelivery = null;
  //              domesticWaybill.TimeDelivery = null;
  //              domesticWaybill.EntranceDeliveryPerson = null;
  //              domesticWaybill.EntranceTransfereePersonName = null;
  //              domesticWaybill.EntranceTransfereePersonNationalCode = null;
  //              domesticWaybill.DescriptionSenderComapny = null;
  //              domesticWaybill.DescriptionReceiverCompany = null;
  //              domesticWaybill.DescriptionSenderCustomer = null;
  //              domesticWaybill.DescriptionReceiverCustomer = null;
  //              domesticWaybill.Lock = null;
  //              domesticWaybill.IsIssueFromCaptainCargoWebSite = null;
  //              domesticWaybill.IsIssueFromCompanyWebSite = null;
  //              domesticWaybill.IsIssueFromCaptainCargoWebService = null;
  //              domesticWaybill.IsIssueFromCompanyWebService = null;
  //              domesticWaybill.IsIssueFromCaptainCargoPanel = null;
  //              domesticWaybill.IsIssueFromCaptainCargoDesktop = null;
  //              domesticWaybill.CaptainCargoPrice = null;
  //              domesticWaybill.CounterId = null;
  //              domesticWaybill.Dirty = null;
  //
  //              var updatedCompanyDomesticWaybill = mapper.Map(backToReadyCompanyDomesticWaybillFromDesktopCommand, domesticWaybill);
  //              if (updatedCompanyDomesticWaybill == null)
  //                  return ApiResponse<int>.Error(400, "مشکل در عملیات تبدیل");
  //
  //              await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //              logger.LogInformation("CompanyDomesticWaybillFromDesktop created successfully ID: {insertCompanyDomesticWaybillFromDesktopCommand}", domesticWaybill.Id);
  //              return ApiResponse<int>.Ok(domesticWaybill.Id, "بارنامه با موفقیت ثبت شد");
  //          }
  //      }
  //      else if (domesticWaybill.State == (short)WaybillState.ReceivedAtReceiverCompany)
  //      {
  //          return ApiResponse<int>.Ok(domesticWaybill.Id, "بارنامه ابتدا باید از مانیفست خارج شود تا بتواند خام شود");
  //      }
  //      else if (domesticWaybill.State == (short)WaybillState.Distribution ||
  //               domesticWaybill.State == (short)WaybillState.Delivery)
  //      {
  //          return ApiResponse<int>.Ok(domesticWaybill.Id, "بارنامه در حال توزیع یا تحویل شده است");
  //      }
  //      else
  //      {
  //          return ApiResponse<int>.Ok(domesticWaybill.Id, "نتیجه نامشخص");
  //      }
  //  }
////
////    public async Task<ApiResponse<bool>> BackCompanyDomesticWaybillToReayStateAsync(BackCompanyDomesticWaybillToReayStateCommand command, CancellationToken cancellationToken)
   // {
   //     logger.LogInformation("BackToReayState is Called with {@BackToReayStateCommand}", backToReayStateCommand);
   //
   //     var domesticWaybill = await domesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(backToReayStateCommand.Id, false, true, false, false, cancellationToken);
   //     if (domesticWaybill == null)
   //         return ApiResponse<bool>.Error(400, "بارنامه وجود ندارد");
   //
   //     var companySender = await companyRepository.GetCompanyByIdAsync(domesticWaybill.CompanySenderId, false, false, cancellationToken);
   //     if (companySender == null)
   //         return ApiResponse<bool>.Error(400, "شرکت فرستنده وجود ندارد");
   //
   //     var user = userContext.GetCurrentUser();
   //     if (user == null)
   //         return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
   //
   //     if (!user.IsSuperAdmin() && !user.IsSuperManager(companySender.CompanyTypeId) && !user.IsManager(companySender.Id))
   //         return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
   //
   //     domesticWaybill.CompanyManifestFormId = null;
   //     domesticWaybill.DateManifested = null;
   //     domesticWaybill.TimeManifested = null;
   //
   //     if (domesticWaybill.State == (short)WaybillState.Issued ||
   //         domesticWaybill.State == (short)WaybillState.Collectiong ||
   //         domesticWaybill.State == (short)WaybillState.ReceivedAtSenderCompany ||
   //         domesticWaybill.State == (short)WaybillState.Manifested ||
   //         domesticWaybill.State == (short)WaybillState.AirlineDelivery)
   //     {
   //         if ((bool)domesticWaybill.IsIssueFromCaptainCargoDesktop)
   //         {
   //             await domesticWaybillRepository.DeleteCompanyDomesticWaybillAsync(domesticWaybill.Id, cancellationToken);
   //             return ApiResponse<bool>.Ok(true, "بارنامه با موفقیت ثبت شد");
   //         }
   //         else
   //         {
   //             domesticWaybill.CompanyReceiverId = null;
   //             domesticWaybill.CompanyReceiverUserInsertedCode = null;
   //             domesticWaybill.SourceCountryId = null;
   //             domesticWaybill.SourceProvinceId = null;
   //             domesticWaybill.SourceCityId = null;
   //             domesticWaybill.SourceMunicipalAreaId = null;
   //             domesticWaybill.SourceLatitude = null;
   //             domesticWaybill.SourceLongitude = null;
   //             domesticWaybill.StopCityName = null;
   //             domesticWaybill.DestinationCountryId = null;
   //             domesticWaybill.DestinationProvinceId = null;
   //             domesticWaybill.DestinationCityId = null;
   //             domesticWaybill.DestinationMunicipalAreaId = null;
   //             domesticWaybill.DestinationLatitude = null;
   //             domesticWaybill.DestinationLongitude = null;
   //             domesticWaybill.CompanyDomesticWaybillPeriodId = null;
   //             domesticWaybill.CompanyManifestFormId = null;
   //             domesticWaybill.InsuranceCompanyId = null;
   //             domesticWaybill.GrossWeight = null;
   //             domesticWaybill.DimensionalWeight = null;
   //             domesticWaybill.ChargeableWeight = null;
   //             domesticWaybill.WeightCount = null;
   //             domesticWaybill.Rate = null;
   //             domesticWaybill.CompanyDomesticWaybillTax = null;
   //             domesticWaybill.ExitFare = null;
   //             domesticWaybill.ExitStampBill = null;
   //             domesticWaybill.ExitPackaging = null;
   //             domesticWaybill.ExitAccumulation = null;
   //             domesticWaybill.ExitDistribution = null;
   //             domesticWaybill.ExitExtraSource = null;
   //             domesticWaybill.ExitExtraDestination = null;
   //             domesticWaybill.ExitPricing = null;
   //             domesticWaybill.ExitRevenue1 = null;
   //             domesticWaybill.ExitRevenue2 = null;
   //             domesticWaybill.ExitRevenue3 = null;
   //             domesticWaybill.ExitTaxCompanySender = null;
   //             domesticWaybill.ExitTaxCompanyReceiver = null;
   //             domesticWaybill.ExitInsuranceCost = null;
   //             domesticWaybill.ExitTaxInsuranceCost = null;
   //             domesticWaybill.ExitInsuranceCostGain = null;
   //             domesticWaybill.ExitTaxInsuranceCostGain = null;
   //             domesticWaybill.ExitDiscount = null;
   //             domesticWaybill.ExitTotalCost = null;
   //             domesticWaybill.HandlingInformation = null;
   //             domesticWaybill.FlightNo = null;
   //             domesticWaybill.FlightDate = null;
   //             domesticWaybill.CompanySenderDateFinancial = null;
   //             domesticWaybill.CompanySenderCashPayment = null;
   //             domesticWaybill.CompanySenderCashOnDelivery = null;
   //             domesticWaybill.CompanySenderBankPayment = null;
   //             domesticWaybill.CompanySenderBankId = null;
   //             domesticWaybill.CompanySenderBankPaymentNo = null;
   //             domesticWaybill.CompanySenderCreditPayment = null;
   //             domesticWaybill.CustomerPanelId = null;
   //             domesticWaybill.CompanyReceiverDateFinancial = null;
   //             domesticWaybill.CompanyReceiverCashPayment = null;
   //             domesticWaybill.CompanyReceiverBankPayment = null;
   //             domesticWaybill.CompanyReceiverCashOnDelivery = null;
   //             domesticWaybill.CompanyReceiverBankId = null;
   //             domesticWaybill.CompanyReceiverBankPaymentNo = null;
   //             domesticWaybill.CompanyReceiverCreditPayment = null;
   //             domesticWaybill.CompanyReceiverResponsibleCustomerId = null;
   //             domesticWaybill.CustomerSenderNameFamily = null;
   //             domesticWaybill.CustomerSenderMobile = null;
   //             domesticWaybill.CustomerSenderAddress = null;
   //             domesticWaybill.TypeOfFactorInSamanehMoadianId = null;
   //             domesticWaybill.CustomerSenderNationalCode = null;
   //             domesticWaybill.CustomerSenderEconomicCode = null;
   //             domesticWaybill.CustomerSenderNationalID = null;
   //             domesticWaybill.CustomerReceiverNameFamily = null;
   //             domesticWaybill.CustomerReceiverMobile = null;
   //             domesticWaybill.CustomerReceiverAddress = null;
   //             domesticWaybill.State = null;
   //             domesticWaybill.DateIssued = null;
   //             domesticWaybill.TimeIssued = null;
   //             domesticWaybill.DateCollectiong = null;
   //             domesticWaybill.TimeCollectiong = null;
   //             domesticWaybill.BikeDeliveryInSenderCompanyId = null;
   //             domesticWaybill.BikeDeliveryInSenderCompanyAgent = null;
   //             domesticWaybill.DateReceivedAtSenderCompany = null;
   //             domesticWaybill.TimeReceivedAtSenderCompany = null;
   //             domesticWaybill.DateManifested = null;
   //             domesticWaybill.TimeManifested = null;
   //             domesticWaybill.DateAirlineDelivery = null;
   //             domesticWaybill.TimeAirlineDelivery = null;
   //             domesticWaybill.DateReceivedAtReceiverCompany = null;
   //             domesticWaybill.TimeReceivedAtReceiverCompany = null;
   //             domesticWaybill.DateDistribution = null;
   //             domesticWaybill.TimeDistribution = null;
   //             domesticWaybill.BikeDeliveryInReceiverCompanyId = null;
   //             domesticWaybill.BikeDeliveryInReceiverCompanyAgent = null;
   //             domesticWaybill.DateDelivery = null;
   //             domesticWaybill.TimeDelivery = null;
   //             domesticWaybill.EntranceDeliveryPerson = null;
   //             domesticWaybill.EntranceTransfereePersonName = null;
   //             domesticWaybill.EntranceTransfereePersonNationalCode = null;
   //             domesticWaybill.DescriptionSenderComapny = null;
   //             domesticWaybill.DescriptionReceiverCompany = null;
   //             domesticWaybill.DescriptionSenderCustomer = null;
   //             domesticWaybill.DescriptionReceiverCustomer = null;
   //             domesticWaybill.Lock = null;
   //             domesticWaybill.IsIssueFromCaptainCargoWebSite = null;
   //             domesticWaybill.IsIssueFromCompanyWebSite = null;
   //             domesticWaybill.IsIssueFromCaptainCargoWebService = null;
   //             domesticWaybill.IsIssueFromCompanyWebService = null;
   //             domesticWaybill.IsIssueFromCaptainCargoPanel = null;
   //             domesticWaybill.IsIssueFromCaptainCargoDesktop = null;
   //             domesticWaybill.CaptainCargoPrice = null;
   //             domesticWaybill.CounterId = null;
   //             domesticWaybill.Dirty = null;
   //
   //             var updatedCompanyDomesticWaybill = mapper.Map(backToReayStateCommand, domesticWaybill);
   //             if (updatedCompanyDomesticWaybill == null)
   //                 return ApiResponse<bool>.Error(400, "مشکل در عملیات تبدیل");
   //
   //             await unitOfWork.SaveChangesAsync(cancellationToken);
   //
   //             logger.LogInformation("CompanyDomesticWaybillFromDesktop created successfully ID: {insertCompanyDomesticWaybillFromDesktopCommand}", domesticWaybill.Id);
   //             return ApiResponse<bool>.Ok(true, "بارنامه با موفقیت ثبت شد");
   //         }
   //     }
   //     else if (domesticWaybill.State == (short)WaybillState.ReceivedAtReceiverCompany)
   //     {
   //         return ApiResponse<bool>.Ok(true, "بارنامه ابتدا باید از مانیفست خارج شود تا بتواند خام شود");
   //     }
   //     else if (domesticWaybill.State == (short)WaybillState.Distribution ||
   //              domesticWaybill.State == (short)WaybillState.Delivery)
   //     {
   //         return ApiResponse<bool>.Ok(true, "بارنامه در حال توزیع یا تحویل شده است");
   //     }
   //     else
   //     {
   //         return ApiResponse<bool>.Ok(true, "نتیجه نامشخص");
   //     }
   // }
}
