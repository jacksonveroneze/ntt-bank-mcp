namespace NttBank.Mcp.Domain.Entities;

public sealed record Customer
{
    public int CustomerId { get; init; }

    public string? Name { get; init; }

    public string? Gender { get; init; }

    public string? DateOfBirth { get; init; }

    public string? City { get; init; }

    public string? State { get; init; }

    public string? Phone { get; init; }

    public string? Email { get; init; }

    public string? Occupation { get; init; }

    public decimal AnnualIncome { get; init; }

    public string? JoinDate { get; init; }

    public int CreditScore { get; init; }
}
