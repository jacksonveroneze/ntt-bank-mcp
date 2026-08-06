using System.Diagnostics;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;

namespace NttBankMcp.Api.Filters;

internal static class OpenTelemetryFilter
{
    internal static McpRequestFilter<CallToolRequestParams, CallToolResult> Handle()
    {
        return next => async (context, cancellationToken) =>
        {
            var serverInfo = context.Server.ServerOptions.ServerInfo;
            
            using var activitySource = new ActivitySource(
                serverInfo!.Name);

            using var activity = activitySource
                .StartActivity(context.Params.Name);

            try
            {
                var result = await next(context, cancellationToken);

                activity?.SetTag("is_success", !result.IsError);
                activity?.SetTag("server_version", serverInfo.Version);

                return result;
            }
            finally
            {
                activity?.Stop();
            }
        };
    }
}
