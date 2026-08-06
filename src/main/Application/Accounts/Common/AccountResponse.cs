using NttBankMcp.Domain.Enums;

namespace NttBankMcp.Application.Accounts.Common;

public sealed record AccountResponse
{
    public int AccountId { get; init; }

    public int BranchId { get; init; }

    public AccountType? AccountType { get; init; }

    public decimal? Balance { get; init; }

    public DateOnly? OpenDate { get; init; }

    public AccountStatus? Status { get; init; }
}
