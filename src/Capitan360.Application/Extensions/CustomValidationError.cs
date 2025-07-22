namespace Capitan360.Application.Extensions;

public class CustomValidationError
{
    public string ErrorMessage { get; set; } = "Unknown";
    public object PropertyName { get; set; } = "Unknown";
    public string ErrorCode { get; set; } = "Unknown";
}