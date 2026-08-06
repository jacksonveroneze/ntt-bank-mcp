using System.Diagnostics.CodeAnalysis;
using ModelContextProtocol.Protocol;
using NttBankMcp.Api.Prompts;
using NttBankMcp.Api.Tools.Accounts;
using NttBankMcp.Api.Tools.Customers;
using NttBankMcp.Infrastructure.Configurations;

namespace NttBankMcp.Api.Extensions;

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
                    Name = appConfiguration.Application.Name,
                    Version = appConfiguration.Application.Version.ToString(),
                    Description = appConfiguration.Application.Description,
                };
            })
            .AddAuthorizationFilters()
            .AddOpenTelemetryFilter()
            .AddExceptionFilter()
            .WithHttpTransport(options => { options.Stateless = true; })
            .WithTools<GetCustomerByIdTool>()
            .WithTools<ListCustomerAccountsTool>()
            .WithTools<GetAccountTool>()
            .WithTools<ListAccountTransactionsTool>()
            .WithTools<SummarizeAccountTransactionsTool>()
            .WithPrompts<ComplexPromptType>();

        return services;
    }
}
