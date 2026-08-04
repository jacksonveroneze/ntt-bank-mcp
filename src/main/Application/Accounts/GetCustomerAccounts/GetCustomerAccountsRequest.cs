using NttBankMcp.Application.Abstractions.UseCases;
using NttBankMcp.Domain.Enums;

namespace NttBankMcp.Application.Accounts.GetCustomerAccounts;

public sealed record GetCustomerAccountsRequest(
    int CustomerId,
    AccountType? AccountType,
    AccountStatus? Status) : IBaseRequest;
