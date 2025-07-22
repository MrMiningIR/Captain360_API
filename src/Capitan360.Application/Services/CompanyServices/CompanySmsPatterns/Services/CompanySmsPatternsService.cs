using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.CreateCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.DeleteCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.UpdateCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Queries.GetAllCompanySmsPatterns;
using Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Queries.GetCompanySmsPatternsById;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Exceptions;
using Capitan360.Domain.Repositories.CompanyRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Services;

public class CompanySmsPatternsService(
    ILogger<CompanySmsPatternsService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanySmsPatternsRepository companySmsPatternsRepository
) : ICompanySmsPatternsService
{
    public async Task<int> CreateCompanySmsPatternsAsync(CreateCompanySmsPatternsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanySmsPatterns is Called with {@CreateCompanySmsPatternsCommand}", command);
        var companySmsPatterns = mapper.Map<Domain.Entities.CompanyEntity.CompanySmsPatterns>(command) ?? throw new ArgumentNullException(nameof(command));
        var companySmsPatternsId = await companySmsPatternsRepository.CreateCompanySmsPatternsAsync(companySmsPatterns, Guid.NewGuid().ToString(), cancellationToken);
        logger.LogInformation("CompanySmsPatterns created successfully with ID: {CompanySmsPatternsId}", companySmsPatternsId);
        return companySmsPatternsId;
    }

    public async Task<PagedResult<CompanySmsPatternsDto>> GetAllCompanySmsPatterns(GetAllCompanySmsPatternsQuery allCompanySmsPatternsQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanySmsPatterns is Called");
        var (companySmsPatterns, totalCount) = await companySmsPatternsRepository.GetMatchingAllCompanySmsPatterns(
            allCompanySmsPatternsQuery.SearchPhrase,
            allCompanySmsPatternsQuery.PageSize,
            allCompanySmsPatternsQuery.PageNumber,
            allCompanySmsPatternsQuery.SortBy,
            allCompanySmsPatternsQuery.SortDirection,
            cancellationToken);
        var companySmsPatternsDto = mapper.Map<IReadOnlyList<CompanySmsPatternsDto>>(companySmsPatterns);
        logger.LogInformation("Retrieved {Count} company SMS patterns", companySmsPatternsDto.Count);
        return new PagedResult<CompanySmsPatternsDto>(companySmsPatternsDto, totalCount, allCompanySmsPatternsQuery.PageSize, allCompanySmsPatternsQuery.PageNumber);
    }

    public async Task<CompanySmsPatternsDto> GetCompanySmsPatternsByIdAsync(GetCompanySmsPatternsByIdQuery getCompanySmsPatternsByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanySmsPatternsById is Called with ID: {Id}", getCompanySmsPatternsByIdQuery.Id);
        if (getCompanySmsPatternsByIdQuery.Id <= 0)
            throw new ArgumentException("شناسه الگو باید بزرگ‌تر از صفر باشد");
        var companySmsPatterns = await companySmsPatternsRepository.GetCompanySmsPatternsById(getCompanySmsPatternsByIdQuery.Id, cancellationToken)
                                 ?? throw new NotFoundException($"الگو با شناسه {getCompanySmsPatternsByIdQuery.Id} یافت نشد");
        var result = mapper.Map<CompanySmsPatternsDto>(companySmsPatterns);
        logger.LogInformation("CompanySmsPatterns retrieved successfully with ID: {Id}", getCompanySmsPatternsByIdQuery.Id);
        return result;
    }

    public async Task DeleteCompanySmsPatternsAsync(DeleteCompanySmsPatternsCommand deleteCompanySmsPatternsCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanySmsPatterns is Called with ID: {Id}", deleteCompanySmsPatternsCommand.Id);
        if (deleteCompanySmsPatternsCommand.Id <= 0)
            throw new ArgumentException("شناسه الگو باید بزرگ‌تر از صفر باشد");
        var companySmsPatterns = await companySmsPatternsRepository.GetCompanySmsPatternsById(deleteCompanySmsPatternsCommand.Id, cancellationToken)
                                 ?? throw new KeyNotFoundException($"الگو با شناسه {deleteCompanySmsPatternsCommand.Id} یافت نشد");
        companySmsPatternsRepository.Delete(companySmsPatterns, Guid.NewGuid().ToString());
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanySmsPatterns soft-deleted successfully with ID: {Id}", deleteCompanySmsPatternsCommand.Id);
    }

    public async Task UpdateCompanySmsPatternsAsync(UpdateCompanySmsPatternsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanySmsPatterns is Called with {@UpdateCompanySmsPatternsCommand}", command);
        var companySmsPatterns = await companySmsPatternsRepository.GetCompanySmsPatternsById(command.Id, cancellationToken)
                                 ?? throw new NotFoundException($"الگو با شناسه {command.Id} یافت نشد");
        mapper.Map(command, companySmsPatterns);
        companySmsPatternsRepository.UpdateShadows(companySmsPatterns, Guid.NewGuid().ToString());
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanySmsPatterns updated successfully with ID: {Id}", command.Id);
    }
}