using System.Text.Json.Serialization;

namespace NttBankMcp.Domain.Results;

public sealed record RelationshipResult
{
    [JsonPropertyName("relationshipId")]
    public int RelationshipId { get; init; }

    [JsonPropertyName("primaryCustomerId")]
    public int PrimaryCustomerId { get; init; }

    [JsonPropertyName("relatedCustomerId")]
    public int RelatedCustomerId { get; init; }

    [JsonPropertyName("relatedCustomerName")]
    public string? RelatedCustomerName { get; init; }

    [JsonPropertyName("relationshipType")]
    public string? RelationshipType { get; init; }
}
