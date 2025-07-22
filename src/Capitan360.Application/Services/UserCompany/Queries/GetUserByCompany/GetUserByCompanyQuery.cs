namespace Capitan360.Application.Services.UserCompany.Queries.GetUserByCompany;

public record GetUserByCompanyQuery
{
    public string UserId { get; set; } = default!;


    public int CompanyId { get; set; } = 0;


}