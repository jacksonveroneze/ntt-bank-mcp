using NttBankMcp.Api.Filters;

namespace NttBankMcp.Api.Extensions;

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
