using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveUp;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetById;
using Microsoft.Extensions.Logging;
using Capitan360.Domain.Interfaces;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveDown;
using Capitan360.Domain.Interfaces.Repositories.PackageTypes;
using Capitan360.Domain.Interfaces.Repositories.Companies;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Services;



public class CompanyPackageTypeService(
    ILogger<CompanyPackageTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyPackageTypeRepository companyPackageTypeRepository,
    IPackageTypeRepository packageTypeRepository, ICompanyRepository companyRepository, IUserContext userContext) : ICompanyPackageTypeService
{
    public async Task<ApiResponse<PagedResult<CompanyPackageTypeDto>>> GetAllCompanyPackageTypesByCompanyAsync(//ch**
        GetAllCompanyPackageTypesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyPackageTypesByCompany is Called");


        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<PagedResult<CompanyPackageTypeDto>>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyPackageTypeDto>>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<PagedResult<CompanyPackageTypeDto>>.Error(403, "مجوز این فعالیت را ندارید");





        var (companyPackageTypes, totalCount) = await companyPackageTypeRepository.GetAllCompanyPackageTypesAsync(
          query.SearchPhrase,
          query.SortBy,
          query.CompanyId,
            true,
          query.PageNumber,
          query.PageSize,
          query.SortDirection,
            cancellationToken);

        var companyPackageTypeDto = mapper.Map<IReadOnlyList<CompanyPackageTypeDto>>(companyPackageTypes) ?? Array.Empty<CompanyPackageTypeDto>();
        logger.LogInformation("Retrieved {Count} package types", companyPackageTypeDto.Count);

        var data = new PagedResult<CompanyPackageTypeDto>(companyPackageTypeDto, totalCount, query.PageSize,
            query.PageNumber);
        return ApiResponse<PagedResult<CompanyPackageTypeDto>>.Ok(data, "بسته‌بندی‌ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<int>> MoveCompanyPackageTypeUpAsync(MoveUpCompanyPackageTypeCommand command, CancellationToken cancellationToken)//ch**
    {

        logger.LogInformation("MoveCompanyPackageTypeUp is Called with {@MoveCompanyPackageTypeUpCommand}", command);

        var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (companyPackageType == null)
            return ApiResponse<int>.Error(400, $"بسته‌بندی نامعتبر است");


        var company = await companyRepository.GetCompanyByIdAsync(companyPackageType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");




        if (companyPackageType.Order == 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        var count = await companyPackageTypeRepository.GetCountCompanyPackageTypeAsync(companyPackageType.CompanyId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await companyPackageTypeRepository.MoveCompanyPackageTypeUpAsync(command.Id, cancellationToken);
        logger.LogInformation(
            "PackageType moved up successfully., CompanyPackageTypeId: {CompanyPackageTypeId}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "بسته‌بندی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> MoveCompanyPackageTypeDownAsync(MoveDownCompanyPackageTypeCommand command, CancellationToken cancellationToken)//ch**
    {
        logger.LogInformation("MoveCompanyPackageTypeUp is Called with {@MoveCompanyPackageTypeUpCommand}", command);

        var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (companyPackageType == null)
            return ApiResponse<int>.Error(400, $"بسته‌بندی نامعتبر است");


        var company = await companyRepository.GetCompanyByIdAsync(companyPackageType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");




        if (companyPackageType.Order == 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        var count = await companyPackageTypeRepository.GetCountCompanyPackageTypeAsync(companyPackageType.CompanyId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await companyPackageTypeRepository.MoveCompanyPackageTypeDownAsync(command.Id, cancellationToken);
        logger.LogInformation(
            "PackageType moved down successfully., CompanyPackageTypeId: {CompanyPackageTypeId}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "بسته‌بندی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<CompanyPackageTypeDto>> GetCompanyPackageTypeByIdAsync(GetCompanyPackageTypeByIdQuery query,//ch**
        CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyPackageTypeByIdQuery is Called with ID: {Id}", query.Id);

        var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(query.Id, false, false, cancellationToken);
        if (companyPackageType is null)
            return ApiResponse<CompanyPackageTypeDto>.Error(400, $"نوع بسته‌بندی با شناسه {query.Id} یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companyPackageType.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyPackageTypeDto>.Error(400, $"شرکت نامعتبر است");


        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyPackageTypeDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyPackageTypeDto>.Error(403, "مجوز این فعالیت را ندارید");




        var result = mapper.Map<CompanyPackageTypeDto>(companyPackageType);
        logger.LogInformation("PackageType retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanyPackageTypeDto>.Ok(result, "بسته‌بندی با موفقیت دریافت شد");
    }

    //public async Task<ApiResponse<int>> UpdateCompanyPackageTypeNameAsync(UpdateCompanyPackageTypeNameAndDescriptionCommand command,//ch**
    //    CancellationToken cancellationToken)
    //{
    //    logger.LogInformation("UpdateCompanyPackageTypeNameAsync is Called with {@UpdateCompanyPackageTypeNameAndDescriptionCommand}", command);
    //    var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.Id, true, false, cancellationToken);
    //    if (companyPackageType == null)
    //        return ApiResponse<int>.Error(400, $"بسته‌بندی نامعتبر است");
    //
    //    var company = await companyRepository.GetCompanyByIdAsync(companyPackageType.CompanyId, false, false, cancellationToken);
    //    if (company == null)
    //        logger.LogInformation("ExistedCompanyPackageType {@CompanyPackageType}", companyPackageType);
    //
    //
    //    var user = userContext.GetCurrentUser();
    //    if (user == null)
    //        return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");
    //
    //
    //    if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
    //        return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");
    //
    //
    //    if (await companyPackageTypeRepository.CheckExistCompanyPackageTypeNameAsync(command.CompanyPackageTypeName, command.Id, companyPackageType.CompanyId, cancellationToken))
    //        return ApiResponse<int>.Error(400, "نام بسته بندی تکراری است");
    //
    //
    //    companyPackageType.Name = companyPackageType.Name;
    //
    //    await unitOfWork.SaveChangesAsync(cancellationToken);
    //
    //
    //    return ApiResponse<int>.Ok(command.Id, "اطلاعات با موفقیت به‌روزرسانی شد");
    //}

    public async Task<ApiResponse<int>> SetCompanyPackageContentActivityStatusAsync(//ch**
        UpdateActiveStateCompanyPackageTypeCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyPackageContentActivityStatus Called with {@UpdateActiveStateCompanyPackageTypeCommand}", command);

        var companyPackageType =
            await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.Id, true, false, cancellationToken);

        if (companyPackageType is null)
            return ApiResponse<int>.Error(400, $"بسته‌بندی نامعتبر است");


        var company = await companyRepository.GetCompanyByIdAsync(companyPackageType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");




        companyPackageType.Active = !companyPackageType.Active;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("SetCompanyPackageContentActivityStatus Updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت بسته‌بندی با موفقیت به‌روزرسانی شد");
    }


    public async Task<ApiResponse<CompanyPackageTypeDto>> UpdateCompanyPackageTypeAsync(UpdateCompanyPackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyPackageTypeAsync is Called with {@UpdateCompanyPackageTypeCommand}", command);
        var companyPackageType = await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.Id, true, false, cancellationToken);
        if (companyPackageType == null)
            return ApiResponse<CompanyPackageTypeDto>.Error(400, $"بسته بندی نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyPackageType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyPackageTypeDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyPackageTypeDto>.Error(400, "مشکل در دریافت اطلاعات");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyPackageTypeDto>.Error(400, "مجوز این فعالیت را ندارید");

        if (await companyPackageTypeRepository.CheckExistCompanyPackageTypeNameAsync(command.Name, command.Id, companyPackageType.CompanyId, cancellationToken))
            return ApiResponse<CompanyPackageTypeDto>.Error(400, "نام بسته بندی تکراری است");

        var updatedCompanyPackageType = mapper.Map(command, companyPackageType);
        if (updatedCompanyPackageType is null)
            return ApiResponse<CompanyPackageTypeDto>.Error(400, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("بسته بندی با موفقیت به‌روزرسانی شد: {Id}", command.Id);

        var updatedCompanyPackageTypeDto = mapper.Map<CompanyPackageTypeDto>(updatedCompanyPackageType);
        return ApiResponse<CompanyPackageTypeDto>.Ok(updatedCompanyPackageTypeDto, "بسته بندی با موفقیت به‌روزرسانی شد");
    }
}