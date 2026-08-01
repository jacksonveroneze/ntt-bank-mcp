using System.Diagnostics.CodeAnalysis;
using Duende.AccessTokenManagement;
using Duende.IdentityModel.Client;
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
            .AddClientCredentialsTokenHandler(
                ClientCredentialsClientName.Parse(appConfiguration.HttpClientNttBank!.Name!))
            .AddStandardResilienceHandler();

        services.AddClientCredentialsTokenManagement()
            .AddClient(appConfiguration.HttpClientNttBank!.Name!, client =>
            {
                var config = appConfiguration.AuthTokenGenerator!;
                
                client.TokenEndpoint = config.TokenEndpoint;
                client.ClientId = ClientId.Parse(config.ClientId!);
                client.ClientSecret = ClientSecret.Parse(config.ClientSecret!);

                if (!string.IsNullOrWhiteSpace(config.Scopes))
                {
                    client.Scope = Scope.Parse(config.Scopes);
                }

                if (!string.IsNullOrWhiteSpace(config.Audience))
                {
                    client.Parameters = new Parameters { { "audience", config.Audience } };
                }
            });
        
        return services;
    }
}
