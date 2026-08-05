using System.Diagnostics.CodeAnalysis;

namespace NttBankMcp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record OpenTelemetryConfiguration
{
    public Uri? EndpointTracing { get; init; }
}
