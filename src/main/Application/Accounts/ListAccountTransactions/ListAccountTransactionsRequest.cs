using NttBankMcp.Application.Abstractions.UseCases;

namespace NttBankMcp.Application.Accounts.ListAccountTransactions;

public sealed record ListAccountTransactionsRequest(
    int CustomerId,
    int AccountId) : IBaseRequest;
