namespace Capitan360.Application.Services.UserCompany.Commands.Update;

public record UpdateUserCompanyCommand(string FullName, string PhoneNumber , string Password , string ConfirmPassword)
{
    public string FullName { get; } = FullName;
    public string PhoneNumber { get; } = PhoneNumber;
 
    public string Password { get; } = Password;
    public string ConfirmPassword { get; } = ConfirmPassword;

    public string UserId { get; set; } = default!;
    public int CompanyId { get; set; } 


};