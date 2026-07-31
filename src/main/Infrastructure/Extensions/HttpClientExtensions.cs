using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using NttBank.Mcp.Infrastructure.Configurations;
using NttBank.Mcp.Infrastructure.HttpClients;
using Refit;

namespace NttBank.Mcp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class HttpClientExtensions
{
    public static IServiceCollection AddHttpClient(
        this IServiceCollection services, 
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(appConfiguration);
        
        services.AddRefitClient<INttBankApi>()
            .ConfigureHttpClient(config =>
            {
                config.BaseAddress = appConfiguration.HttpClientNttBank!.Address;
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(2))
            .AddStandardResilienceHandler();

        return services;
    }
}
