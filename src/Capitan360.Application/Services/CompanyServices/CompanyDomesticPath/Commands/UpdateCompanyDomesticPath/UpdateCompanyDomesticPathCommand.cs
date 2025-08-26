namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.UpdateCompanyDomesticPath;

public record UpdateCompanyDomesticPathCommand(
    bool Active,
    string? Description,
    string? DescriptionForSearch)
{
    public int Id { get; set; }
}