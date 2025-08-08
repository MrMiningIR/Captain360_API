namespace Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.UpdateCompanyUri;

public record UpdateCompanyUriCommand(
     string Uri,
   string? Description,
    bool IsActive)
{
    public int Id { get; set; }
};