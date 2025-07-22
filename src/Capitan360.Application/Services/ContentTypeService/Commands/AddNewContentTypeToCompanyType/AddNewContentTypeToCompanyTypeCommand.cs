namespace Capitan360.Application.Services.ContentTypeService.Commands.AddNewContentTypeToCompanyType;

public class AddNewContentTypeToCompanyTypeCommand
{
    public int CompanyTypeId { get; set; }
    public string ContentTypeName { get; set; } = default!;
    public bool Active { get; set; }
    public string ContentTypeDescription { get; set; } = default!;
    public int OrderContentType { get; set; }
}