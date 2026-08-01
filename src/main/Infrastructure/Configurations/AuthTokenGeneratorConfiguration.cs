using System.Diagnostics.CodeAnalysis;

namespace NttBankMcp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AuthTokenGeneratorConfiguration
{
    public Uri? TokenEndpoint { get; init; }

    public string? ClientId { get; init; }

    public string? ClientSecret { get; init; }

    public string? Audience { get; init; }
    
    public string? Scopes { get; init; }
}
