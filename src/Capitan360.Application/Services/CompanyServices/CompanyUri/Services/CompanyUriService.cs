using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.CreateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.DeleteCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.UpdateActiveStateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.UpdateCaptain360UriStateCompany;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.UpdateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Queries.GetAllCompanyUris;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Queries.GetCompanyUriByCompanyId;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Queries.GetCompanyUriById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.CompanyRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanyUri.Services;



public class CompanyUriService(
    ILogger<CompanyUriService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyUriRepository companyUriRepository, ICompanyRepository companyRepository
) : ICompanyUriService
{
    public async Task<ApiResponse<int>> CreateCompanyUriAsync(CreateCompanyUriCommand createCompanyUriCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyUri is Called with {@CreateCompanyUriCommand}", createCompanyUriCommand);

        var company = await companyRepository.GetCompanyByIdAsync(createCompanyUriCommand.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        if (await companyUriRepository.CheckExistCompanyUriUriAsync(createCompanyUriCommand.Uri, null, cancellationToken))
            return ApiResponse<int>.Error(400, "URI شرکت تکراری است");

        var companyUri = mapper.Map<Capitan360.Domain.Entities.Companies.CompanyUri>(createCompanyUriCommand) ?? null;
        if (companyUri == null)
            return ApiResponse<int>.Error(400, "مشکل در عملیات تبدیل");

        var companyUriId = await companyUriRepository.CreateCompanyUriAsync(companyUri, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyUri created successfully with ID: {CompanyUriId}", companyUriId);
        return ApiResponse<int>.Ok(companyUriId, "URI شرکت با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyUriAsync(DeleteCompanyUriCommand deleteCompanyUriCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyUri is Called with ID: {Id}", deleteCompanyUriCommand.Id);

        var companyUri = await companyUriRepository.GetCompanyUriByIdAsync(deleteCompanyUriCommand.Id, true, false, cancellationToken);
        if (companyUri is null)
            return ApiResponse<int>.Error(400, $"URI شرکت نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyUri.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        await companyUriRepository.DeleteCompanyUriAsync(companyUri);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyUri soft-deleted successfully with ID: {Id}", deleteCompanyUriCommand.Id);
        return ApiResponse<int>.Ok(deleteCompanyUriCommand.Id, ",ريال] شرکت با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> SetCompanyUriActivityStatusAsync(UpdateActiveStateCompanyUriCommand updateActiveStateCompanyUriCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyUriActivityStatus Called with {@UpdateActiveStateCompanyUriCommand}", updateActiveStateCompanyUriCommand);

        var companyUri = await companyUriRepository.GetCompanyUriByIdAsync(updateActiveStateCompanyUriCommand.Id, true, false, cancellationToken);
        if (companyUri is null)
            return ApiResponse<int>.Error(400, $"URI شرکت نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyUri.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        companyUri.Active = !companyUri.Active;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("وضعیت URI شرکت با موفقیت به‌روزرسانی شد: {Id}", updateActiveStateCompanyUriCommand.Id);
        return ApiResponse<int>.Ok(updateActiveStateCompanyUriCommand.Id);
    }

    public async Task<ApiResponse<int>> SetCompanyUriCaptain360UriStatusAsync(UpdateCaptain360UriStateCompanyUriCommand updateCaptain360UriStateCompanyUriCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCaptain360UriStateCompanyUri Called with {@UpdateCaptain360UriStateCompanyUriCommand}", updateCaptain360UriStateCompanyUriCommand);

        var companyUri = await companyUriRepository.GetCompanyUriByIdAsync(updateCaptain360UriStateCompanyUriCommand.Id, true, false, cancellationToken);
        if (companyUri is null)
            return ApiResponse<int>.Error(400, $"URI شرکت نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyUri.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(403, "مجوز این فعالیت را ندارید");

        companyUri.Captain360Uri = !companyUri.Captain360Uri;

        if (companyUri.Captain360Uri)
        {
            var listCompanyUri = await companyUriRepository.GetCompanyUriByCompanyIdAsync(companyUri.CompanyId, true, false, cancellationToken);
            if (listCompanyUri is null)
                return ApiResponse<int>.Error(400, $"URI شرکت نامعتبر است");

            foreach (var item in listCompanyUri)
            {
                if (item.Id != companyUri.Id)
                {
                    item.Captain360Uri = false;
                }
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("وضعیت کاپیتان 360 URI شرکت با موفقیت به‌روزرسانی شد: {Id}", updateCaptain360UriStateCompanyUriCommand.Id);
        return ApiResponse<int>.Ok(updateCaptain360UriStateCompanyUriCommand.Id);
    }

    public async Task<ApiResponse<CompanyUriDto>> UpdateCompanyUriAsync(UpdateCompanyUriCommand updateCompanyUriCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyUri is Called with {@UpdateCompanyUriCommand}", updateCompanyUriCommand);

        var companyUri = await companyUriRepository.GetCompanyUriByIdAsync(updateCompanyUriCommand.Id, true, false, cancellationToken);
        if (companyUri is null)
            return ApiResponse<CompanyUriDto>.Error(400, $"URI شرکت نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyUri.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyUriDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyUriDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyUriDto>.Error(403, "مجوز این فعالیت را ندارید");

        if (await companyUriRepository.CheckExistCompanyUriUriAsync(updateCompanyUriCommand.Uri, updateCompanyUriCommand.Id, cancellationToken))
            return ApiResponse<CompanyUriDto>.Error(400, "نام URI شرکت تکراری است");

        var updatedCompanyUri = mapper.Map(updateCompanyUriCommand, companyUri);

        if (updatedCompanyUri == null)
            return ApiResponse<CompanyUriDto>.Error(400, "مشکل در عملیات تبدیل");
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("URI شرکت با موفقیت به‌روزرسانی شد: {Id}", updateCompanyUriCommand.Id);

        var updatedCompanyUriDto = mapper.Map<CompanyUriDto>(updatedCompanyUri);
        return ApiResponse<CompanyUriDto>.Ok(updatedCompanyUriDto, "URI شرکت با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyUriDto>>> GetAllCompanyUrisAsync(GetAllCompanyUrisQuery getAllCompanyUrisQuery, CancellationToken cancellationToken)
    {
        //logger.LogInformation("GetAllCompanyUrisByCompanyType is Called");
        //var user = userContext.GetCurrentUser();
        //if (user == null)
        //    return ApiResponse<PagedResult<CompanyUriDto>>.Error(400, "مشکل در دریافت اطلاعات");

        //if (getAllCompanyUrisQuery.CompanyId != 0)
        //{
        //    var company = await companyRepository.GetCompanyByIdAsync(getAllCompanyUrisQuery.CompanyId, true, false, cancellationToken);
        //    if (company is null)
        //        return ApiResponse<PagedResult<CompanyUriDto>>.Error(400, $"شرکت نامعتبر است");

        //    if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
        //        return ApiResponse<PagedResult<CompanyUriDto>>.Error(400, "مجوز این فعالیت را ندارید");
        //}
        //else if (getAllCompanyUrisQuery.CompanyId == 0)
        //{
        //    if (!user.IsSuperAdmin())
        //        return ApiResponse<PagedResult<CompanyUriDto>>.Error(400, "مجوز این فعالیت را ندارید");
        //}

        //var (companyUris, totalCount) = await companyUriRepository.GetMatchingAllCompanyUrisAsync(
        //    getAllCompanyUrisQuery.SearchPhrase,
        //    getAllCompanyUrisQuery.SortBy,
        //    getAllCompanyUrisQuery.CompanyId,
        //    getAllCompanyUrisQuery.Active,
        //    getAllCompanyUrisQuery.Captain360Uri,
        //    true,
        //    getAllCompanyUrisQuery.PageNumber,
        //    getAllCompanyUrisQuery.PageSize,
        //    getAllCompanyUrisQuery.SortDirection,
        //    cancellationToken);

        //var companyUriDto = mapper.Map<IReadOnlyList<CompanyUriDto>>(companyUris) ?? Array.Empty<CompanyUriDto>();
        //logger.LogInformation("Retrieved {Count} package types", companyUriDto.Count);

        //var data = new PagedResult<CompanyUriDto>(companyUriDto, totalCount, getAllCompanyUrisQuery.PageSize, getAllCompanyUrisQuery.PageNumber);
        //return ApiResponse<PagedResult<CompanyUriDto>>.Ok(data, "URI های شرکت  با موفقیت دریافت شدند");

        throw new NotImplementedException();
    }

    public async Task<ApiResponse<CompanyUriDto>> GetCompanyUriByCompanyIdAsync(GetCompanyUriByCompanyIdQuery getCompanyUriByCompanyIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyUriByCompanyId is Called with ID: {Id}", getCompanyUriByCompanyIdQuery.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(getCompanyUriByCompanyIdQuery.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyUriDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyUriDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyUriDto>.Error(403, "مجوز این فعالیت را ندارید");

        var companyUri = await companyUriRepository.GetCompanyUriByCompanyIdAsync(getCompanyUriByCompanyIdQuery.CompanyId, false, true, cancellationToken);
        if (companyUri is null)
            return ApiResponse<CompanyUriDto>.Error(400, $"URI شرکت  یافت نشد");

        var result = mapper.Map<CompanyUriDto>(companyUri);
        logger.LogInformation("CompanyUri retrieved successfully with ID: {Id}", getCompanyUriByCompanyIdQuery.CompanyId);
        return ApiResponse<CompanyUriDto>.Ok(result, "URI شرکت با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<CompanyUriDto>> GetCompanyUriByIdAsync(GetCompanyUriByIdQuery getCompanyUriByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyUriById is Called with ID: {Id}", getCompanyUriByIdQuery.Id);

        var companyUri = await companyUriRepository.GetCompanyUriByIdAsync(getCompanyUriByIdQuery.Id, false, true, cancellationToken);
        if (companyUri is null)
            return ApiResponse<CompanyUriDto>.Error(400, $"URI شرکت  یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companyUri.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyUriDto>.Error(400, $"شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyUriDto>.Error(401, "کاربر اهراز هویت نشده است");


        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyUriDto>.Error(403, "مجوز این فعالیت را ندارید");

        var result = mapper.Map<CompanyUriDto>(companyUri);
        logger.LogInformation("CompanyUri retrieved successfully with ID: {Id}", getCompanyUriByIdQuery.Id);
        return ApiResponse<CompanyUriDto>.Ok(result, "URI شرکت با موفقیت دریافت شد");
    }
}