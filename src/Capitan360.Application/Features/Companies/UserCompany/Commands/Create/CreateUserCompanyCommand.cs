using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.Companies.UserCompany.Commands.CreateUserCompany;

public record CreateUserCompanyCommand(string FullName, string PhoneNumber, string Email, string Password, string ConfirmPassword,
    MoadianFactorType MoadianType)
{
    public string FullName { get; } = FullName;
    public string PhoneNumber { get; } = PhoneNumber;
    public string Email { get; } = Email;
    public string Password { get; } = Password;
    public string ConfirmPassword { get; } = ConfirmPassword;
    public MoadianFactorType MoadianFactorType { get; } = MoadianType;
    public int CompanyId { get; set; } = 0;
};