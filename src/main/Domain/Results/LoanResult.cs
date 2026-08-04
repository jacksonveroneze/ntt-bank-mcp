using System.Text.Json.Serialization;

namespace NttBankMcp.Domain.Results;

public sealed record LoanResult
{
    [JsonPropertyName("loanId")]
    public int LoanId { get; init; }

    [JsonPropertyName("customerId")]
    public int? CustomerId { get; init; }

    [JsonPropertyName("branchId")]
    public int? BranchId { get; init; }

    [JsonPropertyName("loanType")]
    public string? LoanType { get; init; }

    [JsonPropertyName("loanAmount")]
    public decimal? LoanAmount { get; init; }

    [JsonPropertyName("interestRate")]
    public decimal? InterestRate { get; init; }

    [JsonPropertyName("termMonths")]
    public int? TermMonths { get; init; }

    [JsonPropertyName("startDate")]
    public DateOnly? StartDate { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}