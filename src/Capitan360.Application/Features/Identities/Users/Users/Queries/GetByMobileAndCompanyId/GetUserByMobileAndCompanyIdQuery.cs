namespace Capitan360.Application.Features.Identities.Users.Users.Queries.GetByMobileAndCompanyId;

public record GetUserByMobileAndCompanyIdQuery(
    int CompanyId,
    string Mobile);
