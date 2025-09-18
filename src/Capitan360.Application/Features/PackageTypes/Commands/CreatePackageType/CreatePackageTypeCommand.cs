namespace Capitan360.Application.Features.PackageTypeService.Commands.CreatePackageType;

public record CreatePackageTypeCommand(
    int CompanyTypeId,
    string PackageTypeName,
    string? PackageTypeDescription,
    bool PackageTypeActive
);