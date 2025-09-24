using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyTypes.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyTypes.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyTypes.Queries.GetById;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.Companies.CompanyTypes.Services;

public class CompanyTypeService(
    ILogger<CompanyTypeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyTypeRepository companyTypeRepository
) : ICompanyTypeService
{
    public async Task<ApiResponse<int>> CreateCompanyTypeAsync(CreateCompanyTypeCommand createCompanyTypeCommand, CancellationToken cancellationToken)//ch**
    {
        logger.LogInformation("CreateCompanyType is Called with {@CreateCompanyTypeCommand}", createCompanyTypeCommand);



        if (await companyTypeRepository.CheckExistCompanyTypeNameAsync(createCompanyTypeCommand.TypeName, null, cancellationToken))
            return ApiResponse<int>.Error(400, "نام نوع شرکت تکراری است");

        var companyType = mapper.Map<Domain.Entities.Companies.CompanyType>(createCompanyTypeCommand) ?? null;
        if (companyType == null)
            return ApiResponse<int>.Error(400, "مشکل در عملیات تبدیل");

        var companyTypeId = await companyTypeRepository.CreateCompanyTypeAsync(companyType, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyType created successfully with ID: {CompanyTypeId}", companyTypeId);
        return ApiResponse<int>.Ok(companyTypeId, "نوع شرکت با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyTypeAsync(DeleteCompanyTypeCommand deleteCompanyTypeCommand, CancellationToken cancellationToken)//ch**
    {
        logger.LogInformation("DeleteCompanyType is Called with ID: {Id}", deleteCompanyTypeCommand.Id);

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(400, "مشکل در دریافت اطلاعات");

        if (!user.IsSuperAdmin())
            return ApiResponse<int>.Error(400, "مجوز این فعالیت را ندارید");

        var companyType = await companyTypeRepository.GetCompanyTypeByIdAsync(deleteCompanyTypeCommand.Id, true, false, cancellationToken);
        if (companyType is null)
            return ApiResponse<int>.Error(400, $"نوع شرکت نامعتبر است");

        await companyTypeRepository.DeleteCompanyTypeAsync(companyType.Id);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("CompanyType soft-deleted successfully with ID: {Id}", deleteCompanyTypeCommand.Id);
        return ApiResponse<int>.Ok(deleteCompanyTypeCommand.Id, "نوع شرکت با موفقیت حذف شد");
    }

    public async Task<ApiResponse<CompanyTypeDto>> UpdateCompanyTypeAsync(UpdateCompanyTypeCommand updateCompanyTypeCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyType is Called with {@UpdateCompanyTypeCommand}", updateCompanyTypeCommand);



        var companyType = await companyTypeRepository.GetCompanyTypeByIdAsync(updateCompanyTypeCommand.Id, true, false, cancellationToken);
        if (companyType is null)
            return ApiResponse<CompanyTypeDto>.Error(400, $"نوع شرکت نامعتبر است");

        if (await companyTypeRepository.CheckExistCompanyTypeNameAsync(updateCompanyTypeCommand.TypeName, updateCompanyTypeCommand.Id, cancellationToken))
            return ApiResponse<CompanyTypeDto>.Error(400, "نام نوع شرکت تکراری است");

        var updatedCompanyType = mapper.Map(updateCompanyTypeCommand, companyType);

        if (updatedCompanyType == null)
            return ApiResponse<CompanyTypeDto>.Error(400, "مشکل در عملیات تبدیل");
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("نوع شرکت با موفقیت به‌روزرسانی شد: {Id}", updateCompanyTypeCommand.Id);

        var updatedCompanyTypeDto = mapper.Map<CompanyTypeDto>(updatedCompanyType);
        return ApiResponse<CompanyTypeDto>.Ok(updatedCompanyTypeDto, "نوع شرکت با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyTypeDto>>> GetAllCompanyTypesAsync(GetAllCompanyTypesQuery getAllCompanyTypesQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyTypesByCompanyType is Called");



        var (companyTypes, totalCount) = await companyTypeRepository.GetAllCompanyTypesAsync(
            getAllCompanyTypesQuery.SearchPhrase,
            getAllCompanyTypesQuery.SortBy,
            true,
            getAllCompanyTypesQuery.PageNumber,
            getAllCompanyTypesQuery.PageSize,
            getAllCompanyTypesQuery.SortDirection,
            cancellationToken);

        var companyTypeDto = mapper.Map<IReadOnlyList<CompanyTypeDto>>(companyTypes) ?? Array.Empty<CompanyTypeDto>();
        logger.LogInformation("Retrieved {Count} package types", companyTypeDto.Count);

        var data = new PagedResult<CompanyTypeDto>(companyTypeDto, totalCount, getAllCompanyTypesQuery.PageSize, getAllCompanyTypesQuery.PageNumber);
        return ApiResponse<PagedResult<CompanyTypeDto>>.Ok(data, "نوع شرکت‌ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<CompanyTypeDto>> GetCompanyTypeByIdAsync(GetCompanyTypeByIdQuery getCompanyTypeByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyTypeById is Called with ID: {Id}", getCompanyTypeByIdQuery.Id);



        var companyType = await companyTypeRepository.GetCompanyTypeByIdAsync(getCompanyTypeByIdQuery.Id, false, true, cancellationToken);
        if (companyType is null)
            return ApiResponse<CompanyTypeDto>.Error(400, $"نوع شرکت یافت نشد");

        var result = mapper.Map<CompanyTypeDto>(companyType);
        logger.LogInformation("CompanyType retrieved successfully with ID: {Id}", getCompanyTypeByIdQuery.Id);
        return ApiResponse<CompanyTypeDto>.Ok(result, "نوع شرکت با موفقیت دریافت شد");
    }
}