using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveDown;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveUp;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetById;
using Capitan360.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Interfaces.Repositories.ContentTypes;
using Capitan360.Domain.Interfaces.Repositories.Companies;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Services;

public class CompanyContentTypeService(
    ILogger<CompanyContentTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyContentTypeRepository companyContentTypeRepository,
    IContentTypeRepository contentTypeRepository, IUserContext userContext, ICompanyRepository companyRepository) : ICompanyContentTypeService
{
    public async Task<ApiResponse<PagedResult<CompanyContentTypeDto>>> GetAllCompanyContentTypesByCompanyAsync(//ch**
        GetAllCompanyContentTypesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyContentTypesByCompany is Called");
        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<PagedResult<CompanyContentTypeDto>>.Error(400, $"شرکت نامعتبر است");



        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyContentTypeDto>>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<PagedResult<CompanyContentTypeDto>>.Error(403, "مجوز این فعالیت را ندارید");



        var (companyContentTypes, totalCount) = await companyContentTypeRepository.GetAllCompanyContentTypesAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyId,

            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);

        var companyContentTypeDto = mapper.Map<IReadOnlyList<CompanyContentTypeDto>>(companyContentTypes) ?? Array.Empty<CompanyContentTypeDto>();
        logger.LogInformation("Retrieved {Count} content types", companyContentTypeDto.Count);

        var data = new PagedResult<CompanyContentTypeDto>(companyContentTypeDto, totalCount, query.PageSize,
            query.PageNumber);
        return ApiResponse<PagedResult<CompanyContentTypeDto>>.Ok(data, "محتوی‌ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<int>> MoveCompanyContentTypeUpAsync(MoveUpCompanyContentTypeCommand command, CancellationToken cancellationToken)//ch**
    {
        var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (companyContentType == null)
            return ApiResponse<int>.Error(400, $"بسته بندی نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyContentType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        if (companyContentType.Order == 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        var count = await companyContentTypeRepository.GetCountCompanyContentTypeAsync(companyContentType.CompanyId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await companyContentTypeRepository.MoveCompanyContentTypeUpAsync(command.Id, cancellationToken);
        logger.LogInformation(
            "ContentType moved up successfully., ContentTypeId: {CompanyContentTypeId}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "محتوی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> MoveContentTypeDownAsync(MoveDownCompanyContentTypeCommand command, CancellationToken cancellationToken)//ch**
    {
        var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (companyContentType == null)
            return ApiResponse<int>.Error(400, $"بسته بندی نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyContentType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");



        if (companyContentType.Order == 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        var count = await companyContentTypeRepository.GetCountCompanyContentTypeAsync(companyContentType.CompanyId, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await companyContentTypeRepository.MoveCompanyContentTypeDownAsync(command.Id, cancellationToken);
        logger.LogInformation(
            "ContentType moved up successfully., ContentTypeId: {CompanyContentTypeId}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "محتوی با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<CompanyContentTypeDto>> GetCompanyContentTypeByIdAsync(GetCompanyContentTypeByIdQuery query,//ch**
        CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyContentTypeByIdQuery is Called with ID: {Id}", query.Id);

        var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(query.Id, false, true, cancellationToken);
        if (companyContentType is null)
            return ApiResponse<CompanyContentTypeDto>.Error(400, $"نوع محتوی با شناسه {query.Id} یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companyContentType.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyContentTypeDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyContentTypeDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyContentTypeDto>.Error(403, "مجوز این فعالیت را ندارید");


        var result = mapper.Map<CompanyContentTypeDto>(companyContentType);
        logger.LogInformation("ContentType retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanyContentTypeDto>.Ok(result, "محتوی با موفقیت دریافت شد");
    }

    //public async Task<ApiResponse<int>> UpdateCompanyContentTypeNameAsync(UpdateCompanyContentTypeNameCommand command,//ch**
    //    CancellationToken cancellationToken)
    //{
    //    logger.LogInformation("UpdateCompanyContentTypeNameAsync is Called with {@UpdateCompanyContentTypeNameCommand}", command);
    //    var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.Id, true, false, cancellationToken);
    //    if (companyContentType == null)
    //        return ApiResponse<int>.Error(400, $"بسته بندی نامعتبر است");
    //    var company = await companyRepository.GetCompanyByIdAsync(companyContentType.CompanyId, false, false, cancellationToken);
    //    if (company == null)
    //        return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");
    //    logger.LogInformation("ExistedCompanyContentType {@CompanyContentType}", companyContentType);
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
    //    if (await companyContentTypeRepository.CheckExistCompanyContentTypeNameAsync(command.CompanyContentTypeName, command.Id, companyContentType.CompanyId, cancellationToken))
    //        return ApiResponse<int>.Error(400, "نام محتوی بار تکراری است");
    //
    //    companyContentType.Name = companyContentType.Name;
    //
    //    await unitOfWork.SaveChangesAsync(cancellationToken);
    //
    //
    //    return ApiResponse<int>.Ok(command.Id, "اطلاعات با موفقیت به‌روزرسانی شد");
    //}

    public async Task<ApiResponse<int>> SetCompanyContentContentActivityStatusAsync(//ch**
        UpdateActiveStateCompanyContentTypeCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyContentActivityStatus Called with {@UpdateActiveStateCompanyContentTypeCommand}", command);

        var companyContentType =
            await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.Id, true, false, cancellationToken);

        if (companyContentType is null)
            return ApiResponse<int>.Error(400, $"بسته بندی نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyContentType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");


        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        companyContentType.Active = !companyContentType.Active;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("SetCompanyContentActivityStatus Updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت بسته بندی با موفقیت به‌روزرسانی شد");
    }



    public async Task<ApiResponse<CompanyContentTypeDto>> UpdateCompanyContentTypeAsync(UpdateCompanyContentTypeCommand command, CancellationToken cancellationToken)//ch**
    {
        logger.LogInformation("UpdateCompanyContentTypeAsync is Called with {@UpdateCompanyContentTypeCommand}", command);

        var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.Id, true, false, cancellationToken);
        if (companyContentType == null)
            return ApiResponse<CompanyContentTypeDto>.Error(400, $"محتوی بار نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyContentType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyContentTypeDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyContentTypeDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyContentTypeDto>.Error(403, "مجوز این فعالیت را ندارید");

        if (await companyContentTypeRepository.CheckExistCompanyContentTypeNameAsync(command.Name, command.Id, companyContentType.CompanyId, cancellationToken))
            return ApiResponse<CompanyContentTypeDto>.Error(400, "نام محتوی بار تکراری است");

        var updatedCompanyContentType = mapper.Map(command, companyContentType);
        if (updatedCompanyContentType is null)
            return ApiResponse<CompanyContentTypeDto>.Error(400, "خطا در عملیات تبدیل");
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("محتوی بار با موفقیت به‌روزرسانی شد: {Id}", command.Id);

        var updatedCompanyContentTypeDto = mapper.Map<CompanyContentTypeDto>(updatedCompanyContentType);
        return ApiResponse<CompanyContentTypeDto>.Ok(updatedCompanyContentTypeDto, "محتوی بار با موفقیت به‌روزرسانی شد");
    }
}