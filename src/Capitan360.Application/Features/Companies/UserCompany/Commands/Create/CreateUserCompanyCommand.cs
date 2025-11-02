namespace Capitan360.Application.Features.Companies.UserCompany.Commands.Create;

public record CreateUserCompanyCommand(string NameFamily, string PhoneNumber, string Email, string Password, string ConfirmPassword,
    short MoadianType)
{
    public string NameFamily { get; } = NameFamily;
    public string PhoneNumber { get; } = PhoneNumber;
    public string Email { get; } = Email;
    public string Password { get; } = Password;
    public string ConfirmPassword { get; } = ConfirmPassword;
    public short TypeOfFactorInSamanehMoadianId { get; } = MoadianType;
    public int CompanyId { get; set; } = 0;
};