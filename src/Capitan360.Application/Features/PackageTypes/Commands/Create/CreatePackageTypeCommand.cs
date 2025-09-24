namespace Capitan360.Application.Features.PackageTypes.Commands.Create;

public record CreatePackageTypeCommand(
    int CompanyTypeId,
    string Name,
    string Description,
    bool Active);