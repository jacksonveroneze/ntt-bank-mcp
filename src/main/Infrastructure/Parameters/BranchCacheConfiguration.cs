using System.Diagnostics.CodeAnalysis;

namespace NttBankMcp.Infrastructure.Parameters;

[ExcludeFromCodeCoverage]
public sealed record BranchCacheConfiguration
{
    public const string SectionName = "BranchCacheConfiguration";

    public required string PrefixKey { get; init; }

    public required int ExpirationMs { get; init; }
    
    public TimeSpan Expiration => TimeSpan.FromMilliseconds(ExpirationMs);
}
