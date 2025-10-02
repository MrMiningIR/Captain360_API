using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Dtos;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.AssignMasterWaybill;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.AssignMasterWaybillFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.BackFromAirlineDeliveryState;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.BackFromAirlineDeliveryStateFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.BackFromReceivedAtReceiverCompanyState;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.BackFromReceivedAtReceiverCompanyStateFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.ChangeStateToAirlineDelivery;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.ChangeStateToAirlineDeliveryFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.ChangeStateToReceivedAtReceiverCompany;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.ChangeStateToReceivedAtReceiverCompanyFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.DetachMasterWaybill;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.DetachMasterWaybillFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.Issue;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.IssueFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.Update;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.UpdateFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Dtos;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.ComapnyManifestForms;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticWaybills;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Services;

public class CompanyManifestFormService(
    ILogger<CompanyManifestFormService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyManifestFormRepository manifestFormRepository,
    ICompanyDomesticWaybillRepository domesticWaybillRepository,
    ICompanyRepository companyRepository,
    ICompanyDomesticPathReceiverCompanyRepository companyDomesticPathReceiverCompanyRepository,
    IUserContext userContext) : ICompanyManifestFormService
{
////    public async Task<ApiResponse<int>> IssueCompanyManifestFormFromDesktopAsync(IssueCompanyManifestFormFromDesktopCommand command, CancellationToken cancellationToken)
////    {
////        logger.LogInformation("IssueCompanyManifestFormFromDesktop is Called with {@IssueCompanyManifestFormFromDesktopCommand}", command);
////
////        var companySender = await companyRepository.GetCompanyByCodeAsync(command.CompanySenderCaptain360Code, false, false, cancellationToken);
////        if (companySender == null)
////            return ApiResponse<int>.Error(400, "شرکت فرستنده وجود ندارد");
////
////        var user = userContext.GetCurrentUser();
////        if (user == null)
////            return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
////
////        if (user.IsManager(companySender.Id))
////            return ApiResponse<int>.Error(400, "مجوز این فعالیت را ندارید");
////
////        Company? companyReceiver = null;
////        IReadOnlyList<CompanyDomesticPathReceiver>? companyDomesticPathReceiver = null;
////
////        companyReceiver = await companyRepository.GetCompanyByCodeAsync(command.CompanyReceiverCaptain360Code, false, false, cancellationToken);
////        if (companyReceiver != null)
////        {
////            companyDomesticPathReceiver = await companyDomesticPathReceiverCompanyRepository.GetCompanyDomesticPathReceiverByCompanySenderIdAndCompanyReceiverIdAsync(companySender.Id, companyReceiver.Id, false, true, cancellationToken);
////            if (companyDomesticPathReceiver == null || companyDomesticPathReceiver.Count < 1)
////                return ApiResponse<int>.Error(400, "نماینده نامعتبر است");
////        }
////        else
////        {
////            companyDomesticPathReceiver = await companyDomesticPathReceiverCompanyRepository.GetCompanyDomesticPathReceiverByCompanySenderIdAndCompanyReceiverCodeAsync(companySender.Id, command.CompanyReceiverCaptain360Code, false, true, cancellationToken);
////            if (companyDomesticPathReceiver == null || companyDomesticPathReceiver.Count < 1)
////                return ApiResponse<int>.Error(400, "نماینده نامعتبر است");
////        }
////
////        var manifestForm = await manifestFormRepository.GetCompanyManifestFormByNoAsync(command.No, true, true, false, false, cancellationToken);
////        if (manifestForm == null)
////            return ApiResponse<int>.Error(400, "بارنامه قبلا ثبت شده است");
////
////        manifestForm.CompanySenderId = companySender.Id;
////        manifestForm.CompanyReceiverId = companyReceiver == null ? null : companyReceiver.Id;
////        manifestForm.CompanyReceiverUserInsertedCode = companyReceiver != null ? null : command.CompanyReceiverCaptain360Code;
////        manifestForm.SourceCountryId = companySender.CountryId;
////        manifestForm.SourceProvinceId = companySender.ProvinceId;
////        manifestForm.SourceCityId = companySender.CityId;
////        manifestForm.DestinationCountryId = companyReceiver != null ? companyReceiver.CountryId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationCountryId;
////        manifestForm.DestinationProvinceId = companyReceiver != null ? companyReceiver.ProvinceId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationProvinceId;
////        manifestForm.DestinationCityId = companyReceiver != null ? companyReceiver.CityId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationCityId;
////        manifestForm.Date = command.Date;
////        manifestForm.CompanySenderDescription = command.CompanySenderDescription;
////        manifestForm.CompanySenderDescriptionForPrint = command.CompanySenderDescriptionForPrint;
////        manifestForm.State = (short)CompanyManifestFormState.Issued;
////        manifestForm.DateIssued = command.DateIssued;
////        manifestForm.TimeIssued = command.TimeIssued;
////        manifestForm.CounterId = user.Id;
////
////        var CompanyManifestForm = mapper.Map<Domain.Entities.CompanyDomesticWaybillEntity.CompanyManifestForm>(command) ?? null;
////        if (CompanyManifestForm == null)
////            return ApiResponse<int>.Error(400, "مشکل در عملیات تبدیل");
////
////        var CompanyManifestFormId = await manifestFormRepository.InsertCompanyManifestFormAsync(CompanyManifestForm, cancellationToken);
////
////        await unitOfWork.SaveChangesAsync(cancellationToken);
////
////        logger.LogInformation("CompanyManifestFormFromDesktop created successfully ID: {insertCompanyManifestFormFromDesktopCommand}", manifestForm.Id);
////        return ApiResponse<int>.Ok(manifestForm.Id, "بارنامه با موفقیت ثبت شد");
////    }
////
////    public async Task<ApiResponse<int>> IssueCompanyManifestFormAsync(IssueCompanyManifestFormCommand command, CancellationToken cancellationToken)
  //  {
  //      //logger.LogInformation("InsertCompanyManifestForm is Called with {@InsertCompanyManifestFormCommand}", insertCompanyManifestFormCommand);
  //      //
  //      //var companySender = await companyRepository.GetCompanyByCodeAsync(insertCompanyManifestFormCommand.CompanySenderCaptain360Code, false, false, cancellationToken);
  //      //if (companySender == null)
  //      //    return ApiResponse<int>.Error(400, "شرکت فرستنده وجود ندارد");
  //      //
  //      //var user = userContext.GetCurrentUser();
  //      //if (user == null)
  //      //        return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //      //
  //      //if (user.IsManager(companySender.Id))
  //      //    return ApiResponse<int>.Error(400, "مجوز این فعالیت را ندارید");
  //      //
  //      //Company? companyReceiver = null;
  //      //IReadOnlyList<CompanyDomesticPathReceiver>? companyDomesticPathReceiver = null;
  //      //
  //      //companyReceiver = await companyRepository.GetCompanyByCodeAsync(insertCompanyManifestFormCommand.CompanyReceiverCaptain360Code, false, false, cancellationToken);
  //      //if (companyReceiver != null)
  //      //{
  //      //    companyDomesticPathReceiver = await companyDomesticPathReceiverRepository.GetCompanyDomesticPathReceiverByCompanySenderIdAndCompanyReceiverIdAsync(companySender.Id, companyReceiver.Id, false, true, cancellationToken);
  //      //    if (companyDomesticPathReceiver == null || companyDomesticPathReceiver.Count < 1)
  //      //        return ApiResponse<int>.Error(400, "نماینده نامعتبر است");
  //      //}
  //      //else
  //      //{
  //      //    companyDomesticPathReceiver = await companyDomesticPathReceiverRepository.GetCompanyDomesticPathReceiverByCompanySenderIdAndCompanyReceiverCodeAsync(companySender.Id, insertCompanyManifestFormCommand.CompanyReceiverCaptain360Code, false, true, cancellationToken);
  //      //    if (companyDomesticPathReceiver == null || companyDomesticPathReceiver.Count < 1)
  //      //        return ApiResponse<int>.Error(400, "نماینده نامعتبر است");
  //      //}
  //      //
  //      //var manifestForm = await manifestFormRepository.GetCompanyManifestFormByNoAsync(insertCompanyManifestFormCommand.No, true, true, false, false, cancellationToken);
  //      //if (manifestForm == null)
  //      //    return ApiResponse<int>.Error(400, "بارنامه قبلا ثبت شده است");
  //      //
  //      //manifestForm.CompanySenderId = companySender.Id;
  //      //manifestForm.CompanyReceiverId = companyReceiver == null ? null : companyReceiver.Id;
  //      //manifestForm.CompanyReceiverUserInsertedCode = companyReceiver != null ? null : insertCompanyManifestFormCommand.CompanyReceiverCaptain360Code;
  //      //manifestForm.SourceCountryId = companySender.CountryId;
  //      //manifestForm.SourceProvinceId = companySender.ProvinceId;
  //      //manifestForm.SourceCityId = companySender.CityId;
  //      //manifestForm.DestinationCountryId = companyReceiver != null ? companyReceiver.CountryId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationCountryId;
  //      //manifestForm.DestinationProvinceId = companyReceiver != null ? companyReceiver.ProvinceId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationProvinceId;
  //      //manifestForm.DestinationCityId = companyReceiver != null ? companyReceiver.CityId : companyDomesticPathReceiver[0].CompanyDomesticPath.DestinationCityId;
  //      //manifestForm.Date = insertCompanyManifestFormCommand.Date;
  //      //manifestForm.CompanySenderDescription = insertCompanyManifestFormCommand.CompanySenderDescription;
  //      //manifestForm.CompanySenderDescriptionForPrint = insertCompanyManifestFormCommand.CompanySenderDescriptionForPrint;
  //      //manifestForm.State = (short)CompanyManifestFormState.Issued;
  //      //manifestForm.DateIssued = insertCompanyManifestFormCommand.DateIssued;
  //      //manifestForm.TimeIssued = insertCompanyManifestFormCommand.TimeIssued;
  //      //manifestForm.CounterId = user.Id;
  //      //
  //      //var CompanyManifestForm = mapper.Map<Domain.Entities.CompanyDomesticWaybillEntity.CompanyManifestForm>(insertCompanyManifestFormCommand) ?? null;
  //      //if (CompanyManifestForm == null)
  //      //    return ApiResponse<int>.Error(400, "مشکل در عملیات تبدیل");
  //      //
  //      //var CompanyManifestFormId = await manifestFormRepository.InsertCompanyManifestFormAsync(CompanyManifestForm, cancellationToken);
  //      //
  //      //await unitOfWork.SaveChangesAsync(cancellationToken);
  //      //
  //      //logger.LogInformation("CompanyManifestForm created successfully ID: {insertCompanyManifestFormCommand}", manifestForm.Id);
  //      //return ApiResponse<int>.Ok(manifestForm.Id, "بارنامه با موفقیت ثبت شد");
  //
  //      return ApiResponse<int>.Ok(1, "بارنامه با موفقیت ثبت شد");
  //  }
////
////    public async Task<ApiResponse<int>> ChangeStateCompanyManifestFormToAirlineDeliveryAsync(ChangeStateCompanyManifestFormToAirlineDeliveryCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("ChangeStateCompanyManifestFormToAirlineDelivery Called with {@ChangeStateCompanyManifestFormToAirlineDeliveryCommand}", changeStateCompanyManifestFormFromToAirlineDeliveryCommand);
  //
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByIdAsync(changeStateCompanyManifestFormFromToAirlineDeliveryCommand.Id, true, false, false, false, cancellationToken);
  //      if (manifestForm is null)
  //          return ApiResponse<int>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (manifestForm.State != (short)CompanyManifestFormState.Issued)
  //          return ApiResponse<int>.Error(404, $"مانیفست باید در وضعیت صادر شده باشد");
  //
  //      var company = await companyRepository.GetCompanyByIdAsync(manifestForm.CompanySenderId, false, false, cancellationToken);
  //      if (company == null)
  //          return ApiResponse<int>.Error(404, $"شرکت نامعتبر است");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      if (!await domesticWaybillRepository.IsManifestStatusConsistentByCompanyManifestFormIdStateAsync(manifestForm.Id, (short)WaybillState.Manifested, cancellationToken))
  //          return ApiResponse<int>.Error(400, "همه بارنامه ها باید در وضعیت مانیسفست شده باشند");
  //
  //      await unitOfWork.BeginTransactionAsync(cancellationToken);
  //      manifestForm.State = (short)CompanyManifestFormState.AirlineDelivery;
  //      manifestForm.DateAirlineDelivery = Tools.GetTodayInPersianDate();
  //      manifestForm.TimeAirlineDelivery = Tools.GetTime();
  //
  //      var waybillIds = await domesticWaybillRepository.GetWaybillIdByCompanyManifestFormIdAsync(manifestForm.Id, cancellationToken);
  //
  //      if (waybillIds != null && waybillIds.Any())
  //      {
  //          await domesticWaybillRepository.ChangeStateAsync(waybillIds, (short)WaybillState.AirlineDelivery, cancellationToken);
  //      }
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //      await unitOfWork.CommitTransactionAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", changeStateCompanyManifestFormFromToAirlineDeliveryCommand.Id);
  //      return ApiResponse<int>.Ok(changeStateCompanyManifestFormFromToAirlineDeliveryCommand.Id, "وضعیت محتوی بار با موفقیت به‌روزرسانی شد");
  //  }
////
////    public async Task<ApiResponse<int>> ChangeStateCompanyManifestFormToAirlineDeliveryFromDesktopAsync(ChangeStateCompanyManifestFormToAirlineDeliveryFromDesktopCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("ChangeStateCompanyManifestFormToAirlineDeliveryFromDesktop Called with {@ChangeStateCompanyManifestFormToAirlineDeliveryFromDesktopCommand}", command);
  //
  //      var companySender = await companyRepository.GetCompanyByCodeAsync(command.CompanySenderCaptain360Code, false, false, cancellationToken);
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
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByNoAsync(command.No, true, false, false, false, cancellationToken);
  //      if (manifestForm is null)
  //          return ApiResponse<int>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (manifestForm.CompanySenderId != companySender.Id)
  //          return ApiResponse<int>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      if (manifestForm.State != (short)CompanyManifestFormState.Issued)
  //          return ApiResponse<int>.Error(404, $"مانیفست باید در وضعیت صادر شده باشد");
  //
  //      if (!await domesticWaybillRepository.IsManifestStatusConsistentByCompanyManifestFormIdStateAsync(manifestForm.Id, (short)WaybillState.Manifested, cancellationToken))
  //          return ApiResponse<int>.Error(400, "همه بارنامه ها باید در وضعیت مانیسفست شده باشند");
  //
  //      await unitOfWork.BeginTransactionAsync(cancellationToken);
  //      manifestForm.State = (short)CompanyManifestFormState.AirlineDelivery;
  //      manifestForm.DateAirlineDelivery = Tools.GetTodayInPersianDate();
  //      manifestForm.TimeAirlineDelivery = Tools.GetTime();
  //
  //      var waybillIds = await domesticWaybillRepository.GetWaybillIdByCompanyManifestFormIdAsync(manifestForm.Id, cancellationToken);
  //
  //      if (waybillIds != null && waybillIds.Any())
  //      {
  //          await domesticWaybillRepository.ChangeStateAsync(waybillIds, (short)WaybillState.AirlineDelivery, cancellationToken);
  //      }
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //      await unitOfWork.CommitTransactionAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", manifestForm.Id);
  //      return ApiResponse<int>.Ok(manifestForm.Id, "وضعیت محتوی بار با موفقیت به‌روزرسانی شد");
  //  }
////
////    public async Task<ApiResponse<int>> BackCompanyManifestFormFromAirlineDeliveryStateAsync(BackCompanyManifestFormFromAirlineDeliveryStateCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("BackCompanyManifestFormFromAirlineDeliveryState Called with {@BackCompanyManifestFormFromAirlineDeliveryStateCommand}", command);
  //
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByIdAsync(command.Id, true, false, false, false, cancellationToken);
  //      if (manifestForm is null)
  //          return ApiResponse<int>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (manifestForm.State != (short)CompanyManifestFormState.AirlineDelivery)
  //          return ApiResponse<int>.Error(404, $"مانیفست باید در وضعیت تحویل ایرلاین باشد");
  //
  //      var company = await companyRepository.GetCompanyByIdAsync(manifestForm.CompanySenderId, false, false, cancellationToken);
  //      if (company == null)
  //          return ApiResponse<int>.Error(404, $"شرکت نامعتبر است");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      if (!await domesticWaybillRepository.IsManifestStatusConsistentByCompanyManifestFormIdStateAsync(manifestForm.Id, (short)WaybillState.AirlineDelivery, cancellationToken))
  //          return ApiResponse<int>.Error(400, "همه بارنامه ها باید در وضعیت تحویل ایرلاین باشند");
  //
  //      await unitOfWork.BeginTransactionAsync(cancellationToken);
  //      manifestForm.State = (short)CompanyManifestFormState.Issued;
  //      manifestForm.DateAirlineDelivery = null;
  //      manifestForm.TimeAirlineDelivery = null;
  //
  //      var waybillIds = await domesticWaybillRepository.GetWaybillIdByCompanyManifestFormIdAsync(manifestForm.Id, cancellationToken);
  //
  //      if (waybillIds != null && waybillIds.Any())
  //      {
  //          await domesticWaybillRepository.ChangeStateAsync(waybillIds, (short)WaybillState.Manifested, cancellationToken);
  //      }
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //      await unitOfWork.CommitTransactionAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", command.Id);
  //      return ApiResponse<int>.Ok(command.Id, "وضعیت محتوی بار با موفقیت به‌روزرسانی شد");
  //  }
////
////    public async Task<ApiResponse<int>> BackCompanyManifestFormFromAirlineDeliveryStateFromDesktopAsync(BackCompanyManifestFormFromAirlineDeliveryStateFromDesktopCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("BackCompanyManifestFormFromAirlineDeliveryStateFromDesktop Called with {@BackCompanyManifestFormFromAirlineDeliveryStateFromDesktopCommand}", command);
  //
  //      var companySender = await companyRepository.GetCompanyByCodeAsync(command.CompanySenderCaptain360Code, false, false, cancellationToken);
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
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByNoAsync(command.No, true, false, false, false, cancellationToken);
  //      if (manifestForm is null)
  //          return ApiResponse<int>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (manifestForm.CompanySenderId != companySender.Id)
  //          return ApiResponse<int>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      if (manifestForm.State != (short)CompanyManifestFormState.AirlineDelivery)
  //          return ApiResponse<int>.Error(404, $"مانیفست باید در وضعیت تحویل ایرلاین باشد");
  //
  //
  //
  //      if (!await domesticWaybillRepository.IsManifestStatusConsistentByCompanyManifestFormIdStateAsync(manifestForm.Id, (short)WaybillState.AirlineDelivery, cancellationToken))
  //          return ApiResponse<int>.Error(400, "همه بارنامه ها باید در وضعیت تحویل ایرلاین باشند");
  //
  //      await unitOfWork.BeginTransactionAsync(cancellationToken);
  //      manifestForm.State = (short)CompanyManifestFormState.Issued;
  //      manifestForm.DateAirlineDelivery = null;
  //      manifestForm.TimeAirlineDelivery = null;
  //
  //      var waybillIds = await domesticWaybillRepository.GetWaybillIdByCompanyManifestFormIdAsync(manifestForm.Id, cancellationToken);
  //
  //      if (waybillIds != null && waybillIds.Any())
  //      {
  //          await domesticWaybillRepository.ChangeStateAsync(waybillIds, (short)WaybillState.Manifested, cancellationToken);
  //      }
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //      await unitOfWork.CommitTransactionAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", manifestForm.Id);
  //      return ApiResponse<int>.Ok(manifestForm.Id, "وضعیت محتوی بار با موفقیت به‌روزرسانی شد");
  //  }
////
////    public async Task<ApiResponse<int>> ChangeStateCompanyManifestFormToReceivedAtReceiverCompanyAsync(ChangeStateCompanyManifestFormToReceivedAtReceiverCompanyCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("ChangeStateCompanyManifestFormToReceivedAtReceiverCompany Called with {@ChangeStateCompanyManifestFormToReceivedAtReceiverCompanyCommand}", command);
  //
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByIdAsync(command.Id, true, false, false, false, cancellationToken);
  //      if (manifestForm is null)
  //          return ApiResponse<int>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (manifestForm.State != (short)CompanyManifestFormState.AirlineDelivery)
  //          return ApiResponse<int>.Error(404, $"مانیفست باید در وضعیت صادر شده باشد");
  //
  //      if (manifestForm.CompanyReceiverId == null)
  //          return ApiResponse<int>.Error(404, $"شرکت نامعتبر است");
  //
  //      var company = await companyRepository.GetCompanyByIdAsync((int)manifestForm.CompanyReceiverId, false, false, cancellationToken);
  //      if (company == null)
  //          return ApiResponse<int>.Error(404, $"شرکت نامعتبر است");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      if (!await domesticWaybillRepository.IsManifestStatusConsistentByCompanyManifestFormIdStateAsync(manifestForm.Id, (short)WaybillState.AirlineDelivery, cancellationToken))
  //          return ApiResponse<int>.Error(400, "همه بارنامه ها باید در وضعیت مانیسفست شده باشند");
  //
  //      await unitOfWork.BeginTransactionAsync(cancellationToken);
  //      manifestForm.State = (short)CompanyManifestFormState.ReceivedAtReceiverCompany;
  //      manifestForm.DateReceivedAtReceiverCompany = Tools.GetTodayInPersianDate();
  //      manifestForm.TimeReceivedAtReceiverCompany = Tools.GetTime();
  //
  //      var waybillIds = await domesticWaybillRepository.GetWaybillIdByCompanyManifestFormIdAsync(manifestForm.Id, cancellationToken);
  //
  //      if (waybillIds != null && waybillIds.Any())
  //      {
  //          await domesticWaybillRepository.ChangeStateAsync(waybillIds, (short)WaybillState.ReceivedAtReceiverCompany, cancellationToken);
  //      }
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //      await unitOfWork.CommitTransactionAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", command.Id);
  //      return ApiResponse<int>.Ok(command.Id, "وضعیت محتوی بار با موفقیت به‌روزرسانی شد");
  //  }
////
////    public async Task<ApiResponse<int>> ChangeStateCompanyManifestFormToReceivedAtReceiverCompanyFromDesktopAsync(ChangeStateCompanyManifestFormToReceivedAtReceiverCompanyFromDesktopCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("ChangeStateCompanyManifestFormToReceivedAtReceiverCompanyFromDesktop Called with {@ChangeStateCompanyManifestFormToReceivedAtReceiverCompanyFromDesktopCommand}", command);
  //
  //      var companyReceiver = await companyRepository.GetCompanyByCodeAsync(command.CompanyReceiverCaptain360Code, false, false, cancellationToken);
  //      if (companyReceiver == null)
  //          return ApiResponse<int>.Error(400, "شرکت فرستنده وجود ندارد");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsManager(companyReceiver.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByNoAsync(command.No, true, false, false, false, cancellationToken);
  //      if (manifestForm is null)
  //          return ApiResponse<int>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (manifestForm.CompanyReceiverId == null || manifestForm.CompanyReceiverId != companyReceiver.Id)
  //          return ApiResponse<int>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      if (manifestForm.State != (short)CompanyManifestFormState.AirlineDelivery)
  //          return ApiResponse<int>.Error(404, $"مانیفست باید در وضعیت صادر شده باشد");
  //
  //      if (!await domesticWaybillRepository.IsManifestStatusConsistentByCompanyManifestFormIdStateAsync(manifestForm.Id, (short)WaybillState.AirlineDelivery, cancellationToken))
  //          return ApiResponse<int>.Error(400, "همه بارنامه ها باید در وضعیت مانیسفست شده باشند");
  //
  //      await unitOfWork.BeginTransactionAsync(cancellationToken);
  //      manifestForm.State = (short)CompanyManifestFormState.ReceivedAtReceiverCompany;
  //      manifestForm.DateReceivedAtReceiverCompany = Tools.GetTodayInPersianDate();
  //      manifestForm.TimeReceivedAtReceiverCompany = Tools.GetTime();
  //
  //      var waybillIds = await domesticWaybillRepository.GetWaybillIdByCompanyManifestFormIdAsync(manifestForm.Id, cancellationToken);
  //
  //      if (waybillIds != null && waybillIds.Any())
  //      {
  //          await domesticWaybillRepository.ChangeStateAsync(waybillIds, (short)WaybillState.ReceivedAtReceiverCompany, cancellationToken);
  //      }
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //      await unitOfWork.CommitTransactionAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", manifestForm.Id);
  //      return ApiResponse<int>.Ok(manifestForm.Id, "وضعیت محتوی بار با موفقیت به‌روزرسانی شد");
  //  }
////
////    public async Task<ApiResponse<int>> BackCompanyManifestFormFromReceivedAtReceiverCompanyStateAsync(BackCompanyManifestFormFromReceivedAtReceiverCompanyStateCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("BackCompanyManifestFormFromReceivedAtReceiverCompanyState Called with {@BackCompanyManifestFormFromReceivedAtReceiverCompanyStateCommand}", command);
  //
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByIdAsync(command.Id, true, false, false, false, cancellationToken);
  //      if (manifestForm is null)
  //          return ApiResponse<int>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (manifestForm.State != (short)CompanyManifestFormState.ReceivedAtReceiverCompany)
  //          return ApiResponse<int>.Error(404, $"مانیفست باید در وضعیت تحویل ایرلاین باشد");
  //
  //      if (manifestForm.CompanyReceiverId == null)
  //          return ApiResponse<int>.Error(404, $"شرکت نامعتبر است");
  //
  //      var company = await companyRepository.GetCompanyByIdAsync((int)manifestForm.CompanyReceiverId, false, false, cancellationToken);
  //      if (company == null)
  //          return ApiResponse<int>.Error(404, $"شرکت نامعتبر است");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      if (!await domesticWaybillRepository.IsManifestStatusConsistentByCompanyManifestFormIdStateAsync(manifestForm.Id, (short)WaybillState.ReceivedAtReceiverCompany, cancellationToken))
  //          return ApiResponse<int>.Error(400, "همه بارنامه ها باید در وضعیت تحویل ایرلاین باشند");
  //
  //      await unitOfWork.BeginTransactionAsync(cancellationToken);
  //      manifestForm.State = (short)CompanyManifestFormState.AirlineDelivery;
  //      manifestForm.DateReceivedAtReceiverCompany = null;
  //      manifestForm.TimeReceivedAtReceiverCompany = null;
  //
  //      var waybillIds = await domesticWaybillRepository.GetWaybillIdByCompanyManifestFormIdAsync(manifestForm.Id, cancellationToken);
  //
  //      if (waybillIds != null && waybillIds.Any())
  //      {
  //          await domesticWaybillRepository.ChangeStateAsync(waybillIds, (short)WaybillState.Manifested, cancellationToken);
  //      }
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //      await unitOfWork.CommitTransactionAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", command.Id);
  //      return ApiResponse<int>.Ok(command.Id, "وضعیت محتوی بار با موفقیت به‌روزرسانی شد");
  //  }
////
////    public async Task<ApiResponse<int>> BackCompanyManifestFormFromReceivedAtReceiverCompanyStateFromDesktopAsync(BackCompanyManifestFormFromReceivedAtReceiverCompanyStateFromDesktopCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("BackCompanyManifestFormFromReceivedAtReceiverCompanyStateFromDesktop Called with {@BackCompanyManifestFormFromReceivedAtReceiverCompanyStateFromDesktopCommand}", command);
  //
  //      var companyReceiver = await companyRepository.GetCompanyByCodeAsync(command.CompanyReceiverCaptain360Code, false, false, cancellationToken);
  //      if (companyReceiver == null)
  //          return ApiResponse<int>.Error(400, "شرکت فرستنده وجود ندارد");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsManager(companyReceiver.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByNoAsync(command.No, true, false, false, false, cancellationToken);
  //      if (manifestForm is null)
  //          return ApiResponse<int>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      if (manifestForm.CompanyReceiverId == null || manifestForm.CompanyReceiverId != companyReceiver.Id)
  //          return ApiResponse<int>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      if (manifestForm.State != (short)CompanyManifestFormState.ReceivedAtReceiverCompany)
  //          return ApiResponse<int>.Error(404, $"مانیفست باید در وضعیت تحویل ایرلاین باشد");
  //
  //      if (!await domesticWaybillRepository.IsManifestStatusConsistentByCompanyManifestFormIdStateAsync(manifestForm.Id, (short)WaybillState.ReceivedAtReceiverCompany, cancellationToken))
  //          return ApiResponse<int>.Error(400, "همه بارنامه ها باید در وضعیت تحویل ایرلاین باشند");
  //
  //      await unitOfWork.BeginTransactionAsync(cancellationToken);
  //      manifestForm.State = (short)CompanyManifestFormState.AirlineDelivery;
  //      manifestForm.DateReceivedAtReceiverCompany = null;
  //      manifestForm.TimeReceivedAtReceiverCompany = null;
  //
  //      var waybillIds = await domesticWaybillRepository.GetWaybillIdByCompanyManifestFormIdAsync(manifestForm.Id, cancellationToken);
  //
  //      if (waybillIds != null && waybillIds.Any())
  //      {
  //          await domesticWaybillRepository.ChangeStateAsync(waybillIds, (short)WaybillState.Manifested, cancellationToken);
  //      }
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //      await unitOfWork.CommitTransactionAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", manifestForm.Id);
  //      return ApiResponse<int>.Ok(manifestForm.Id, "وضعیت محتوی بار با موفقیت به‌روزرسانی شد");
  //  }
////
////    public async Task<ApiResponse<bool>> AssignMasterWaybillToCompanyManifestFormAsync(AssignMasterWaybillToCompanyManifestFormCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("AssignMasterWaybillToCompanyManifestForm Called with {@AssignMasterWaybillToCompanyManifestFormCommand}", command);
  //
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByIdAsync(command.Id, true, false, false, false, cancellationToken);
  //      if (manifestForm is null)
  //          return ApiResponse<bool>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      var company = await companyRepository.GetCompanyByIdAsync((int)manifestForm.CompanySenderId, false, false, cancellationToken);
  //      if (company == null)
  //          return ApiResponse<bool>.Error(404, $"شرکت نامعتبر است");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      manifestForm.MasterWaybillNo = command.MasterWaybillNo;
  //      manifestForm.MasterWaybillWeight = command.MasterWaybillWeight;
  //      manifestForm.MasterWaybillAirline = command.MasterWaybillAirline;
  //      manifestForm.MasterWaybillFlightNo = command.MasterWaybillFlightNo;
  //      manifestForm.MasterWaybillFlightDate = command.MasterWaybillFlightDate;
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", manifestForm.Id);
  //      return ApiResponse<bool>.Ok(true, "وضعیت محتوی بار با موفقیت به‌روزرسانی شد");
  //  }
////
////    public async Task<ApiResponse<bool>> AssignMasterWaybillToCompanyManifestFormFromDesktopAsync(AssignMasterWaybillToCompanyManifestFormFromDesktopCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("AssignMasterWaybillToCompanyManifestFormFromDesktop Called with {@AssignMasterWaybillToCompanyManifestFormFromDesktopCommand}", command);
  //
  //      var companySender = await companyRepository.GetCompanyByCodeAsync(command.CompanySenderCaptain360Code, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<bool>.Error(400, "شرکت فرستنده وجود ندارد");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsManager(companySender.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByNoAsync(command.No, false, true, false, false, cancellationToken);
  //      if (manifestForm == null)
  //          return ApiResponse<bool>.Error(400, "بارنامه وجود ندارد");
  //
  //      if (manifestForm.CompanySenderId != companySender.Id)
  //          return ApiResponse<bool>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      manifestForm.MasterWaybillNo = command.MasterWaybillNo;
  //      manifestForm.MasterWaybillWeight = command.MasterWaybillWeight;
  //      manifestForm.MasterWaybillAirline = command.MasterWaybillAirline;
  //      manifestForm.MasterWaybillFlightNo = command.MasterWaybillFlightNo;
  //      manifestForm.MasterWaybillFlightDate = command.MasterWaybillFlightDate;
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", manifestForm.Id);
  //      return ApiResponse<bool>.Ok(true, "وضعیت محتوی بار با موفقیت به‌روزرسانی شد");
  //  }
////
////    public async Task<ApiResponse<bool>> DetachMasterWaybillFromCompanyManifestFormFormAsync(DetachMasterWaybillFromCompanyManifestFormCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("DetachMasterWaybillFromCompanyManifestForm Called with {@DetachMasterWaybillFromCompanyManifestFormCommand}", command);
  //
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByIdAsync(command.Id, true, false, false, false, cancellationToken);
  //      if (manifestForm is null)
  //          return ApiResponse<bool>.Error(404, $"محتوی بار نامعتبر است");
  //
  //      var company = await companyRepository.GetCompanyByIdAsync((int)manifestForm.CompanySenderId, false, false, cancellationToken);
  //      if (company == null)
  //          return ApiResponse<bool>.Error(404, $"شرکت نامعتبر است");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
  //          return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
  //
  //      manifestForm.MasterWaybillNo = null;
  //      manifestForm.MasterWaybillWeight = null;
  //      manifestForm.MasterWaybillAirline = null;
  //      manifestForm.MasterWaybillFlightNo = null;
  //      manifestForm.MasterWaybillFlightDate = null;
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", manifestForm.Id);
  //      return ApiResponse<bool>.Ok(true, "وضعیت محتوی بار با موفقیت به‌روزرسانی شد");
  //  }
////
////    public async Task<ApiResponse<bool>> DetachMasterWaybillFromCompanyManifestFormFromDesktopAsync(DetachMasterWaybillFromCompanyManifestFormFromDesktopCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("DetachMasterWaybillFromCompanyManifestFormFromDesktop Called with {@DetachMasterWaybillFromCompanyManifestFormFromDesktopCommand}", command);
  //
  //      var companySender = await companyRepository.GetCompanyByCodeAsync(command.CompanySenderCaptain360Code, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<bool>.Error(400, "شرکت فرستنده وجود ندارد");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (user.IsManager(companySender.Id))
  //          return ApiResponse<bool>.Error(400, "مجوز این فعالیت را ندارید");
  //
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByNoAsync(command.No, false, true, false, false, cancellationToken);
  //      if (manifestForm == null)
  //          return ApiResponse<bool>.Error(400, "بارنامه وجود ندارد");
  //
  //      if (manifestForm.CompanySenderId != companySender.Id)
  //          return ApiResponse<bool>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      manifestForm.MasterWaybillNo = null;
  //      manifestForm.MasterWaybillWeight = null;
  //      manifestForm.MasterWaybillAirline = null;
  //      manifestForm.MasterWaybillFlightNo = null;
  //      manifestForm.MasterWaybillFlightDate = null;
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("وضعیت محتوی بار با موفقیت به‌روزرسانی شد: {Id}", manifestForm.Id);
  //      return ApiResponse<bool>.Ok(true, "وضعیت محتوی بار با موفقیت به‌روزرسانی شد");
  //  }
////
////    public async Task<ApiResponse<CompanyManifestFormDto>> UpdateCompanyManifestFormFromDesktopAsync(UpdateCompanyManifestFormFromDesktopCommand command, CancellationToken cancellationToken)
  //  {
  //      logger.LogInformation("UpdateCompanyManifestFormFromDesktop is Called with {@UpdateCompanyManifestFormFromDesktopCommand}", command);
  //
  //      var companySender = await companyRepository.GetCompanyByCodeAsync(command.CompanySenderCaptain360Code, false, false, cancellationToken);
  //      if (companySender == null)
  //          return ApiResponse<CompanyManifestFormDto>.Error(400, "شرکت فرستنده وجود ندارد");
  //
  //      var user = userContext.GetCurrentUser();
  //      if (user == null)
  //          return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
  //
  //      if (user.IsManager(companySender.Id))
  //          return ApiResponse<CompanyManifestFormDto>.Error(400, "مجوز این فعالیت را ندارید");
  //
  //      var manifestForm = await manifestFormRepository.GetCompanyManifestFormByNoAsync(command.No, false, true, false, false, cancellationToken);
  //      if (manifestForm == null)
  //          return ApiResponse<CompanyManifestFormDto>.Error(400, "بارنامه وجود ندارد");
  //
  //      if (manifestForm.CompanySenderId != companySender.Id)
  //          return ApiResponse<CompanyManifestFormDto>.Error(400, "بارنامه مربوط به شرکت دیگری است");
  //
  //      manifestForm.CompanySenderDescription = command.CompanySenderDescription;
  //      manifestForm.CompanySenderDescriptionForPrint = command.CompanySenderDescriptionForPrint;
  //
  //      if (manifestForm.State == (short)CompanyManifestFormState.Issued ||
  //          manifestForm.State == (short)CompanyManifestFormState.AirlineDelivery)
  //      {
  //          manifestForm.Dirty = false;
  //      }
  //      else if (manifestForm.State == (short)CompanyManifestFormState.ReceivedAtReceiverCompany)
  //      {
  //          manifestForm.Dirty = true;
  //      }
  //
  //      var updatedCompanyManifestForm = mapper.Map(command, manifestForm);
  //      if (updatedCompanyManifestForm == null)
  //          return ApiResponse<CompanyManifestFormDto>.Error(400, "مشکل در عملیات تبدیل");
  //
  //      await unitOfWork.SaveChangesAsync(cancellationToken);
  //
  //      logger.LogInformation("CompanyManifestFormFromDesktop created successfully ID: {insertCompanyManifestFormFromDesktopCommand}", manifestForm.Id);
  //      var updatedCompanyManifestFormDto = mapper.Map<CompanyManifestFormDto>(updatedCompanyManifestForm);
  //      return ApiResponse<CompanyManifestFormDto>.Ok(updatedCompanyManifestFormDto, "بارنامه با موفقیت ثبت شد");
  //  }
////
////    public async Task<ApiResponse<CompanyManifestFormDto>> UpdateCompanyManifestFormAsync(UpdateCompanyManifestFormCommand command, CancellationToken cancellationToken)
//    {
//        logger.LogInformation("UpdateCompanyManifestForm is Called with {@UpdateCompanyManifestFormCommand}", command);
//
//        var manifestForm = await manifestFormRepository.GetCompanyManifestFormByIdAsync(command.Id, true, false, false, false, cancellationToken);
//        if (manifestForm is null)
//            return ApiResponse<CompanyManifestFormDto>.Error(404, $"محتوی بار نامعتبر است");
//
//        var company = await companyRepository.GetCompanyByIdAsync((int)manifestForm.CompanySenderId, false, false, cancellationToken);
//        if (company == null)
//            return ApiResponse<CompanyManifestFormDto>.Error(404, $"شرکت نامعتبر است");
//
//        var user = userContext.GetCurrentUser();
//        if (user == null)
//            return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
//
//        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
//            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
//
//        manifestForm.CompanySenderDescription = command.CompanySenderDescription;
//        manifestForm.CompanySenderDescriptionForPrint = command.CompanySenderDescriptionForPrint;
//
//        if (manifestForm.State == (short)CompanyManifestFormState.Issued ||
//            manifestForm.State == (short)CompanyManifestFormState.AirlineDelivery)
//        {
//            manifestForm.Dirty = false;
//        }
//        else if (manifestForm.State == (short)CompanyManifestFormState.ReceivedAtReceiverCompany)
//        {
//            manifestForm.Dirty = true;
//        }
//
//        var updatedCompanyManifestForm = mapper.Map(command, manifestForm);
//        if (updatedCompanyManifestForm == null)
//            return ApiResponse<CompanyManifestFormDto>.Error(400, "مشکل در عملیات تبدیل");
//
//        await unitOfWork.SaveChangesAsync(cancellationToken);
//
//        logger.LogInformation("CompanyManifestForm created successfully ID: {insertCompanyManifestFormCommand}", manifestForm.Id);
//        var updatedCompanyManifestFormDto = mapper.Map<CompanyManifestFormDto>(updatedCompanyManifestForm);
//        return ApiResponse<CompanyManifestFormDto>.Ok(updatedCompanyManifestFormDto, "بارنامه با موفقیت ثبت شد");
//    }
}
