namespace Capitan360.Application.Services.Identity.Commands.UpdateUser;

public record UpdateUserCommand
{
    public string UserId { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public int MoadianFactorType { get; set; }
    public string? Email { get; set; }
    public string? RoleId { get; set; }
    public string? RoleName { get; set; }
    public int CompanyId { get; set; }
    public int CompanyType { get; set; }
    public int UserKind { get; set; }


}