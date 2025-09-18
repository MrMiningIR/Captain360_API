namespace Capitan360.Application.Features.Companies.UserCompany.Queries.GetUserByCompany;

public record GetUserByCompanyQuery
{
    public string UserId { get; set; } = default!;


    public int CompanyId { get; set; } = 0;


}