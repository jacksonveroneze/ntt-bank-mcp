using NttBankMcp.Application.Abstractions.UseCases;

namespace NttBankMcp.Application.Customers.GetCustomerAccounts;

public sealed record GetCustomerAccountsRequest(
    int CustomerId) : IBaseRequest;
