using System.Text.Json.Serialization;

namespace NttBankMcp.Domain.Results;

public sealed record TransactionGroupSummaryResult
{
    [JsonPropertyName("groupKey")]
    public string? GroupKey { get; init; }

    [JsonPropertyName("totalAmount")]
    public decimal? TotalAmount { get; init; }

    [JsonPropertyName("transactionCount")]
    public long? TransactionCount { get; init; }

    [JsonPropertyName("averageAmount")]
    public decimal? AverageAmount { get; init; }
}
