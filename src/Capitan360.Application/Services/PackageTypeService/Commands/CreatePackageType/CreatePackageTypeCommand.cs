namespace Capitan360.Application.Services.PackageTypeService.Commands.CreatePackageType;

public record CreatePackageTypeCommand(
    int CompanyTypeId,
    string PackageTypeName,
    string PackageTypeDescription,
    bool Active
);