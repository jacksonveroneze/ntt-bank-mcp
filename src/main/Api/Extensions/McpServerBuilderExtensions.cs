using NttBankMcp.Api.Filters;

namespace NttBankMcp.Api.Extensions;

internal static class McpServerBuilderExtensions
{
    extension(IMcpServerBuilder builder)
    {
        internal IMcpServerBuilder AddOpenTelemetryFilter()
        {
            return builder.WithRequestFilters(f =>
                f.AddCallToolFilter(OpenTelemetryFilter.Handle()));
        }
        
        internal IMcpServerBuilder AddExceptionFilter()
        {
            return builder.WithRequestFilters(f =>
                f.AddCallToolFilter(ExceptionToolFilter.Handle()));
        }
    }
}
