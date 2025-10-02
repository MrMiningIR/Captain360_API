namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.Update;

public record UpdateCompanyManifestFormCommand(
    string CompanySenderDescription,
    string CompanySenderDescriptionForPrint)
{
    public int Id { get; set; }
};
