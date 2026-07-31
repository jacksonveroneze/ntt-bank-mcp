using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using NttBank.Mcp.Infrastructure.Configurations;

namespace NttBank.Mcp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        return services;
    }
}
