using AutoMapper;
using Capitan360.Application.Common;
using Microsoft.Extensions.Logging;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.UpdateCaptain360UriState;
using Capitan360.Application.Features.Companies.CompanyUris.Dtos;
using Capitan360.Application.Features.Companies.CompanyUris.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyUris.Queries.GetByCompanyId;
using Capitan360.Application.Features.Companies.CompanyUris.Queries.GetById;
using Capitan360.Domain.Interfaces;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Entities.Companies;
using Microsoft.AspNetCore.Http;

namespace Capitan360.Application.Features.Companies.CompanyUris.Services;

public class CompanyUriService(
    ILogger<CompanyUriService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyUriRepository companyUriRepository,
    ICompanyRepository companyRepository
) : ICompanyUriService
{
    public async Task<ApiResponse<int>> CreateCompanyUriAsync(CreateCompanyUriCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyUri is Called with {@CreateCompanyUriCommand}", command);

        var company = await companyRepository.GetCompanyByIdAsync(command.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await companyUriRepository.CheckExistCompanyUriUriAsync(command.Uri, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "URI شرکت تکراری است");

        var companyUri = mapper.Map<CompanyUri>(command) ?? null;
        if (companyUri == null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        var companyUriId = await companyUriRepository.CreateCompanyUriAsync(companyUri, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyUri created successfully with {@CompanyUri}", companyUri);
        return ApiResponse<int>.Created(companyUriId, "URI شرکت با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyUriAsync(DeleteCompanyUriCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyUri is Called with {@Id}", command.Id);

        var companyUri = await companyUriRepository.GetCompanyUriByIdAsync(command.Id, false, false, cancellationToken);
        if (companyUri is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "URI شرکت نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyUri.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        await companyUriRepository.DeleteCompanyUriAsync(companyUri.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyUri Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, ",ريال] شرکت با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> SetCompanyUriActivityStatusAsync(UpdateActiveStateCompanyUriCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyUriActivityStatus Called with {@Id}", command.Id);

        var companyUri = await companyUriRepository.GetCompanyUriByIdAsync(command.Id, false, true, cancellationToken);
        if (companyUri is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "URI شرکت نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyUri.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        companyUri.Active = !companyUri.Active;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyUri activity status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت URI با موفقیت به روز رسانی شد");
    }

    public async Task<ApiResponse<int>> SetCompanyUriCaptain360UriStatusAsync(UpdateCaptain360UriStateCompanyUriCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyUriCaptain360Status Called with {@Id}", command.Id);

        var companyUri = await companyUriRepository.GetCompanyUriByIdAsync(command.Id, false, true, cancellationToken);
        if (companyUri is null)
            return ApiResponse<int>.Error(404, "URI شرکت نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyUri.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(404, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        companyUri.Captain360Uri = !companyUri.Captain360Uri;

        if (companyUri.Captain360Uri)
        {
            var listCompanyUri = await companyUriRepository.GetCompanyUriByCompanyIdAsync(companyUri.CompanyId, cancellationToken);
            if (listCompanyUri is null)
                return ApiResponse<int>.Error(404, "URI شرکت نامعتبر است");

            foreach (var item in listCompanyUri)
            {
                if (item.Id != companyUri.Id)
                {
                    item.Captain360Uri = false;
                }
            }
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("CompanyUri activity Captain360 updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت URI اصلی با موفقیت به روز رسانی شد");
    }

    public async Task<ApiResponse<CompanyUriDto>> UpdateCompanyUriAsync(UpdateCompanyUriCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyUri is Called with {@UpdateCompanyUriCommand}", command);

        var companyUri = await companyUriRepository.GetCompanyUriByIdAsync(command.Id, false, true, cancellationToken);
        if (companyUri is null)
            return ApiResponse<CompanyUriDto>.Error(StatusCodes.Status404NotFound, "URI شرکت نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyUri.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyUriDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyUriDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId))
            return ApiResponse<CompanyUriDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await companyUriRepository.CheckExistCompanyUriUriAsync(command.Uri, command.Id, cancellationToken))
            return ApiResponse<CompanyUriDto>.Error(StatusCodes.Status409Conflict, "نام URI شرکت تکراری است");

        var updatedCompanyUri = mapper.Map(command, companyUri);
        if (updatedCompanyUri == null)
            return ApiResponse<CompanyUriDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyUri updated successfully with {@UpdateCompanyUriCommand}", command);

        var updatedCompanyUriDto = mapper.Map<CompanyUriDto>(updatedCompanyUri);
        if (updatedCompanyUriDto == null)
            return ApiResponse<CompanyUriDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<CompanyUriDto>.Ok(updatedCompanyUriDto, "URI شرکت با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyUriDto>>> GetAllCompanyUrisAsync(GetAllCompanyUrisQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyUris is Called");
        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyUriDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (query.CompanyId != 0)
        {
            var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, true, false, cancellationToken);
            if (company is null)
                return ApiResponse<PagedResult<CompanyUriDto>>.Error(400, "شرکت نامعتبر است");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<PagedResult<CompanyUriDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }
        else if (query.CompanyId == 0)
        {
            if (!user.IsSuperAdmin())
                return ApiResponse<PagedResult<CompanyUriDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        }

        var (companyUris, totalCount) = await companyUriRepository.GetAllCompanyUrisAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyId,
            query.Active,
            query.Captain360Uri,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);
        
        var companyUriDtos = mapper.Map<IReadOnlyList<CompanyUriDto>>(companyUris) ?? Array.Empty<CompanyUriDto>();
        if (companyUriDtos == null)
            return ApiResponse<PagedResult<CompanyUriDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company URIs", companyUriDtos.Count);

        var data = new PagedResult<CompanyUriDto>(companyUriDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyUriDto>>.Ok(data, "URI ها  با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<IReadOnlyList<CompanyUriDto>>> GetCompanyUriByCompanyIdAsync(GetCompanyUriByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyUriByCompanyId is Called with {@Id}", query.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<IReadOnlyList<CompanyUriDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<IReadOnlyList<CompanyUriDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<IReadOnlyList<CompanyUriDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyUris = await companyUriRepository.GetCompanyUriByCompanyIdAsync(query.CompanyId, cancellationToken);
        if (companyUris is null)
            return ApiResponse<IReadOnlyList<CompanyUriDto>>.Error(StatusCodes.Status404NotFound, "URI شرکت  یافت نشد");

        var companyUriDtos = mapper.Map<IReadOnlyList<CompanyUriDto>>(companyUris) ?? Array.Empty<CompanyUriDto>();
        if (companyUriDtos == null)
            return ApiResponse<IReadOnlyList<CompanyUriDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company URIs GetByCompanyId", companyUriDtos.Count);

        return ApiResponse<IReadOnlyList<CompanyUriDto>>.Ok(companyUriDtos, "URI ها  با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<CompanyUriDto>> GetCompanyUriByIdAsync(GetCompanyUriByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyUriById is Called with {@Id}", query.Id);

        var companyUri = await companyUriRepository.GetCompanyUriByIdAsync(query.Id, false, false, cancellationToken);
        if (companyUri is null)
            return ApiResponse<CompanyUriDto>.Error(404, "URI شرکت  یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companyUri.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyUriDto>.Error(404, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyUriDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyUriDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyUriDto = mapper.Map<CompanyUriDto>(companyUri);
        if (companyUriDto == null)
            return ApiResponse<CompanyUriDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("CompanyUri retrieved successfully with {@Id}", query.Id);
        return ApiResponse<CompanyUriDto>.Ok(companyUriDto, "URI شرکت با موفقیت دریافت شد");
    }
}