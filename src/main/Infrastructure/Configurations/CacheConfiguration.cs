using System.Diagnostics.CodeAnalysis;

namespace NttBankMcp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record CacheConfiguration
{
    public bool UseDistributed { get; init; }

    public string? Endpoint { get; init; }
}
