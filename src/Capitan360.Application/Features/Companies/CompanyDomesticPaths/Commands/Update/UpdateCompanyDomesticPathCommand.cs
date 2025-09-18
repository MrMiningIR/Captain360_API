namespace Capitan360.Application.Features.Companies.CompanyDomesticPaths.Commands.Update;

public record UpdateCompanyDomesticPathCommand(
    bool Active,
    string? Description,
    string? DescriptionForSearch)
{
    public int Id { get; set; }
}