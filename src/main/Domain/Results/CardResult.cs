using System.Text.Json.Serialization;

namespace NttBankMcp.Domain.Results;

public sealed record CardResult
{
    [JsonPropertyName("cardId")]
    public int CardId { get; init; }

    [JsonPropertyName("customerId")]
    public int? CustomerId { get; init; }

    [JsonPropertyName("accountId")]
    public int? AccountId { get; init; }

    [JsonPropertyName("cardType")]
    public string? CardType { get; init; }

    [JsonPropertyName("issueDate")]
    public DateOnly? IssueDate { get; init; }

    [JsonPropertyName("expiryDate")]
    public DateOnly? ExpiryDate { get; init; }

    [JsonPropertyName("creditLimit")]
    public decimal? CreditLimit { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}
