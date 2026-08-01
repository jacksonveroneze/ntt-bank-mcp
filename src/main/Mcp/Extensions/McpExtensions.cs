using System.Diagnostics.CodeAnalysis;
using ModelContextProtocol.Protocol;
using NttBankMcp.Infrastructure.Configurations;
using NttBankMcp.Mcp.Prompts;
using NttBankMcp.Mcp.Tools.Customer;

namespace NttBankMcp.Mcp.Extensions;

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
            .AddExceptionFilter()
            .WithHttpTransport(options => { options.Stateless = true; })
            .WithTools<GetCustumerByIdTool>()
            .WithPrompts<ComplexPromptType>();

        return services;
    }
}
