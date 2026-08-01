using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using NttBankMcp.Infrastructure.Configurations;
using NttBankMcp.Infrastructure.HttpClients;
using Refit;

namespace NttBankMcp.Infrastructure.Extensions;

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
