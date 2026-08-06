using System.Text.Json.Serialization;

namespace NttBankMcp.Domain.Results;

public sealed record EmployeeResult
{
    [JsonPropertyName("employeeId")]
    public int EmployeeId { get; init; }

    [JsonPropertyName("branchId")]
    public int BranchId { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("position")]
    public string? Position { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("phone")]
    public string? Phone { get; init; }

    [JsonPropertyName("hireDate")]
    public DateOnly? HireDate { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("employeeCode")]
    public string? EmployeeCode { get; init; }
}
