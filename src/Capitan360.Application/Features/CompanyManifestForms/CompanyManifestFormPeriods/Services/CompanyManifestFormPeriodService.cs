using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.Create;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.Delete;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.Update;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.UpdateActiveState;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Dtos;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Queries.GetAll;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Queries.GetById;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Entities.CompanyManifestForms;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.ComapnyManifestForms;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Services;

public class CompanyManifestFormPeriodService(
    ILogger<CompanyManifestFormPeriodService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyManifestFormPeriodRepository companyManifestFormPeriodRepository,
    ICompanyManifestFormRepository companyManifestFormRepository,
    ICompanyRepository companyRepository,
    IUserContext userContext) : ICompanyManifestFormPeriodService
{
    public async Task<ApiResponse<int>> CreateCompanyManifestFormPeriodAsync(CreateCompanyManifestFormPeriodCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyManifestFormPeriod is Called with {@CreateCompanyManifestFormPeriod Command}", command);

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
            return ApiResponse<int>.Error(StatusCodes.Status400BadRequest, "شماره فرم مانیفست باید با کد شرکت شروع شود");

        if (await companyManifestFormPeriodRepository.CheckExistCompanyManifestFormPeriodCodeAsync(command.Code, command.CompanyId, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "کد مخزن تکراری است");

        var manifestFormPeriods = await companyManifestFormPeriodRepository.GetCompanyManifestFormPeriodByCompanyIdAsync(command.CompanyId, cancellationToken);
        if (manifestFormPeriods == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "مخزن یافت نشد");

        for (int i = 0; i < manifestFormPeriods.Count; i++)
        {
            if (manifestFormPeriods[i].StartNumber <= command.EndNumber && command.StartNumber <= manifestFormPeriods[i].EndNumber)
            {
                return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "مخزن تداخل دارد");
            }
        }

        var companyManifestFormPeriod = mapper.Map<CompanyManifestFormPeriod>(command) ?? null;
        if (companyManifestFormPeriod is null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var companyManifestFormPeriodId = await companyManifestFormPeriodRepository.CreateCompanyManifestFormPeriodAsync(companyManifestFormPeriod, cancellationToken);

        for (long i = command.StartNumber; i <= command.EndNumber; i++)
            await companyManifestFormRepository.InsertManifestFormAsync(new CompanyManifestForm
            {
                No = i,
                CompanySenderId = company.Id,
                CompanyReceiverId = null,
                CompanyReceiverUserInsertedCode = null,
                SourceCountryId = company.CountryId,
                SourceProvinceId = company.ProvinceId,
                SourceCityId = company.CityId,
                DestinationCountryId = null,
                DestinationProvinceId = null,
                DestinationCityId = null,
                CompanyManifestFormPeriodId = companyManifestFormPeriodId,
                Date = null,
                CompanySenderDescription = null,
                CompanySenderDescriptionForPrint = null,
                CompanyReceiverDescription = null,
                MasterWaybillNo = null,
                MasterWaybillWeight = null,
                MasterWaybillAirline = null,
                MasterWaybillFlightNo = null,
                MasterWaybillFlightDate = null,
                State = (short)CompanyManifestFormState.Ready,
                DateIssued = null,
                TimeIssued = null,
                DateAirlineDelivery = null,
                TimeAirlineDelivery = null,
                DateReceivedAtReceiverCompany = null,
                TimeReceivedAtReceiverCompany = null,
                CounterId = null,
                Dirty = null,
            }, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("CompanyManifestFormPeriod created successfully with {@CompanyManifestFormPeriod}", companyManifestFormPeriod);
        return ApiResponse<int>.Created(companyManifestFormPeriodId, "مخزن با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyManifestFormPeriodAsync(DeleteCompanyManifestFormPeriodCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyManifestFormPeriod is Called with {@Id}", command.Id);

        var companyManifestFormPeriod = await companyManifestFormPeriodRepository.GetCompanyManifestFormPeriodByIdAsync(command.Id, false, false, cancellationToken);
        if (companyManifestFormPeriod is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "مخزن نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyManifestFormPeriod.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await companyManifestFormRepository.AnyIssunedManifestFormByDomesticPeriodIdAsync(command.Id, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "فرم مانیفست صادر شده وجود دارد");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var listManifestFormId = await companyManifestFormRepository.GetManifestFormIdByManifestFormPeriodIdAsync(command.Id, cancellationToken);
        for (int i = 0; i < listManifestFormId.Count; i++)
        {
            await companyManifestFormRepository.DeleteManifestFormAsync(listManifestFormId[i], cancellationToken);
        }
        await companyManifestFormPeriodRepository.DeleteCompanyManifestFormPeriodAsync(command.Id, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("CompanyManifestFormPeriod Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "مخزن با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> SetCompanyManifestFormPeriodActivityStatusAsync(UpdateActiveStateCompanyManifestFormPeriodCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyManifestFormPeriodActivityStatus Called with {@Id}", command.Id);

        var companyManifestFormPeriod = await companyManifestFormPeriodRepository.GetCompanyManifestFormPeriodByIdAsync(command.Id, false, true,  cancellationToken);
        if (companyManifestFormPeriod is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "مخزن نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyManifestFormPeriod.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        companyManifestFormPeriod.Active = !companyManifestFormPeriod.Active;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyManifestFormPeriod activity status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت مخزن با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<CompanyManifestFormPeriodDto>> UpdateCompanyManifestFormPeriodAsync(UpdateCompanyManifestFormPeriodCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyManifestFormPeriod is Called with {@UpdateCompanyManifestFormPeriodCommand}", command);

        var manifestFormPeriod = await companyManifestFormPeriodRepository.GetCompanyManifestFormPeriodByIdAsync(command.Id, false, true, cancellationToken);
        if (manifestFormPeriod == null)
            return ApiResponse<CompanyManifestFormPeriodDto>.Error(StatusCodes.Status404NotFound, "مخزن نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(manifestFormPeriod.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyManifestFormPeriodDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyManifestFormPeriodDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyManifestFormPeriodDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (!command.StartNumber.ToString().StartsWith(company.Code.Trim()) ||
            !command.StartNumber.ToString().StartsWith(company.Code.Trim()))
            return ApiResponse<CompanyManifestFormPeriodDto>.Error(StatusCodes.Status400BadRequest, "شماره بارنامه باید با کد شرکت شروع شود");

        if (await companyManifestFormPeriodRepository.CheckExistCompanyManifestFormPeriodCodeAsync(command.Code, manifestFormPeriod.CompanyId, manifestFormPeriod.Id, cancellationToken))
            return ApiResponse<CompanyManifestFormPeriodDto>.Error(400, "کد مخزن تکراری است");

        var companyManifestFormPeriods = await companyManifestFormPeriodRepository.GetCompanyManifestFormPeriodByCompanyIdAsync(company.Id, cancellationToken);
        if (companyManifestFormPeriods == null)
            return ApiResponse<CompanyManifestFormPeriodDto>.Error(StatusCodes.Status404NotFound, "مشکل در دریافت اطلاعات");

        for (int i = 0; i < companyManifestFormPeriods.Count; i++)
        {
            if (companyManifestFormPeriods[i].Id != manifestFormPeriod.Id && companyManifestFormPeriods[i].StartNumber <= command.EndNumber && command.StartNumber <= companyManifestFormPeriods[i].EndNumber)
            {
                return ApiResponse<CompanyManifestFormPeriodDto>.Error(StatusCodes.Status409Conflict, "مخزن تداخل دارد");
            }
        }

        if (await companyManifestFormRepository.AnyIssunedManifestFormByDomesticPeriodIdStartNumberEndNumberAsync(command.Id, command.StartNumber, manifestFormPeriod.EndNumber, cancellationToken))
            return ApiResponse<CompanyManifestFormPeriodDto>.Error(StatusCodes.Status409Conflict, "بارنامه صادر شده وجود دارد");

        bool StartIsExpand = manifestFormPeriod.StartNumber > command.StartNumber;
        bool EndIsExpand = manifestFormPeriod.EndNumber < command.EndNumber;
        bool StartIsLesser = manifestFormPeriod.StartNumber < command.StartNumber;
        bool EndIsLesser = manifestFormPeriod.EndNumber > command.EndNumber;

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        if (StartIsLesser)
        {
            List<int> listManifestFormId = await companyManifestFormRepository.GetManifestFormIdByManifestFormPeriodIdAndLessThanStartNumberAsync(command.Id, command.StartNumber, cancellationToken);
            for (int i = 0; i < listManifestFormId.Count; i++)
            {
                await companyManifestFormRepository.DeleteManifestFormAsync(listManifestFormId[i], cancellationToken);
            }
        }

        if (EndIsLesser)
        {
            List<int> listManifestFormId = await companyManifestFormRepository.GetManifestFormIdByManifestFormPeriodIdAndGreatherThanEndNumberAsync(command.Id, command.EndNumber, cancellationToken);
            for (int i = 0; i < listManifestFormId.Count; i++)
            {
                await companyManifestFormRepository.DeleteManifestFormAsync(listManifestFormId[i], cancellationToken);
            }
        }

        if (StartIsExpand)
        {
            for (long i = command.StartNumber; i < manifestFormPeriod.StartNumber && i <= command.EndNumber; i++)
            {
                await companyManifestFormRepository.InsertManifestFormAsync(new CompanyManifestForm
                {
                    No = i,
                    CompanySenderId = company.Id,
                    CompanyReceiverId = null,
                    CompanyReceiverUserInsertedCode = null,
                    SourceCountryId = company.CountryId,
                    SourceProvinceId = company.ProvinceId,
                    SourceCityId = company.CityId,
                    DestinationCountryId = null,
                    DestinationProvinceId = null,
                    DestinationCityId = null,
                    CompanyManifestFormPeriodId = command.Id,
                    Date = null,
                    CompanySenderDescription = null,
                    CompanySenderDescriptionForPrint = null,
                    CompanyReceiverDescription = null,
                    MasterWaybillNo = null,
                    MasterWaybillWeight = null,
                    MasterWaybillAirline = null,
                    MasterWaybillFlightNo = null,
                    MasterWaybillFlightDate = null,
                    State = (short)CompanyManifestFormState.Ready,
                    DateIssued = null,
                    TimeIssued = null,
                    DateAirlineDelivery = null,
                    TimeAirlineDelivery = null,
                    DateReceivedAtReceiverCompany = null,
                    TimeReceivedAtReceiverCompany = null,
                    CounterId = null,
                    Dirty = null,
                }, cancellationToken);
            }
        }

        if (EndIsExpand)
        {
            List<string> storeProcedure = new List<string>();

            for (long i = Math.Max(command.StartNumber, manifestFormPeriod.EndNumber) + 1; i <= command.EndNumber; i++)
            {
                await companyManifestFormRepository.InsertManifestFormAsync(new CompanyManifestForm
                {

                    No = i,
                    CompanySenderId = company.Id,
                    CompanyReceiverId = null,
                    CompanyReceiverUserInsertedCode = null,
                    SourceCountryId = company.CountryId,
                    SourceProvinceId = company.ProvinceId,
                    SourceCityId = company.CityId,
                    DestinationCountryId = null,
                    DestinationProvinceId = null,
                    DestinationCityId = null,
                    CompanyManifestFormPeriodId = command.Id,
                    Date = null,
                    CompanySenderDescription = null,
                    CompanySenderDescriptionForPrint = null,
                    CompanyReceiverDescription = null,
                    MasterWaybillNo = null,
                    MasterWaybillWeight = null,
                    MasterWaybillAirline = null,
                    MasterWaybillFlightNo = null,
                    MasterWaybillFlightDate = null,
                    State = (short)CompanyManifestFormState.Ready,
                    DateIssued = null,
                    TimeIssued = null,
                    DateAirlineDelivery = null,
                    TimeAirlineDelivery = null,
                    DateReceivedAtReceiverCompany = null,
                    TimeReceivedAtReceiverCompany = null,
                    CounterId = null,
                    Dirty = null,
                }, cancellationToken);
            }
        }

        var updatedManifestFormPeriod = mapper.Map(command, manifestFormPeriod);
        if (updatedManifestFormPeriod is null)
            return ApiResponse<CompanyManifestFormPeriodDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("CompanyManifestFormPeriod updated successfully with {@UpdateCompanyManifestFormPeriodCommand}", command);

        var updatedManifestFormPeriodDto = mapper.Map<CompanyManifestFormPeriodDto>(updatedManifestFormPeriod);
        return ApiResponse<CompanyManifestFormPeriodDto>.Ok(updatedManifestFormPeriodDto, "مخزن با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyManifestFormPeriodDto>>> GetAllCompanyManifestFormPeriodsAsync(GetAllCompanyManifestFormPeriodsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyManifestFormPeriods is Called");

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<PagedResult<CompanyManifestFormPeriodDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyManifestFormPeriodDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<PagedResult<CompanyManifestFormPeriodDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var (companyManifestFormPeriods, totalCount) = await companyManifestFormPeriodRepository.GetAllCompanyManifestFormPeriodsAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyId,
            query.Active,
            query.HasReadyForm,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);

        var manifestFormPeriodDtos = mapper.Map<IReadOnlyList<CompanyManifestFormPeriodDto>>(companyManifestFormPeriods) ?? Array.Empty<CompanyManifestFormPeriodDto>();
        if (manifestFormPeriodDtos == null)
            return ApiResponse<PagedResult<CompanyManifestFormPeriodDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company manifestForm periods", manifestFormPeriodDtos.Count);

        var data = new PagedResult<CompanyManifestFormPeriodDto>(manifestFormPeriodDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyManifestFormPeriodDto>>.Ok(data, "محتوهای بار با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<CompanyManifestFormPeriodDto>> GetCompanyManifestFormPeriodByIdAsync(GetCompanyManifestFormPeriodByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyManifestFormPeriodById is Called with {@Id}", query.Id);

        var manifestFormPeriod = await companyManifestFormPeriodRepository.GetCompanyManifestFormPeriodByIdAsync(query.Id, false, false, cancellationToken);
        if (manifestFormPeriod is null)
            return ApiResponse<CompanyManifestFormPeriodDto>.Error(404, "مخزن یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(manifestFormPeriod.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyManifestFormPeriodDto>.Error(404, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyManifestFormPeriodDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyManifestFormPeriodDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyManifestFormPeriodDto = mapper.Map<CompanyManifestFormPeriodDto>(manifestFormPeriod);
        if (companyManifestFormPeriodDto == null)
            return ApiResponse<CompanyManifestFormPeriodDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("CompanyManifestFormPeriod retrieved successfully with {@Id}", query.Id);

        return ApiResponse<CompanyManifestFormPeriodDto>.Ok(companyManifestFormPeriodDto, "مخزن با موفقیت دریافت شد");
    }
}
