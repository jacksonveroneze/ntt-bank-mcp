using NttBankMcp.Application.Abstractions.UseCases;

namespace NttBankMcp.Application.Accounts.GetAccount;

public sealed record GetAccountRequest(
    int AccountId) : IBaseRequest;
