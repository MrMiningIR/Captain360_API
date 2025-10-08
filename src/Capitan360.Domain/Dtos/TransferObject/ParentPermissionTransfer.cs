namespace Capitan360.Domain.Dtos.TransferObject;

public class ParentPermissionTransfer
{

    public required string ParentName { get; set; }
    public required string ParentDisplayName { get; set; }
    public Guid ParentCode { get; set; }


}

