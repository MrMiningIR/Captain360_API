namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageTypeNameAndDescription;

public record UpdateCompanyPackageTypeNameAndDescriptionCommand(string CompanyPackageTypeName, string? CompanyPackageTypeDescription)
{
    public int Id { get; set; }



}