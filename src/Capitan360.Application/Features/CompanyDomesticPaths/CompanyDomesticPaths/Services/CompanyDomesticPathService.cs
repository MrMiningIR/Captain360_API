using AutoMapper;
using Capitan360.Application.Common;
using Microsoft.Extensions.Logging;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Create;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Delete;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.UpdateActiveState;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Dtos;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Queries.GetAll;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Queries.GetByCompanyId;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Queries.GetById;
using Capitan360.Domain.Interfaces;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Interfaces.Repositories.Addresses;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Microsoft.AspNetCore.Http;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Commands.Create;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Services;

public class CompanyDomesticPathService(
    ILogger<CompanyDomesticPathService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyDomesticPathRepository companyDomesticPathRepository,
    ICompanyDomesticPathReceiverCompanyRepository companyDomesticPathReceiverCompanyRepository,
    ICompanyRepository companyRepository,
    IAreaRepository areaRepository)
    : ICompanyDomesticPathService
{
    public async Task<ApiResponse<int>> CreateCompanyDomesticPathAsync(CreateCompanyDomesticPathCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyDomesticPath is Called with {@CreateCompanyDomesticPathCommand}", command);

        var company = await companyRepository.GetCompanyByIdAsync(command.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await companyDomesticPathRepository.CheckExistCompanyDomesticPathAsync(command.SourceCityId, command.DestinationCityId, command.CompanyId, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "مسیر تکراری است");

        if (company.CountryId != command.SourceCountryId ||
            company.ProvinceId != command.SourceProvinceId ||
            company.CityId != command.SourceCityId)
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "اطلاعات شهر مبدا با شرکت همخوانی ندارد است");

        if (!await areaRepository.CheckExistAreaByIdAndParentId(command.DestinationCityId, (int)AreaType.City, command.DestinationProvinceId, cancellationToken) ||
            !await areaRepository.CheckExistAreaByIdAndParentId(command.DestinationProvinceId, (int)AreaType.Province, command.DestinationCountryId, cancellationToken) ||
            !await areaRepository.CheckExistAreaByIdAndParentId(command.DestinationCountryId, (int)AreaType.Country, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "اطلاعات شهر مقصد نامعتبر است");

        var companyDomesticPath = mapper.Map<CompanyDomesticPath>(command);
        if (companyDomesticPath == null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var (destinationCityRegionMunicipalities, totalCount) = await areaRepository.GetAllRegionMunicipality("", 100, 1, null, SortDirection.Ascending, command.DestinationCityId, false, cancellationToken);
        if (totalCount == 0)
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "اطلاعات مناطق شهری مقصد نامعتبر است");

        var companyDomesticPathsId = await companyDomesticPathRepository.CreateCompanyDomesticPathAsync(companyDomesticPath, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var item in destinationCityRegionMunicipalities)
        {
            var companyDomesticPathReceiverCompany = mapper.Map<CompanyDomesticPathReceiverCompany>(item);
            if (companyDomesticPathReceiverCompany == null)
                return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

            await companyDomesticPathReceiverCompanyRepository.CreateCompanyDomesticPathCompanyReceiverAsync(companyDomesticPathReceiverCompany, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticPath created successfully with {@CompanyDomesticPath}", companyDomesticPath);
        return ApiResponse<int>.Created(companyDomesticPathsId, "مسیر با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyDomesticPathAsync(DeleteCompanyDomesticPathCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyDomesticPath is Called with {@Id}", command.Id);

        var companyDomesticPath = await companyDomesticPathRepository.GetCompanyDomesticPathByIdAsync(command.Id, false, false, false, false, cancellationToken);
        if (companyDomesticPath is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "مسیر نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticPath.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        await companyDomesticPathRepository.DeleteCompanyDomesticPathAsync(companyDomesticPath.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticPath Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "مسیر با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> SetCompanyDomesticPathActivityStatusAsync(UpdateActiveStateCompanyDomesticPathCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyDomesticPathActivityStatus Called with {@Id}", command.Id);

        var companyDomesticPath = await companyDomesticPathRepository.GetCompanyDomesticPathByIdAsync(command.Id, false, false, false, true, cancellationToken);
        if (companyDomesticPath is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "مسیر نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticPath.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        companyDomesticPath.Active = !companyDomesticPath.Active;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticPath activity status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت مسیر با موفقیت به روز رسانی شد");
    }

    public async Task<ApiResponse<CompanyDomesticPathDto>> UpdateCompanyDomesticPathAsync(UpdateCompanyDomesticPathCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyDomesticPath is Called with {@UpdateCompanyDomesticPathCommand}", command);

        var companyDomesticPath = await companyDomesticPathRepository.GetCompanyDomesticPathByIdAsync(command.Id, false, false, false, true, cancellationToken);
        if (companyDomesticPath is null)
            return ApiResponse<CompanyDomesticPathDto>.Error(StatusCodes.Status404NotFound, "مسیر نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticPath.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyDomesticPathDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyDomesticPathDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyDomesticPathDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var updatedCompanyDomesticPath = mapper.Map(command, companyDomesticPath);
        if (updatedCompanyDomesticPath == null)
            return ApiResponse<CompanyDomesticPathDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticPath updated successfully with {@UpdateCompanyDomesticPathCommand}", command);

        var updatedCompanyDomesticPathDto = mapper.Map<CompanyDomesticPathDto>(updatedCompanyDomesticPath);
        if (updatedCompanyDomesticPathDto == null)
            return ApiResponse<CompanyDomesticPathDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<CompanyDomesticPathDto>.Ok(updatedCompanyDomesticPathDto, "مسیر با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyDomesticPathDto>>> GetAllCompanyDomesticPathsAsync(GetAllCompanyDomesticPathsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyDomesticPaths is Called");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyDomesticPathDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (query.CompanyId != 0)
        {
            var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, true, false, cancellationToken);
            if (company is null)
                return ApiResponse<PagedResult<CompanyDomesticPathDto>>.Error(400, "شرکت نامعتبر است");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<PagedResult<CompanyDomesticPathDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId == 0)
        {
            if (!user.IsSuperAdmin())
                return ApiResponse<PagedResult<CompanyDomesticPathDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }

        var (companyDomesticPaths, totalCount) = await companyDomesticPathRepository.GetAllCompanyDomesticPathsAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyId,
            query.Active,
            query.SourceCityId,
            query.DestinationCityId,
            true,
            true,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);

        var companyDomesticPathDtos = mapper.Map<IReadOnlyList<CompanyDomesticPathDto>>(companyDomesticPaths) ?? Array.Empty<CompanyDomesticPathDto>();
        if (companyDomesticPathDtos == null)
            return ApiResponse<PagedResult<CompanyDomesticPathDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company domestic paths", companyDomesticPathDtos.Count);

        var data = new PagedResult<CompanyDomesticPathDto>(companyDomesticPathDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyDomesticPathDto>>.Ok(data, "مسیرها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<IReadOnlyList<CompanyDomesticPathDto>>> GetCompanyDomesticPathByCompanyIdAsync(GetCompanyDomesticPathByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyDomesticPathByCompanyId is Called with {@Id}", query.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<IReadOnlyList<CompanyDomesticPathDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<IReadOnlyList<CompanyDomesticPathDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<IReadOnlyList<CompanyDomesticPathDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyDomesticPaths = await companyDomesticPathRepository.GetCompanyDomesticPathsByCompanyIdAsync(query.CompanyId, cancellationToken);
        if (companyDomesticPaths is null)
            return ApiResponse<IReadOnlyList<CompanyDomesticPathDto>>.Error(StatusCodes.Status404NotFound, "مسیر یافت نشد");

        var companyDomesticPathDtos = mapper.Map<IReadOnlyList<CompanyDomesticPathDto>>(companyDomesticPaths) ?? Array.Empty<CompanyDomesticPathDto>();
        if (companyDomesticPathDtos == null)
            return ApiResponse<IReadOnlyList<CompanyDomesticPathDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company domestic paths GetByCompanyId", companyDomesticPaths.Count);

        return ApiResponse<IReadOnlyList<CompanyDomesticPathDto>>.Ok(companyDomesticPathDtos, "مسیرها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<CompanyDomesticPathDto>> GetCompanyDomesticPathByIdAsync(GetCompanyDomesticPathByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyDomesticPathById is Called with {@Id}", query.Id);

        var companyDomesticPath = await companyDomesticPathRepository.GetCompanyDomesticPathByIdAsync(query.Id, false, false, false, true, cancellationToken);
        if (companyDomesticPath is null)
            return ApiResponse<CompanyDomesticPathDto>.Error(StatusCodes.Status404NotFound, "مسیر یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticPath.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyDomesticPathDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyDomesticPathDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyDomesticPathDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyDomesticPathDto = mapper.Map<CompanyDomesticPathDto>(companyDomesticPath);
        if (companyDomesticPathDto == null)
            return ApiResponse<CompanyDomesticPathDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("CompanyDomesticPath retrieved successfully with {@Id}", query.Id);
        return ApiResponse<CompanyDomesticPathDto>.Ok(companyDomesticPathDto, "مسیر با موفقیت دریافت شد");
    }
}