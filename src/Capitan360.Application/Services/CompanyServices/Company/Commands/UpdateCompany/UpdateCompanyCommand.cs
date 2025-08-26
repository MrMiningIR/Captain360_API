namespace Capitan360.Application.Services.CompanyServices.Company.Commands.UpdateCompany;

public record UpdateCompanyCommand(
    string MobileCounter,
    string Name,
    string? Description,
    bool Active)
{
    public int Id { get; set; }

}