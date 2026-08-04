using System.Text.Json.Serialization;
using NttBankMcp.Domain.Enums;

namespace NttBankMcp.Domain.Results;

public sealed record AccountResult
{
    [JsonPropertyName("accountId")]
    public int AccountId { get; init; }
    
    [JsonPropertyName("branchId")]
    public int BranchId { get; init; }

    [JsonPropertyName("accountType")]
    public AccountType? AccountType { get; init; }

    [JsonPropertyName("balance")]
    public decimal? Balance { get; init; }

    [JsonPropertyName("openDate")]
    public DateOnly? OpenDate { get; init; }

    [JsonPropertyName("status")]
    public AccountStatus? Status { get; init; }
}
