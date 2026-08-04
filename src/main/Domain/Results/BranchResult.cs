using System.Text.Json.Serialization;

namespace NttBankMcp.Domain.Results;

public sealed record BranchResult
{
    [JsonPropertyName("branchId")]
    public int BranchId { get; init; }

    [JsonPropertyName("branchName")]
    public string? BranchName { get; init; }

    [JsonPropertyName("city")]
    public string? City { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }
}

