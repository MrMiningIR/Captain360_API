using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;

namespace Capitan360.Api.Middlewares;

public class DynamicTenantStore : IMultiTenantStore<TenantInfo>
{
    public Task<bool> TryAddAsync(TenantInfo tenantInfo)
    {
        return Task.FromResult(true);
    }

    public Task<bool> TryRemoveAsync(string identifier)
    {
        return Task.FromResult(true);
    }

    public Task<IEnumerable<TenantInfo>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<TenantInfo>>(new List<TenantInfo>());
    }

    public Task<bool> TryUpdateAsync(TenantInfo tenantInfo)
    {
        return Task.FromResult(true);
    }

    public Task<TenantInfo?> TryGetByIdentifierAsync(string identifier)
    {
        return TryGetAsync(identifier);
    }

    public Task<TenantInfo?> TryGetAsync(string id)
    {
        var tenantInfo = new TenantInfo
        {
            Id = id,
            Identifier = id,
            Name = id
        };
        return Task.FromResult<TenantInfo?>(tenantInfo);
    }

    public Task<IEnumerable<TenantInfo>> GetAllAsync(int take, int skip)
    {
        return Task.FromResult<IEnumerable<TenantInfo>>(new List<TenantInfo>());
    }
}

//public class TenantInfo : ITenantInfo
//{
//    public string Id { get; set; }
//    public string Identifier { get; set; }
//    public string Name { get; set; }
//    public string ConnectionString { get; set; }
//    public IDictionary<string, object> Items { get; } = new Dictionary<string, object>();
//}