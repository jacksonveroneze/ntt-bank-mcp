using NttBankMcp.Application.Accounts.Common;

namespace NttBankMcp.Application.Accounts.ListCustomerAccounts;

public sealed record ListCustomerAccountsResponse
{
    public IReadOnlyCollection<AccountResponse> Accounts { get; init; } = [];
}
