using System.Diagnostics.CodeAnalysis;

namespace NttBankMcp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record HttpClientConfiguration
{
    public string? Name { get; init; }
    
    public Uri? Address { get; init; }
}
