using System.Diagnostics.CodeAnalysis;

namespace NttBankMcp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record HttpClientConfiguration
{
    public Uri? Address { get; init; }
}
