namespace Capitan360.Application.Services.UserCompany.Queries;

public record GetUsersByCompanyQuery(int CompanyId)
{
    public int CompanyId { get; } = CompanyId;
}