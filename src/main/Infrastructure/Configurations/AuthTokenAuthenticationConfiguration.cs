using System.Diagnostics.CodeAnalysis;

namespace NttBankMcp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AuthTokenAuthenticationConfiguration
{
    public required string Authority { get; init; }
    
    public required string Audience { get; init; }

    public required string Issuer { get; set; }
}
