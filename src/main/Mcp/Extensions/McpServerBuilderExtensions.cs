using NttBankMcp.Mcp.Filters;

namespace NttBankMcp.Mcp.Extensions;

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
