namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentType;

public record UpdateCompanyContentTypeNameCommand
{
    public int Id { get; set; }
    public int OriginalContentId { get; set; }
    public string ContentTypeName { get; set; } = default!;
}