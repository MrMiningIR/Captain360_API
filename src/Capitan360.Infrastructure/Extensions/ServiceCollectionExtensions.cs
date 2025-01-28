using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
namespace Capitan360.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection service,
            IConfigurationManager configurationManager)
        {
        }
    }
}
