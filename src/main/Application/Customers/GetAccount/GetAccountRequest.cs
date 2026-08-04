using NttBankMcp.Application.Abstractions.UseCases;

namespace NttBankMcp.Application.Customers.GetAccount;

public sealed record GetAccountRequest(
    int CustomerId,
    int AccountId) : IBaseRequest;
