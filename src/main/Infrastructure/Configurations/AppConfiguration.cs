using System.Diagnostics.CodeAnalysis;

namespace NttBankMcp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AppConfiguration
{
    public AppInfoConfiguration? Application { get; init; }
    
    public AuthTokenAuthenticationConfiguration? AuthTokenAuthentication { get; init; }

    public AuthTokenGeneratorConfiguration? AuthTokenGenerator { get; init; }
    
    public HttpClientConfiguration? HttpClientNttBank { get; init; }
}
