using Capitan360.Api.RequestHelpers.Cache;
using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Application.Services.Permission.Services;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Hybrid;
using System.Reflection;

namespace Capitan360.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TestController(IUserContext userContext, HybridCache cache,
        ILogger<TestController> logger, PermissionCollectorService permissionCollector, IMultiTenantContextAccessor<TenantInfo> tenantContext) : ControllerBase
    {
        [HttpGet]
        //[Authorize(Policy = "CreateRoles")]
        //[Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> Get(CancellationToken token)
        {
            //string cacheKey = $"product-{5067}";
            //string[] tags = ["products", $"product-{5067}"];

            //var user = userContext.GetCurrentUser();
            ////user.Id
            //if (user is not null && user.ValidateCompanyId())
            //{
            //    return Ok("Hello SuperAdmin");
            //}

            //return await cache.GetOrCreateAsync(
            //    cacheKey,
            //    async (token) =>
            //    {
            //        logger.LogInformation("Fetching product {ProductId} from database", 5067);
            //        //return await _dbContext.Products
            //        //    .AsNoTracking()
            //        //    .FirstOrDefaultAsync(p => p.Id == id, token);

            //        await Task.Delay(5000, token);
            //        return Ok("Hello World");
            //    },
            //    tags: tags,
            //    cancellationToken: token
            //);
            string cacheKey = "product-5067";
            string[] tags = ["products", $"product-{token}"];

            var user = userContext.GetCurrentUser();
            if (user is not null && user.ValidateCompanyId())
            {
                return Ok("Hello SuperAdmin");
            }

            string result = await cache.GetOrCreateAsync(
                cacheKey,
                async (token) =>
                {
                    logger.LogInformation("Fetching product {ProductId} from database", 5067);
                    await Task.Delay(5000, token); // شبیه‌سازی دسترسی به دیتابیس
                    return "Hello World"; // نوع داده خام
                },
                tags: tags,
                cancellationToken: token
            );

            return Ok(result); // تبدیل به IActionResult
        }

        [HttpGet("Another")]
        //[Authorize(Roles = "Admin")]

        public IActionResult AnotherGet()
        {
            var permissions = permissionCollector.GetActionsWithPermissionFilter(Assembly.GetExecutingAssembly());
            return Ok(string.Join(",", permissions));
        }

        [HttpGet("test-cache/{id}")]

        public async Task<IActionResult> TestCache(int id, CancellationToken token)
        {
            string cacheKey = $"product-{id}";

            //string[] tags = ["products", $"product-{id}"];
            // var tags = [$"category-{id}", "products"];

            string result = await cache.GetOrCreateAsync(
                cacheKey,
                async (token) =>
                {
                    logger.LogInformation("Fetching data for {CacheKey} from source", cacheKey);
                    await Task.Delay(2000, token); // شبیه‌سازی تأخیر دیتابیس
                    return $"Data for {id} at {DateTime.UtcNow:HH:mm:ss}";
                }
                //,
                //new HybridCacheEntryOptions()
                //{
                //    LocalCacheExpiration = TimeSpan.Zero // غیرفعال کردن MemoryCache
                //}
                ,
                tags: ["products"],

                cancellationToken: token
            );

            return Ok(result);
        }

        [HttpPost("update-product/{id}")]
        //  [InvalidateCache("products")]

        [InvalidateCache("product-{id}")] // تگ پویا با استفاده از پارامتر id
        public async Task<IActionResult> UpdateProduct(int id, CancellationToken token)
        {
            // شبیه‌سازی به‌روزرسانی محصول
            logger.LogInformation("Updating product {ProductId}", id);
            await Task.Delay(1000, token); // شبیه‌سازی عملیات دیتابیس
            return Ok($"Product {id} updated");
        }

        [HttpPost("invalidate/{id}")]

        public async Task<IActionResult> InvalidateCache(int id, CancellationToken token)
        {
            string cacheKey = $"product-{id}";
            await cache.RemoveAsync(cacheKey, token);
            logger.LogInformation("Invalidated cache for {CacheKey}", cacheKey);
            return NoContent();
        }

        [HttpPost("invalidate-tag/{tag}")]

        public async Task<IActionResult> InvalidateTag(string tag, CancellationToken token)
        {
            //await cache.RemoveByTagAsync($"product-{tag}", token);
            await cache.RemoveByTagAsync(tag, token);
            logger.LogInformation("Invalidated cache for tag {Tag}", "zzz");
            return NoContent();
        }

        [HttpGet("test-redis-direct")]
        [ExcludeFromPermission]
        public async Task<IActionResult> TestRedisDirect(CancellationToken token)
        {
            var tenant = tenantContext?.MultiTenantContext?.TenantInfo?.Id;

            try
            {
                string key = "direct-test";
                await cache.GetOrCreateAsync(key, async (_) => "Redis test value", cancellationToken: token);
                string value = await cache.GetOrCreateAsync(key, async (_) => "Should not hit this", cancellationToken: token);
                return Ok($"Redis direct test: {value}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Redis direct test failed");
                return StatusCode(500, $"Redis test failed: {ex.Message}");
            }
        }

        [HttpGet("TestGet")]

        public IActionResult TestGet()
        {
            return Ok("Hello World");
        }
    }
}