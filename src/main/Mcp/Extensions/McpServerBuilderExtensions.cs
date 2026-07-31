using NttBank.Mcp.Mcp.Filters;

namespace NttBank.Mcp.Mcp.Extensions;

internal static class McpServerBuilderExtensions
{
    extension(IMcpServerBuilder builder)
    {
        internal IMcpServerBuilder AddExceptionFilter()
        {
            return builder.WithRequestFilters(f =>
                f.AddCallToolFilter(ExceptionToolFilter.Handle()));
        }
    }
}
