using System.Diagnostics.CodeAnalysis;
using ModelContextProtocol.Protocol;
using NttBank.Mcp.Infrastructure.Configurations;
using NttBank.Mcp.Mcp.Mcp.Prompts;
using NttBank.Mcp.Mcp.Mcp.Tools;

namespace NttBank.Mcp.Mcp.Extensions;

[ExcludeFromCodeCoverage]
public static class McpExtensions
{
    public static IServiceCollection AddMcp(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        services.AddMcpServer(configureOption =>
            {
                configureOption.ServerInfo = new Implementation
                {
                    Name = appConfiguration.Application!.Name!,
                    Version = appConfiguration.Application!.Version!.ToString(),
                    Description = appConfiguration.Application!.Description,
                };
            })
            .AddAuthorizationFilters()
            .WithHttpTransport(options => { options.Stateless = true; })
            .WithTools<CustomerTools>()
            .WithPrompts<ComplexPromptType>();

        return services;
    }
}
