using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Hybrid;

namespace Capitan360.Api.RequestHelpers.Cache;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class InvalidateCacheAttribute(string tagPattern) : Attribute, IAsyncActionFilter
{
    private readonly string _tagPattern = tagPattern ?? throw new ArgumentNullException(nameof(tagPattern));

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var _logger = context.HttpContext.RequestServices.GetService<ILogger<InvalidateCacheAttribute>>();

        string tag = _tagPattern;
        if (_tagPattern.Contains("{id}"))
        {
            if (context.ActionArguments.TryGetValue("id", out var idValue) && idValue != null)
            {
                tag = _tagPattern.Replace("{id}", idValue.ToString());
            }
            else
            {
                _logger?.LogWarning("Could not resolve 'id' for tag pattern {TagPattern}", _tagPattern);
                return;
            }
        }

        var resultContext = await next();

        if (resultContext.Result is not StatusCodeResult { StatusCode: >= 200 and < 300 }
            and not ObjectResult { StatusCode: >= 200 and < 300 })
        {
            _logger?.LogDebug("Skipping cache invalidation for tag {Tag} due to unsuccessful response", tag);
            return;
        }

        try
        {
            _logger?.LogInformation(tag);
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<HybridCache>();
            var cancellationToken = context.HttpContext.RequestAborted;

            await cacheService.RemoveByTagAsync(tag, cancellationToken);

            _logger?.LogInformation("Successfully invalidated cache for tag {Tag}", tag);
        }
        catch (Exception ex)
        {
            _logger?.LogWarning(ex, "Failed to invalidate cache for tag {Tag}", tag);
        }
    }
}