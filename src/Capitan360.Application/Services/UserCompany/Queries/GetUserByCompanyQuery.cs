namespace Capitan360.Application.Services.UserCompany.Queries;

public record GetUserByCompanyQuery(string UserId, int CompanyId)
{
    public string UserId { get;  } = UserId;
    public int CompanyId { get;  } = CompanyId;

}