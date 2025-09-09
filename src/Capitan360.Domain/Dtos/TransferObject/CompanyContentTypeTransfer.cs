namespace Capitan360.Domain.Dtos.TransferObject;

public class CompanyContentTypeTransfer
{
    public int Id { get; set; }
    public string ContentTypeName { get; set; } = default!;
    public bool ContentTypeActive { get; set; }
    public int ContentTypeOrder { get; set; }
    public string? CompanyContentTypeDescription { get; set; }
}