using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Commands.Create;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Commands.Delete;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Dtos;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Queries.GetAll;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Queries.GetByDomesticPathId;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Queries.GetById;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Services;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Addresses;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Services;

public class CompanyDomesticPathReceiverCompanyService(
ILogger<CompanyDomesticPathService> logger,
IMapper mapper,
IUnitOfWork unitOfWork,
IUserContext userContext,
ICompanyDomesticPathRepository companyDomesticPathRepository,
ICompanyDomesticPathReceiverCompanyRepository companyDomesticPathReceiverCompanyRepository,
ICompanyRepository companyRepository,
IAreaRepository areaRepository)
: ICompanyDomesticPathReceiverCompanyService
{
    public async Task<ApiResponse<int>> CreateCompanyDomesticPathReceiverCompanyAsync(CreateCompanyDomesticPathReceiverCompanyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyDomesticPathReceiverCompany is Called with {@CreateCompanyDomesticPathReceiverCompanyCommand}", command);

        var companyDomesticPath = await companyDomesticPathRepository.GetCompanyDomesticPathByIdAsync(command.CompanyDomesticPathId, false, false, false, false, cancellationToken);
        if (companyDomesticPath == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "مسیر نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticPath.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "نماینده نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (!await areaRepository.CheckExistAreaByIdAndParentId(command.MunicipalAreaId, (int)AreaType.RegionMunicipality, companyDomesticPath.DestinationCityId, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "اطلاعات منطقه شهری نامعتبر است");

        if (command.ReceiverCompanyId != null && await companyRepository.GetCompanyByIdAsync((int)command.ReceiverCompanyId, false, false, cancellationToken) == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "نماینده مقصد نامعتبر است");

        if (await companyDomesticPathReceiverCompanyRepository.CheckExistCompanyDomesticPathReceiverCompanyAsync(command.CompanyDomesticPathId, command.MunicipalAreaId, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "نماینده تکراری است");

        var companyDomesticPathReceiverCompany = mapper.Map<CompanyDomesticPathReceiverCompany>(command) ?? null;
        if (companyDomesticPathReceiverCompany == null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        var companyDomesticPathReceiversId = await companyDomesticPathReceiverCompanyRepository.CreateCompanyDomesticPathCompanyReceiverAsync(companyDomesticPathReceiverCompany, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticPathReceiverCompany created successfully with {@CompanyDomesticPathReceiverCompany}", companyDomesticPathReceiverCompany);
        return ApiResponse<int>.Ok(companyDomesticPathReceiversId, "نماینده مسیر با موفقیت ایجاد شد");
    }

    public async Task DeleteCompanyDomesticPathReceiverCompanyAsync(DeleteCompanyDomesticPathReceiverCompanyCommand command, CancellationToken cancellationToken)
    {
        await Task.Yield();
    }

    public async Task<ApiResponse<CompanyDomesticPathReceiverCompanyDto>> UpdateCompanyDomesticPathReceiverCompanyAsync(UpdateCompanyDomesticPathReceiverCompanyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyDomesticPathReceiverCompany is Called with {@UpdateCompanyDomesticPathReceiverCompanyCommand}", command);

        var companyDomesticPathReceiverCompany = await companyDomesticPathReceiverCompanyRepository.GetCompanyDomesticPathReceiverCompanyByIdAsync(command.Id, false, false, cancellationToken);
        if (companyDomesticPathReceiverCompany is null)
            return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Error(StatusCodes.Status404NotFound, "نماینده مسیر نامعتبر است");

        var companyDomesticPath = await companyDomesticPathRepository.GetCompanyDomesticPathByIdAsync(companyDomesticPathReceiverCompany.CompanyDomesticPathId, false, false, false, false, cancellationToken);
        if (companyDomesticPath == null)
            return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Error(StatusCodes.Status404NotFound, "مسیر نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticPath.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Error(StatusCodes.Status404NotFound, "نماینده نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (command.ReceiverCompanyId != null && await companyRepository.GetCompanyByIdAsync((int)command.ReceiverCompanyId, false, false, cancellationToken) == null)
            return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Error(StatusCodes.Status404NotFound, "نماینده مقصد نامعتبر است");

        var updatedCompanyDomesticPathReceiverCompany = mapper.Map(command, companyDomesticPathReceiverCompany);
        if (updatedCompanyDomesticPathReceiverCompany == null)
            return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticPathReceiverCompany updated successfully with {@UpdateCompanyDomesticPathReceiverCompanyCommand}", command);

        var updatedCompanyDomesticPathReceiverCompanyDto = mapper.Map<CompanyDomesticPathReceiverCompanyDto>(updatedCompanyDomesticPathReceiverCompany);
        if (updatedCompanyDomesticPathReceiverCompanyDto == null)
            return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Ok(updatedCompanyDomesticPathReceiverCompanyDto, "نماینده مسیر با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyDomesticPathReceiverCompanyDto>>> GetAllCompanyDomesticPathReceiverCompaniesAsync(GetAllCompanyDomesticPathReceiverCompanyQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyDomesticPathReceiverCompanies is Called");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyDomesticPathReceiverCompanyDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (query.CompanyPathId != 0)
        {
            var companyDomesticPath = await companyDomesticPathRepository.GetCompanyDomesticPathByIdAsync(query.CompanyPathId, false, false, false, false, cancellationToken);
            if (companyDomesticPath == null)
                return ApiResponse<PagedResult<CompanyDomesticPathReceiverCompanyDto>>.Error(StatusCodes.Status404NotFound, "مسیر نامعتبر است");

            var company = await companyRepository.GetCompanyByIdAsync(companyDomesticPath.CompanyId, true, false, cancellationToken);
            if (company is null)
                return ApiResponse<PagedResult<CompanyDomesticPathReceiverCompanyDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<PagedResult<CompanyDomesticPathReceiverCompanyDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyPathId == 0)
        {
            if (!user.IsSuperAdmin())
                return ApiResponse<PagedResult<CompanyDomesticPathReceiverCompanyDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }

        var (companyDomesticPathReceivers, totalCount) = await companyDomesticPathReceiverCompanyRepository.GetAllCompanyDomesticPathReceiverCompaniesAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyPathId,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);

        var companyDomesticPathReceiverCompanyDtos = mapper.Map<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>(companyDomesticPathReceivers) ?? Array.Empty<CompanyDomesticPathReceiverCompanyDto>();
        if (companyDomesticPathReceiverCompanyDtos == null)
            return ApiResponse<PagedResult<CompanyDomesticPathReceiverCompanyDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company domestic path receiver companies", companyDomesticPathReceiverCompanyDtos.Count);

        var data = new PagedResult<CompanyDomesticPathReceiverCompanyDto>(companyDomesticPathReceiverCompanyDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyDomesticPathReceiverCompanyDto>>.Ok(data, "نماینده های مسیر با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>> GetCompanyDomesticPathReceiverCompanyByDomesticPathIdAsync(GetCompanyDomesticPathReceiverCompanyByDomesticPathIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyDomesticPathReceiverCompanyByCompanyId is Called with {@Id}", query.DomesticPathId);

        var companyDomesticPath = await companyDomesticPathRepository.GetCompanyDomesticPathByIdAsync(query.DomesticPathId, false, false, false, false, cancellationToken);
        if (companyDomesticPath == null)
            return ApiResponse<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>.Error(StatusCodes.Status404NotFound, "مسیر نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticPath.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyDomesticPathReceiverCompanies = await companyDomesticPathReceiverCompanyRepository.GetCompanyDomesticPathReceiverCompanyByDomesticPathIdAsync(query.DomesticPathId, cancellationToken);
        if (companyDomesticPathReceiverCompanies is null)
            return ApiResponse<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>.Error(StatusCodes.Status404NotFound, "مسیر یافت نشد");

        var companyDomesticPathReceiverCompanyDtos = mapper.Map<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>(companyDomesticPathReceiverCompanies);
        if (companyDomesticPathReceiverCompanyDtos == null)
            return ApiResponse<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company domestic path receiver company ByDomesticPathId", companyDomesticPathReceiverCompanyDtos.Count);

        return ApiResponse<IReadOnlyList<CompanyDomesticPathReceiverCompanyDto>>.Ok(companyDomesticPathReceiverCompanyDtos, "نماینده های مسیر با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<CompanyDomesticPathReceiverCompanyDto>> GetCompanyDomesticPathReceiverCompanyByIdAsync(GetCompanyDomesticPathReceiverCompanyByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyDomesticPathReceiverCompanyById is Called with {@Id}", query.Id);

        var companyDomesticPathReceiverCompany = await companyDomesticPathReceiverCompanyRepository.GetCompanyDomesticPathReceiverCompanyByIdAsync(query.Id, false, false, cancellationToken);
        if (companyDomesticPathReceiverCompany is null)
            return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Error(StatusCodes.Status404NotFound, "مسیر نامعتبر است");

        var companyDomesticPath = await companyDomesticPathRepository.GetCompanyDomesticPathByIdAsync(companyDomesticPathReceiverCompany.CompanyDomesticPathId, false, false, false, false, cancellationToken);
        if (companyDomesticPath == null)
            return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Error(StatusCodes.Status404NotFound, "مسیر نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticPath.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Error(404, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyDomesticPathReceiverCompanyDto = mapper.Map<CompanyDomesticPathReceiverCompanyDto>(companyDomesticPathReceiverCompany);
        if (companyDomesticPathReceiverCompanyDto == null)
            return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("CompanyDomesticPathReceiverCompany retrieved successfully with {@Id}", query.Id);
        return ApiResponse<CompanyDomesticPathReceiverCompanyDto>.Ok(companyDomesticPathReceiverCompanyDto, "نماینده مسیر با موفقیت دریافت شد");
    }
}



