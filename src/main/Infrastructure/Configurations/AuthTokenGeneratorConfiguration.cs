using System.Diagnostics.CodeAnalysis;

namespace NttBankMcp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AuthTokenGeneratorConfiguration
{
    public required Uri TokenEndpoint { get; init; }

    public required string ClientId { get; init; }

    public required string ClientSecret { get; init; }

    public required string Audience { get; init; }
    
    public required string Scopes { get; init; }
}
