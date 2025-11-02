namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.AddToCompanyManifestForm;

public record AddCompanyDomesticWaybillToCompanyManifestFormCommand(
    int CompanyManifestFormId)
{
    public int Id { get; set; }
};
