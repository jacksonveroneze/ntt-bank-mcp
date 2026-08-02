using System.Text.Json.Serialization;

namespace NttBankMcp.Domain.Results;

public sealed record CustomerAccountResult
{
    [JsonPropertyName("accountId")]
    public int AccountId { get; init; }
    
    [JsonPropertyName("branchId")]
    public int BranchId { get; init; }

    [JsonPropertyName("accountType")]
    public string? AccountType { get; init; }

    [JsonPropertyName("balance")]
    public decimal? Balance { get; init; }

    [JsonPropertyName("openDate")]
    public DateOnly? OpenDate { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}
