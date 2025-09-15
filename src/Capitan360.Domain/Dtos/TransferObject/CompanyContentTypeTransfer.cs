namespace Capitan360.Domain.Dtos.TransferObject;

public class CompanyContentTypeTransfer
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public bool Active { get; set; }
    public int Order { get; set; }
    public string Description { get; set; } = default!;
}