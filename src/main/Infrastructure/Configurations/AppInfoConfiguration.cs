using System.Diagnostics.CodeAnalysis;

namespace NttBankMcp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AppInfoConfiguration
{
    public required string Name { get; init; }

    public required Version Version { get; init; }
    
    public required string Description { get; init; }
}
