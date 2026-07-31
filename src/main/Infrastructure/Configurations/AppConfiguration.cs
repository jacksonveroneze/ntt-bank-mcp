using System.Diagnostics.CodeAnalysis;

namespace NttBank.Mcp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AppConfiguration
{
    public AppInfoConfiguration? Application { get; init; }
    
    public AuthConfiguration? Auth { get; init; }

    public HttpClientConfiguration? HttpClientNttBank { get; set; }
}
