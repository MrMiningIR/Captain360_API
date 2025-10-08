using AutoMapper;
using Capitan360.Application.Common;
using Microsoft.Extensions.Logging;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveDown;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveUp;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateName;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetById;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Application.Features.Identities.Identities.Services;
using Microsoft.AspNetCore.Http;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetByCompanyId;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Services;

public class CompanyContentTypeService(
    ILogger<CompanyContentTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyContentTypeRepository companyContentTypeRepository,
    ICompanyRepository companyRepository,
    IUserContext userContext) : ICompanyContentTypeService
{
    public async Task<ApiResponse<int>> MoveUpCompanyContentTypeAsync(MoveUpCompanyContentTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("MoveUpCompanyContentType is Called with {@Id}", command.Id);

        var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (companyContentType == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyContentType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (companyContentType.Order == 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        var count = await companyContentTypeRepository.GetCountCompanyContentTypeAsync(companyContentType.CompanyId, cancellationToken);
        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await companyContentTypeRepository.MoveCompanyContentTypeUpAsync(command.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
      
        logger.LogInformation("CompanyContentType moved up successfully with {@CompanyContentTypeId}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "محتوی بار با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> MoveDownCompanyContentTypeAsync(MoveDownCompanyContentTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("MoveDownCompanyContentType is Called with {@Id}", command.Id);

        var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (companyContentType == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyContentType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var count = await companyContentTypeRepository.GetCountCompanyContentTypeAsync(companyContentType.CompanyId, cancellationToken);

        if (companyContentType.Order == count)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await companyContentTypeRepository.MoveCompanyContentTypeDownAsync(command.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("CompanyContentType moved down successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "محتوی بار با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> SetCompanyContentTypeActivityStatusAsync(UpdateActiveStateCompanyContentTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyContentTypeActivityStatus Called with {@Id}", command.Id);

        var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.Id, false, true, cancellationToken);
        if (companyContentType is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyContentType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        companyContentType.Active = !companyContentType.Active;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyContentType activity status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت محتوی بار با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<int>> UpdateCompanyContentTypeNameAsync(UpdateCompanyContentTypeNameCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyContentTypeName is Called with {@UpdateCompanyContentTypeNameCommand}", command);

        var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.Id, false, true, cancellationToken);
        if (companyContentType is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyContentType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
        if (await companyContentTypeRepository.CheckExistCompanyContentTypeNameAsync(command.Name, command.Id, companyContentType.CompanyId, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "نام محتوی بار تکراری است");

        companyContentType.Name = companyContentType.Name;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyContentTypeName updated successfully with {@UpdateCompanyContentTypeNameCommand}", command);

        return ApiResponse<int>.Ok(command.Id, "نام محتوی بار با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<CompanyContentTypeDto>> UpdateCompanyContentTypeAsync(UpdateCompanyContentTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyContentType is Called with {@UpdateCompanyContentTypeCommand}", command);

        var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.Id, false, true, cancellationToken);
        if (companyContentType == null)
            return ApiResponse<CompanyContentTypeDto>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyContentType.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyContentTypeDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyContentTypeDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyContentTypeDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await companyContentTypeRepository.CheckExistCompanyContentTypeNameAsync(command.Name, command.Id, companyContentType.CompanyId, cancellationToken))
            return ApiResponse<CompanyContentTypeDto>.Error(StatusCodes.Status409Conflict, "نام محتوی بار تکراری است");

        var updatedComapnyContentType = mapper.Map(command, companyContentType);
        if (updatedComapnyContentType is null)
            return ApiResponse<CompanyContentTypeDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyContentType updated successfully with {@UpdateCompanyContentTypeCommand}", command);

        var updatedComapnyContentTypeDto = mapper.Map<CompanyContentTypeDto>(updatedComapnyContentType);
        if (updatedComapnyContentTypeDto == null)
            return ApiResponse<CompanyContentTypeDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<CompanyContentTypeDto>.Ok(updatedComapnyContentTypeDto, "محتوی بار با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyContentTypeDto>>> GetAllCompanyContentTypesAsync(GetAllCompanyContentTypesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyContentTypes is Called");

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<PagedResult<CompanyContentTypeDto>>.Error(404, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<CompanyContentTypeDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<PagedResult<CompanyContentTypeDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var (companyContentTypes, totalCount) = await companyContentTypeRepository.GetAllCompanyContentTypesAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyId,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);

        var companyContentTypeDtos = mapper.Map<IReadOnlyList<CompanyContentTypeDto>>(companyContentTypes) ?? Array.Empty<CompanyContentTypeDto>();
        if (companyContentTypeDtos == null)
            return ApiResponse<PagedResult<CompanyContentTypeDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");
        
        logger.LogInformation("Retrieved {Count} companyContent types", companyContentTypeDtos.Count);

        var data = new PagedResult<CompanyContentTypeDto>(companyContentTypeDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyContentTypeDto>>.Ok(data, "محتوهای بار با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<IReadOnlyList<CompanyContentTypeDto>>> GetCompanyContentTypeByCompanyIdAsync(GetCompanyContentTypeByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyContentTypeByCompanyId is Called with {@Id}", query.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<IReadOnlyList<CompanyContentTypeDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<IReadOnlyList<CompanyContentTypeDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");
        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<IReadOnlyList<CompanyContentTypeDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyContentTypes = await companyContentTypeRepository.GetCompanyContentTypeByCompanyIdAsync(query.CompanyId, cancellationToken);
        if (companyContentTypes is null)
            return ApiResponse<IReadOnlyList<CompanyContentTypeDto>>.Error(StatusCodes.Status404NotFound, "محتوی بار یافت نشد");

        var companyContentTypeDtos = mapper.Map<IReadOnlyList<CompanyContentTypeDto>>(companyContentTypes) ?? Array.Empty<CompanyContentTypeDto>();
        if (companyContentTypeDtos == null)
            return ApiResponse<IReadOnlyList<CompanyContentTypeDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company content types GetByCompanyId", companyContentTypeDtos.Count);

        return ApiResponse<IReadOnlyList<CompanyContentTypeDto>>.Ok(companyContentTypeDtos, "محتوهای بار با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<CompanyContentTypeDto>> GetCompanyContentTypeByIdAsync(GetCompanyContentTypeByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyContentTypeById is Called with {@Id}", query.Id);

        var companyContentType = await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(query.Id, false, false, cancellationToken);
        if (companyContentType is null)
            return ApiResponse<CompanyContentTypeDto>.Error(StatusCodes.Status404NotFound, "محتوی بار یافت نشد");

        var company = await companyRepository.GetCompanyByIdAsync(companyContentType.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<CompanyContentTypeDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyContentTypeDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyContentTypeDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyContentTypeDto = mapper.Map<CompanyContentTypeDto>(companyContentType);
        if (companyContentTypeDto == null)
            return ApiResponse<CompanyContentTypeDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("CompanyContentType retrieved successfully with {@Id}", query.Id);
        return ApiResponse<CompanyContentTypeDto>.Ok(companyContentTypeDto, "محتوی بار با موفقیت دریافت شد");
    }
}
