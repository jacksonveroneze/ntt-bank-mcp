using System.Diagnostics.CodeAnalysis;

namespace NttBank.Mcp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AppInfoConfiguration
{
    public string? Name { get; init; }

    public Version? Version { get; init; }
    
    public string? Description { get; init; }
}
