namespace Capitan360.Application.Features.Companies.CompanyBanks.Commands.Create;

public record CreateCompanyBankCommand(
   int CompanyId,
   string Code,
   string Name,
   bool Active,
   string Description);
