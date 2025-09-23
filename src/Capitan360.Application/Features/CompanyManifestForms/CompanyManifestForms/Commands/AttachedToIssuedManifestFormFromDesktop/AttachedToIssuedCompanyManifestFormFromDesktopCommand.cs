namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.AttachedToIssuedManifestFormFromDesktop;

public record AttachedToIssuedCompanyManifestFormFromDesktopCommand(
string DomesticWaybillNo,
string CompanyReceiverCaptain360Code,
long CompanyManifestFormNo
)
{
    public object CompanyDomesticWaybillNo { get; internal set; }
}
