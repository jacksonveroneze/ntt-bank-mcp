using NttBankMcp.Application.Abstractions.UseCases;

namespace NttBankMcp.Application.Accounts.SummarizeAccountTransactions;

public sealed record SummarizeAccountTransactionsRequest(
    int AccountId,
    string GroupBy,
    DateTime? From,
    DateTime? ToDate) : IBaseRequest;
