namespace Capitan360.Domain.Dtos.TransferObject;

public class ParentPermissionTransfer
{

    public required string Parent { get; set; }
    public required string ParentDisplayName { get; set; }
    public Guid ParentCode { get; set; }


}