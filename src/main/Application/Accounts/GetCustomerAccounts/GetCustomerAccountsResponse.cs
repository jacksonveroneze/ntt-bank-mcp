using NttBankMcp.Application.Accounts.Common;

namespace NttBankMcp.Application.Accounts.GetCustomerAccounts;

public sealed record GetCustomerAccountsResponse
{
    public IReadOnlyCollection<AccountResponse> Accounts { get; init; } = [];
}
