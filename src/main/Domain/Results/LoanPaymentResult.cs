using System.Text.Json.Serialization;

namespace NttBankMcp.Domain.Results;

public sealed record LoanPaymentResult
{
    [JsonPropertyName("paymentId")]
    public long PaymentId { get; init; }

    [JsonPropertyName("loanId")]
    public int LoanId { get; init; }

    [JsonPropertyName("paymentDate")]
    public DateTime? PaymentDate { get; init; }

    [JsonPropertyName("amount")]
    public decimal? Amount { get; init; }

    [JsonPropertyName("paymentType")]
    public string? PaymentType { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("principalPortion")]
    public decimal? PrincipalPortion { get; init; }

    [JsonPropertyName("interestPortion")]
    public decimal? InterestPortion { get; init; }
}
