namespace Capitan360.Application.Services.CompanyServices.CompanyType.Commands.UpdateCompanyType;

public record UpdateCompanyTypeCommand(
    string TypeName,
    string DisplayName,
    string? Description)
{
    public int Id { get; set; }
}