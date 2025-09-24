using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Dtos;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Create;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Delete;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.UpdateActiveState;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Queries.GetAll;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Queries.GetByCompanyId;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Queries.GetById;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;
using Capitan360.Domain.Interfaces.Repositories.Addresses;
using Capitan360.Domain.Interfaces.Repositories.Companies;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Services;

public class CompanyDomesticPathsService(
    ILogger<CompanyDomesticPathsService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyDomesticPathsRepository companyDomesticPathsRepository, ICompanyRepository companyRepository, IAreaRepository areaRepository)
    : ICompanyDomesticPathsService
{


    public async Task<ApiResponse<int>> CreateCompanyDomesticPathAsync(CreateCompanyDomesticPathCommand createCompanyDomesticPathCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyDomesticPath is Called with {@CreateCompanyDomesticPathCommand}", createCompanyDomesticPathCommand);

        var company = await companyRepository.GetCompanyByIdAsync(createCompanyDomesticPathCommand.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        if (await companyDomesticPathsRepository.CheckExistCompanyDomesticPathPathAsync(createCompanyDomesticPathCommand.SourceCityId, createCompanyDomesticPathCommand.DestinationCityId, createCompanyDomesticPathCommand.CompanyId, null, cancellationToken))
            return ApiResponse<int>.Error(400, "مسیر تکراری است");

        if (company.CountryId != createCompanyDomesticPathCommand.SourceCountryId ||
            company.ProvinceId != createCompanyDomesticPathCommand.SourceProvinceId ||
            company.CityId != createCompanyDomesticPathCommand.SourceCityId)
            return ApiResponse<int>.Error(400, $"اطلاعات شهر مبدا نامعتبر است");

        if (!await areaRepository.CheckExistAreaByIdAndParentId(createCompanyDomesticPathCommand.DestinationCityId, (int)AreaType.City, createCompanyDomesticPathCommand.DestinationProvinceId, cancellationToken) ||
            !await areaRepository.CheckExistAreaByIdAndParentId(createCompanyDomesticPathCommand.DestinationProvinceId, (int)AreaType.Province, createCompanyDomesticPathCommand.DestinationCountryId, cancellationToken) ||
            !await areaRepository.CheckExistAreaByIdAndParentId(createCompanyDomesticPathCommand.DestinationCountryId, (int)AreaType.Country, null, cancellationToken))
            return ApiResponse<int>.Error(400, "اطلاعات شهر مقصد نامعتبر است");

        var companyDomesticPath = mapper.Map<CompanyDomesticPath>(createCompanyDomesticPathCommand);
        if (companyDomesticPath == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        var companyDomesticPathsId = await companyDomesticPathsRepository.CreateCompanyDomesticPathAsync(companyDomesticPath, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticPath created successfully with ID: {CompanyDomesticPathId}", companyDomesticPathsId);
        return ApiResponse<int>.Ok(companyDomesticPathsId, "مسیر با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyDomesticPathAsync(DeleteCompanyDomesticPathCommand deleteCompanyDomesticPathCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete CompanyDomesticPath is Called with ID: {Id}", deleteCompanyDomesticPathCommand.Id);

        var companyDomesticPath = await companyDomesticPathsRepository.GetCompanyDomesticPathByIdAsync(deleteCompanyDomesticPathCommand.Id, true, false, cancellationToken);
        if (companyDomesticPath is null)
            return ApiResponse<int>.Error(400, $"مسیر نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticPath.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        await companyDomesticPathsRepository.DeleteCompanyDomesticPathAsync(companyDomesticPath.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticPath soft-deleted successfully with ID: {Id}", deleteCompanyDomesticPathCommand.Id);
        return ApiResponse<int>.Ok(deleteCompanyDomesticPathCommand.Id, "مسیر با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> SetCompanyDomesticPathActivityStatusAsync(UpdateActiveStateCompanyDomesticPathCommand updateActiveStateCompanyDomesticPathCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyDomesticPathActivityStatus Called with {@UpdateActiveStateCompanyDomesticPathCommand}", updateActiveStateCompanyDomesticPathCommand);

        var companyDomesticPath = await companyDomesticPathsRepository.GetCompanyDomesticPathByIdAsync(updateActiveStateCompanyDomesticPathCommand.Id, true, false, cancellationToken);
        if (companyDomesticPath is null)
            return ApiResponse<int>.Error(400, $"مسیر نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticPath.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        companyDomesticPath.Active = !companyDomesticPath.Active;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("وضعیت مسیر با موفقیت به‌روزرسانی شد: {Id}", updateActiveStateCompanyDomesticPathCommand.Id);
        return ApiResponse<int>.Ok(updateActiveStateCompanyDomesticPathCommand.Id);
    }

    public async Task<ApiResponse<CompanyDomesticPathDto>> UpdateCompanyDomesticPathAsync(UpdateCompanyDomesticPathCommand updateCompanyDomesticPathCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyDomesticPath is Called with {@UpdateCompanyDomesticPathCommand}", updateCompanyDomesticPathCommand);

        var companyDomesticPath = await companyDomesticPathsRepository.GetCompanyDomesticPathByIdAsync(updateCompanyDomesticPathCommand.Id, true, false, cancellationToken);
        if (companyDomesticPath is null)
            return ApiResponse<CompanyDomesticPathDto>.Error(400, $"مسیر نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticPath.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyDomesticPathDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyDomesticPathDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyDomesticPathDto>.Error(403, "مجوز این فعالیت را ندارید");

        var updatedCompanyDomesticPath = mapper.Map(updateCompanyDomesticPathCommand, companyDomesticPath);

        if (updatedCompanyDomesticPath == null)
            return ApiResponse<CompanyDomesticPathDto>.Error(400, "مشکل در عملیات تبدیل");
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("مسیر با موفقیت به‌روزرسانی شد: {Id}", updateCompanyDomesticPathCommand.Id);

        var updatedCompanyDomesticPathDto = mapper.Map<CompanyDomesticPathDto>(updatedCompanyDomesticPath);
        return ApiResponse<CompanyDomesticPathDto>.Ok(updatedCompanyDomesticPathDto, "مسیر با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyDomesticPathDto>>> GetAllCompanyDomesticPathsAsync(GetAllCompanyDomesticPathsQuery getAllCompanyDomesticPathsQuery, CancellationToken cancellationToken)
    {
        //logger.LogInformation("GetAllCompanyDomesticPathsByCompanyType is Called");

        //var user = userContext.GetCurrentUser();
        //if (user == null)
        //    return ApiResponse<PagedResult<CompanyDomesticPathDto>>.Error(400, "مشکل در دریافت اطلاعات");

        //if (getAllCompanyDomesticPathsQuery.CompanyId != 0)
        //{
        //    var company = await companyRepository.GetCompanyByIdAsync(getAllCompanyDomesticPathsQuery.CompanyId, true, false, cancellationToken);
        //    if (company is null)
        //        return ApiResponse<PagedResult<CompanyDomesticPathDto>>.Error(400, $"شرکت نامعتبر است");

        //    if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
        //        return ApiResponse<PagedResult<CompanyDomesticPathDto>>.Error(400, "مجوز این فعالیت را ندارید");
        //}
        //else if (getAllCompanyDomesticPathsQuery.CompanyId == 0)
        //{
        //    if (!user.IsSuperAdmin())
        //        return ApiResponse<PagedResult<CompanyDomesticPathDto>>.Error(400, "مجوز این فعالیت را ندارید");
        //}

        //var (companyDomesticPaths, totalCount) = await companyDomesticPathsRepository.GetMatchingAllCompanyDomesticPathsAsync(
        //    getAllCompanyDomesticPathsQuery.SearchPhrase,
        //    getAllCompanyDomesticPathsQuery.SortBy,
        //    getAllCompanyDomesticPathsQuery.CompanyId,
        //    true,
        //    getAllCompanyDomesticPathsQuery.Active,
        //    getAllCompanyDomesticPathsQuery.SourceCityId,
        //    getAllCompanyDomesticPathsQuery.DestinationCityId,
        //    getAllCompanyDomesticPathsQuery.PageNumber,
        //    getAllCompanyDomesticPathsQuery.PageSize,
        //    getAllCompanyDomesticPathsQuery.SortDirection,
        //    cancellationToken);

        //var companyDomesticPathDto = mapper.Map<IReadOnlyList<CompanyDomesticPathDto>>(companyDomesticPaths) ?? Array.Empty<CompanyDomesticPathDto>();
        //logger.LogInformation("Retrieved {Count} package types", companyDomesticPathDto.Count);

        //var data = new PagedResult<CompanyDomesticPathDto>(companyDomesticPathDto, totalCount, getAllCompanyDomesticPathsQuery.PageSize, getAllCompanyDomesticPathsQuery.PageNumber);
        //return ApiResponse<PagedResult<CompanyDomesticPathDto>>.Ok(data, "مسیرها با موفقیت دریافت شدند");

        throw new NotImplementedException();
    }

    public async Task<ApiResponse<CompanyDomesticPathDto>> GetCompanyDomesticPathByCompanyIdAsync(GetCompanyDomesticPathByCompanyIdQuery getCompanyDomesticPathByCompanyIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyDomesticPathByCompanyId is Called with CompanyId: {Id}", getCompanyDomesticPathByCompanyIdQuery.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(getCompanyDomesticPathByCompanyIdQuery.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyDomesticPathDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyDomesticPathDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyDomesticPathDto>.Error(403, "مجوز این فعالیت را ندارید");

        var companyDomesticPath = await companyDomesticPathsRepository.GetCompanyDomesticPathsByCompanyIdAsync(getCompanyDomesticPathByCompanyIdQuery.CompanyId, false, true, cancellationToken);
        if (companyDomesticPath is null)
            return ApiResponse<CompanyDomesticPathDto>.Error(400, $"مسیر یافت نشد");

        var result = mapper.Map<CompanyDomesticPathDto>(companyDomesticPath);
        logger.LogInformation("CompanyDomesticPath retrieved successfully with CompanyId: {Id}", getCompanyDomesticPathByCompanyIdQuery.CompanyId);
        return ApiResponse<CompanyDomesticPathDto>.Ok(result, "مسیرها با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<CompanyDomesticPathDto>> GetCompanyDomesticPathByIdAsync(GetCompanyDomesticPathByIdQuery getCompanyDomesticPathByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyDomesticPathById is Called with ID: {Id}", getCompanyDomesticPathByIdQuery.Id);

        var companyDomesticPath = await companyDomesticPathsRepository.GetCompanyDomesticPathByIdAsync(getCompanyDomesticPathByIdQuery.Id, false, true, cancellationToken);
        if (companyDomesticPath is null)
            return ApiResponse<CompanyDomesticPathDto>.Error(400, $"مسیر یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticPath.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyDomesticPathDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyDomesticPathDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyDomesticPathDto>.Error(403, "مجوز این فعالیت را ندارید");

        var result = mapper.Map<CompanyDomesticPathDto>(companyDomesticPath);
        logger.LogInformation("CompanyDomesticPath retrieved successfully with ID: {Id}", getCompanyDomesticPathByIdQuery.Id);
        return ApiResponse<CompanyDomesticPathDto>.Ok(result, "مسیر با موفقیت دریافت شد");
    }
}


