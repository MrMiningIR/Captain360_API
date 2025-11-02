namespace Capitan360.Application.Features.Identities.Identities.Commands.CreateUser;

public record CreateUserCommand
{
    public string NameFamily { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
    public int TypeOfFactorInSamanehMoadianId { get; set; }
    public string? Email { get; set; }
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public int CompanyId { get; set; }
    public int CompanyType { get; set; }



}