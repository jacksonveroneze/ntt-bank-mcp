using System.Diagnostics.CodeAnalysis;
using ModelContextProtocol.Protocol;
using NttBankMcp.Api.Prompts;
using NttBankMcp.Api.Tools.Accounts;
using NttBankMcp.Api.Tools.Customer;
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
                    Name = appConfiguration.Application!.Name!,
                    Version = appConfiguration.Application.Version!.ToString(),
                    Description = appConfiguration.Application.Description,
                };
            })
            .AddAuthorizationFilters()
            .AddExceptionFilter()
            .WithHttpTransport(options => { options.Stateless = true; })
            .WithTools<GetCustumerByIdTool>()
            .WithTools<GetCustomerAccountsTool>()
            .WithTools<GetAccountTool>()
            .WithTools<ListAccountTransactionsTool>()
            .WithPrompts<ComplexPromptType>();

        return services;
    }
}
