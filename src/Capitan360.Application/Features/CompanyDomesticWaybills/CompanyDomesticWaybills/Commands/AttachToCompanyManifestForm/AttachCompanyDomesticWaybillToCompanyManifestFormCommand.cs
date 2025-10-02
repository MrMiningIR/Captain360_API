namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.AttachToCompanyManifestForm;

public record AttachCompanyDomesticWaybillToCompanyManifestFormCommand(
    int CompanyManifestFormId)
{
    public int Id { get; set; }
};
