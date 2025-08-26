namespace Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.UpdateCompanyUri;

public record UpdateCompanyUriCommand(
    string Uri,
    string? Description,
    bool Active,
    bool Captain360Uri)
{
    public int Id { get; set; }
};