namespace Capitan360.Application.Features.Companies.CompanyBanks.Commands.Update;

public record UpdateCompanyBankCommand(
    string Code,
    string Name,
    string Description,
    bool Active)
{
    public int Id { get; set; }
};
