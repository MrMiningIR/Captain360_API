namespace Capitan360.Application.Features.Companies.CompanySmsPatterns.Queries.GetCompanySmsPatternsByCompanyId;

public record GetCompanySmsPatternsByCompanyId
{
    public record GetCompanySmsPatternsByCompanyIdQuery(int CompanyId);
}