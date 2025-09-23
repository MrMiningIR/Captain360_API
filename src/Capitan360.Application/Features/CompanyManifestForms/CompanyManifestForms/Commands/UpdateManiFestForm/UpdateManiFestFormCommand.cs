namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.UpdateManiFestForm;

public record UpdateManiFestFormCommand(
string? CompanySenderDescription,
string? CompanySenderDescriptionForPrint
)
{
    public int Id { get; set; }
};
