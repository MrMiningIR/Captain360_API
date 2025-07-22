using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.CreateCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.DeleteCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.UpdateCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetAllCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetCompanyCommissionsById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Exceptions;
using Capitan360.Domain.Repositories.CompanyRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.Services;

public class CompanyCommissionsService(
    ILogger<CompanyCommissionsService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyCommissionsRepository companyCommissionsRepository
) : ICompanyCommissionsService
{
    public async Task<int> CreateCompanyCommissionsAsync(CreateCompanyCommissionsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyCommissions is Called with {@CreateCompanyCommissionsCommand}", command);
        var companyCommissions = mapper.Map<Domain.Entities.CompanyEntity.CompanyCommissions>(command) ?? throw new ArgumentNullException(nameof(command));
        var companyCommissionsId = await companyCommissionsRepository.CreateCompanyCommissionsAsync(companyCommissions, Guid.NewGuid().ToString(), cancellationToken);
        logger.LogInformation("CompanyCommissions created successfully with ID: {CompanyCommissionsId}", companyCommissionsId);
        return companyCommissionsId;
    }

    public async Task<PagedResult<CompanyCommissionsDto>> GetAllCompanyCommissions(GetAllCompanyCommissionsQuery allCompanyCommissionsQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyCommissions is Called");
        var (companyCommissions, totalCount) = await companyCommissionsRepository.GetMatchingAllCompanyCommissions(
            allCompanyCommissionsQuery.SearchPhrase,
            allCompanyCommissionsQuery.PageSize,
            allCompanyCommissionsQuery.PageNumber,
            allCompanyCommissionsQuery.SortBy,
            allCompanyCommissionsQuery.SortDirection,
            cancellationToken);
        var companyCommissionsDto = mapper.Map<IReadOnlyList<CompanyCommissionsDto>>(companyCommissions);
        logger.LogInformation("Retrieved {Count} company commissions", companyCommissionsDto.Count);
        return new PagedResult<CompanyCommissionsDto>(companyCommissionsDto, totalCount, allCompanyCommissionsQuery.PageSize, allCompanyCommissionsQuery.PageNumber);
    }

    public async Task<CompanyCommissionsDto> GetCompanyCommissionsByIdAsync(GetCompanyCommissionsByIdQuery getCompanyCommissionsByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyCommissionsById is Called with ID: {Id}", getCompanyCommissionsByIdQuery.Id);
        if (getCompanyCommissionsByIdQuery.Id <= 0)
            throw new ArgumentException("شناسه کمیسیون باید بزرگ‌تر از صفر باشد");
        var companyCommissions = await companyCommissionsRepository.GetCompanyCommissionsById(getCompanyCommissionsByIdQuery.Id, cancellationToken)
                                 ?? throw new NotFoundException($"کمیسیون با شناسه {getCompanyCommissionsByIdQuery.Id} یافت نشد");
        var result = mapper.Map<CompanyCommissionsDto>(companyCommissions);
        logger.LogInformation("CompanyCommissions retrieved successfully with ID: {Id}", getCompanyCommissionsByIdQuery.Id);
        return result;
    }

    public async Task DeleteCompanyCommissionsAsync(DeleteCompanyCommissionsCommand deleteCompanyCommissionsCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyCommissions is Called with ID: {Id}", deleteCompanyCommissionsCommand.Id);
        if (deleteCompanyCommissionsCommand.Id <= 0)
            throw new ArgumentException("شناسه کمیسیون باید بزرگ‌تر از صفر باشد");
        var companyCommissions = await companyCommissionsRepository.GetCompanyCommissionsById(deleteCompanyCommissionsCommand.Id, cancellationToken)
                                 ?? throw new KeyNotFoundException($"کمیسیون با شناسه {deleteCompanyCommissionsCommand.Id} یافت نشد");
        companyCommissionsRepository.Delete(companyCommissions, Guid.NewGuid().ToString());
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyCommissions soft-deleted successfully with ID: {Id}", deleteCompanyCommissionsCommand.Id);
    }

    public async Task UpdateCompanyCommissionsAsync(UpdateCompanyCommissionsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyCommissions is Called with {@UpdateCompanyCommissionsCommand}", command);
        var companyCommissions = await companyCommissionsRepository.GetCompanyCommissionsById(command.Id, cancellationToken)
                                 ?? throw new NotFoundException($"کمیسیون با شناسه {command.Id} یافت نشد");
        mapper.Map(command, companyCommissions);
        companyCommissionsRepository.UpdateShadows(companyCommissions, Guid.NewGuid().ToString());
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyCommissions updated successfully with ID: {Id}", command.Id);
    }

}