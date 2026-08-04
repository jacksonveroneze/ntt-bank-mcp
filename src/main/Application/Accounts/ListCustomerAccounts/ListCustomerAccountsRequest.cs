using NttBankMcp.Application.Abstractions.UseCases;
using NttBankMcp.Domain.Enums;

namespace NttBankMcp.Application.Accounts.ListCustomerAccounts;

public sealed record ListCustomerAccountsRequest(
    int CustomerId,
    AccountType? AccountType,
    AccountStatus? Status) : IBaseRequest;
