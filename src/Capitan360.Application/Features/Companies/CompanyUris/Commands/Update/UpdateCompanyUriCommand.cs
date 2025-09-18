namespace Capitan360.Application.Features.Companies.CompanyUri.Commands.UpdateCompanyUri;

public record UpdateCompanyUriCommand(
    string Uri,
    string? Description,
    bool Active,
    bool Captain360Uri)
{
    public int Id { get; set; }
};