namespace Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.UpdateCompanyUri;

public record UpdateCompanyUriCommand(
   
    int CompanyId,
    string Uri,
    string? Description,
    bool IsActive)
{
    public int Id { get; set; }
};