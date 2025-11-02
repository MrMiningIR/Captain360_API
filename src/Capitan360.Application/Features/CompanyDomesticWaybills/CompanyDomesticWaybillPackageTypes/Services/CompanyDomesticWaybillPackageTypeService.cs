using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticWaybills;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.Delete;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.DeleteByCompanyDomesticWaybillId;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.Issue;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.IssueFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Dtos;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Queries.GetByCompanyDomesticWaybillId;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Queries.GetById;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Services;

public class CompanyDomesticWaybillPackageTypeService(
    ILogger<CompanyDomesticWaybillPackageTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyDomesticWaybillPackageTypeRepository companyDomesticWaybillPackageTypeRepository,
    ICompanyDomesticWaybillRepository companyDomesticWaybillRepository,
    ICompanyPackageTypeRepository companyPackageTypeRepository,
    ICompanyContentTypeRepository companyContentTypeRepository,
    IUserContext userContext,
    ICompanyRepository companyRepository)
    : ICompanyDomesticWaybillPackageTypeService
{
    public async Task<ApiResponse<int>> IssueCompanyDomesticWaybillPackageTypeAsync(IssueCompanyDomesticWaybillPackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("IssueCompanyDomesticWaybillPackageType is Called with {@IssueCompanyDomesticWaybillPackageTypeCommand}", command);

        var companyDomesticWaybill = await companyDomesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(command.CompanyDomesticWaybillId, false, false, false, false, cancellationToken);
        if (companyDomesticWaybill is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بارنامه نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticWaybill.CompanySenderId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await companyContentTypeRepository.GetCompanyContentTypeByIdAsync(command.CompanyContentTypeId, false, false, cancellationToken) == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        if (await companyPackageTypeRepository.GetCompanyPackageTypeByIdAsync(command.CompanyPackageTypeId, false, false, cancellationToken) == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بسته بندی نامعتبر است");

        var companyDomesticWaybillPackageType = mapper.Map<CompanyDomesticWaybillPackageType>(command) ?? null;
        if (companyDomesticWaybillPackageType == null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        var companyDomesticWaybillPackageTypeId = await companyDomesticWaybillPackageTypeRepository.IssueCompanyDomesticWaybillPackageTypeAsync(companyDomesticWaybillPackageType, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticWaybillPackageType created successfully with {@CompanyDomesticWaybillPackageType}", companyDomesticWaybillPackageType);
        return ApiResponse<int>.Created(companyDomesticWaybillPackageTypeId, "بسته مرسواه با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> IssueCompanyDomesticWaybillPackageTypeFromDesktopAsync(IssueCompanyDomesticWaybillPackageTypeFromDesktopCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("IssueCompanyDomesticWaybillPackageTypeFromDesktop is Called with {@IssueCompanyDomesticWaybillPackageTypeFromDesktopCommand}", command);

        var companyDomesticWaybill = await companyDomesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(command.CompanyDomesticWaybillId, false, false, false, false, cancellationToken);
        if (companyDomesticWaybill is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بارنامه نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticWaybill.CompanySenderId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyContentTypes = await companyContentTypeRepository.GetCompanyContentTypeByCompanyIdAsync(companyDomesticWaybill.CompanySenderId, cancellationToken);
        var companyPackageTypes = await companyPackageTypeRepository.GetCompanyPackageTypeByCompanyIdAsync(companyDomesticWaybill.CompanySenderId, cancellationToken);

        //IssueCompanyDomesticWaybillPackageTypeCommand issueCompanyDomesticWaybillPackageTypeCommand = new IssueCompanyDomesticWaybillPackageTypeCommand
        //{
        //    CompanyContentTypeId = companyContentTypes.Count(item => item.Name.ToLower() == command.UserInsertedContentName.Trim().ToLower()) > 0 ?
        //                           companyContentTypes.First(item => item.Name.ToLower() == command.UserInsertedContentName.Trim().ToLower()).Id :
        //                           companyContentTypes.First(item => item.Order == 1).Id,
        //    CompanyPackageTypeId = companyPackageTypes.Count(item => item.Name.ToLower() == command.UserInsertedPackageName.Trim().ToLower()) > 0 ?
        //                           companyPackageTypes.First(item => item.Name.ToLower() == command.UserInsertedPackageName.Trim().ToLower()).Id :
        //                           companyPackageTypes.First(item => item.Order == 1).Id,
        //    CompanyDomesticWaybillId = command.CompanyDomesticWaybillId,
        //    CountDimension = command.CountDimension,
        //    DeclaredValue = command.DeclaredValue,
        //    DimensionalWeight = command.DimensionalWeight,
        //    Dimensions = command.Dimensions,
        //    GrossWeight = command.GrossWeight,
        //    UserInsertedContentName = command.UserInsertedContentName,
        //};

        var companyDomesticWaybillPackageTypeFromDesktop = mapper.Map<CompanyDomesticWaybillPackageType>(command) ?? null;
        if (companyDomesticWaybillPackageTypeFromDesktop == null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        var companyDomesticWaybillPackageTypeFromDesktopId = await companyDomesticWaybillPackageTypeRepository.IssueCompanyDomesticWaybillPackageTypeAsync(companyDomesticWaybillPackageTypeFromDesktop, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticWaybillPackageTypeFromDesktop created successfully with {@CompanyDomesticWaybillPackageTypeFromDesktop}", companyDomesticWaybillPackageTypeFromDesktop);
        return ApiResponse<int>.Created(companyDomesticWaybillPackageTypeFromDesktopId, "بسته مرسواه با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyDomesticWaybillPackageTypeAsync(DeleteCompanyDomesticWaybillPackageTypeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyDomesticWaybillPackageType is Called with {@Id}", command.Id);

        var companyDomesticWaybillPackageType = await companyDomesticWaybillPackageTypeRepository.GetCompanyDomesticWaybillPackageTypeByIdAsync(command.Id, false, false, cancellationToken);
        if (companyDomesticWaybillPackageType is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بسته مرسوله نامعتبر است");

        var companyDomesticWaybill = await companyDomesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(companyDomesticWaybillPackageType.CompanyDomesticWaybillId, false, false, false, false, cancellationToken);
        if (companyDomesticWaybill is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بارنامه نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticWaybill.CompanySenderId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        await companyDomesticWaybillPackageTypeRepository.DeleteCompanyDomesticWaybillPackageTypeAsync(companyDomesticWaybillPackageType.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyDomesticWaybillPackageType Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "بسته مرسوله با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyDomesticWaybillPackageTypeByComapnyDomesticWaybillIdAsync(DeleteDomesticWaybillPackageTypeByDomesticWaybillIdCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyDomesticWaybillPackageTypeByComapnyDomesticWaybillId is Called with {@Id}", command.CompanyDomesticWaybillId);

        var companyDomesticWaybill = await companyDomesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(command.CompanyDomesticWaybillId, false, false, false, false, cancellationToken);
        if (companyDomesticWaybill is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بارنامه نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticWaybill.CompanySenderId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        await companyDomesticWaybillPackageTypeRepository.DeleteCompanyDomesticWaybillPackageTypeByByCompanyDomesticWaybillIdAsync(command.CompanyDomesticWaybillId, cancellationToken); await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("DeleteCompanyDomesticWaybillPackageTypeByComapnyDomesticWaybillId Deleted successfully with {@Id}", command.CompanyDomesticWaybillId);
        return ApiResponse<int>.Ok(command.CompanyDomesticWaybillId, "بسته های مرسوله با موفقیت حذف شد");
    }

    public async Task<ApiResponse<IReadOnlyList<CompanyDomesticWaybillPackageTypeDto>>> GetCompanyDomesticWaybillPackageTypeByCompanyDomesticWaybillIdAsync(GetDomesticWaybillPackageTypeByDomesticWaybillIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyDomesticWaybillPackageTypeByCompanyDomesticWaybillId is Called with {@Id}", query.CompanyDomesticWaybillId);

        var companyDomesticWaybill = await companyDomesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(query.CompanyDomesticWaybillId, false, false, false, false, cancellationToken);
        if (companyDomesticWaybill is null)
            return ApiResponse<IReadOnlyList<CompanyDomesticWaybillPackageTypeDto>>.Error(StatusCodes.Status404NotFound, "بارنامه نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticWaybill.CompanySenderId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<IReadOnlyList<CompanyDomesticWaybillPackageTypeDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<IReadOnlyList<CompanyDomesticWaybillPackageTypeDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<IReadOnlyList<CompanyDomesticWaybillPackageTypeDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyDomesticWaybillPackageTypes = await companyDomesticWaybillPackageTypeRepository.GetCompanyDomesticWaybillPackageTypeByCompanyDomesticWaybillIdAsync(query.CompanyDomesticWaybillId, cancellationToken);
        if (companyDomesticWaybillPackageTypes is null)
            return ApiResponse<IReadOnlyList<CompanyDomesticWaybillPackageTypeDto>>.Error(StatusCodes.Status404NotFound, "بسته مرسوله یافت نشد");

        var companyDomesticWaybillPackageTypeDtos = mapper.Map<IReadOnlyList<CompanyDomesticWaybillPackageTypeDto>>(companyDomesticWaybillPackageTypes) ?? Array.Empty<CompanyDomesticWaybillPackageTypeDto>();
        if (companyDomesticWaybillPackageTypeDtos == null)
            return ApiResponse<IReadOnlyList<CompanyDomesticWaybillPackageTypeDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} company domestic waybill package type GetByCompanyDomesticWaybillId", companyDomesticWaybillPackageTypeDtos.Count);

        return ApiResponse<IReadOnlyList<CompanyDomesticWaybillPackageTypeDto>>.Ok(companyDomesticWaybillPackageTypeDtos, "بسته های مرسوله با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<CompanyDomesticWaybillPackageTypeDto>> GetCompanyDomesticWaybillPackageTypeByIdAsync(GetCompanyDomesticWaybillPackageTypeByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyDomesticWaybillPackageTypeById is Called with {@Id}", query.Id);

        var companyDomesticWaybillPackageType = await companyDomesticWaybillPackageTypeRepository.GetCompanyDomesticWaybillPackageTypeByIdAsync(query.Id, false, false, cancellationToken);
        if (companyDomesticWaybillPackageType is null)
            return ApiResponse<CompanyDomesticWaybillPackageTypeDto>.Error(StatusCodes.Status404NotFound, "بسته مرسوله نامعتبر است");

        var companyDomesticWaybill = await companyDomesticWaybillRepository.GetCompanyDomesticWaybillByIdAsync(companyDomesticWaybillPackageType.CompanyDomesticWaybillId, false, false, false, false, cancellationToken);
        if (companyDomesticWaybill is null)
            return ApiResponse<CompanyDomesticWaybillPackageTypeDto>.Error(StatusCodes.Status404NotFound, "بارنامه نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(companyDomesticWaybill.CompanySenderId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<CompanyDomesticWaybillPackageTypeDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<CompanyDomesticWaybillPackageTypeDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<CompanyDomesticWaybillPackageTypeDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var companyDomesticWaybillPackageTypeDto = mapper.Map<CompanyDomesticWaybillPackageTypeDto>(companyDomesticWaybillPackageType);
        if (companyDomesticWaybillPackageTypeDto == null)
            return ApiResponse<CompanyDomesticWaybillPackageTypeDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("CompanyDomesticWaybillPackageType retrieved successfully with {@Id}", query.Id);
        return ApiResponse<CompanyDomesticWaybillPackageTypeDto>.Ok(companyDomesticWaybillPackageTypeDto, "بسته مرسوله با موفقیت دریافت شد");
    }
}