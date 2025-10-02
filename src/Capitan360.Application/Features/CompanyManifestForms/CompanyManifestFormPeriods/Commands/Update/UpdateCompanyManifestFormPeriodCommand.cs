namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.Update;

public record UpdateCompanyManifestFormPeriodCommand(
    string Code,
    long StartNumber,
    long EndNumber,
    bool Active,
    string Description)
{
    public int Id { get; set; }
};
