using System.Text.Json.Serialization;

namespace NttBankMcp.Domain.Results;

public sealed record TicketResult
{
    [JsonPropertyName("ticketId")]
    public int TicketId { get; init; }

    [JsonPropertyName("customerId")]
    public int CustomerId { get; init; }

    [JsonPropertyName("subject")]
    public string? Subject { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("priority")]
    public string? Priority { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; init; }

    [JsonPropertyName("resolvedAt")]
    public DateTime? ResolvedAt { get; init; }

    [JsonPropertyName("assignedAgentId")]
    public int? AssignedAgentId { get; init; }
}
