namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Update;

public record UpdateCompanyDomesticPathCommand(
    bool Active,
    string Description,
    string DescriptionForSearch)
{
    public int Id { get; set; }
}