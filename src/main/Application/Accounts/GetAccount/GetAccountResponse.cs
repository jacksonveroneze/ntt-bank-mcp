using NttBankMcp.Application.Accounts.Common;

namespace NttBankMcp.Application.Accounts.GetAccount;

public sealed record GetAccountResponse
{
    public AccountResponse? Account { get; init; }
}
