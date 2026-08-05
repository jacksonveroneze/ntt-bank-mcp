using System.Diagnostics.CodeAnalysis;

namespace NttBankMcp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AppConfiguration
{
    public required AppInfoConfiguration Application { get; init; }

    public required CacheConfiguration Cache { get; init; }

    public required OpenTelemetryConfiguration OpenTelemetry { get; init; }

    public required AuthTokenAuthenticationConfiguration AuthTokenAuthentication { get; init; }

    public required AuthTokenGeneratorConfiguration AuthTokenGenerator { get; init; }

    public required HttpClientConfiguration HttpClientNttBank { get; init; }
}
