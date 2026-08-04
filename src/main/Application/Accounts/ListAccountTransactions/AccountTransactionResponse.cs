using NttBankMcp.Domain.Enums;

namespace NttBankMcp.Application.Accounts.ListAccountTransactions;

public sealed record AccountTransactionResponse
{
    public int TransactionId { get; init; }

    public int AccountId { get; init; }

    public DateOnly? TransactionDate { get; init; }

    public TransactionType? TransactionType { get; init; }

    public decimal? Amount { get; init; }

    public TransactionChannel? Channel { get; init; }

    public TransactionMerchantCategory? MerchantCategory { get; init; }
}
