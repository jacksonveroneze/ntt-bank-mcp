using System.Text.Json.Serialization;

namespace NttBankMcp.Domain.Results;

public sealed record TransactionSummaryResult
{
    [JsonPropertyName("accountId")]
    public int AccountId { get; init; }

    [JsonPropertyName("groupBy")]
    public string? GroupBy { get; init; }

    [JsonPropertyName("periodFrom")]
    public DateTime? PeriodFrom { get; init; }

    [JsonPropertyName("periodTo")]
    public DateTime? PeriodTo { get; init; }

    [JsonPropertyName("totalCredits")]
    public decimal? TotalCredits { get; init; }

    [JsonPropertyName("totalDebits")]
    public decimal? TotalDebits { get; init; }

    [JsonPropertyName("netFlow")]
    public decimal? NetFlow { get; init; }

    [JsonPropertyName("totalTransactionsCount")]
    public long? TotalTransactionsCount { get; init; }

    [JsonPropertyName("groups")]
    public IReadOnlyCollection<TransactionGroupSummaryResult> Groups { get; init; } = [];
}
