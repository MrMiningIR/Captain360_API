namespace Capitan360.Application.Features.Companies.UserCompany.Commands.UpdateUserCompany;

public record UpdateUserCompanyCommand(string FullName, string PhoneNumber, string Email)
{
    public string FullName { get; } = FullName;
    public string PhoneNumber { get; } = PhoneNumber;
    public string Email { get; } = Email;

    public string UserId { get; set; } = default!;
    public int CompanyId { get; set; } = 0;
};