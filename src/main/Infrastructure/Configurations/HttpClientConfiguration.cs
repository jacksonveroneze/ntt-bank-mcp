using System.Diagnostics.CodeAnalysis;

namespace NttBank.Mcp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record HttpClientConfiguration
{
    public Uri? Address { get; init; }
}
