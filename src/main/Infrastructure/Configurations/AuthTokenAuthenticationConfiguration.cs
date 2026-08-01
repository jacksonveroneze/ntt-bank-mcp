using System.Diagnostics.CodeAnalysis;

namespace NttBankMcp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AuthTokenAuthenticationConfiguration
{
    public string? Authority { get; init; }
    
    public string? Audience { get; init; }

    public string? Issuer { get; set; }
}
