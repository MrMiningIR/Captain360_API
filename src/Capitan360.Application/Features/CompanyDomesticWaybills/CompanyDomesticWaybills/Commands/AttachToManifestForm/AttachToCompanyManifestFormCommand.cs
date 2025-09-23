namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.AttachToManifestForm;

public record AttachToCompanyManifestFormCommand(
    int CompanyManifestFormId)
{
    public int Id { get; set; }
};
