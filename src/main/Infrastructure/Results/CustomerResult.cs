namespace NttBank.Mcp.Infrastructure.Results;

using System.Text.Json.Serialization;

public sealed record CustomerResult
{
    [JsonPropertyName("customerId")]
    public int CustomerId { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("gender")]
    public string? Gender { get; init; }

    [JsonPropertyName("dateOfBirth")]
    public string? DateOfBirth { get; init; }

    [JsonPropertyName("city")]
    public string? City { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }

    [JsonPropertyName("phone")]
    public string? Phone { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("occupation")]
    public string? Occupation { get; init; }

    [JsonPropertyName("annualIncome")]
    public decimal AnnualIncome { get; init; }

    [JsonPropertyName("joinDate")]
    public string? JoinDate { get; init; }

    [JsonPropertyName("creditScore")]
    public int CreditScore { get; init; }
}
