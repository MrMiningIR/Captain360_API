namespace Capitan360.Application.Features.Companies.CompanyUris.Commands.Update;

public record UpdateCompanyUriCommand(
    string Uri,
    string Description,
    bool Active,
    bool Captain360Uri)
{
    public int Id { get; set; }
};