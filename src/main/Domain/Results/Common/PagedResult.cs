using System.Text.Json.Serialization;

namespace NttBankMcp.Domain.Results.Common;

public sealed record PagedResult<T>
{
    [JsonPropertyName("items")]
    public IReadOnlyCollection<T> Items { get; init; } = [];

    [JsonPropertyName("page")]
    public int Page { get; init; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; init; }

    [JsonPropertyName("totalCount")]
    public long TotalCount { get; init; }

    [JsonPropertyName("hasNext")]
    public bool HasNext { get; init; }
}
