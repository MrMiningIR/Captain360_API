namespace Capitan360.Application.Features.Companies.Companies.Commands.Update;

public record UpdateCompanyCommand(
    string MobileCounter,
    string Name,
    string Description,
    bool Active)
{
    public int Id { get; set; }
}