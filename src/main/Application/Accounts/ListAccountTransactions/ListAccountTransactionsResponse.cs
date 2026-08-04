namespace NttBankMcp.Application.Accounts.ListAccountTransactions;

public sealed record ListAccountTransactionsResponse
{
    public IReadOnlyCollection<AccountTransactionResponse> Transactions { get; init; } = [];
}
