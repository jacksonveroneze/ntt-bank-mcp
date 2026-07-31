using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NttBank.Mcp.Mcp.Extensions;
using NttBank.Mcp.Mcp.Mcp.Util;

namespace NttBank.Mcp.Mcp.Filters;

internal static class ExceptionToolFilter
{
    internal static McpRequestFilter<CallToolRequestParams, CallToolResult> Handle()
    {
        return next => async (context, cancellationToken) =>
        {
            try
            {
                return await next(context, cancellationToken);
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                var logger = context.Services
                    ?.GetService<ILogger>();

                logger?.LogToolUnhandledException(ex, context.Params.Name);

                return McpToolResult.Error(
                    code: "INTERNAL_ERROR",
                    message: "An internal error occurred. Please try again later.");
            }
        };
    }
}
