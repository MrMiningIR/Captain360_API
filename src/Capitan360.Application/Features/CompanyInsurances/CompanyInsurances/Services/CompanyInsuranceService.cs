using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Create;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Delete;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Update;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.UpdateActiveState;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Dtos;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Queries.GetAll;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Queries.GetByCompanyId;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Queries.GetById;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Entities.CompanyInsurances;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Interfaces.Repositories.CompanyInsurances;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Services;

public class CompanyInsuranceService(
    ILogger<CompanyInsuranceService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyInsuranceRepository companyInsuranceRepository,
    IUserContext userContext,
    ICompanyRepository companyRepository)
    : ICompanyInsuranceService
{
    public async Task<ApiResponse<int>> CreateCompanyInsuranceAsync(CreateCompanyInsuranceCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyInsurance is Called with {@CreateCompanyInsuranceCommand}", command);

        var company = await companyRepository.GetCompanyByIdAsync(command.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await companyInsuranceRepository.CheckExistCompanyInsuranceNameAsync(command.Name, command.CompanyId, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "نام شرکت بیمه تکراری است");

        if (await companyInsuranceRepository.CheckExistCompanyInsuranceCodeAsync(command.Code, command.CompanyId, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "کد شرکت بیمه تکراری است");

        var companyInsurance = mapper.Map<CompanyInsurance>(command) ?? null;
        if (companyInsurance == null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        var companyInsuranceId = await companyInsuranceRepository.CreateCompanyInsuranceAsync(companyInsurance, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyInsurance created successfully with {@CompanyInsurance}", companyInsurance);
        return ApiResponse<int>.Ok(companyInsuranceId, "شرکت بیمه با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyInsuranceAsync(DeleteCompanyInsuranceCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyInsurance is Called with {@Id}", command.Id);

        var companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceByIdAsync(command.Id, false, false, cancellationToken);
        if (companyInsurance is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت بیمه نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyInsurance.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        await companyInsuranceRepository.DeleteCompanyInsuranceAsync(companyInsurance.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyInsurance Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "شرکت بیمه با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> SetCompanyInsuranceActivityStatusAsync(UpdateActiveStateCompanyInsuranceCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyInsuranceActivityStatus Called with {@Id}", command.Id);

        var companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceByIdAsync(command.Id, false, true, cancellationToken);
        if (companyInsurance is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت بیمه نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyInsurance.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        companyInsurance.Active = !companyInsurance.Active;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyInsurance activity status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت شرکت بیمه با موفقیت به روز رسانی شد");
    }

    public async Task<ApiResponse<CompanyInsuranceDto>> UpdateCompanyInsuranceAsync(UpdateCompanyInsuranceCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyInsurance is Called with {@UpdateCompanyInsuranceCommand}", command);

        var companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceByIdAsync(command.Id, false, true, cancellationToken);
        if (companyInsurance is null)
            return ApiResponse<CompanyInsuranceDto>.Error(StatusCodes.Status404NotFound, "شرکت بیمه نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyInsurance.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyInsuranceDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyInsuranceDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyInsuranceDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await companyInsuranceRepository.CheckExistCompanyInsuranceNameAsync(command.Name, command.Id, companyInsurance.CompanyId, cancellationToken))
            return ApiResponse<CompanyInsuranceDto>.Error(StatusCodes.Status409Conflict, "نام شرکت بیمه تکراری است");

        if (await companyInsuranceRepository.CheckExistCompanyInsuranceCodeAsync(command.Code, command.Id, companyInsurance.CompanyId, cancellationToken))
            return ApiResponse<CompanyInsuranceDto>.Error(StatusCodes.Status409Conflict, "کد شرکت بیمه تکراری است");

        var updatedCompanyInsurance = mapper.Map(command, companyInsurance);
        if (updatedCompanyInsurance == null)
            return ApiResponse<CompanyInsuranceDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyInsurance updated successfully with {@UpdateCompanyInsuranceCommand}", command);

        var updatedCompanyInsuranceDto = mapper.Map<CompanyInsuranceDto>(updatedCompanyInsurance);
        if (updatedCompanyInsuranceDto == null)
            return ApiResponse<CompanyInsuranceDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<CompanyInsuranceDto>.Ok(updatedCompanyInsuranceDto, "شرکت بیمه با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyInsuranceDto>>> GetAllCompanyInsurancesAsync(GetAllCompanyInsurancesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyInsurances is Called");
        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (query.CompanyId != 0)
        {
            var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, true, false, cancellationToken);
            if (company is null)
                return ApiResponse<PagedResult<CompanyInsuranceDto>>.Error(400, "شرکت نامعتبر است");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<PagedResult<CompanyInsuranceDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId == 0)
        {
            if (!user.IsSuperAdmin())
                return ApiResponse<PagedResult<CompanyInsuranceDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }

        var (companyInsurances, totalCount) = await companyInsuranceRepository.GetAllCompanyInsurancesAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyId,
            query.Active,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);

        var companyInsuranceDtos = mapper.Map<IReadOnlyList<CompanyInsuranceDto>>(companyInsurances) ?? Array.Empty<CompanyInsuranceDto>();
        if (companyInsuranceDtos == null)
            return ApiResponse<PagedResult<CompanyInsuranceDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company insurances", companyInsuranceDtos.Count);

        var data = new PagedResult<CompanyInsuranceDto>(companyInsuranceDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyInsuranceDto>>.Ok(data, "شرکت های بیمه با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<IReadOnlyList<CompanyInsuranceDto>>> GetCompanyInsuranceByCompanyIdAsync(GetCompanyInsuranceByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyInsuranceByCompanyId is Called with {@Id}", query.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyInsurances = await companyInsuranceRepository.GetCompanyInsuranceByCompanyIdAsync(query.CompanyId, cancellationToken);
        if (companyInsurances is null)
            return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status404NotFound, "شرکت بیمه  یافت نشد");

        var companyInsuranceDtos = mapper.Map<IReadOnlyList<CompanyInsuranceDto>>(companyInsurances) ?? Array.Empty<CompanyInsuranceDto>();
        if (companyInsuranceDtos == null)
            return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company insurance GetByCompanyId", companyInsuranceDtos.Count);

        return ApiResponse<IReadOnlyList<CompanyInsuranceDto>>.Ok(companyInsuranceDtos, "شرکت های بیمه با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<CompanyInsuranceDto>> GetCompanyInsuranceByIdAsync(GetCompanyInsuranceByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyInsuranceById is Called with {@Id}", query.Id);

        var companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceByIdAsync(query.Id, false, false, cancellationToken);
        if (companyInsurance is null)
            return ApiResponse<CompanyInsuranceDto>.Error(StatusCodes.Status404NotFound, "شرکت بیمه  یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companyInsurance.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyInsuranceDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyInsuranceDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyInsuranceDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyInsuranceDto = mapper.Map<CompanyInsuranceDto>(companyInsurance);
        if (companyInsuranceDto == null)
            return ApiResponse<CompanyInsuranceDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("CompanyInsurance retrieved successfully with {@Id}", query.Id);
        return ApiResponse<CompanyInsuranceDto>.Ok(companyInsuranceDto, "شرکت بیمه با موفقیت دریافت شد");
    }
}