namespace Capitan360.Application.Attributes.Authorization;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ExcludeFromPermissionAttribute : Attribute
{
}