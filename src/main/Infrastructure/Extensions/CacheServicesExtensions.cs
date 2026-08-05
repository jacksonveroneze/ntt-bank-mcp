using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;
using NttBankMcp.Infrastructure.Configurations;
using StackExchange.Redis;

namespace NttBankMcp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class CacheServicesExtensions
{
    public static IServiceCollection AddCached(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddHybridCache(options =>
        {
            options.MaximumPayloadBytes = 1024 * 1024;
            options.MaximumKeyLength = 1024;
            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(5),
                LocalCacheExpiration = TimeSpan.FromMinutes(5),
            };
        });

        if (!appConfiguration.Cache.UseDistributed)
        {
            services.AddDistributedMemoryCache();
        }
        else
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName =
                    $"{appConfiguration.Application.Name}:" +
                    $"{appConfiguration.Application.Version}:";

                options.ConfigurationOptions = new ConfigurationOptions
                {
                    Ssl = false,
                    AbortOnConnectFail = false,
                    EndPoints = { appConfiguration.Cache.Endpoint! },
                    ClientName = $"{appConfiguration.Application.Name}-{Guid.NewGuid()}",
                };
            });
        }

        return services;
    }
}
