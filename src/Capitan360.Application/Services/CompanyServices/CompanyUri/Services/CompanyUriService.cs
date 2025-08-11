using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.CreateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.DeleteCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.UpdateActiveStateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.UpdateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Queries.GetAllCompanyUris;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Queries.GetCompanyUriById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Repositories.CompanyUriRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanyUri.Services;



public class CompanyUriService(
    ILogger<CompanyUriService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyUriRepository companyUriRepository
) : ICompanyUriService
{
    public async Task<ApiResponse<int>> CreateCompanyUriAsync(CreateCompanyUriCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyUri is Called with {@CreateCompanyUriCommand}", command);

        if (await companyUriRepository.CheckExistUriAsync(command.Uri, cancellationToken))
            return ApiResponse<int>.Error(400, "Uri تکراری است");

        var companyUri = mapper.Map<Domain.Entities.CompanyEntity.CompanyUri>(command);
        if (companyUri == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        var companyUriId = await companyUriRepository.CreateCompanyUriAsync(companyUri, cancellationToken);
        logger.LogInformation("CompanyUri created successfully with ID: {CompanyUriId}", companyUriId);
        return ApiResponse<int>.Ok(companyUriId, "URI با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<PagedResult<CompanyUriDto>>> GetAllCompanyUrisAsync(GetAllCompanyUrisQuery allCompanyUrisQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyUris is Called");

        var (companyUris, totalCount) = await companyUriRepository.GetAllCompanyUrisAsync(
            allCompanyUrisQuery.SearchPhrase,
            allCompanyUrisQuery.CompanyId,
            allCompanyUrisQuery.Active,
            allCompanyUrisQuery.PageSize,
            allCompanyUrisQuery.PageNumber,
            allCompanyUrisQuery.SortBy,
            allCompanyUrisQuery.SortDirection,
            cancellationToken);
        var companyUriDtos = mapper.Map<IReadOnlyList<CompanyUriDto>>(companyUris) ?? Array.Empty<CompanyUriDto>();

        logger.LogInformation("Retrieved {Count} company URIs", companyUriDtos.Count);

        var data = new PagedResult<CompanyUriDto>(companyUriDtos, totalCount, allCompanyUrisQuery.PageSize, allCompanyUrisQuery.PageNumber);
        return ApiResponse<PagedResult<CompanyUriDto>>.Ok(data, "شناسه های ادرس با موفقیت بازیابی شدند.");
    }

    public async Task<ApiResponse<CompanyUriDto>> GetCompanyUriByIdAsync(GetCompanyUriByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyUriById is Called with ID: {Id}", query.Id);

        var companyUri = await companyUriRepository.GetCompanyUriByIdAsync(query.Id, false, cancellationToken);
        if (companyUri is null)
            return ApiResponse<CompanyUriDto>.Error(404, $"شناسه ادرس نامعتبر است");

        var result = mapper.Map<CompanyUriDto>(companyUri);
        logger.LogInformation("CompanyUri retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanyUriDto>.Ok(result, "URI با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<int>> DeleteCompanyUriAsync(DeleteCompanyUriCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyUri is Called with ID: {Id}", command.Id);


        var companyUri = await companyUriRepository.GetCompanyUriByIdAsync(command.Id, true, cancellationToken);
        if (companyUri is null)
            return ApiResponse<int>.Error(404, $"URI شرکت با شناسه {command.Id} یافت نشد");

        companyUriRepository.Delete(companyUri);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyUri soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "URI با موفقیت حذف شد");
    }

    public async Task<ApiResponse<CompanyUriDto>> UpdateCompanyUriAsync(UpdateCompanyUriCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyUri is Called with {@UpdateCompanyUriCommand}", command);

        var companyUri = await companyUriRepository.GetCompanyUriByIdAsync(command.Id, true, cancellationToken);
        if (companyUri == null)
            return ApiResponse<CompanyUriDto>.Error(404, $"URI نامعتبر است");

        var exist = await companyUriRepository.CheckExistUriAsync(command.Uri, cancellationToken);
        if (exist)
            return ApiResponse<CompanyUriDto>.Error(400, "URI تکراری است");

        var updatedCompanyUri = mapper.Map(command, companyUri);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyUri updated successfully with ID: {Id}", command.Id);

        var updatedCompanyUriDto = mapper.Map<CompanyUriDto>(updatedCompanyUri);
        return ApiResponse<CompanyUriDto>.Ok(updatedCompanyUriDto, "URI با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<int>> SetCompanyUriActivityStatusAsync(UpdateActiveStateCompanyUriCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetCompanyUriActivityStatus Called with {@UpdateActiveStateCompanyUriCommand}", command);

        var companyUri = await companyUriRepository.GetCompanyUriByIdAsync(command.Id, true, cancellationToken);
        if (companyUri == null)
            return ApiResponse<int>.Error(404, $"URI نامعتبر است");

        companyUri.Active = !companyUri.Active;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("SetCompanyUriActivityStatus Updated successfully with ID: {Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت URI با موفقیت به‌روزرسانی شد");
    }
}