using System.Text.Json.Serialization;

namespace NttBankMcp.Domain.Results;

public sealed record CardTransactionResult
{
    [JsonPropertyName("transactionId")]
    public long TransactionId { get; init; }

    [JsonPropertyName("cardId")]
    public int CardId { get; init; }

    [JsonPropertyName("transactionDate")]
    public DateTime? TransactionDate { get; init; }

    [JsonPropertyName("amount")]
    public decimal? Amount { get; init; }

    [JsonPropertyName("merchantName")]
    public string? MerchantName { get; init; }

    [JsonPropertyName("merchantCategory")]
    public string? MerchantCategory { get; init; }

    [JsonPropertyName("transactionType")]
    public string? TransactionType { get; init; }

    [JsonPropertyName("channel")]
    public string? Channel { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }
}
