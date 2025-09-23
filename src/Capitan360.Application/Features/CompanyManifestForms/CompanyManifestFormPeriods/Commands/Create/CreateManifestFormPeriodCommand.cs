namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.Create;

public record CreateManifestFormPeriodCommand(
int CompanyId,
string Code,
long StartNumber,
long EndNumber,
bool Active,
string? Description
);
