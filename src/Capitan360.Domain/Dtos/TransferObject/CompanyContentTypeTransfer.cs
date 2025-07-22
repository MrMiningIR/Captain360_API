namespace Capitan360.Domain.Dtos.TransferObject;

public class CompanyContentTypeTransfer
{
    public int Id { get; set; }
    public string ContentTypeName { get; set; } = default!;
    public bool Active { get; set; }
    public int OrderContentType { get; set; }

}