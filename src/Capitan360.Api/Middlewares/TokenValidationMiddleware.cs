namespace Capitan360.Api.Middlewares
{
    public class TokenValidationMiddleware(ITokenBlacklistsRepository blacklistsRepository) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var blacklistedToken = await blacklistsRepository.GetByTokenAsync(token);

                if (blacklistedToken != null && blacklistedToken.ExpiryDate > DateTime.UtcNow)

                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token is revoked.");
                    return;
                }
            }
            await next(context);
        }
    }
}