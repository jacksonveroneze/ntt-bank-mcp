using NttBankMcp.Domain.Enums;

namespace NttBankMcp.Application.Customers.GetCustomerAccounts;

public sealed record CustomerAccountResponse
{
    public int AccountId { get; init; }

    public int BranchId { get; init; }

    public AccountType? AccountType { get; init; }

    public decimal? Balance { get; init; }

    public DateOnly? OpenDate { get; init; }

    public AccountStatus? Status { get; init; }
}
