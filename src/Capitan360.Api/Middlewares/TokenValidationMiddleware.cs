using Capitan360.Domain.Repositories.PermissionRepository;

namespace Capitan360.Api.Middlewares
{
    public class TokenValidationMiddleware(RequestDelegate next, ITokenBlacklistsRepository blacklistsRepository)
    {
   


        public async Task InvokeAsync(HttpContext context, CancellationToken cancellationToken)
        {
            var token = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var blacklistedToken = await blacklistsRepository.GetByTokenAsync(token, cancellationToken);

                if (blacklistedToken != null && blacklistedToken.ExpiryDate > DateTime.UtcNow)

                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token is revoked.", cancellationToken: cancellationToken);
                    return;
                }
            }

            await next(context);
        }
    }
}
