namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Commands.CreateCompanyDomesticPathCharge;

public record CreateCompanyDomesticPathContentItemsCommand
{
    public List<CreateCompanyDomesticPathContentItemCommand> ContentItemsList { get; set; } = [];
}