using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Companies;
using Microsoft.Extensions.Logging;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Entities.CompanyInsurances;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Dtos;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Create;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Delete;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Update;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.UpdateActiveState;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Queries.GetAll;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Queries.GetByCompanyId;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Queries.GetById;
using Capitan360.Domain.Interfaces.Repositories.CompanyInsurances;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Services;

public class CompanyInsuranceService(
    ILogger<CompanyInsuranceService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyInsuranceRepository companyInsuranceRepository,
    ICompanyTypeRepository companyTypeRepository,
    ICompanyRepository companyRepository, IUserContext userContext)
    : ICompanyInsuranceService
{
    public async Task<ApiResponse<int>> CreateCompanyInsuranceAsync(CreateCompanyInsuranceCommand createCompanyInsuranceCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyInsurance is Called with {@CreateCompanyInsuranceCommand}", createCompanyInsuranceCommand);

        var company = await companyRepository.GetCompanyByIdAsync(createCompanyInsuranceCommand.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        if (await companyInsuranceRepository.CheckExistCompanyInsuranceNameAsync(createCompanyInsuranceCommand.Name, createCompanyInsuranceCommand.CompanyId, null, cancellationToken))
            return ApiResponse<int>.Error(400, "نام شرکت بیمه تکراری است");

        if (await companyInsuranceRepository.CheckExistCompanyInsuranceCodeAsync(createCompanyInsuranceCommand.Code, createCompanyInsuranceCommand.CompanyId, null, cancellationToken))
            return ApiResponse<int>.Error(400, "کد شرکت بیمه تکراری است");

        var companyInsurance = mapper.Map<CompanyInsurance>(createCompanyInsuranceCommand) ?? null;
        if (companyInsurance == null)
            return ApiResponse<int>.Error(400, "مشکل در عملیات تبدیل");

        var companyInsuranceId = await companyInsuranceRepository.CreateCompanyInsuranceAsync(companyInsurance, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyInsurance created successfully with ID: {CompanyInsuranceId}", companyInsuranceId);
        return ApiResponse<int>.Ok(companyInsuranceId, "شرکت بیمه با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyInsuranceAsync(DeleteCompanyInsuranceCommand deleteCompanyInsuranceCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyInsurance is Called with ID: {Id}", deleteCompanyInsuranceCommand.Id);

        var companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceByIdAsync(deleteCompanyInsuranceCommand.Id, true, false, cancellationToken);
        if (companyInsurance is null)
            return ApiResponse<int>.Error(400, $"Insurance شرکت بیمه نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyInsurance.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        await companyInsuranceRepository.DeleteCompanyInsuranceAsync(companyInsurance.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyInsurance soft-deleted successfully with ID: {Id}", deleteCompanyInsuranceCommand.Id);
        return ApiResponse<int>.Ok(deleteCompanyInsuranceCommand.Id, "شرکت بیمه با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> SetCompanyInsuranceActivityStatusAsync(UpdateActiveStateCompanyInsuranceCommand updateActiveStateCompanyInsuranceCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyInsuranceActivityStatus Called with {@UpdateActiveStateCompanyInsuranceCommand}", updateActiveStateCompanyInsuranceCommand);

        var companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceByIdAsync(updateActiveStateCompanyInsuranceCommand.Id, true, false, cancellationToken);
        if (companyInsurance is null)
            return ApiResponse<int>.Error(400, $"شرکت بیمه نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyInsurance.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        companyInsurance.Active = !companyInsurance.Active;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("وضعیت شرکت بیمه با موفقیت به‌روزرسانی شد: {Id}", updateActiveStateCompanyInsuranceCommand.Id);
        return ApiResponse<int>.Ok(updateActiveStateCompanyInsuranceCommand.Id);
    }

    public async Task<ApiResponse<CompanyInsuranceDto>> UpdateCompanyInsuranceAsync(UpdateCompanyInsuranceCommand updateCompanyInsuranceCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyInsurance is Called with {@UpdateCompanyInsuranceCommand}", updateCompanyInsuranceCommand);

        var companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceByIdAsync(updateCompanyInsuranceCommand.Id, true, false, cancellationToken);
        if (companyInsurance is null)
            return ApiResponse<CompanyInsuranceDto>.Error(400, $"شرکت بیمه نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyInsurance.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyInsuranceDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyInsuranceDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyInsuranceDto>.Error(403, "مجوز این فعالیت را ندارید");
        if (await companyInsuranceRepository.CheckExistCompanyInsuranceNameAsync(updateCompanyInsuranceCommand.Name, updateCompanyInsuranceCommand.Id, companyInsurance.CompanyId, cancellationToken))
            return ApiResponse<CompanyInsuranceDto>.Error(400, "نام شرکت بیمه تکراری است");

        if (await companyInsuranceRepository.CheckExistCompanyInsuranceCodeAsync(updateCompanyInsuranceCommand.Code, updateCompanyInsuranceCommand.Id, companyInsurance.CompanyId, cancellationToken))
            return ApiResponse<CompanyInsuranceDto>.Error(400, "کد شرکت بیمه تکراری است");

        var updatedCompanyInsurance = mapper.Map(updateCompanyInsuranceCommand, companyInsurance);

        if (updatedCompanyInsurance == null)
            return ApiResponse<CompanyInsuranceDto>.Error(400, "مشکل در عملیات تبدیل");
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("شرکت بیمه با موفقیت به‌روزرسانی شد: {Id}", updateCompanyInsuranceCommand.Id);

        var updatedCompanyInsuranceDto = mapper.Map<CompanyInsuranceDto>(updatedCompanyInsurance);
        return ApiResponse<CompanyInsuranceDto>.Ok(updatedCompanyInsuranceDto, "شرکت بیمه با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyInsuranceDto>>> GetAllCompanyInsurancesAsync(GetAllCompanyInsurancesQuery getAllCompanyInsurancesQuery, CancellationToken cancellationToken)
    {
        //logger.LogInformation("GetAllCompanyInsurancesByCompanyType is Called");
        //var user = userContext.GetCurrentUser();
        //if (user == null)
        //    return ApiResponse<PagedResult<CompanyInsuranceDto>>.Error(400, "مشکل در دریافت اطلاعات");

        //if (getAllCompanyInsurancesQuery.CompanyId != 0)
        //{
        //    var company = await companyRepository.GetCompanyByIdAsync(getAllCompanyInsurancesQuery.CompanyId, true, false, cancellationToken);
        //    if (company is null)
        //        return ApiResponse<PagedResult<CompanyInsuranceDto>>.Error(400, $"شرکت نامعتبر است");

        //    if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
        //        return ApiResponse<PagedResult<CompanyInsuranceDto>>.Error(400, "مجوز این فعالیت را ندارید");
        //}
        //else if (getAllCompanyInsurancesQuery.CompanyId == 0)
        //{
        //    if (!user.IsSuperAdmin())
        //        return ApiResponse<PagedResult<CompanyInsuranceDto>>.Error(400, "مجوز این فعالیت را ندارید");
        //}

        //var (companyInsurances, totalCount) = await companyInsuranceRepository.GetMatchingAllCompanyInsurancesAsync(
        //    getAllCompanyInsurancesQuery.SearchPhrase,
        //    getAllCompanyInsurancesQuery.SortBy,
        //    getAllCompanyInsurancesQuery.CompanyId,
        //    getAllCompanyInsurancesQuery.Active,
        //    true,
        //    getAllCompanyInsurancesQuery.PageNumber,
        //    getAllCompanyInsurancesQuery.PageSize,
        //    getAllCompanyInsurancesQuery.SortDirection,
        //    cancellationToken);

        //var companyInsuranceDto = mapper.Map<IReadOnlyList<CompanyInsuranceDto>>(companyInsurances) ?? Array.Empty<CompanyInsuranceDto>();
        //logger.LogInformation("Retrieved {Count} package types", companyInsuranceDto.Count);

        //var data = new PagedResult<CompanyInsuranceDto>(companyInsuranceDto, totalCount, getAllCompanyInsurancesQuery.PageSize, getAllCompanyInsurancesQuery.PageNumber);
        //return ApiResponse<PagedResult<CompanyInsuranceDto>>.Ok(data, "Insurance  شرکت های بیمه با موفقیت دریافت شدند");

        throw new NotImplementedException();

    }

    public async Task<ApiResponse<CompanyInsuranceDto>> GetCompanyInsuranceByCompanyIdAsync(GetCompanyInsuranceByCompanyIdQuery getCompanyInsuranceByCompanyIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyInsuranceByCompanyId is Called with ID: {Id}", getCompanyInsuranceByCompanyIdQuery.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(getCompanyInsuranceByCompanyIdQuery.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyInsuranceDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyInsuranceDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyInsuranceDto>.Error(403, "مجوز این فعالیت را ندارید");

        var companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceByCompanyIdAsync(getCompanyInsuranceByCompanyIdQuery.CompanyId, false, true, cancellationToken);
        if (companyInsurance is null)
            return ApiResponse<CompanyInsuranceDto>.Error(400, $"شرکت بیمه  یافت نشد");

        var result = mapper.Map<CompanyInsuranceDto>(companyInsurance);
        logger.LogInformation("CompanyInsurance retrieved successfully with ID: {Id}", getCompanyInsuranceByCompanyIdQuery.CompanyId);
        return ApiResponse<CompanyInsuranceDto>.Ok(result, "شرکت بیمه با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<CompanyInsuranceDto>> GetCompanyInsuranceByIdAsync(GetCompanyInsuranceByIdQuery getCompanyInsuranceByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyInsuranceById is Called with ID: {Id}", getCompanyInsuranceByIdQuery.Id);

        var companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceByIdAsync(getCompanyInsuranceByIdQuery.Id, false, true, cancellationToken);
        if (companyInsurance is null)
            return ApiResponse<CompanyInsuranceDto>.Error(400, $"شرکت بیمه  یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companyInsurance.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyInsuranceDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyInsuranceDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyInsuranceDto>.Error(403, "مجوز این فعالیت را ندارید");

        var result = mapper.Map<CompanyInsuranceDto>(companyInsurance);
        logger.LogInformation("CompanyInsurance retrieved successfully with ID: {Id}", getCompanyInsuranceByIdQuery.Id);
        return ApiResponse<CompanyInsuranceDto>.Ok(result, "شرکت بیمه با موفقیت دریافت شد");
    }
}