namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Commands.UpdateCompanyDomesticPathCharge;

public record UpdateCompanyDomesticPathContentItemsCommand
{
    public List<UpdateCompanyDomesticPathContentItemCommand> ContentItemsList { get; set; } = [];
}