namespace Capitan360.Application.Services.CompanyServices.Company.Commands.UpdateCompany;

public record UpdateCompanyCommand(


    string PhoneNumber,
    string Name,
    string Description,
   bool Active,
    int UserCompanyTypeId)
{
    public int Id { get; set; }

}