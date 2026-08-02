namespace NttBankMcp.Application.Customers.GetCustomerAccounts;

public sealed record CustomerAccountResponse
{
    public int AccountId { get; init; }

    public int BranchId { get; init; }

    public string? AccountType { get; init; }

    public decimal? Balance { get; init; }

    public DateOnly? OpenDate { get; init; }

    public string? Status { get; init; }
}
