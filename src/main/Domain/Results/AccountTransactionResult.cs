using System.Text.Json.Serialization;
using NttBankMcp.Domain.Enums;

namespace NttBankMcp.Domain.Results;

public sealed record AccountTransactionResult
{
    [JsonPropertyName("transactionId")]
    public int TransactionId { get; init; }

    [JsonPropertyName("accountId")]
    public int AccountId { get; init; }

    [JsonPropertyName("transactionDate")]
    public DateOnly? TransactionDate { get; init; }

    [JsonPropertyName("transactionType")]
    public TransactionType? TransactionType { get; init; }

    [JsonPropertyName("amount")]
    public decimal? Amount { get; init; }

    [JsonPropertyName("channel")]
    public TransactionChannel? Channel { get; init; }

    [JsonPropertyName("merchantCategory")]
    public TransactionMerchantCategory? MerchantCategory { get; init; }
}
