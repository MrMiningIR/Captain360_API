namespace Capitan360.Domain.Dtos.TransferObject;

public class CompanyPackageTypeTransfer
{
    public int Id { get; set; }

    public string PackageTypeName { get; set; } = default!;
    public bool PackageTypeActive { get; set; }

    public int PackageTypeOrder { get; set; }
    public string? CompanyPackageTypeDescription { get; set; }
}